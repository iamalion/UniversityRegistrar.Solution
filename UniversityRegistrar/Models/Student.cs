using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniversityRegistrar.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        [Display(Name = "Student Name")]
        public string StudentName { get; set; }
        [Display(Name = "Enrollment Date")]
        [DataType(DataType.Date)]
        public string EnrollDate { get; set; }
        public List<CourseStudent> JoinEntities { get; }
    }
}