using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBackend.Core.Dto
{
    public class SharedFileDto
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Issuer { get; set; }
    }
}
