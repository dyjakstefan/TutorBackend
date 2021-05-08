using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TutorBackend.Core.Requests
{
    public class UpdateTutorProfileRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }

        public string Location { get; set; }

        public bool HasRemoteLessons { get; set; }

        public bool HasLocalLessons { get; set; }

        public string Description { get; set; }
    }
}
