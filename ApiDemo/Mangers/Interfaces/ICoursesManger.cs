using System.Diagnostics.CodeAnalysis;
using ApiDemo.Models.Courses.Course;
using ApiDemo.Models.Courses.CourseChapter;

namespace ApiDemo.Mangers.Interfaces;

public interface ICoursesManger {
    public bool TryGetCourse(Guid courseId, [NotNullWhen(true)] out CourseData? course);
    public Guid CreateCourse(Guid uuid, CreateCourse course, out string response);
    public void UpdateCourse(Guid courseId, CourseData course);
    public void DeleteCourse(Guid courseId);
    public IEnumerable<CourseChapter> GetCourseChapters(Guid courseId);
    public void AddCourseChapter(Guid courseId, CourseChapter chapter);
    public void UpdateCourseChapter(Guid courseId, int chapterId, CourseChapter chapter);
    public void DeleteCourseChapter(Guid courseId, int chapterId);
    public Stream GetPicture(Guid courseId);
    public Task SetPicture(Guid courseId, Stream picture);
    public Stream GetVideo(Guid courseId);
    public Task SetVideo(Guid courseId, Stream video);
    public void AddTag(Guid courseId, string tag);
    public void DeleteTag(Guid courseId, string tag);
}