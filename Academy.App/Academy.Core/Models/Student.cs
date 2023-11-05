using Academy.Core.Enum;
using Academy.Core.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Core.Models
{
    public class Student : BaseModel
    {
        static int _id;
        public string FullName { get; set; }
        public string Group { get; set; }
        public double Average { get; set; }
        public StudentEducation StudentEducation { get; set; }

        public Student(string fullName, string group, double average, StudentEducation studenteducation)
        {
            _id++;
            FullName = fullName;
            Group = group;
            Average = average;
            StudentEducation = studenteducation;
            Id = $"{StudentEducation.ToString()[0]}-{_id}";

        }
    }
}
