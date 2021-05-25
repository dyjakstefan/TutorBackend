using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorBackend.Core.Entities;

namespace TutorBackend.Infrastructure.Repositories.Interfaces
{
    public interface ISharedFileRepository
    {
        Task<Guid> CreateFile(SharedFile file);

        Task<SharedFile> GetFile(Guid fileId);
    }
}
