using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TutorBackend.Core.Entities;
using TutorBackend.Core.Requests;
using TutorBackend.Infrastructure.Repositories.Interfaces;
using TutorBackend.Infrastructure.SqlServerContext;

namespace TutorBackend.Infrastructure.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        private readonly DatabaseContext dbContext;

        public LessonRepository(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> ReserveLesson(Lesson lesson)
        {
            lesson.Id = Guid.NewGuid();
            lesson.CreatedAt = DateTime.Now;
            lesson.UpdatedAt = DateTime.Now;
            lesson.IsAccepted = false;
            await dbContext.Lessons.AddAsync(lesson);
            var result = await dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<IList<Lesson>> GetPlannedLessons(Guid userId)
        {
            var lessons = await dbContext.Lessons
                .Include(x => x.User)
                .Include(x => x.ScheduleDay)
                .ThenInclude(x => x.Tutor)
                .Where(x => x.StartAt > DateTime.Now && (x.UserId == userId || x.TutorId == userId))
                .ToListAsync();

            return lessons;
        }

        public async Task<IList<Lesson>> GetHistoryLessons(Guid userId)
        {
            var lessons = await dbContext.Lessons
                .Include(x => x.ScheduleDay)
                .ThenInclude(x => x.Tutor)
                .Where(x => x.StartAt < DateTime.Now && (x.UserId == userId || x.TutorId == userId))
                .ToListAsync();

            return lessons;
        }

        public async Task<bool> AcceptLesson(Lesson lesson)
        {
            lesson.IsAccepted = true;
            var result = await dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> RejectLesson(Lesson lesson)
        {
            dbContext.Remove(lesson);
            var result = await dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<Lesson> GetById(Guid id)
        {
            var lesson = await dbContext.Lessons.FirstOrDefaultAsync(x => x.Id == id);
            return lesson;
        }
    }
}
