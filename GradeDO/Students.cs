using GradeDO.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GradeDO.Exceptions.StudentNotExsistException;

namespace GradeDO
{
    public class Students : IStudents
    {
        DataSource studentsList = new DataSource();
        public Students()
        {
            studentsList.Initialize();
        }

        //פונקציות תלמידה 
        // הוספת תלמידה
        public void AddStudent(Student student)
        {
            if (studentsList.Students.Any(stu => stu.ID == student.ID))
                throw new StudentNotExsistException(student.ID);
            student.Password = student.ID;
            studentsList.Students.Add(student);

        }
        // מקבלת ID תלמידה ומחזירה אותה
        public Student ReturnStudent(string StudentId)
        {
            Student student = studentsList.Students.FirstOrDefault(stu => stu.ID == StudentId);
            if (student == null)
                throw new StudentAlradyExsistException(StudentId);
            return student;
        }
        //מחיקת תלמידה
        public void RemoveStudent(Student student)
        {
            if (!studentsList.Students.Any(stu => stu.ID == student.ID))
                throw new StudentNotExsistException(student.ID);
            studentsList.Students.Remove(student);
        }
        //עריכת תלמידה
        public void EditStudent(string StudentId, string newName)
        {
            Student student = ReturnStudent(StudentId);
            student.Name = newName;
        }
        // הצגת תלמידה
        public string DisplayStudent(string StudentId)
        {
            Student student = ReturnStudent(StudentId);
            return student.ToString();
        }
        //הצגת כל התלמידות
        public List<string> DisplayAllStudents()
        {
            List<string> studentsDetailes = new List<string>();
            foreach (Student student in studentsList.Students)
            {
                studentsDetailes.Add(DisplayStudent(student.ID));
            }
            return studentsDetailes;
        }
        // פונקציות ציונים
        //פונקציה שמקבלת רשימה של ציונים ותלמידות ומכניסה לכל תלמידה ציון
        public void InsertGradesToStudents(List<Grade> grades, List<Student> stusents)
        {
            if (grades.Count != stusents.Count)
                throw new ListsAreNotEqualInLengthException();
            for (int i = 0; i < stusents.Count; i++)
            {
                AddGradeToStudent(stusents[i].ID, grades[i]);
            }
        }
        public List<Student> GetAllStudents()
        {
            return studentsList.Students;
        }
        //	פונקציה שמקבלת ת.ז. של תלמידה, וציון ומעדכנת לה מוחקת אם קיים ישן
        public void EditGradeToStudent(string StudentId, Grade grade)
        {
            Student student = ReturnStudent(StudentId);

            foreach (Grade g in student.ExeList)
            {
                if (g.Name.Equals(grade.Name))
                {
                    g.GradeNumber = grade.GradeNumber;
                    g.Comment = grade.Comment;
                    return;
                }
            }
        }
        //פונקציה שמקבלת ת.ז. וקוד תרגיל ומחזירה את הציון של התלמידה
        public int ShowGradeToStudent(string StudentId, int exeNumber)
        {
            Student student = ReturnStudent(StudentId);
            foreach (Grade g in student.ExeList)
            {
                if (g.ExeNumber == exeNumber)
                {

                    return g.GradeNumber;
                }
            }
            return -1;
        }
        //הצגת כל הציונים של תמידה מסוימת
        public List<string> DisplayAllGradesToStudent(string StudentId)
        {
            Student student = ReturnStudent(StudentId);

            List<string> detailesGrades = new List<string>();
            foreach (Grade g in student.ExeList)
            {
                detailesGrades.Add(g.ToString());
            }
            return detailesGrades;
        }
        //הוספת ציון לתלמידה
        public void AddGradeToStudent(string StudentId, Grade grade)
        {
            Student student = ReturnStudent(StudentId);
            grade.Date = DateTime.Today;
            student.ExeList.Add(grade);
        }
        //צפיה בציונים של תרגיל ספציפי
        public List<int> DisplayAllGradesOfSpecificExercise(int exeNumber)
        {

            List<int> exeGrades = studentsList.Students
                .SelectMany(s => s.ExeList)
                .Where(g => g.ExeNumber == exeNumber)
                .Select(g => g.GradeNumber)
                .ToList();
            return exeGrades;
        }
        //הצגת ההגשה האחרונה של התלמידה 
        public Grade ReturnLastGradeOfTheStudent(Student student)
        {
            return student.ExeList[student.ExeList.Count - 1];
        }

    }
}
