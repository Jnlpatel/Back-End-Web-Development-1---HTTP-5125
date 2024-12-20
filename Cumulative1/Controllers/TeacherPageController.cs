﻿
using Microsoft.AspNetCore.Mvc;
using Cumulative1.Models;
using Microsoft.AspNetCore.Http;
using System;
using MySql.Data.MySqlClient;
namespace Cumulative1.Controllers
{
    public class TeacherPageController : Controller
    {


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

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Teacher teacher)
        {
            IActionResult result = _api.AddTeacher(teacher);
            if (result is ObjectResult objectResult && objectResult.StatusCode != 200)
            {
                ViewData["Error"] = objectResult.Value.ToString();
                return View("New", teacher);
            }
            return RedirectToAction("List");
        }

        public IActionResult DeleteConfirm(int id)
        {
            Teacher teacher = _api.FindTeacherDetail(id);
            if (teacher == null)
            {
                return RedirectToAction("List");
            }
            return View(teacher);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            IActionResult result = _api.DeleteTeacher(id);
            if (result is ObjectResult objectResult && objectResult.StatusCode != 200)
            {
                ViewData["Error"] = objectResult.Value.ToString();
                return RedirectToAction("DeleteConfirm", new { id });
            }
            return RedirectToAction("List");
        }



        /// <summary>
        /// Displays a form for editing an existing teacher's information.
        /// </summary>
        /// <param name="id">The ID of the teacher to edit.</param>
        /// <returns>
        /// A view containing the form pre-filled with the selected teacher's information, or redirects to List if not found.
        /// </returns>
        public IActionResult Edit(int id)
        {
            Console.WriteLine(id);
            Teacher teacher = _api.FindTeacherDetail(id);
            if (teacher == null)
            {
                return RedirectToAction("List");
            }
            return View(teacher);
        }

        /// <summary>
        /// Updates a teacher's information.
        /// </summary>
        /// <param name="id">The ID of the teacher.</param>
        /// <param name="teacher">The updated teacher details.</param>
        [HttpPost]
        public IActionResult Update(int id, Teacher teacher)
        {
            Console.WriteLine(id);
            if (!ModelState.IsValid)
            {
                return View("Edit", teacher);
            }

            IActionResult result = _api.UpdateTeacher(id, teacher);
            if (result is BadRequestObjectResult badRequest)
            {
                ViewData["Error"] = badRequest.Value.ToString();
                return View("Edit", teacher);
            }

            return RedirectToAction("Show", new { id = id });

        }




    }
}
