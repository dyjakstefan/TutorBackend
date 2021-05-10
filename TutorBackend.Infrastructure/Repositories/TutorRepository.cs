using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorBackend.Core;
using TutorBackend.Core.Entities;
using TutorBackend.Core.Requests;
using TutorBackend.Infrastructure.Repositories.Interfaces;
using TutorBackend.Infrastructure.SqlServerContext;

namespace TutorBackend.Infrastructure.Repositories
{
    public class TutorRepository : ITutorRepository
    {
        private readonly DatabaseContext dbContext;
        private readonly IMapper mapper;

        public TutorRepository(DatabaseContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<bool> Add(Tutor tutor)
        {
            var topics = new List<string>();

            foreach (var topic in tutor.Topics)
            {
                topics.Add(topic.Name);
            }

            tutor.Topics = await dbContext.Topics.Where(x => topics.Contains(x.Name)).ToListAsync();

            var user = await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == tutor.Id);
            mapper.Map(user, tutor);
            tutor.UserType = Constants.Tutor;

            dbContext.Tutors.Update(tutor);
            var result = await dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> AnyExists(Guid id)
        {
            return await dbContext.Tutors.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> AnyExists(string username)
        {
            return await dbContext.Tutors.AnyAsync(x => x.Username == username);
        }

        public async Task<Tutor> GetByUsername(string username)
        {
            return await dbContext.Tutors.Include(x => x.Topics).FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<Tutor> GetById(Guid id)
        {
            return await dbContext.Tutors.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Update(UpdateTutorProfileRequest request)
        {
            var tutor = await dbContext.Tutors.FirstOrDefaultAsync(x => x.Id == request.UserId);
            mapper.Map(request, tutor);

            dbContext.Tutors.Update(tutor);
            var result = await dbContext.SaveChangesAsync(); 

            return result > 0;
        }

        public async Task<bool> Delete(Guid id)
        {
            var tutor = await dbContext.Tutors.Include(x => x.Topics).FirstOrDefaultAsync(x => x.Id == id);

            tutor.Topics.RemoveAll(x => true);
            tutor.UserType = Constants.User;
            dbContext.Tutors.Update(tutor);
            var result = await dbContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
