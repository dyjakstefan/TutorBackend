using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBackend.Core.Entities
{
    public class Conversation
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid TutorId { get; set; }
        
        public virtual User User { get; set; }

        public virtual Tutor Tutor { get; set; }

        public virtual List<Message> Messages { get; set; }

        public virtual List<SharedFile> SharedFiles { get; set; }
    }
}
