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
    public class ReviewRepository : IReviewRepository
    {
        private readonly DatabaseContext dbContext;

        public ReviewRepository(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> Add(Review review)
        {
            await dbContext.AddAsync(review);
            var result = await dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Update(Review review)
        {
            dbContext.Update(review);
            var result = await dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Delete(Guid reviewId)
        {
            var review = await dbContext.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);

            dbContext.Remove(review);
            var result = await dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<Review> GetReview(Guid id)
        {
            var review = await dbContext.Reviews.FirstOrDefaultAsync(x => x.Id == id);
            return review;
        }

        public async Task<IList<Review>> GetReviewForTutor(Guid tutorId)
        {
            var reviews = await dbContext.Reviews.Include(x => x.Tutor).Where(x => x.TutorId == tutorId).ToListAsync();

            return reviews;
        }
    }
}
