using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<TaskEntity> Tasks { get; set; }
    }
}
