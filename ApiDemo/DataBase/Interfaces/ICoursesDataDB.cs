using ApiDemo.Models.Courses;
using ApiDemo.Models.Courses.Course;

namespace ApiDemo.DataBase.Interfaces;

public interface ICoursesDataDB {
    public CourseData GetCourse(Guid courseId);
    public void CreateCourse(Guid uuid, CreateCourse course);
    public void UpdateCourse(Guid courseId, CourseData course);
    public void DeleteCourse(Guid courseId);
    // public IEnumerable<CourseChapter> GetCourseChapters(Guid courseId);
    // public void AddCourseChapter(Guid courseId, CourseChapter chapter);
    // public void UpdateCourseChapter(Guid courseId, CourseChapter chapter);
    // public void DeleteCourseChapter(Guid courseId, int chapterId);
    public void AddTag(Guid courseId, string tag);
    public void DeleteTag(Guid courseId, string tag);
}