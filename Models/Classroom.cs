
using System.Collections.Generic;

namespace testclassroomAPI.Models
{
    public class Classroom
    {
        public string ClassroomId { get; set; }
        public string ClassroomName { get; set; }
        public List<Student> ClassStudent { get; set; }
        public List<Teacher> ClassTeacher { get; set; }
    }

    public class Student
    {
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public int StudentAge { get; set; }
        public string StudentTel { get; set; }
    }


    public class Teacher
    {
        public string TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string TeacherTel { get; set; }
        public string SubjectTaught { get; set; }
    }


}