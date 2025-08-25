using ApiDemo.DataBase.Interfaces;
using ApiDemo.Models.Courses;
using ApiDemo.Models.Courses.CourseChapter;
using Cassandra.Data.Linq;
using ISession = Cassandra.ISession;

namespace ApiDemo.DataBase.Classes;

public class CourseChaptersDB : ICourseChaptersDB {
    Table<CourseChapter> _courseChapters;

    public CourseChaptersDB(ISession cassandraSession) {
        _courseChapters = new Table<CourseChapter>(cassandraSession);
        _courseChapters.CreateIfNotExists();
    }

    private CqlQuery<CourseChapter> chapterData(Guid ucid) {
        return _courseChapters.Where(c => c.Ucid == ucid);
    }
        /// <summary>
    /// todo this collection can be empty add fail safe code in the manger to insure it fucking isn't
    /// </summary>
    /// <param name="courseId"></param>
    /// <returns></returns>
    public IEnumerable<CourseChapter> GetCourseChapters(Guid courseId) {
        return chapterData(courseId).Execute().AsEnumerable();
    }

    public void AddCourseChapter(Guid ucid, CourseChapter chapter) {
        chapter.Ucid = ucid;
        _courseChapters.Insert(chapter);
    }

    public void UpdateCourseChapter(Guid ucid, int chapterId, CourseChapter chapter) {
        _courseChapters.Where(c => c.Ucid == ucid)
            .Where(c => c.ChapterId == chapterId)
            .Select(c => chapter).Update().Execute();
    }

    public void DeleteCourseChapter(Guid ucid, int chapterId) {
        chapterData(ucid).DeleteIf(c => c.ChapterId == chapterId).Execute();
    }
}