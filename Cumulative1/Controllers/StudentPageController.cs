using Microsoft.AspNetCore.Mvc;
using Cumulative1.Models;
using System.Collections.Generic;

namespace Cumulative1.Controllers
{
    public class StudentPageController : Controller
    {
        private readonly StudentAPIController _api;

        public StudentPageController(StudentAPIController api)
        {
            _api = api;
        }

        /// <summary>
        /// Displays a list of all students
        /// </summary>
        /// <example>
        /// GET : api/StudentPage/List  ->  Gives the list of all Students in the Database 
        /// </example>
        public IActionResult List()
        {
            List<Student> students = _api.ListOfStudents();
            return View(students); // Pass the list of students to the view
        }

        /// <summary>
        /// Displays details of a selected student by ID
        /// /// </summary>
        /// /// <example>
        /// GET :api/StudentPage/Show/{id}  ->  Gives the information of the Student
        /// </example>
        public IActionResult Show(int id)
        {
            Student student = _api.FindStudentDetail(id);
            return View(student); // Pass the student details to the view
        }
    }
}
