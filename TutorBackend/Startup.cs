using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TutorBackend.Core.Entities;
using TutorBackend.Infrastructure.MappingProfile;
using TutorBackend.Infrastructure.Settings;
using TutorBackend.Infrastructure.Services;
using TutorBackend.Infrastructure.Services.Interfaces;
using TutorBackend.Infrastructure.SqlServerContext;
using TutorBackend.Infrastructure.Repositories.Interfaces;
using TutorBackend.Infrastructure.Repositories;

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

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["Jwt:issuer"],
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:key"]))
                    };
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tutor", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                           new string[] {}
                    }
                });
            });

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserProfile());
                mc.AddProfile(new TutorProfile());
                mc.AddProfile(new ScheduleProfile());
                mc.AddProfile(new LessonProfile());
                mc.AddProfile(new ReviewProfile());
            });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.Configure<JwtSettigns>(Configuration.GetSection(JwtSettigns.Jwt));
            services.AddTransient<IJwtService, JwtService>();
            services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITutorService, TutorService>();
            services.AddTransient<ITopicService, TopicService>();
            services.AddTransient<IScheduleService, ScheduleService>();
            services.AddTransient<ILessonService, LessonService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<IRatingService, RatingService>();

            services.AddTransient<ITutorRepository, TutorRepository>();
            services.AddTransient<ITopicRepository, TopicRepository>();
            services.AddTransient<IScheduleRepository, ScheduleRepository>();
            services.AddTransient<ILessonRepository, LessonRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IReviewRepository, ReviewRepository>();
            services.AddTransient<IRatingRepository, RatingRepository>();
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
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
