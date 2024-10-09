
using BuisnessLibrary.Bl.Generic;
using BuisnessLibrary.Bl.Repository;
using BuisnessLibrary.Data;
using BuisnessLibrary.Data.Interceptors;
using Microsoft.EntityFrameworkCore;

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


            builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));


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
