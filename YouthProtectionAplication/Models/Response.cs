using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouthProtectionAplication.Models
{
    public class Response
    {
        public class ResponseModel
        {
            public long Id { get; set; }
            public long AttendanceId { get; set; }
            public Attendance attendance { get; set; }
            public object messages { get; set; }
        }
        public class Attendance

        {
            public long Id { get; set; }
            public long VolunteerId { get; set; }
            public object Volunteer { get; set; }

            public long PubicationId { get; set; }
            public object Pubication { get; set; }

            public DateTime StartedAt { get; set; }

            public bool IsCompleted { get; set; }
        }
    }
}
