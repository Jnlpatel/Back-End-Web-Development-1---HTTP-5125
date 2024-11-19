namespace Cumulative1.Models
{
    public class Teacher
    {
        // Unique identifier for each teacher. It is used as the primary key in Teachers table.
        public int TeacherId { get; set; }

        // First name of the teacher. It stores the teacher's first name as a string.
        public string TeacherFName { get; set; }

        // Last name of the teacher. It stores the teacher's last name as a string.
        public string TeacherLName { get; set; }

        // The date when the teacher was hired. It is used to track employment start date.
        public DateTime TeacherHireDate { get; set; }

        // It is a unique employee number assigned to each teacher. 
        public string EmployeeNumber { get; set; }

        // It is the salary of the teacher. It is stored as a decimal to accommodate monetary values.
        public decimal TeacherSalary { get; set; }
    }
}
