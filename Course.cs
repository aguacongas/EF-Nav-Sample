using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class Course
    {
        public Guid CourseID { get; set; }

        [Required]
        public string CourseCode { get; set; }

        [Required]
        public string CourseName { get; set; }

        public string CourseSubject { get; set; }

        public string CourseCredits { get; set; }

        public virtual Subject Subject { get; set; }
    }
}
