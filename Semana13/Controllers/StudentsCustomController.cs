using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Semana13.Data;
using Semana13.Models;
using Semana13.Requests.Students;
using Semana13.Response.Students;
using System.Collections.Generic;
using System.Linq;

namespace Semana13.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentsCustomController : ControllerBase
    {
        private readonly DemoContext _context;

        public StudentsCustomController(DemoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<StudentResponseV1> GetStudents()
        {
            var students = _context.Students
                                   .Include(s => s.Grade)
                                   .Where(s => s.Activo == 1)
                                   .ToList();

            var response = students.Select(s => new StudentResponseV1
            {
                StudentId = s.StudentId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Phone = s.Phone,
                Email = s.Email,
                GradeId = s.GradeId,
                GradeName = s.Grade != null ? s.Grade.Name : "N/A"
            }).ToList();

            return response;
        }

        [HttpPost]
        public void InsertStudent([FromBody] StudentRequestV1 request)
        {
            if (!ModelState.IsValid)
            {
            }

            var student = new Student
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.Phone,
                Email = request.Email,
                GradeId = request.GradeId,
                Activo = 1
            };

            _context.Students.Add(student);
            _context.SaveChanges();
        }

        [HttpGet]
        public List<StudentResponseV1> SearchStudents([FromQuery] string searchTerm = null)
        {
            IQueryable<Student> query = _context.Students
                                                .Include(s => s.Grade)
                                                .Where(s => s.Activo == 1);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string lowerSearchTerm = searchTerm.ToLower();
                query = query.Where(s =>
                    s.FirstName.ToLower().Contains(lowerSearchTerm) ||
                    s.LastName.ToLower().Contains(lowerSearchTerm) ||
                    s.Email.ToLower().Contains(lowerSearchTerm)
                );
            }

            query = query.OrderByDescending(s => s.LastName);

            var students = query.ToList();

            var response = students.Select(s => new StudentResponseV1
            {
                StudentId = s.StudentId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Phone = s.Phone,
                Email = s.Email,
                GradeId = s.GradeId,
                GradeName = s.Grade != null ? s.Grade.Name : "N/A"
            }).ToList();

            return response;
        }

        [HttpPut]
        public void UpdateStudentContact([FromBody] StudentContactUpdateRequest request)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentId == request.StudentId && s.Activo == 1);

            if (student != null)
            {
                student.Phone = request.Phone;
                student.Email = request.Email;

                _context.SaveChanges();
            }
        }

        [HttpPut]
        public void UpdateStudentPersonal([FromBody] StudentPersonalUpdateRequest request)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentId == request.StudentId && s.Activo == 1);

            if (student != null)
            {
                student.FirstName = request.FirstName;
                student.LastName = request.LastName;

                _context.SaveChanges();
            }
        }

        [HttpPost]
        public void InsertStudentListByGrade([FromBody] StudentListByGradeRequest request)
        {
            var students = request.Students.Select(s => new Student
            {
                FirstName = s.FirstName,
                LastName = s.LastName,
                Phone = s.Phone,
                Email = s.Email,
                GradeId = request.GradeId,
                Activo = 1
            }).ToList();

            _context.Students.AddRange(students);
            _context.SaveChanges();
        }


    }
}