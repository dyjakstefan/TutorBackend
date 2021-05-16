using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TutorBackend.Core.Requests
{
    public class CreateReviewRequest
    {
        public string Message { get; set; }

        [JsonIgnore]
        public string Username { get; set; }

        [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string TutorUsername { get; set; }
    }
}
