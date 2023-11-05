using Academy.Core.Enum;
using Academy.Core.Models;
using Academy.Core.Repositories;
using Academy.Data.Repositores;
using Academy.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Service.Services.Implementations
{
    public class StudentService : IStudentService
    {
        IStudentRepository _studentRepository = new StudentRepository();
        public async Task<string> CreateAsync(string fullname, string group, double average, StudentEducation education)
        {
            if (string.IsNullOrWhiteSpace(fullname))
                return "Fullname can not be empty";
            if (string.IsNullOrWhiteSpace(group))
                return "Group can not be empty";
            if (average < 0 || average > 100)
            {
                return "Average can not be less or equal to 0";
            }


            Student student = new Student(fullname, group, average, education);
            student.CreatedAt = DateTime.UtcNow.AddHours(4);
            await _studentRepository.AddAsync(student);
            return "Created";

        }
        public async Task GetAllAsync()
        {
            List<Student> students = await _studentRepository.GetAllAsync();

            if (students.Count == 0)
            {
                Console.WriteLine("There are no students");
                return;
            }

            foreach (Student student in students)
            {
                Console.WriteLine($"Id:{student.Id} fullname:{student.FullName} Group:{student.Group} Average:{student.Average} Education:{student.StudentEducation} CreatedAt:{student.CreatedAt} UpdatedAt:{student.UptatedAt}");
            }
        }
        public async Task<string> GetAsync(string id)
        {
            Student student = await _studentRepository.GetAsync(x => x.Id == id);
            if (student == null)
                return "Student not found";
            Console.WriteLine($"Id:{student.Id} fullname:{student.FullName} Group:{student.Group} Average:{student.Average} Education:{student.StudentEducation} CreatedAt:{student.CreatedAt} UpdatedAt:{student.UptatedAt}");
            return "Success";
        }
        public async Task<string> RemoveAsync(string id)
        {
            Student student = await _studentRepository.GetAsync(x => x.Id == id);
            if (student == null)
                return "Student not found";
            await _studentRepository.RemoveAsync(student);
            return "Removed successfully";

        }
        public async Task<string> UpdateAsync(string id, string fullname, string group, double average, StudentEducation education)
        {
            Student student = await _studentRepository.GetAsync(x => x.Id == id);
            if (student == null)
                return "Student not found";

            if (string.IsNullOrEmpty(fullname))
                return "Fullname can not be empty";
            if (string.IsNullOrEmpty(group))
                return "Group can not be empty";
            if (average < 0 || average > 100)
                return "Average can not be less or equal to 0";


            student.FullName = fullname;
            student.Group = group;
            student.Average = average;
            student.StudentEducation = education;
            student.UptatedAt = DateTime.UtcNow.AddHours(4);

            return "Updated successfully";
        }
    }
}
