using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorBackend.Core.Entities;

namespace TutorBackend.Infrastructure.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        Task<bool> Add(Review review);

        Task<bool> Update(Review review);

        Task<bool> Delete(Guid reviewId);

        Task<Review> GetReview(Guid id);

        Task<IList<Review>> GetReviewForTutor(Guid tutorId);
    }
}
