using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TutorBackend.Core.Requests
{
    public class UpdateReviewRequest
    {
        public Guid ReviewId { get; set; }

        public string Message { get; set; }

        [JsonIgnore]
        public string Username { get; set; }

        [JsonIgnore]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
