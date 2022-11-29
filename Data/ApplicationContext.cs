using Microsoft.EntityFrameworkCore;
using ServiceTestTask.Models;
using Task = ServiceTestTask.Models.Task;

namespace ServiceTestTask.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>().ToTable("Comment");
            modelBuilder.Entity<Task>().ToTable("Task");
            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}
