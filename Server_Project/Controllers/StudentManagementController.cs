using GradeDO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Server_Project.Configuration;
using Server_Project.Models;

namespace Server_Project.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class StudentManagementController : Controller
    {
        IConfiguration _configuration;
        IStudents _studentsList;
        ILogger<StudentManagementController> _logger;
        PercentagesForExercise _percents;
        public StudentManagementController(IConfiguration configuration, IStudents studentsList, ILogger<StudentManagementController> logger, IOptions<PercentagesForExercise> percents)
        {
            _configuration = configuration;
            _studentsList = studentsList;
            _logger = logger;
            _percents = percents.Value;
        }
        //הוספת תלמידה חדשה למערכת 
        [HttpPost("AddStudent")]
        public IActionResult AddStudent([FromBody] M_Student m_student)
        {
            if (m_student == null)
            {
                _logger.LogError("didn't manage to add a student - a null student");
                throw new ArgumentNullException(nameof(m_student));
            }
            Student student = m_student.Convert();
            _studentsList.AddStudent(student);
            _logger.LogDebug($"Student with ID: {student.ID} has been updated. The Name is: {student.Name}");
            return Ok(student);
        }

        //מחיקת תלמידה קיימת מהמערכת
        [HttpDelete("")]
        public IActionResult DeleteStudent(string id)
        {
            Student student = _studentsList.ReturnStudent(id);
            _studentsList.RemoveStudent(student);
            _logger.LogDebug($"Student with ID: {student.ID} remove from the list");
            return Ok(student);
        }
        //עריכת תלמידה
        [HttpPut("{EditStudent}")]
        public IActionResult EditStudent([FromQuery]string id,[FromQuery] string newName)
        {
            _studentsList.EditStudent(id, newName);
            _logger.LogDebug($"student with ID: {id} has been updated.New Name: {newName}");
            return Ok("The student's details have been successfully updated");
        }
        //הצגת כל התלמידות
        [HttpGet("DisplayAllStudents")]
        public IActionResult DisplayAllStudents()
        {
            _logger.LogDebug("Display All Students");
            return Ok(_studentsList.DisplayAllStudents());
        }
        // צפיה בכל הציונים של תלמידה מסוימת
        [HttpGet("DisplayAllGradesToStudent")]
        public IActionResult DisplayAllGradesToStudent([FromQuery]string id)
        {
            _logger.LogDebug("Display All Grades To Student");
            return Ok(_studentsList.DisplayAllGradesToStudent(id));
        }


    }
}
