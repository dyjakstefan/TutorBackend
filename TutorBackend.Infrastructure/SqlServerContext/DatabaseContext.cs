using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorBackend.Core.Entities;

namespace TutorBackend.Infrastructure.SqlServerContext
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Tutor> Tutors { get; set; }

        public DbSet<ScheduleDay> ScheduleDays { get; set; }

        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<Topic> Topics { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasDiscriminator(x => x.UserType)
                .HasValue<User>("User")
                .HasValue<Tutor>("Tutor");

            modelBuilder
                .Entity<Tutor>()
                .HasMany(x => x.Topics)
                .WithMany(x => x.Tutors)
                .UsingEntity(x => x.ToTable("TutorTopics"));

            modelBuilder
                .Entity<Tutor>()
                .HasMany(x => x.ScheduleDays)
                .WithOne(x => x.Tutor);

            modelBuilder
                .Entity<ScheduleDay>()
                .HasIndex(x => x.TutorId);

            modelBuilder
                .Entity<User>()
                .HasMany(x => x.Lessons)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
