using GradeDO;
using GradeDO.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Server_Project.Configuration;
using Server_Project.Models;
using Server_Project.Services;
using static GradeDO.Exceptions.StudentNotExsistException;

namespace Server_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesManagementController : Controller
    {
        GradeManager _gradeManager;
        IConfiguration _configuration;
        IStudents _studentsList;
        ILogger<GradesManagementController> _logger;
        PercentagesForExercise _percents;
        public GradesManagementController(IConfiguration configuration, IStudents studentsList, ILogger<GradesManagementController> logger, IOptions<PercentagesForExercise> percents, GradeManager gradeManager)
        {
            _configuration = configuration;
            _studentsList = studentsList;
            _logger = logger;
            _percents = percents.Value;
            _gradeManager = gradeManager;
        }
      //  הוספת ציונים לתלמידות
        [HttpPost("AddGradesToStudents")]
        public IActionResult AddGradesToStudents([FromBody]AddGradesRequest request)
        {
            if (request.M_Grade == null || request.ListId == null)
            {
                _logger.LogError("There was an error entering grades for students.\nOne of the inputs was NULL");
                throw new ArgumentNullException();
            }
         request.M_Grade.ForEach(grade => { grade.ExeNumber =request.ExeNumber; });
            List<Grade> list_grades = request.M_Grade.Select(g => g.Convert()).ToList();
            List<Student> list_students =request.ListId.Select(id => _studentsList.ReturnStudent(id)).ToList();
            _studentsList.InsertGradesToStudents(list_grades, list_students);
            _logger.LogDebug("Grades were entered for the students according to the number of exercises");
            return Ok(list_grades);

        }
        //עריכת ציון ספציפי לתלמידה ספציפית
        [HttpPut("EditGradeToStudent")]
        public IActionResult EditGradeToStudent([FromQuery]string id, [FromBody]M_Grade m_grade)
        {
            if (m_grade == null)
            {
                _logger.LogError("didn't manage EditGradeToStudent - grade is null");
                throw new ArgumentNullException();
            }
            Grade grade = m_grade.Convert();
            _studentsList.EditGradeToStudent(id, grade);
            _logger.LogDebug($"A grade {grade.Name} for a student with {id} ID has been successfully updated");
            return Ok(_studentsList.ReturnStudent(id));

        }
        //הצגת כל הציונים של תרגיל מסוים
        [HttpGet("DisplayAllGradesOfSpecificExercise")]
        public IActionResult DisplayAllGradesOfSpecificExercise([FromQuery]int exeNumber)
        {
            if (exeNumber > _percents.NumOfExe)
            {
                _logger.LogError($"Exercise number {exeNumber} does not exist in the system");
                throw new ExerciseDoesNotExistException(exeNumber);
            }

            List<int> exeGrades = _studentsList.DisplayAllGradesOfSpecificExercise(exeNumber);            
            _logger.LogDebug($"Viewing scores for exercise number{exeNumber}");
            return Ok(exeGrades);
        }
        //	חישוב ציון סופי על פי פרמטרים
        [HttpGet("CalculateFinalGrade")]
        public IActionResult CalculateFinalGrade([FromQuery]string StudentId)
        {
            double finalGrade = _gradeManager.CalculateFinalGrade(StudentId);
            _logger.LogDebug($"The final grade of a student with {StudentId} ID : {finalGrade}");
            return Ok(finalGrade);
        }
        // צפיה בכל התרגילים וכל הציונים 
        [HttpGet("ShowAllGradesToAllStudents")]
        public IActionResult ShowAllGradesToAllStudents()
        {
            Dictionary<int, List<int>> gradesDictionary = new Dictionary<int, List<int>>();
            for (int i = 1; i <= _percents.NumOfExe; i++)
            {
                List<int> grades = (_studentsList.DisplayAllGradesOfSpecificExercise(i));
                gradesDictionary[i] = grades;
            }
            _logger.LogDebug("Viewing all the exercises with their score list");
            return Ok(gradesDictionary);
        }
    }
}
