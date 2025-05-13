using GradeDO;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Server_Project.Models
{
    public class M_Student
    {
        [BindRequired]
        [StringLength(9)]
        public string ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(9)]
        public string Password { get; set; }

        public List<Grade> ExeList { get; set; } = new List<Grade>(); // אתחול ברירת מחדל

        public Grade TestGrade { get; set; }

        public Student Convert()
        {
            Student student = new Student()
            {
                ID = ID,
                Name = Name,
                Password = Password,
                ExeList = ExeList ?? new List<Grade>(), // בדוק אם ExeList לא null
                TestGrade = TestGrade ?? new Grade()
            };
            return student;
        }
    }
}
