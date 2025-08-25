using ApiDemo.DataBase.Interfaces;
using ApiDemo.Models.Courses.Course;
using Cassandra.Data.Linq;
using ISession = Cassandra.ISession;

namespace ApiDemo.DataBase.Classes;

public class CoursesDataDB : ICoursesDataDB {
    private readonly Table<CourseData> _courseData;

    public CoursesDataDB(ISession cassandraSession) {
        _courseData = new Table<CourseData>(cassandraSession);
        _courseData.CreateIfNotExists();
    }

    public void CreateCourse(Guid newCourseId, CreateCourse course) {
        _courseData.Insert(new CourseData {
            OwnerUserId = course.uuid,
            UniqueCourseId = newCourseId,
            Name = course.Name,
            Description = course.Description,
            Tags = course.Tags,
            Created = DateTime.Now,
        }).Execute();
    }

    public void UpdateCourse(Guid courseId, CourseData courseData) {
        var query = _courseData.Where(c => c.UniqueCourseId == courseId);
        var batch = _courseData.GetSession().CreateBatch();

        if (courseData.OwnerUserId != Guid.Empty) batch.Append(query.Select(c => new CourseData { OwnerUserId = courseData.OwnerUserId, }).Update());
        if (courseData.Name != string.Empty) batch.Append(query.Select(c => new CourseData { Name = courseData.Name, }).Update());
        if (courseData.Description != string.Empty)
            batch.Append(query.Select(c => new CourseData { Description = courseData.Description, }).Update());
        if (courseData.Tags.Count != 0) batch.Append(query.Select(c => new CourseData { Tags = courseData.Tags, }).Update());
        if (courseData.Chapters.Count != 0) batch.Append(query.Select(c => new CourseData { Chapters = courseData.Chapters, }).Update());
        if (courseData.videoFileId != Guid.Empty) batch.Append(query.Select(c => new CourseData { videoFileId = courseData.videoFileId, }).Update());
        if (courseData.RequiredPermissions.Count != 0)
            batch.Append(query.Select(c => new CourseData { RequiredPermissions = courseData.RequiredPermissions, }).Update());
        batch.Execute();
    }

    //Me messing around
    // public void UpdateCourse(Guid courseId, CourseData courseData) {
    //     var query = _courseData.Where(c => c.UniqueCourseId == courseId);
    //     var batch = _courseData.GetSession().CreateBatch();
    //
    //     void AddUpdate<T>(
    //         Expression<Func<CourseData, T>> propertyExpr,
    //         Func<T, bool> isDefault) {
    //         var member = (MemberExpression)propertyExpr.Body;
    //         var propInfo = (System.Reflection.PropertyInfo)member.Member;
    //
    //         // read the value from courseData
    //         var value = (T)propInfo.GetValue(courseData);
    //
    //         if (!isDefault(value)) {
    //             // create a new object with only this property set
    //             var newData = new CourseData();
    //             propInfo.SetValue(newData, value);
    //
    //             batch.Append(query.Select(_ => newData).Update());
    //         }
    //     }
    //
    //     AddUpdate(c => c.OwnerUserId, v => v == Guid.Empty);
    //     AddUpdate(c => c.Name, string.IsNullOrEmpty);
    //     AddUpdate(c => c.Description, string.IsNullOrEmpty);
    //     AddUpdate(c => c.Tags, v => v.Count == 0);
    //     AddUpdate(c => c.Chapters, v => v.Count == 0);
    //     AddUpdate(c => c.videoFileId, v => v == Guid.Empty);
    //     AddUpdate(c => c.RequiredPermissions, v => v.Count == 0);
    //
    //     batch.Execute();
    // }

    public void DeleteCourse(Guid courseId) {
        _courseData.DeleteIf(c => c.UniqueCourseId == courseId).Execute();
    }

    public CourseData GetCourse(Guid courseId) {
        return _courseData.FirstOrDefault(x => x.UniqueCourseId == courseId).Execute();
    }

    public void AddTag(Guid courseId, string tag) {
        courseData(courseId).Select(c => new CourseData { Tags = new List<string> { tag, }, }).Update()
            .Execute();
    }

    public void DeleteTag(Guid courseId, string tag) {
        courseData(courseId).Select(c => c.Tags.Remove(tag)).Update().Execute();
    }

    private CqlQuery<CourseData> courseData(Guid courseId) {
        return _courseData.Where(c => c.UniqueCourseId == courseId);
    }
    //todo AVC and VP9
}