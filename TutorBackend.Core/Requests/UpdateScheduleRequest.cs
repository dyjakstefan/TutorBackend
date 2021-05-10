using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TutorBackend.Core.Requests
{
    public class UpdateScheduleRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }

        public Guid ScheduleId { get; set; }

        public bool IsRepeatable { get; set; }

        public int RepeatAfterWeeks { get; set; }

        public DateTime EndOfRepetition { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }
    }
}
