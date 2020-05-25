using Microsoft.EntityFrameworkCore;
using TaskTracker.API.Models;

namespace TaskTracker.API.Data
{
    public class ValueDBContext:DbContext
    {
        public ValueDBContext(DbContextOptions<ValueDBContext> options):base(options)
        {
            
        }

        public DbSet<Value> Values { get; set; }
    }
}