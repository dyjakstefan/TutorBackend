using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBackend.Core.Dto
{
    public class LessonDto
    {
        public Guid Id { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }

        public int DurationInMinutes { get; set; }

        public bool IsAccepted { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string TutorUsername { get; set; }

        public string StudentUsername { get; set; }

        public string Topic { get; set; }
    }
}
