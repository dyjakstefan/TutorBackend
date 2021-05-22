using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TutorBackend.Core.Entities;
using TutorBackend.Infrastructure.Repositories.Interfaces;
using TutorBackend.Infrastructure.SqlServerContext;

namespace TutorBackend.Infrastructure.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly DatabaseContext dbContext;

        public RatingRepository(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> SaveRating(Rating rating)
        {
            await dbContext.Ratings.AddAsync(rating);
            var result = await dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateRating(Rating rating)
        {
            dbContext.Ratings.Update(rating);
            var result = await dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<IList<Rating>> GetRatingsForUser(Guid userId)
        {
            var ratings = await dbContext.Ratings.Where(x => x.UserId == userId).ToListAsync();
            return ratings;
        }

        public async Task<Rating> GetRatingForUser(Guid userId, Guid issuerId)
        {
            var rating = await dbContext.Ratings.FirstOrDefaultAsync(x => x.UserId == userId && x.IssuerId == issuerId);
            return rating;
        }

        public async Task<double> GetAverageFromRatings(string username)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Username == username);

            if (user == null)
            {
                return 0;
            }

            var averageRating = await dbContext.Ratings.Where(x => x.UserId == user.Id).AverageAsync(x => x.Score);
            return averageRating;
        }
    }
}
