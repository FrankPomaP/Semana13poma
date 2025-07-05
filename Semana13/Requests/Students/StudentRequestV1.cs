using System.ComponentModel.DataAnnotations;

namespace Semana13.Requests.Students
{
    public class StudentRequestV1
    {
        
        public string FirstName { get; set; }

   
        public string LastName { get; set; }

        public string? Phone { get; set; } // Hacemos que sea anulable (nullable) si no es obligatorio

        public string Email { get; set; }

        public int GradeId { get; set; }
    }

    public class StudentContactUpdateRequest
    {
        public int StudentId { get; set; }
        public string? Phone { get; set; } 
        public string Email { get; set; }  
    }

    public class StudentPersonalUpdateRequest
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class StudentListByGradeRequest
    {
        public int GradeId { get; set; }
        public List<StudentBasicInfo> Students { get; set; }
    }

    public class StudentBasicInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; }
    }
}
