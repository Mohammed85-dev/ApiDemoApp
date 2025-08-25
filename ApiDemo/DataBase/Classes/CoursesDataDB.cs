using System.Configuration;
using ApiDemo.DataBase.Interfaces;
using ApiDemo.Models.Courses;
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
            Ucid = newCourseId,
            Name = course.Name,
            Description = course.Description,
            Tags = course.Tags,
            Created = DateTime.Now,
        }).Execute();
    }

    public void UpdateCourse(Guid courseId, CourseData courseData) {
        var query = _courseData.Where(c => c.Ucid == courseId);
        var batch = _courseData.GetSession().CreateBatch();

        //todo make this actually work
        if (courseData.OwnerUserId != Guid.Empty) {
            batch.Append(query.Select(c => new CourseData { OwnerUserId = courseData.OwnerUserId }).Update());
        }
        if (courseData.Name != string.Empty) {
            batch.Append( query.Select(c => new CourseData { Name = courseData.Name }).Update());
        }
        if (courseData.Description != string.Empty) {
            batch.Append(query.Select(c => new CourseData { Description = courseData.Description }).Update());
        }
        if (courseData.Tags.Count != 0) {
            batch.Append(query.Select(c => new CourseData { Tags = courseData.Tags }).Update());
        }
        if (courseData.Chapters.Count != 0) {
            batch.Append(query.Select(c => new CourseData { Chapters = courseData.Chapters }).Update());
        }
        if (courseData.videoFileId != Guid.Empty) {
            batch.Append(query.Select(c => new CourseData { videoFileId = courseData.videoFileId }).Update());
        }
        batch.Execute();
    }

    public void DeleteCourse(Guid courseId) {
        _courseData.DeleteIf(c => c.Ucid == courseId).Execute();
    }

    public CourseData GetCourse(Guid courseId) {
        return _courseData.FirstOrDefault(x => x.Ucid == courseId).Execute();
    }

    public void AddTag(Guid courseId, string tag) {
        courseData(courseId).Select(c => new CourseData() { Tags = new List<string> { tag } }).Update()
            .Execute();
    }

    public void DeleteTag(Guid courseId, string tag) {
        courseData(courseId).Select(c => c.Tags.Remove(tag)).Update().Execute();
    }

    private CqlQuery<CourseData> courseData(Guid courseId) {
        return _courseData.Where(c => c.Ucid == courseId);
    }
    //todo AVC and VP9
}