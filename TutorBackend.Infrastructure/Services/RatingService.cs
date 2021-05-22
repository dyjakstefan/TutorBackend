using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TutorBackend.Core.Entities;
using TutorBackend.Core.Requests;
using TutorBackend.Infrastructure.Repositories.Interfaces;
using TutorBackend.Infrastructure.Services.Interfaces;

namespace TutorBackend.Infrastructure.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository ratingRepository;
        private readonly IReviewRepository reviewRepository;
        private readonly ILessonRepository lessonRepository;
        private readonly IUserRepository userRepository;
        private readonly ITutorRepository tutorRepository;
        private readonly IMapper mapper;

        public RatingService(IRatingRepository ratingRepository, IReviewRepository reviewRepository, ILessonRepository lessonRepository, IUserRepository userRepository, ITutorRepository tutorRepository, IMapper mapper)
        {
            this.ratingRepository = ratingRepository;
            this.reviewRepository = reviewRepository;
            this.lessonRepository = lessonRepository;
            this.userRepository = userRepository;
            this.tutorRepository = tutorRepository;
            this.mapper = mapper;
        }

        public async Task<bool> CreateRating(CreateRatingRequest request)
        {
            var user = await userRepository.GetByUsername(request.Username);
            var issuer = await userRepository.GetByUsername(request.Issuer);

            if (user == null || issuer == null)
            {
                return false;
            }

            var hasCommonLessons1 = await lessonRepository.AnyLessonForUser(user.Id, issuer.Id);
            var hasCommonLessons2 = await lessonRepository.AnyLessonForUser(issuer.Id, user.Id);

            if (!hasCommonLessons1 && !hasCommonLessons2)
            {
                return false;
            }

            var rating = await ratingRepository.GetRatingForUser(user.Id, issuer.Id);

            if (rating == null)
            {
                rating = mapper.Map<Rating>(request);
                rating.UserId = user.Id;
                rating.IssuerId = issuer.Id;
                return await ratingRepository.SaveRating(rating);
            }

            rating.Score = request.Score;
            return await ratingRepository.UpdateRating(rating);
        }

        public async Task<double> GetRating(string username)
        {
            return await ratingRepository.GetAverageFromRatings(username);
        }
    }
}
