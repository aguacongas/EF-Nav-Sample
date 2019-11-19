using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApplication1
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
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Test;Trusted_Connection=True;"));
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using var scope1 = app.ApplicationServices.CreateScope();
            var dbContext1 = scope1.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext1.Database.EnsureCreated();
            var subject = new Subject { SubjectID = Guid.NewGuid(), SubjectName = "test" };
            dbContext1.Subjects.Add(subject);
            dbContext1.Courses.Add(new Course { CourseID = Guid.NewGuid(), CourseCode = "test", CourseName = "test", Subject = subject });
            dbContext1.SaveChanges();
            using var scope2 = app.ApplicationServices.CreateScope();
            var dbContext2 = scope2.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var courses = dbContext2.Courses.Include(c => c.Subject).ToList();
            foreach (var course in courses)
            {
                Console.WriteLine($"course code {course.CourseCode}, name {course.CourseName}, subject id {course.Subject.SubjectID} subject name {course.Subject.SubjectName}");
            }
        }
    }
}
