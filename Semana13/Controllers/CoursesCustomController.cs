using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Semana13.Data; // Asegúrate de importar el namespace del contexto
using Semana13.Requests.Courses;

namespace Semana13.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesCustomController : ControllerBase
    {
        private readonly DemoContext _context;

        public CoursesCustomController(DemoContext context)
        {
            _context = context;
        }

        [HttpPost]
        public void DeleteCourses([FromBody] Courserequestv1 request)
        {
            var coursesToDelete = _context.Courses
                .Where(c => request.CourseIds.Contains(c.CourseId) && c.Activo == 1)
                .ToList();

            foreach (var course in coursesToDelete)
            {
                course.Activo = 0;
            }

            _context.SaveChanges();
        }
    }
}
