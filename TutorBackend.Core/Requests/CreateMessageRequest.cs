using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TutorBackend.Core.Requests
{
    public class CreateMessageRequest
    {
        public string Text { get; set; }

        [JsonIgnore]
        public Guid AuthorId { get; set; }

        public string Username { get; set; }
    }
}
