



using GradeDO;
using GradeDO.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Server_Project.Configuration;
using Server_Project.Services;
using static GradeDO.Exceptions.StudentNotExsistException;

namespace Server_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewingTheGradeController : Controller
    {
        GradeManager _gradeManager;
        IConfiguration _configuration;
        IStudents _studentsList;
        ILogger<StudentManagementController> _logger;
        PercentagesForExercise _percents;
        PasswordManager _passwordManager;
        public ViewingTheGradeController(IConfiguration configuration, IStudents studentsList, ILogger<StudentManagementController> logger, IOptions<PercentagesForExercise> percents, PasswordManager passwordManager, GradeManager gradeManager)
        {
            _configuration = configuration;
            _studentsList = studentsList;
            _logger = logger;
            _percents = percents.Value;
            _passwordManager = passwordManager;
            _gradeManager = gradeManager;
        }
        //הכנסה של ת.ז. וסיסמא וצפיה בציון של ההגשה האחרונה
        [HttpGet("ViewingGradeOfTheLastSubmission")]
        public IActionResult ViewingGradeOfTheLastSubmission([FromQuery] string id, [FromQuery] string password)
        {

            if (!_passwordManager.CheckPassword(id, password))
            {
                _logger.LogError($"the password - {password} isn't compatible to the ID - {id}");
                throw new IncorrectPasswordException(id, password);
            }
            _logger.LogDebug($"The student's last grade with the code: {id}");
            return Ok(_studentsList.ReturnLastGradeOfTheStudent(_studentsList.ReturnStudent(id)));
        }
        //הכנסה של ת.ז. סיסמא וקוד תרגיל, וצפיה בציון של תרגיל ספציפי
        [HttpGet("ViewingExeGrade")]
        public IActionResult ViewingExeGrade([FromQuery] string id, [FromQuery] string password, [FromQuery] int exeNum)
        {
            if (!_passwordManager.CheckPassword(id, password))
            {
                _logger.LogError($"the password - {password} isn't compatible to the ID - {id}");
                throw new IncorrectPasswordException(id, password);
            }
            if (exeNum > _percents.NumOfExe)
            {
                _logger.LogError($"Exercise number {exeNum} isnt the test code");
                throw new ExerciseDoesNotExistException(exeNum);
            }
            _logger.LogDebug($"Presenting the grade of exercise {exeNum} to a student with ID card {id}");
            return Ok(_studentsList.ShowGradeToStudent(id, exeNum));
        }
        //כנ"ל וצפיה בציון מבחן
        [HttpGet("ViewTestGrade")]
        public IActionResult ViewTestGrade([FromQuery] string id, [FromQuery] string password, [FromQuery] int exeNum)
        {
            if (!_passwordManager.CheckPassword(id, password))
            {
                _logger.LogError($"the password - {password} isn't compatible to the ID - {id}");
                throw new IncorrectPasswordException(id, password);
            }
            if (exeNum != 99)
            {
                _logger.LogError($"Exercise number {exeNum} does not exist in the system");
                throw new ExerciseDoesNotExistException(exeNum);
            }
            _logger.LogDebug($"Presenting the grade of the test {exeNum} to a student with ID card {id}");
            return Ok(_studentsList.ReturnStudent(id).TestGrade);
        }
        //כנ"ל וצפיה בציון סופי
        [HttpGet("ViewingTheFinalGrade")]
        public IActionResult ViewingTheFinalGrade([FromQuery] string id, [FromQuery] string password)
        {
            if (!_passwordManager.CheckPassword(id, password))
            {
                _logger.LogError($"the password - {password} isn't compatible to the ID - {id}");
                throw new IncorrectPasswordException(id, password);
            }

            _logger.LogDebug($"Viewing the final grade to the student with ID card {id}");
            return Ok(_gradeManager.CalculateFinalGrade(id));
        }
    }
}


