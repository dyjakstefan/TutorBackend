using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TutorBackend.Infrastructure.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var guidInString = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Guid.Parse(guidInString);
        }
    }
}
