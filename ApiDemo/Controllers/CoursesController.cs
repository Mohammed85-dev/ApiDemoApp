using ApiDemo.Mangers.Interfaces;
using ApiDemo.Models.Courses.Course;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController(IFileManger fileManger, ICoursesManger courses, ITokenAuthorizationManger tokenAuthorization) : ControllerBase {
    //api/Courses/UploadCourse/{ucid}
    [HttpPost]
    [Route("Create")]
    public ActionResult<Guid> POST([FromHeader(Name = "Authorization")] string Authorization, [FromBody] CreateCourse course) {
        if (!tokenAuthorization.IsAuthorized(course.uuid, Authorization, PresetTokenPermissions.permissionsLevelZero, out var response1))
            return Unauthorized(response1);
        var ucid = courses.CreateCourse(course.uuid, course, out var response2);
        if (response2 != string.Empty) return BadRequest(response2);
        return Ok(ucid);
    }

    //api/Courses/Upload/{ucid}
    [HttpPut]
    [Route("Upload/{ucid:guid}")]
    [DisableRequestSizeLimit]
    [RequestFormLimits(MultipartBodyLengthLimit = long.MaxValue)]
    public async Task<IActionResult> PUT(Guid ucid, [FromHeader(Name = "Authorization")] string Authorization, IFormFile? file) {
        if (!courses.TryGetCourse(ucid, out var courseData)) return NotFound();
        foreach (var rp in courseData.RequiredPermissions) {
            if (tokenAuthorization.IsAuthorized(Authorization, rp, out var response)) continue;
            return Unauthorized(response);
        }
        if (file == null || file.Length == 0)
            return BadRequest("No video uploaded.");

        await using var stream = file.OpenReadStream();
        courses.SetVideo(ucid, stream);

        return Ok();
    }

    //api/Courses/{ucid}
    [HttpGet]
    [Route("{ucid:guid}")]
    public ActionResult<CourseData> GetCourseData(Guid ucid) {
        var tryGetCourse = courses.TryGetCourse(ucid, out var data);
        if (!tryGetCourse) return NotFound();
        return Ok(data);
    }

    //api/Courses/Video/{ucid}
    [HttpGet]
    [Route("Video/{ucid:guid}")]
    public IActionResult GetVideo([FromHeader(Name = "Authorization")] string Authorization, Guid ucid) {
        if (!tokenAuthorization.IsAuthorized(Authorization, ucid.ToString(), out var response1)) return Unauthorized(response1);
        var stream = courses.GetVideo(ucid);

        return File(stream, "video/mp4", $"{ucid}.mp4");
    }
}