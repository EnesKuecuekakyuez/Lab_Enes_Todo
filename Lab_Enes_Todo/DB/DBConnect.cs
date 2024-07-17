using Lab_Enes_Todo.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab_Enes_Todo.DB
{
    public class DBConnect : DbContext
    {
        public DBConnect(DbContextOptions options): base(options)
        {
        }
        public DbSet<Todo> todos { get; set; }
    }
}
