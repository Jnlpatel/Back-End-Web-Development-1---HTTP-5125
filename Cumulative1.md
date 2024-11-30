
---

# Cumulative1 - ASP.NET Core MVC and Web API

This project demonstrates a **Teacher Management System** built with **ASP.NET Core MVC and Web API**. The system provides **CRUD operations** on the `Teachers` table of the school database and supports dynamic server-rendered pages for user interaction.

---

## Features

1. **Add Teacher**: Users can add new teachers with details like name, hire date, and salary.
2. **Delete Teacher**: Users can delete a teacher by ID.
3. **List Teachers**: Displays all teachers in the database.
4. **Dynamic Web Pages**
   - **Teacher List View** (`/Teacher/List.cshtml`)
   - **Teacher Detail View** (`/Teacher/Show.cshtml`)
   - **Teacher Add View** (`/Teacher/Add.cshtml`)

---

## API Endpoints

1. **GET: api/TeacherAPI/ListOfTeachers**
   - Retrieves a list of all teachers in the database.

2. **GET: api/TeacherAPI/FindTeacherDetail/{id}**
   - Retrieves details of a specific teacher by ID.

3. **POST: api/TeacherAPI/AddTeacher**
   - Adds a new teacher to the database.

4. **DELETE: api/TeacherAPI/DeleteTeacher/{id}**
   - Deletes a teacher from the database.


## Testing

**API Testing**:
- Use `cURL` to test endpoints:
  ```bash
  curl -X GET "http://localhost:5000/api/TeacherAPI/ListOfTeachers"
  curl -X GET "http://localhost:5000/api/TeacherAPI/FindTeacherDetail/1"
  curl -X GET "http://localhost:5000/api/TeacherAPI/AddTeacher"
  curl -X GET "http://localhost:5000/api/TeacherAPI/DeleteTeacher/1"
  ```

**Web Page Testing**:
- Visit:
  - `http://localhost:<port>/Teacher/List`
  - `http://localhost:<port>/Teacher/Show/{id}`
  - `http://localhost:<port>/Teacher/New`

---

## How to Run

1. **Clone Repository**:
   ```bash
   git clone https://github.com/Jnlpatel/Back-End-Web-Development-1---HTTP-5125.git
   cd Back-End-Web-Development-1---HTTP-5125
   cd Cumulative1
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
