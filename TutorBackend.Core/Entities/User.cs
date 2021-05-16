using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorBackend.Core.Enums;

namespace TutorBackend.Core.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string UserType { get; set; }

        public virtual List<Lesson> Lessons { get; set; }

        public virtual List<Rating> Ratings { get; set; }
    }
}
