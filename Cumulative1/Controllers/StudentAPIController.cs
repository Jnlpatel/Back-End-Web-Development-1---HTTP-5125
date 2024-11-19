using Microsoft.AspNetCore.Mvc;
using Cumulative1.Models;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Cumulative1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public StudentAPIController(SchoolDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all students from the database
        /// </summary>
        /// <returns>A list of all students</returns>
        [HttpGet]
        [Route("ListOfStudents")]
        public List<Student> ListOfStudents()
        {
            List<Student> students = new List<Student>();

            using (MySqlConnection connection = _context.AccessDatabase())
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM students"; // Replace with actual query if necessary
                Console.WriteLine("here");
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Student student = new Student
                        {
                            StudentId = Convert.ToInt32(reader["StudentId"]),
                            StudentFName = reader["StudentFName"].ToString(),
                            StudentLName = reader["StudentLName"].ToString(),
                            StudentNumber = reader["StudentNumber"].ToString(),
                            EnrolDate = Convert.ToDateTime(reader["EnrolDate"])
                        };
                        students.Add(student);
                    }
                }
            }
            return students;
        }

        /// <summary>
        /// Get a student by their ID
        /// </summary>
        /// <param name="id">The ID of the student</param>
        /// <returns>Details of the student</returns>
        [HttpGet]
        [Route("FindStudentDetail/{id}")]
        public Student FindStudentDetail(int id)
        {
            Student student = null;

            using (MySqlConnection connection = _context.AccessDatabase())
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM students WHERE StudentId=@id";
                command.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        student = new Student
                        {
                            StudentId = Convert.ToInt32(reader["StudentId"]),
                            StudentFName = reader["StudentFName"].ToString(),
                            StudentLName = reader["StudentLName"].ToString(),
                            StudentNumber = reader["StudentNumber"].ToString(),
                            EnrolDate = Convert.ToDateTime(reader["EnrolDate"])
                        };
                    }
                }
            }

            return student;
        }
    }
}
