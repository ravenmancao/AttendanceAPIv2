using System.Collections.Generic;

namespace AttendanceAPI.Models
{
    public class AttendanceViewModel
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public double Rate { get; set; }
        public string Remark { get; set; } = "";
        public List<string> Attendance { get; set; } = new();
    }
}