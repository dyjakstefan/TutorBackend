using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorBackend.Core.Entities;
using TutorBackend.Core.Requests;

namespace TutorBackend.Infrastructure.Repositories.Interfaces
{
    public interface ITutorRepository
    {
        Task<bool> Add(Tutor tutor);

        Task<bool> Update(UpdateTutorProfileRequest request);

        Task<bool> AnyExists(Guid id);

        Task<bool> AnyExists(string username);

        Task<Tutor> GetByUsername(string username);

        Task<Tutor> GetById(Guid id);

        Task<bool> Delete(Guid id);

        Task<IList<Tutor>> GetTutors(FilterTutorsRequest request);
    }
}
