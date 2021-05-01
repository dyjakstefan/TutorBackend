using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorBackend.Infrastructure.SqlServerContext;

namespace TutorBackend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddSwaggerGen();
            //services.AddSwaggerGen(c =>
            //{
            //    var security = new Dictionary<string, IEnumerable<string>>
            //    {
            //        {"Bearer", new string[] { }},
            //    };

            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tutor", Version = "v1" });
            //    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            //    {
            //        Description = "JWT Authorization header using the Bearer scheme.",
            //        Name = "Authorization",
            //        In = "header",
            //        Type = "apiKey"
            //    });
            //    c.AddSecurityRequirement(security);
            //});
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tutor V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //app.UseAuthentication();
            //app.UseAuthorization();

            //app.UseCors(builder =>
            //    builder.AllowAnyHeader()
            //        .AllowAnyMethod()
            //        .AllowAnyOrigin()
            //        .AllowCredentials()
            //    );
        }
    }
}
