using System;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace QuickAudit.Domain
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class QuickAuditDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        //public QuickAuditDbContext(DbContextOptions<QuickAuditDbContext> options) 
        //    : base(options)
        //{
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("Person");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("QuickAuditDatabase"));
        }
    }

    //public class QuickAuditDbContextFactory : IDesignTimeDbContextFactory<QuickAuditDbContext>
    //{
    //    //public QuickAuditDbContext Create(DbContextFactoryOptions options)
    //    //{
    //    //    return new QuickAuditDbContext();
    //    //}

    //    public QuickAuditDbContext CreateDbContext(string[] args)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
