using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorBackend.Core.Responses;

namespace TutorBackend.Infrastructure.Services.Interfaces
{
    public interface IJwtService
    {
        JwtResponse CreateToken(Guid userId, string username);
    }
}
