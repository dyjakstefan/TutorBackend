using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TutorBackend.Core;
using TutorBackend.Core.Entities;
using TutorBackend.Core.Requests;
using TutorBackend.Infrastructure.Repositories.Interfaces;
using TutorBackend.Infrastructure.SqlServerContext;

namespace TutorBackend.Infrastructure.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly DatabaseContext dbContext;

        public ScheduleRepository(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<bool> CreateScheduleDay(ScheduleDay schedule)
        {
            schedule.Id = Guid.NewGuid();
            schedule.CreatedAt = DateTime.Now;
            schedule.UpdatedAt = DateTime.Now;
            await dbContext.ScheduleDays.AddAsync(schedule);
            var result = await dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<IList<ScheduleDay>> GetAllForTutor(string username)
        {
            var tutor = await dbContext.Tutors.Include(x => x.ScheduleDays).FirstOrDefaultAsync(x =>
                x.Username == username && x.UserType == Constants.Tutor);

            return tutor?.ScheduleDays;
        }

        public async Task<ScheduleDay> GetScheduleById(Guid scheduleId)
        {
            var schedule = await dbContext.ScheduleDays.Include(x => x.Lessons).FirstOrDefaultAsync(x => x.Id == scheduleId);
            return schedule;
        }

        public async Task<bool> DeleteScheduleDay(Guid scheduleId, Guid userId)
        {
            var scheduleDay =
                await dbContext.ScheduleDays.FirstOrDefaultAsync(x => x.Id == scheduleId && x.TutorId == userId);

            dbContext.ScheduleDays.Remove(scheduleDay);
            var result = await dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateScheduleDay(UpdateScheduleRequest request)
        {
            var schedule = await dbContext.ScheduleDays.FirstOrDefaultAsync(x => x.Id == request.ScheduleId);

            if (schedule == null) return false;

            schedule.UpdatedAt = DateTime.Now;
            schedule.StartAt = request.StartAt;
            schedule.EndAt = request.EndAt;
            dbContext.ScheduleDays.Update(schedule);
            var result = await dbContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
