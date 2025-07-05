using Microsoft.AspNetCore.Mvc;
using Semana13.Data;
using Semana13.Models;
using Semana13.Requests.enrollment;


namespace Semana13.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EnrollmentsCustomController : ControllerBase
    {
        private readonly DemoContext _context;

        public EnrollmentsCustomController(DemoContext context)
        {
            _context = context;
        }

        [HttpPost]
        public void InsertEnrollments([FromBody] EnrollmentWithCourseListRequest request)
        {
            var dateNow = DateTime.Now;

            var enrollments = request.Courses.Select(c => new Enrollment
            {
                StudentId = request.StudentId,
                CourseId = c.CourseId,
                Date = dateNow,
                Activo = 1
            }).ToList();

            _context.Enrollments.AddRange(enrollments);
            _context.SaveChanges();
        }
    }
}
