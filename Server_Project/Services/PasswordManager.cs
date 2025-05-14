


using GradeDO;
using GradeDO.Exceptions;
using static GradeDO.Exceptions.StudentNotExsistException;

namespace Server_Project.Services
{
    public class PasswordManager
    {
        IConfiguration _configuration;
        IStudents _studentsList;
        string _teacherPassword;
        string _teacherUserName;

        public PasswordManager(IConfiguration configuration, IStudents students)
        {
            _configuration = configuration;
            _studentsList = students;
            _teacherPassword = _configuration["Teacher:Password"];
            _teacherUserName = _configuration["Teacher:UserName"];
        }
        public bool CheckPassword(string password, string id)
        {
            // נניח ששם המורה הוא תעודת הזהות 
            if (_teacherUserName == id)
                if (password == _teacherPassword)
                    return true;
            Student student = _studentsList.GetAllStudents().FirstOrDefault(stu => stu.ID == id);
            if (student == null)
                throw new StudentAlradyExsistException(id);
            if (password == id)
                return true;
            return false;
        }
    }
}




