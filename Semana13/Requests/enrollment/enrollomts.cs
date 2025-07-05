namespace Semana13.Requests.enrollment
{
    public class EnrollmentWithCourseListRequest
    {
        public int StudentId { get; set; }
        public List<CourseInfo> Courses { get; set; }
    }

    public class CourseInfo
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public int Credit { get; set; }
    }
}
