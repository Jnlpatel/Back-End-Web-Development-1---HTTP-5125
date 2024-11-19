namespace Cumulative1.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public int TeacherId { get; set; } // Foreign key to the Teacher table

        // Navigation property to the Teacher
        public Teacher Teacher { get; set; }
    }
}
