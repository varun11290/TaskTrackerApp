using Microsoft.EntityFrameworkCore;
using TaskTracker.API.Models;

namespace TaskTracker.API.Data
{
    public class DataDBContext:DbContext
    {
        public DataDBContext(DbContextOptions<DataDBContext> options):base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
    }
}