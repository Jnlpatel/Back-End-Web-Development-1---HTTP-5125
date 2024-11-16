
using Microsoft.AspNetCore.Mvc;
using Cumulative1.Models;
using Microsoft.AspNetCore.Http;
using System;
using MySql.Data.MySqlClient;
namespace Cumulative1.Controllers
{
    public class TeacherPageController : Controller
    {

        // API is responsible for gathering the information from the Database and MVC is responsible for giving an HTTP response
        // as a web page that shows the information written in the View

        // In practice, both the TeacherAPI and TeacherPage controllers
        // should rely on a unified "Service", with an explicit interface

        private readonly TeacherAPIController _api;

        public TeacherPageController(TeacherAPIController api)
        {
            _api = api;
        }


        /// <summary>
        /// When we click on the Teachers button in Navugation Bar, it returns the web page displaying all the teachers in the Database school
        /// </summary>
        /// <returns>
        /// List of all Teachers in the Database school
        /// </returns>
        /// <example>
        /// GET : api/TeacherPage/List  ->  Gives the list of all Teachers in the Database school
        /// </example>

        public IActionResult List()
        {
            List<Teacher> teachers = _api.ListOfTeachers();
            return View(teachers);
        }



        /// <summary>
        /// When we Select one Teacher from the list, it returns the web page displaying the information of the SelectedTeacher from the database school
        /// </summary>
        /// <returns>
        /// Information of the Teacher from the database school
        /// </returns>
        /// <example>
        /// GET :api/TeacherPage/Show/{id}  ->  Gives the information of the Teacher
        /// </example>
        /// 
        public IActionResult Show(int id)
        {
            Teacher TeacherData = _api.FindTeacherDetail(id);
            return View(TeacherData);
        }


    }
}
