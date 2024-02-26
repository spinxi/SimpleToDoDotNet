using Microsoft.EntityFrameworkCore;
using SimpleToDo.Controllers.Entities;

namespace SimpleToDo.Controllers.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<ToDoModel> SimpleToDo { get; set; }
    }
}
