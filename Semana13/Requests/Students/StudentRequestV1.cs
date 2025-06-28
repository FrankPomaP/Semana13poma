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
}
