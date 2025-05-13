using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeDO
{
    public class Student
    {
        public string ID {  get; set; }
        public string Name { get; set; }
        public string  Password { get; set; }
        public List<Grade> ExeList { get; set; }
        public Grade TestGrade { get; set; }

        public double GradeAverage()
        {
            return ExeList.Average(grade => grade.GradeNumber);
        }
        public override string ToString()
        {
            return $"student detailes: ID - {ID},  Name - {Name}, ExeList - {ExeList}, TestGrade - {TestGrade}, GradeAverage - {GradeAverage()}";
        }
        public Student()
        {
            ExeList = new List<Grade>();    
        }

    }
}
