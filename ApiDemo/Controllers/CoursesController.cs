using ApiDemo.Mangers.Interfaces;
using ApiDemo.Models.Courses;
using ApiDemo.Models.Courses.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace ApiDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController(IFileManger fileManger, ICoursesManger courses, ITokenAuthorizationManger tokenAuthorization) : ControllerBase {
    //todo fix the fact that the user uuid is used as the ucid
    //api/Courses/UploadCourse/{ucid}
    [HttpPost]
    [Route("Create")]
    public ActionResult<CreateCourseResponse> POST([FromHeader(Name = "Authorization")] string Authorization, [FromBody] CreateCourse course) {
        if (!tokenAuthorization.IsAuthorized(course.uuid, Authorization, PresetTokenPermissions.permissionsLevelZero, out var response1)) {
            return Unauthorized(response1);
        }
        Guid ucid = (courses.CreateCourse(course.uuid, course, out var response2));
        if (response2 != string.Empty) {
            return BadRequest(response2);
        }
        CreateCourseResponse response = new(ucid);
        return Ok(ucid);
    }

    //api/Courses/Upload/{ucid}
    [HttpPut]
    [Route("Upload/{ucid:guid}")]
    [DisableRequestSizeLimit]
    [RequestFormLimits(MultipartBodyLengthLimit = long.MaxValue)]
    public async Task<IActionResult> PUT(Guid ucid, [FromHeader(Name = "Authorization")] string Authorization, IFormFile? file) {
        //todo add code which checks if there is even a course with this ucid
        //todo add checks if the user has permissions todo this
        if (file == null || file.Length == 0)
            return BadRequest("No video uploaded.");

        await using Stream stream = file.OpenReadStream();
        CourseData courseData = courses.GetCourse(ucid);
        courses.SetVideo(ucid, stream);
        
        return Ok();
    }

    //api/Courses/{ucid}
    [HttpGet]
    [Route("{ucid:guid}")]
    public ActionResult<CourseData> Get(Guid ucid) {
        return Ok(courses.GetCourse(ucid));
    }

    //api/Courses/Video/{ucid}
    [HttpGet]
    [Route("Video/{ucid:guid}")]
    public async Task<IActionResult> Get2(Guid ucid) {
        // if (!tokenAuthorization.IsAuthorized(Authorization, ucid.ToString(), out var response1)) {
        //     return Unauthorized(response1);
        // }
        var stream = courses.GetVideo(ucid);

        return File(stream, "video/mp4", $"{ucid}.mp4");
    }
}