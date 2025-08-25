using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiDemo.Models.Courses.Course;

public class CreateCourseResponse(Guid courseId) {
    [Required][JsonPropertyName("Ucid")] public Guid Ucid = courseId;
}