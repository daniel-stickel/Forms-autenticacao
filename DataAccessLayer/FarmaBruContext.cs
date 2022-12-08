using Metadata;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class FarmaBruContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString: @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\moc\Documents\FarmaBruDB.mdf;Integrated Security=True;Connect Timeout=5",
                x => x.EnableRetryOnFailure(3));
            base.OnConfiguring(optionsBuilder);
        }

        public FarmaBruContext()
        {

        }

        public FarmaBruContext(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(connectionString: @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\moc\Documents\FarmaBruDB.mdf;Integrated Security=True;Connect Timeout=5",
                x=> x.EnableRetryOnFailure(3));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
    }
}
