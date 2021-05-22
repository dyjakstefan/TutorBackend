using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TutorBackend.Core.Requests
{
    public class CreateRatingRequest
    {
        public string Username { get; set; }

        [JsonIgnore]
        public string Issuer { get; set; }

        public int Score { get; set; }
    }
}
