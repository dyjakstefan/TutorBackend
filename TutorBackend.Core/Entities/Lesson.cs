using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBackend.Core.Entities
{
    public class Lesson
    {
        public Guid Id { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }

        public int DurationInMinutes => (int)(EndAt - StartAt).TotalMinutes;

        public bool IsReserved { get; set; }

        public bool IsAccepted { get; set; }

        public bool IsRepeatable { get; set; }

        public int RepeatAfterWeeks { get; set; }

        public int RepeatAfterDays { get; set; }

        public DateTime EndOfRepetition { get; set; }

        public Guid ScheduleDayId { get; set; }

        public virtual ScheduleDay ScheduleDay { get; set; }
    }
}
