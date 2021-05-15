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
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext dbContext;

        public UserRepository(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User> GetByUsername(string username)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Username == username);
            return user;
        }

        public async Task<User> GetById(Guid userId)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            return user;
        }
    }
}
