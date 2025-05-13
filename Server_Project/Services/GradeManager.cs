using GradeDO;
using GradeDO.Exceptions;
using Microsoft.Extensions.Options;
using Server_Project.Configuration;
using Server_Project.Models;
using static GradeDO.Exceptions.StudentNotExsistException;

namespace Server_Project.Services
{
    public class GradeManager
    {

        IConfiguration _configuration;
        IStudents _studentsList;
        PercentagesForExercise _percents;
        public GradeManager(IConfiguration configuration, IStudents studentsList, IOptions<PercentagesForExercise> percents)
        {
            _configuration = configuration;
            this._studentsList = studentsList;
            _percents = percents.Value;
        }
        //פונקציה לחישוב ציון סופי לתלמידה

        public double CalculateFinalGrade(string StudentId)
        {
            double finalGrade = 0;
            Student student = _studentsList.GetAllStudents().FirstOrDefault(stu => stu.ID == StudentId);
            if (student == null)
                throw new StudentAlradyExsistException(StudentId);

            foreach (Grade g in student.ExeList)
            {
//return _configuration.GetValue<double>("PercentagesForExercise:exe1");
                if (g.ExeNumber == 1)
                finalGrade += _percents.exe1 * g.GradeNumber;
                if (g.ExeNumber == 2)
                    finalGrade += _percents.exe2 * g.GradeNumber;
                if (g.ExeNumber == 3)
                    finalGrade += _percents.exe3 * g.GradeNumber;
            }
            finalGrade += _percents.test * student.TestGrade.GradeNumber;

            return finalGrade;
        }


        //מחזירה מערך של פרטי תלמידה וציון סופי שלה
        public List<string> GetStudentsDetailesAndFinalGrade()
        {
            List<string> studentsDetailes = new List<string>();
            foreach (Student s in _studentsList.GetAllStudents())
            {
                studentsDetailes.Add($"{s.ToString()}, final grade: {CalculateFinalGrade(s.ID)}");
            }
            return studentsDetailes;
        }
        //מחשבת את ממוצע הציונים של קוד תרגיל מסוים
        public int CalculationOfExerciseAverage(int exeNumber)
        {
            int sumOfGrades = 0, countOfGrades = 0;
            foreach (Student s in _studentsList.GetAllStudents())
            {
                foreach (Grade g in s.ExeList)
                {
                    if (g.ExeNumber == exeNumber)
                    {
                        countOfGrades++;
                        sumOfGrades += g.GradeNumber;
                    }

                }
            }
            return sumOfGrades / countOfGrades;

        }
    }
}
