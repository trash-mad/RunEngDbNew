using DbElems;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DbServer
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
            if (Database.EnsureCreated()) Database.Migrate();
        }

        public DbSet<Project> Projects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=RusEngDb;Trusted_Connection=True;");
        }
    }
}
