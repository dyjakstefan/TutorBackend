using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TutorBackend.Core.Dto;
using TutorBackend.Core.Requests;

namespace TutorBackend.Infrastructure.Services.Interfaces
{
    public interface ITutorService
    {
        Task<bool> CreateTutorProfile(CreateTutorProfileRequest request);

        Task<bool> UpdateTutorProfile(UpdateTutorProfileRequest request);

        Task<bool> DeleteTutorProfile(Guid id);

        Task<TutorDto> GetTutorProfile(string username);

        Task<IList<TutorDto>> GetTutors(FilterTutorsRequest request);
    }
}
