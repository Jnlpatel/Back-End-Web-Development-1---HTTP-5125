using Cumulative1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cumulative1.Controllers
{
    public class CoursePageController : Controller
    {
        private readonly CourseAPIController _api;

        public CoursePageController(CourseAPIController api)
        {
            _api = api;
        }

        /// <summary>
        /// Displays a list of all courses
        /// </summary>
        public IActionResult List()
        {
            List<Course> courses = _api.ListOfCourses();
            return View(courses); // Pass the list of courses to the view
        }

        /// <summary>
        /// Displays details of a selected course by ID
        /// </summary>
        public IActionResult Show(int id)
        {
            Course course = _api.FindCourseDetail(id);
            return View(course); // Pass the course details to the view
        }

    }
}
