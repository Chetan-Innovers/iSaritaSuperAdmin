
using IGR.SuperAdmin.Application.EmployeeManagement.Interfaces;
using IGR.SuperAdmin.Application.EmployeeManagement.Services;
using IGR.SuperAdmin.Infrastructure.Data;
using IGR.SuperAdmin.Infrastructure.Repositories.EmployeeManagement;
using Microsoft.EntityFrameworkCore;

namespace IGR.SuperAdmin.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Register Entity Framework Core with PostgreSQL
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register Repository and Service
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
