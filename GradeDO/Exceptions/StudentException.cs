using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeDO.Exceptions
{
    public class statuscodeException : Exception
    {
        public int StatusCode { get; set; }

        public statuscodeException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }

    public class StudentNotExsistException : statuscodeException
    {
        public StudentNotExsistException(string studentId) : base($"The student with Id {studentId} does not exist", 498)
        {
        }
    }

    public class StudentAlradyExsistException : statuscodeException
    {
        public StudentAlradyExsistException(string studentId) : base($"The student with Id {studentId} already exists", 498)
        {
        }
    }

    public class ListsAreNotEqualInLengthException : statuscodeException
    {
        public ListsAreNotEqualInLengthException() : base("The number of grades is not equal to the number of students", 498)
        {
        }
    }

    public class ExerciseDoesNotExistException : statuscodeException
    {
        public ExerciseDoesNotExistException(int exeNumber) : base($"The exercise number {exeNumber} does not exist", 498)
        {
        }
    }

    public class IncorrectPasswordException : statuscodeException
    {
        public IncorrectPasswordException(string id, string password) : base($"The password - {password} isn't compatible with the id {id}", 498)
        {
        }
    }
}

