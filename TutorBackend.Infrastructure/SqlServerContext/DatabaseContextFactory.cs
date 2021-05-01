using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBackend.Infrastructure.SqlServerContext
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer("Server=tcp:tutor-sql-server.database.windows.net,1433;Database=TutorDB;User ID=tutoradmin;Password=1qazXSW@3edc;Encrypt=true;Connection Timeout=30;");

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
