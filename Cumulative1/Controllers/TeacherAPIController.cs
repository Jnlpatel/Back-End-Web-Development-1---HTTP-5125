using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cumulative1.Models;
using System;
using MySql.Data.MySqlClient;



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
    }


}

