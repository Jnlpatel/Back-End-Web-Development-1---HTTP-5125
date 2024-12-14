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

        public IActionResult Add()
        {
            return View(); // Show a form for adding a new student
        }

        // Action to handle adding a student from the form submission
        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                _api.AddStudent(student); // Call the API to add the student
                return RedirectToAction("List"); // Redirect to the student list after adding
            }
            return View(student); // If model is not valid, return the form again with validation errors
        }

        // Action to delete a student
        public IActionResult DeleteStudent(int id)
        {
            _api.DeleteStudent(id); // Call the API to delete the student
            return RedirectToAction("List"); // Redirect to the student list after deletion
        }
    
    }
}
