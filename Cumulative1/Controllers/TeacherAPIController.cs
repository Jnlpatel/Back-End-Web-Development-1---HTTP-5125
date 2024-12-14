using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cumulative1.Models;
using System;
using MySql.Data.MySqlClient;
using Mysqlx.Datatypes;



namespace Cumulative1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherAPIController : ControllerBase
    {

        // This is dependancy injection
        private readonly SchoolDbContext _context;
        public TeacherAPIController(SchoolDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// When we click on Teachers tab in Navigation bar on Home page, We are directed to a webpage that lists all teachers in the database school
        /// </summary>
        /// <example>
        /// GET api/Teacher/ListTeachers -> [{"TeacherFname":"Jiya", "TeacherLName":"Patel"},{"TeacherFname":"Sima", "TeacherLName":"Mehta"},.............]
        /// GET api/Teacher/ListTeachers -> [{"TeacherFname":"Urvi", "TeacherLName":"Shah"},{"TeacherFname":"Raj", "TeacherLName":"Patel"},.............]
        /// </example>
        /// <returns>
        /// A list all the teachers in the database school
        /// </returns>


        [HttpGet]
        [Route(template: "ListOfTeachers")]
        public List<Teacher> ListOfTeachers()
        {
            // Create an empty list of Teachers
            List<Teacher> teachers = new List<Teacher>();

            // 'using' keyword is used that will close the connection by itself after executing the code given inside
            using (MySqlConnection Connection = _context.AccessDatabase())
            {

                // Opening the connection
                Connection.Open();


                // Establishing a new query for our database 
                MySqlCommand Command = Connection.CreateCommand();


                // Writing the SQL Query we want to give to database to access information
                Command.CommandText = "select * from teachers";


                // Storing the Result Set query in a variable
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {

                    // While loop is used to loop through each row in the ResultSet 
                    while (ResultSet.Read())
                    {

                        // Accessing the information of Teacher using the Column name as an index
                        int T_Id = Convert.ToInt32(ResultSet["teacherid"]);
                        string FName = ResultSet["teacherfname"].ToString();
                        string LName = ResultSet["teacherlname"].ToString();
                        string EmpNumber = ResultSet["employeenumber"].ToString();
                        DateTime HireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                        decimal Salary = Convert.ToDecimal(ResultSet["salary"]);


                        // Assigning short names for properties of the Teacher
                        Teacher EachTeacher = new Teacher()
                        {
                            TeacherId = T_Id,
                            TeacherFName = FName,
                            TeacherLName = LName,
                            TeacherHireDate = HireDate,
                            EmployeeNumber = EmpNumber,
                            TeacherSalary = Salary
                        };


                        // Adding all the values of properties of EachTeacher in Teachers List
                        teachers.Add(EachTeacher);

                    }
                }
            }


            //Return the final list of Teachers 
            return teachers;
        }


        /// <summary>
        /// When we select one teacher , it returns information of the selected Teacher in the database by their ID 
        /// </summary>
        /// <example>
        /// GET api/Teacher/FindTeacher/3 -> {"TeacherId":3,"TeacherFname":"Sam","TeacherLName":"Cooper"}
        /// </example>
        /// <returns>
        /// Information about the Teacher selected
        /// </returns>



        [HttpGet]
        [Route(template: "FindTeacherDetail/{id}")]
        public Teacher FindTeacherDetail(int id)
        {

            // Created an object using Teacher definition defined as Class in Models
            Teacher TeacherData = new Teacher();


            // 'using' keyword is used that will close the connection by itself after executing the code 
            using (MySqlConnection Connection = _context.AccessDatabase())
            {

                // Opening Connection
                Connection.Open();

                MySqlCommand Command = Connection.CreateCommand();


                // @id is replaced with a 'sanitized'(masked) id so that id can be referenced

                Command.CommandText = "select * from teachers where teacherid=@id";
                Command.Parameters.AddWithValue("@id", id);


                // Storing the Result Set query 
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {

                    // While loop is used to loop through each row  
                    while (ResultSet.Read())
                    {

                        // Accessing the information of Teacher 
                        int T_Id = Convert.ToInt32(ResultSet["teacherid"]);
                        string FName = ResultSet["teacherfname"].ToString();
                        string LName = ResultSet["teacherlname"].ToString();
                        string EmpNumber = ResultSet["employeenumber"].ToString();
                        DateTime HireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                        decimal Salary = Convert.ToDecimal(ResultSet["salary"]);


                        // Accessing the information of the properties of Teacher 
                        // created above for all properties of the Teacher
                        TeacherData.TeacherId = T_Id;
                        TeacherData.TeacherFName = FName;
                        TeacherData.TeacherLName = LName;
                        TeacherData.TeacherHireDate = HireDate;
                        TeacherData.EmployeeNumber = EmpNumber;
                        TeacherData.TeacherSalary = Salary;

                    }
                }
            }


            //Return the Information of the Teacher
            return TeacherData;
        }

        /// <summary>
        /// Adds a teacher to the database.
        /// </summary>
        /// <param name="newTeacher">Teacher object containing details of the new teacher.</param>
        /// <example>
        /// POST: api/TeacherAPI/AddTeacher
        /// Headers: Content-Type: application/json
        /// Request Body:
        /// {
        ///     "TeacherFName": "John",
        ///     "TeacherLName": "Doe",
        ///     "EmployeeNumber": "T12345",
        ///     "TeacherHireDate": "2023-01-15",
        ///     "TeacherSalary": 55000
        /// } -> 200
        /// </example>
        /// <returns>
        /// The inserted Teacher Id from the database if successful. 0 if unsuccessful.
        /// </returns>
        /// 
        [HttpPost]
        [Route("AddTeacher")]
        public IActionResult AddTeacher([FromBody] Teacher newTeacher)
        {
            if (string.IsNullOrWhiteSpace(newTeacher.TeacherFName) || string.IsNullOrWhiteSpace(newTeacher.TeacherLName))
                return BadRequest("First Name and Last Name cannot be empty.");

            if (newTeacher.TeacherHireDate > DateTime.Now)
                return BadRequest("Hire date cannot be in the future.");

            using (MySqlConnection connection = _context.AccessDatabase())
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = @"
            INSERT INTO teachers (teacherfname, teacherlname, employeenumber, hiredate, salary)
            VALUES (@fname, @lname, @empnum, @hiredate, @salary)";
                command.Parameters.AddWithValue("@fname", newTeacher.TeacherFName);
                command.Parameters.AddWithValue("@lname", newTeacher.TeacherLName);
                command.Parameters.AddWithValue("@empnum", newTeacher.EmployeeNumber);
                command.Parameters.AddWithValue("@hiredate", newTeacher.TeacherHireDate);
                command.Parameters.AddWithValue("@salary", newTeacher.TeacherSalary);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    if (ex.Message.Contains("Duplicate entry"))
                        return Conflict("Employee number already exists.");
                    return StatusCode(500, "An error occurred while adding the teacher.");
                }
            }

            return Ok("Teacher added successfully.");
        }

        /// <summary>
        /// Deletes a teacher from the database.
        /// </summary>
        /// <param name="id">Primary key of the teacher to delete.</param>
        /// <example>
        /// DELETE: api/TeacherAPI/DeleteTeacher/3 -> 1
        /// </example>
        /// <returns>
        /// Number of rows affected by the delete operation.
        /// </returns>
        /// 
        [HttpDelete]
        [Route("DeleteTeacher/{id}")]
        public IActionResult DeleteTeacher(int id)
        {
            using (MySqlConnection connection = _context.AccessDatabase())
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM teachers WHERE teacherid = @id";
                command.Parameters.AddWithValue("@id", id);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 0)
                    return NotFound("Teacher not found.");

                return Ok("Teacher updated successfully.");
            }
        }

        /// <summary>
        /// Updates a Teacher in the database. Data is provided as a Teacher object, and the request query contains the Teacher ID.
        /// </summary>
        /// <param name="TeacherData">The Teacher object containing updated information.</param>
        /// <param name="TeacherId">The primary key ID of the Teacher to update.</param>
        /// <example>
        /// PUT: api/Teacher/UpdateTeacher/4
        /// Headers: Content-Type: application/json
        /// Request Body:
        /// {
        ///     "TeacherFName": "John",
        ///     "TeacherLName": "Doe",
        ///     "HireDate": "2020-01-01",
        ///     "Salary": 55000.00
        /// }
        /// Response:
        /// {
        ///     "TeacherId": 4,
        ///     "TeacherFName": "John",
        ///     "TeacherLName": "Doe",
        ///     "HireDate": "2020-01-01",
        ///     "Salary": 55000.00
        /// }
        /// </example>
        /// <returns>
        /// The updated Teacher object if successful, or a NotFound result if the teacher does not exist.
        /// </returns>

        [HttpPost("UpdateTeacher/{id}")]
        public IActionResult UpdateTeacher(int id, [FromBody] Teacher updatedTeacher)
        {
            
            if (updatedTeacher == null)
                return BadRequest("Invalid data. Please check the request body.");

            if (string.IsNullOrWhiteSpace(updatedTeacher.TeacherFName) || string.IsNullOrWhiteSpace(updatedTeacher.TeacherLName))
                return BadRequest("First Name and Last Name cannot be empty.");

            if (updatedTeacher.TeacherHireDate > DateTime.Now)
                return BadRequest("Hire date cannot be in the future.");

            if (updatedTeacher.TeacherSalary < 0)
                return BadRequest("Salary cannot be negative.");

            using (MySqlConnection connection = _context.AccessDatabase())
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE teachers SET teacherfname=@fname, teacherlname=@lname, employeenumber=@empnum, " +
                                      "hiredate=@hiredate, salary=@salary WHERE teacherid=@id";
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@fname", updatedTeacher.TeacherFName);
                command.Parameters.AddWithValue("@lname", updatedTeacher.TeacherLName);
                command.Parameters.AddWithValue("@empnum", updatedTeacher.EmployeeNumber);
                command.Parameters.AddWithValue("@hiredate", updatedTeacher.TeacherHireDate);
                command.Parameters.AddWithValue("@salary", updatedTeacher.TeacherSalary);

               
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows == 0)
                    return NotFound($"Teacher with ID {id} not found or no changes detected.");
            }

            return Ok("Teacher updated successfully.");
        }



    }


}

