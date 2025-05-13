
namespace GradeDO
{
    public interface IStudents
    {
        void AddGradeToStudent(string StudentId, Grade grade);
        void AddStudent(Student student);
        List<int> DisplayAllGradesOfSpecificExercise(int exeNumber);
        List<string> DisplayAllGradesToStudent(string StudentId);
        List<string> DisplayAllStudents();
        string DisplayStudent(string StudentId);
        void EditGradeToStudent(string StudentId, Grade grade);
        void EditStudent(string StudentId, string newName);
        List<Student> GetAllStudents();
        void InsertGradesToStudents(List<Grade> grades, List<Student> stusents);
        void RemoveStudent(Student student);
        Grade ReturnLastGradeOfTheStudent(Student student);
        Student ReturnStudent(string StudentId);
        int ShowGradeToStudent(string StudentId, int exeNumber);
    }
}