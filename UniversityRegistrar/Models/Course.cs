using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniversityRegistrar.Models
{
    public class Course{
        public int CourseId { get; set; }
        [Display(Name = "Course Name: ")]
        public string CourseName { get; set; }
        public List<CourseStudent> JoinEntities { get; }
    }
}