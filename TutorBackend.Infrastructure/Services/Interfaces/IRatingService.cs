using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorBackend.Core.Requests;

namespace TutorBackend.Infrastructure.Services.Interfaces
{
    public interface IRatingService
    {
        Task<bool> CreateRating(CreateRatingRequest request);

        Task<double> GetRating(string username);
    }
}
