using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TutorBackend.Core.Requests
{
    public class AcceptLessonRequest
    {
        public Guid LessonId { get; set; }

        [JsonIgnore]
        public Guid TutorId { get; set; }
    }
}
