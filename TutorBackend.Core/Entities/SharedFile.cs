using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBackend.Core.Entities
{
    public class SharedFile
    {
        public Guid Id { get; set; }

        public string ContainerId { get; set; }

        public string FileName { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Issuer { get; set; }

        public Guid ConversationId { get; set; }

        public virtual Conversation Conversation { get; set; }
    }
}
