using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBackend.Core.Entities
{
    public class Topic
    {
        [Key]
        public string Name { get; set; }

        public virtual List<Tutor> Tutors { get; set; }
    }
}
