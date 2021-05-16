using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TutorBackend.Core.Dto;
using TutorBackend.Core.Entities;
using TutorBackend.Core.Requests;
using TutorBackend.Infrastructure.Repositories.Interfaces;
using TutorBackend.Infrastructure.Services.Interfaces;

namespace TutorBackend.Infrastructure.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository reviewRepository;
        private readonly ILessonRepository lessonRepository;
        private readonly IUserRepository userRepository;
        private readonly ITutorRepository tutorRepository;
        private readonly IMapper mapper;

        public ReviewService(IReviewRepository reviewRepository, ILessonRepository lessonRepository, IUserRepository userRepository, ITutorRepository tutorRepository, IMapper mapper)
        {
            this.reviewRepository = reviewRepository;
            this.lessonRepository = lessonRepository;
            this.userRepository = userRepository;
            this.tutorRepository = tutorRepository;
            this.mapper = mapper;
        }

        public async Task<bool> CreateReview(CreateReviewRequest request)
        {
            var user = await userRepository.GetByUsername(request.Username);
            var tutor = await tutorRepository.GetByUsername(request.TutorUsername);

            if (user == null || tutor == null)
            {
                return false;
            }

            var hasCommonLessons = await lessonRepository.AnyLessonForUser(user.Id, tutor.Id);

            if (!hasCommonLessons)
            {
                return false;
            }

            var review = mapper.Map<Review>(request);
            review.TutorId = tutor.Id;

            var result = await reviewRepository.Add(review);

            return result;
        }

        public async Task<bool> UpdateReview(UpdateReviewRequest request)
        {
            var user = await userRepository.GetByUsername(request.Username);
            var review = await reviewRepository.GetReview(request.ReviewId);

            if (user == null || review == null || review.Username != user.Username)
            {
                return false;
            }

            review.Message = request.Message;
            review.UpdatedAt = request.UpdatedAt;

            var result = await reviewRepository.Update(review);
            return result;
        }

        public async Task<bool> DeleteReview(DeleteReviewRequest request)
        {
            var user = await userRepository.GetByUsername(request.Username);
            var review = await reviewRepository.GetReview(request.ReviewId);

            if (user == null || review == null || review.Username != user.Username)
            {
                return false;
            }

            var result = await reviewRepository.Delete(review.Id);
            return result;
        }

        public async Task<IList<ReviewDto>> GetReviewForTutor(string username)
        {
            var tutor = await tutorRepository.GetByUsername(username);

            var reviews = await reviewRepository.GetReviewForTutor(tutor.Id);

            var reviewsDto = mapper.Map<IList<ReviewDto>>(reviews);
            return reviewsDto;
        }
    }
}
