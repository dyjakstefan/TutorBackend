using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBackend.Core.Dto
{
    public class ConversationDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid TutorId { get; set; }

        public string Username { get; set; }

        public string TutorUsername { get; set; }

        public IList<MessageDto> Messages { get; set; }
    }
}
