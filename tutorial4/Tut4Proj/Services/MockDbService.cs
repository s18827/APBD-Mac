using System.Xml.Linq;
using System;
using System.Collections.Generic;
using Tut4Proj.Models;
using System.Linq;

namespace Tut4Proj.Services
{
    public class MockDbService : IDbService
    {
        private static IEnumerable<Student> _students;

        static MockDbService()
        {
            _students = new List<Student>
            {
                new Student{IdStudent = 1, FirstName = "Jan", LastName = "Kowalski", IndexNumber = "s1234"},
                new Student{IdStudent = 2, FirstName = "Anna", LastName = "Król", IndexNumber = "s2456"},
                new Student{IdStudent = 3, FirstName = "Bob", LastName = "Marczak", IndexNumber = "s3444"}
            };
        }
        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }

        public string GetStudentById(int id)
        {
            foreach (var student in _students)
            {
                if (student.IdStudent == id)
                {
                    return student.ToString();
                }
            }
            return "no student found"; // should not occurr - case should be handled by IdExists(id) 
        }

        public bool IdExists(int id)
        {
            foreach (var student in _students)
            {
                if (student.IdStudent == id)
                {
                    return true;
                }
            }
            return false;
        }

        public string AddStudent(Student student)
        {
            ((List<Student>)_students).Add(student);
            return "Student add success";
        }

        public string EditStudentById(int id, string newFname, string newLname, string newIndexNumber)
        {
            foreach (var student in _students)
            {
                if (student.IdStudent == id)
                {
                    if (newFname != null)
                    {
                        student.FirstName = newFname;
                    }
                    if (newLname != null)
                    {
                        student.LastName = newLname;
                    }
                    if (newIndexNumber != null)
                    {
                        student.IndexNumber = newIndexNumber;
                    }
                    return "Student update success";
                }
            }
            return "Student update failure"; // shouldn't occurr
        }


        public string RemoveStudentById(int id)
        {
            var studentToRm = ((List<Student>)_students).SingleOrDefault(student => student.IdStudent == id);
            if (studentToRm != null)
            {
                ((List<Student>)_students).Remove(studentToRm);
                return "Student remove success";
            }
            return "Student remove failure";
        }

    }
}