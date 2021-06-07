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

        public bool IsAccepted { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string Topic { get; set; }

        public Guid ScheduleDayId { get; set; }

        public Guid UserId { get; set; }

        public Guid TutorId { get; set; }

        public virtual ScheduleDay ScheduleDay { get; set; }

        public virtual User User { get; set; }
    }
}
