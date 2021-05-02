using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBackend.Core.Responses
{
    public class JwtResponse
    {
        public string Token { get; set; }

        public DateTime Expires { get; set; }
    }
}
