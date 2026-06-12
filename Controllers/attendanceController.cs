using Microsoft.AspNetCore.Mvc;
using attendanceAppService;
using AttendanceModels;
using System.Linq;
using System.Collections.Generic;

namespace AttendanceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly AppService _appService;

        private static List<Student> _students = new();

        public AttendanceController()
        {
            _appService = new AppService();

            if (!_students.Any())
            {
                var s1 = new Student
                {
                    Id = "34d7b7df",
                    Name = "Elle Mar"
                };

                var s2 = new Student
                {
                    Id = "7803ca92",
                    Name = "Raven"
                };

                _appService.GenerateAttendance(s1);
                _appService.ComputeRateAndRemark(s1);

                _appService.GenerateAttendance(s2);
                _appService.ComputeRateAndRemark(s2);

                _students.Add(s1);
                _students.Add(s2);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _students.Select(s => new
            {
                s.Id,
                s.Name,
                s.Rate,
                s.Remark,

                Attendance = s.Attendance.Select(a => a.ToString()).ToList()
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var student = _students.FirstOrDefault(x => x.Id == id);

            if (student == null)
                return NotFound(new { message = "Student not found" });

            return Ok(new
            {
                student.Id,
                student.Name,
                student.Rate,
                student.Remark,
                Attendance = student.Attendance.Select(a => a.ToString()).ToList()
            });
        }
    }
}