using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBackend.Core.Entities
{
    public class Tutor : User
    {
        public string Location { get; set; }

        public bool HasRemoteLessons { get; set; }

        public bool HasLocalLessons { get; set; }

        public string Description { get; set; }

        public virtual List<Topic> Topics { get; set; }

        public virtual List<ScheduleDay> ScheduleDays { get; set; }

        public virtual List<Review> Reviews { get; set; }
    }
}
