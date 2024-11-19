
---

# Teacher Management System - ASP.NET Core MVC and Web API

This project demonstrates a **Teacher Management System** built with **ASP.NET Core MVC and Web API**. The system provides **CRUD operations** on the `Teachers` table of the school database and supports dynamic server-rendered pages for user interaction.

---

## Features

### **Quantitative (8 Marks)**
1. **API Endpoints**
   - **List All Teachers**
     ```csharp
     [HttpGet("ListOfTeachers")]
     public List<Teacher> ListOfTeachers()
     {
         // Returns all teachers from the database
     }
     ```
   - **Find Teacher by ID**
     ```csharp
     [HttpGet("FindTeacherDetail/{id}")]
     public Teacher FindTeacherDetail(int id)
     {
         // Returns details of a specific teacher by ID
     }
     ```

2. **Dynamic Web Pages**
   - **Teacher List View** (`/Teacher/List.cshtml`)
   - **Teacher Detail View** (`/Teacher/Show.cshtml`)

---


## Testing

**API Testing**:
- Use `cURL` to test endpoints:
  ```bash
  curl -X GET "http://localhost:5000/api/TeacherAPI/ListOfTeachers"
  curl -X GET "http://localhost:5000/api/TeacherAPI/FindTeacherDetail/1"
  ```

**Web Page Testing**:
- Visit:
  - `http://localhost:<port>/Teacher/List`
  - `http://localhost:<port>/Teacher/Show/{id}`

---

## How to Run

1. **Clone Repository**:
   ```bash
   git clone https://github.com/your-repo/teacher-management-system.git
   cd teacher-management-system
   ```

2. **Setup Database**:
   - Update `appsettings.json` with your MySQL connection details.

3. **Run Application**:
   - Open the solution in **Visual Studio** and start the project.

4. **Test**:
   - Use the API endpoints and web views as described above.

---

## Notes

### **Code Highlights**
- **Teacher Model**:
  ```csharp
  public class Teacher
  {
      public int TeacherId { get; set; }
      public string TeacherFName { get; set; }
      public string TeacherLName { get; set; }
      public DateTime TeacherHireDate { get; set; }
  }
  ```


---
