using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testclassroomAPI.Models;
// using testclassroomAPI.Models.Student;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace testclassroomAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClassroomController : ControllerBase
    {
        public static List<Student> StudentData = new List<Student>
        {
            new Student { StudentId = "S00001", StudentName = "Student01",StudentAge = 17, StudentTel = "0212345202"},
            new Student { StudentId = "S00002", StudentName = "Student02",StudentAge = 18, StudentTel = "0874513100"},
        };

        public static List<Teacher> TeacherData = new List<Teacher>
        {
            new Teacher { TeacherId = "T00001", TeacherName = "Teacher01",TeacherTel = "031231230", SubjectTaught = "Computer Programming"},
            new Teacher { TeacherId = "T00002", TeacherName = "Teacher02",TeacherTel = "032155488", SubjectTaught = "Database Management"},
        };

        public static List<Classroom> ClassroomData = new List<Classroom>
        {

        };

        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetdataStudentAll()
        {
            return StudentData.ToList();
        }

        [HttpGet("{studentId}")]
        public ActionResult<Student> GetdataStudentByid(string studentId)
        {
            return StudentData.FirstOrDefault(it => it.StudentId == studentId.ToString());
        }

        [HttpPost]
        public Student AddDataStudent([FromForm] Student Studentx)
        {
            var id = Guid.NewGuid().ToString();
            var item = new Student
            {
                StudentId = id.ToString(),
                StudentName = Studentx.StudentName,
                StudentAge = Studentx.StudentAge,
                StudentTel = Studentx.StudentTel
            };
            StudentData.Add(item);
            return item;
        }

        [HttpPut]
        public Student EditDataStudent([FromForm] Student Studentx)
        {
            var find = StudentData.FirstOrDefault(it => it.StudentId == Studentx.StudentId);
            var item = new Student
            {
                StudentId = find.StudentId.ToString(),
                StudentName = Studentx.StudentName,
                StudentAge = Studentx.StudentAge,
                StudentTel = Studentx.StudentTel
            };
            StudentData.Remove(find);
            StudentData.Add(item);
            for (int i = 0; i < ClassroomData.Count; i++)
            {
                if (ClassroomData[i].ClassStudent != null)
                {
                    for (int j = 0; j < ClassroomData[i].ClassStudent.Count; j++)
                    {
                        if (ClassroomData[i].ClassStudent[j] != null)
                        {
                            ClassroomData[i].ClassStudent.Remove(ClassroomData[i].ClassStudent.FirstOrDefault(it => it.StudentId == Studentx.StudentId));
                            ClassroomData[i].ClassStudent.Add(item);
                        }
                    }
                }
            }
            return item;
        }

        [HttpDelete("{studentId}")]
        public void DeleteDataStudent(string studentId)
        {
            var find = StudentData.FirstOrDefault(it => it.StudentId == studentId.ToString());
            StudentData.Remove(find);
        }

        //------------------------------------------------- Teacher ----------------------------------------------------------------

        [HttpGet]
        public ActionResult<IEnumerable<Teacher>> GetdataTeacherAll()
        {
            return TeacherData.ToList();
        }

        [HttpGet("{teacherId}")]
        public ActionResult<Teacher> GetdataTeacherByid(string teacherId)
        {
            return TeacherData.FirstOrDefault(it => it.TeacherId == teacherId.ToString());
        }

        [HttpPost]
        public Teacher AddDataTeacher([FromForm] Teacher Teacherx)
        {
            var id = Guid.NewGuid().ToString();
            var item = new Teacher
            {
                TeacherId = id.ToString(),
                TeacherName = Teacherx.TeacherName,
                TeacherTel = Teacherx.TeacherTel,
                SubjectTaught = Teacherx.SubjectTaught
            };
            TeacherData.Add(item);
            return item;
        }

        [HttpPut]
        public Teacher EditDataTeacher([FromForm] Teacher Teacherx)
        {
            var find = TeacherData.FirstOrDefault(it => it.TeacherId == Teacherx.TeacherId);
            var item = new Teacher
            {
                TeacherId = find.TeacherId.ToString(),
                TeacherName = Teacherx.TeacherName,
                TeacherTel = Teacherx.TeacherTel,
                SubjectTaught = Teacherx.SubjectTaught
            };
            TeacherData.Remove(find);
            TeacherData.Add(item);
            for (int i = 0; i < ClassroomData.Count; i++)
            {
                if (ClassroomData[i].ClassTeacher != null)
                {
                    for (int j = 0; j < ClassroomData[i].ClassTeacher.Count; j++)
                    {
                        if (ClassroomData[i].ClassTeacher[j] != null)
                        {
                            ClassroomData[i].ClassTeacher.Remove(ClassroomData[i].ClassTeacher.FirstOrDefault(it => it.TeacherId == Teacherx.TeacherId));
                            ClassroomData[i].ClassTeacher.Add(item);
                        }
                    }
                }
            }
            return item;
        }

        [HttpDelete("{teacherId}")]
        public void DeleteDataTeacher(string teacherId)
        {
            var find = TeacherData.FirstOrDefault(it => it.TeacherId == teacherId.ToString());
            TeacherData.Remove(find);
        }

        //----------------------------------------------------- Classroom -------------------------------------------------------

        [HttpGet]
        public ActionResult<IEnumerable<Classroom>> GetdataClassroomAll()
        {
            return ClassroomData.ToList();
        }

        [HttpGet("{classroomId}")]
        public ActionResult<Classroom> GetdataClassroomByid(string classroomId)
        {
            return ClassroomData.FirstOrDefault(it => it.ClassroomId == classroomId.ToString());
        }

        [HttpPost]
        public Classroom CreateClassroom([FromBody] Classroom Classroomx)
        {
            var id = Guid.NewGuid().ToString();
            var item = new Classroom
            {
                ClassroomId = id.ToString(),
                ClassroomName = Classroomx.ClassroomName,
                ClassStudent = null,
                ClassTeacher = null
            };
            ClassroomData.Add(item);
            return item;
        }

        [HttpGet("{classroomId}/{teacherId}")]
        public Classroom AddTeacherInClassroom(string classroomId, string teacherId)
        {
            var findClassroomOdd = ClassroomData.FirstOrDefault(it => it.ClassroomId == classroomId.ToString());
            var findClassroomNew = ClassroomData.FirstOrDefault(it => it.ClassroomId == classroomId.ToString());
            var findTeacher = TeacherData.FirstOrDefault(it => it.TeacherId == teacherId.ToString());
            List<Teacher> classTeacher01 = new List<Teacher>();

            if (findClassroomOdd.ClassTeacher != null)
            {
                classTeacher01.AddRange(findClassroomOdd.ClassTeacher);
                classTeacher01.Add(findTeacher);
            }
            else
            {
                classTeacher01.Add(findTeacher);
            }


            var item = new Classroom
            {
                ClassroomId = findClassroomOdd.ClassroomId,
                ClassroomName = findClassroomOdd.ClassroomName,
                ClassStudent = findClassroomOdd.ClassStudent,
                ClassTeacher = classTeacher01
            };

            ClassroomData.Remove(findClassroomNew);
            ClassroomData.Add(item);
            return item;
        }

        [HttpGet("{classroomId}/{studentId}")]
        public Classroom AddStudentInClassroom(string classroomId, string studentId)
        {
            var findClassroomOdd = ClassroomData.FirstOrDefault(it => it.ClassroomId == classroomId.ToString());
            var findClassroomNew = ClassroomData.FirstOrDefault(it => it.ClassroomId == classroomId.ToString());
            var findStudent = StudentData.FirstOrDefault(it => it.StudentId == studentId.ToString());
            List<Student> classStudent01 = new List<Student>();

            if (findClassroomOdd.ClassStudent != null)
            {
                classStudent01.AddRange(findClassroomOdd.ClassStudent);
                classStudent01.Add(findStudent);
            }
            else
            {
                classStudent01.Add(findStudent);
            }


            var item = new Classroom
            {
                ClassroomId = findClassroomOdd.ClassroomId,
                ClassroomName = findClassroomOdd.ClassroomName,
                ClassStudent = classStudent01,
                ClassTeacher = findClassroomOdd.ClassTeacher
            };

            ClassroomData.Remove(findClassroomNew);
            ClassroomData.Add(item);
            return item;
        }

        [HttpDelete("{classroomId}")]
        public void DeletedClassroom(string clasroomId)
        {
            var findDel = ClassroomData.FirstOrDefault(it => it.ClassroomId == clasroomId);
            ClassroomData.Remove(findDel);
        }


    }
}