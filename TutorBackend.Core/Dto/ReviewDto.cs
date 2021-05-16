using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBackend.Core.Dto
{
    public class ReviewDto
    {
        public Guid Id { get; set; }

        public string Message { get; set; }

        public string Username { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string TutorUsername { get; set; }
    }
}
