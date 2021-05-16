using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorBackend.Core.Dto;
using TutorBackend.Core.Requests;

namespace TutorBackend.Infrastructure.Services.Interfaces
{
    public interface IReviewService
    {
        Task<bool> CreateReview(CreateReviewRequest request);

        Task<bool> UpdateReview(UpdateReviewRequest request);

        Task<bool> DeleteReview(DeleteReviewRequest request);

        Task<IList<ReviewDto>> GetReviewForTutor(string username);
    }
}
