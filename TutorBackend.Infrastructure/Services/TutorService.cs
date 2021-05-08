using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TutorBackend.Core.Dto;
using TutorBackend.Core.Entities;
using TutorBackend.Core.Requests;
using TutorBackend.Infrastructure.Repositories.Interfaces;
using TutorBackend.Infrastructure.Services.Interfaces;
using TutorBackend.Infrastructure.SqlServerContext;

namespace TutorBackend.Infrastructure.Services
{
    public class TutorService : ITutorService
    {
        private readonly ITutorRepository tutorRepository;
        private readonly ITopicRepository topicRepository;
        private readonly IMapper mapper;

        public TutorService(ITutorRepository tutorRepository, IMapper mapper, ITopicRepository topicRepository)
        {
            this.tutorRepository = tutorRepository;
            this.mapper = mapper;
            this.topicRepository = topicRepository;
        }

        public async Task<bool> CreateTutorProfile(CreateTutorProfileRequest request)
        {
            if (await tutorRepository.AnyExists(request.UserId))
            {
                return false;
            }

            var tutor = mapper.Map<CreateTutorProfileRequest, Tutor>(request);

            var savingTopicsResult = await topicRepository.AddAll(tutor.Topics);
            var result = await tutorRepository.Add(tutor);

            return savingTopicsResult && result;
        }

        public async Task<bool> UpdateTutorProfile(UpdateTutorProfileRequest request)
        {
            if (!await tutorRepository.AnyExists(request.UserId))
            {
                return false;
            }

            var result = await tutorRepository.Update(request);

            return result;

        }

        public async Task<TutorDto> GetTutorProfile(string username)
        {
            var tutor = await tutorRepository.GetByUsername(username);

            return mapper.Map<TutorDto>(tutor);
        }

        public async Task<bool> DeleteTutorProfile(Guid id)
        {
            var result = await tutorRepository.Delete(id);

            return result;
        }
    }
}
