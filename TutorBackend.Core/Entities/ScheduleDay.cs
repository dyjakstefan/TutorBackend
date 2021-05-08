using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBackend.Core.Entities
{
    public class ScheduleDay
    {
        public Guid Id { get; set; }

        public bool IsRepeatable { get; set; }

        public int RepeatAfterWeeks { get; set; }

        public DateTime EndOfRepetition { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }

        public Guid TutorId { get; set; }

        public virtual Tutor Tutor { get; set; }

        public virtual List<Lesson> Lessons { get; set; }
    }
}
