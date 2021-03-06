using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBackend.Core.Entities
{
    public class Rating
    {
        public Guid Id { get; set; }

        public int Score { get; set; }

        public Guid IssuerId { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid UserId { get; set; }

        public virtual User User { get; set; }
    }
}
