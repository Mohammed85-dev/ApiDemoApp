using System.Configuration;
using ApiDemo.DataBase.Interfaces;
using ApiDemo.Mangers.Interfaces;
using ApiDemo.Models.Courses;
using ApiDemo.Models.Courses.Course;
using ApiDemo.Models.Courses.CourseChapter;

namespace ApiDemo.Mangers.Classes;

public class CoursesManger(IFileManger fileManger, ICoursesDataDB coursesData, ICourseChaptersDB courseChapters, ITokenAuthorizationManger auth)
    : ICoursesManger {
    private Guid ownerUserId;
    private Guid videoFileId;

    public CourseData GetCourse(Guid courseId) {
        return coursesData.GetCourse(courseId);
    }

    public Guid CreateCourse(Guid uuid, CreateCourse course, out string response) {
        Guid ucid = Guid.NewGuid();
        if (!auth.GiveCustomAuthorizationLevelZero(uuid, PresetTokenPermissions.permissionsLevelZero, ucid.ToString(), out var response1)) {
            response = response1;
            return Guid.Empty;
        }
        coursesData.CreateCourse(ucid, course);
        response = string.Empty;
        return ucid;
    }

    public void UpdateCourse(Guid courseId, CourseData course) {
        coursesData.UpdateCourse(courseId, course);
    }

    public void DeleteCourse(Guid courseId) {
        coursesData.DeleteCourse(courseId);
    }

    public IEnumerable<CourseChapter> GetCourseChapters(Guid courseId) {
        return courseChapters.GetCourseChapters(courseId);
    }

    public void AddCourseChapter(Guid courseId, CourseChapter chapter) {
        courseChapters.AddCourseChapter(courseId, chapter);
    }

    public void UpdateCourseChapter(Guid courseId, int chapterId, CourseChapter chapter) {
        courseChapters.UpdateCourseChapter(courseId, chapterId, chapter);
    }

    public void DeleteCourseChapter(Guid courseId, int chapterId) {
        courseChapters.DeleteCourseChapter(courseId, chapterId);
    }

    public byte[]? GetPicture(Guid courseId) {
        throw new InvalidOperationException();
    }

    public void SetPicture(Guid courseId, byte[] picture) {
        throw new InvalidOperationException();
    }

    public Stream GetVideo(Guid courseId) {
        videoFileId = coursesData.GetCourse(courseId).videoFileId;
        return fileManger.GetFileStream(videoFileId);
    }

    public void SetVideo(Guid courseId, Stream video) {
        ownerUserId = coursesData.GetCourse(courseId).OwnerUserId;
        Guid fileId = fileManger.UploadFile(ownerUserId, video, "randomSHIt", FileManger.FileType.courseVideo);
        coursesData.UpdateCourse(courseId, new CourseData() {
            videoFileId = fileId,
        });
    }

    public void AddTag(Guid courseId, string tag) {
        coursesData.AddTag(courseId, tag);
    }

    public void DeleteTag(Guid courseId, string tag) {
        coursesData.DeleteTag(courseId, tag);
    }
}