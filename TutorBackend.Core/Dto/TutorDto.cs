using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBackend.Core.Dto
{
    public class TutorDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Location { get; set; }

        public bool HasRemoteLessons { get; set; }

        public bool HasLocalLessons { get; set; }

        public string Description { get; set; }

        public double Rating { get; set; }

        public IList<string> Topics { get; set; }

        public IList<ScheduleDayDto> ScheduleDays { get; set; }
    }
}
