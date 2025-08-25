using ApiDemo.Models.Courses;
using ApiDemo.Models.Courses.CourseChapter;

namespace ApiDemo.DataBase.Interfaces;

public interface ICourseChaptersDB {
    public IEnumerable<CourseChapter> GetCourseChapters(Guid courseId);
    public void AddCourseChapter(Guid ucid, CourseChapter chapter);
    public void UpdateCourseChapter(Guid ucid, int chapterId, CourseChapter chapter);
    public void DeleteCourseChapter(Guid ucid, int chapterId);
}