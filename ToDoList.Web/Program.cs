using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using ToDoList.DAL;
using ToDoList.DAL.Interfaces;
using ToDoList.DAL.Repositories;
using ToDoList.Services.Implementations;
using ToDoList.Services.Interfaces;

namespace ToDoList.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConncetion")));

            builder.Services.AddScoped<IBaseRepository<TaskEntity>, TaskRepository>();
            builder.Services.AddScoped<ITaskService, TaskService>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Task}/{action=Index}/{id?}");

            app.Run();
        }
    }
}