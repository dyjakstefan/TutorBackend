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
    public class SharedFileRepository : ISharedFileRepository
    {
        private readonly DatabaseContext dbContext;

        public SharedFileRepository(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Guid> CreateFile(SharedFile file)
        {
            await dbContext.SharedFiles.AddAsync(file);
            await dbContext.SaveChangesAsync();

            return file.Id;
        }

        public async Task<SharedFile> GetFile(Guid fileId)
        {
            return await dbContext.SharedFiles.Include(x => x.Conversation).FirstOrDefaultAsync(x => x.Id == fileId);
        }
    }
}
