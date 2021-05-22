using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorBackend.Core.Entities;

namespace TutorBackend.Infrastructure.Repositories.Interfaces
{
    public interface IRatingRepository
    {
        Task<bool> SaveRating(Rating rating);

        Task<bool> UpdateRating(Rating rating);

        Task<IList<Rating>> GetRatingsForUser(Guid userId);

        Task<Rating> GetRatingForUser(Guid userId, Guid issuerId);

        Task<double> GetAverageFromRatings(string username);
    }
}
