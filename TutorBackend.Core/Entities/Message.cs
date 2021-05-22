using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBackend.Core.Entities
{
    public class Message
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Author { get; set; }

        public Guid ConversationId { get; set; }

        public virtual Conversation Conversation { get; set; }
    }
}
