using System.Collections.Generic;

namespace UniversityRegistrar.Models
{
    public class Course{
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public List<CourseStudent> JoinEntities { get; }
    }
}