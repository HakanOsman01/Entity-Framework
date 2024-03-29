using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Rental.Core.Contracts;
using Rental.Core.Services;
using Rental.Infrustructer.DataBase;
using Rental.Infrustructer.DataBase.Comman;

namespace Rental
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<RentalDbContext>(options
                => options.UseSqlServer(builder.Configuration
                .GetConnectionString("RentalContext")));


            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IRepository,Repository>();
            builder.Services.AddScoped<IProperyService, PropertService>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}