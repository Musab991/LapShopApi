using BuisnessLibrary.Bl.Repository;
using BuisnessLibrary.Data;
using BuisnessLibrary.Data.Interceptors;
using DomainLibrary.Generic;
using Microsoft.EntityFrameworkCore;
using BuisnessLibrary.Bl.UnitOfWork.Interface;
using BuisnessLibrary.Bl.UnitOfWork;
using Microsoft.AspNetCore.Hosting;
using AutoMapper;
using LapShop.Api.AutoMapping;
namespace LapShop.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("constr"))
                .AddInterceptors(new SoftDeleteInterceptor()));// Add services to the container.

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(MappingProfile)); // Automatically scan for profiles

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
