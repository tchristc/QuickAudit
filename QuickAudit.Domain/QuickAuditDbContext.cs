using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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

        public override int SaveChanges()
        {

            var changes = from e in ChangeTracker.Entries()
                where e.State != EntityState.Unchanged
                select e;

            foreach (var change in changes)
            {
                var type = change.Entity.GetType();
                if (change.State == EntityState.Added)
                {
                    // Log Added
                }
                else if (change.State == EntityState.Modified)
                {
                    // Log Modified
                    //var item = change.Cast<IEntity>().Entity;
                    var originalValues = Entry(change.Entity).OriginalValues;
                    var currentValues = Entry(change.Entity).CurrentValues;

                    foreach (var property in originalValues.Properties)
                    {
                        
                        var original = originalValues[property.Name];
                        var current = currentValues[property.Name];

                        if (!Equals(original, current))
                        {
                            Console.WriteLine($"{property.Name}: {original} => {current}");
                            //var originalIdx = originalValues[property];
                            //Console.WriteLine("{6} = {0}.{4} [{7}][{2}] [{1}] --> [{3}]  Rel:{5}",
                            //    type,
                            //    change.OriginalValues.GetValue(originalIdx),
                            //    change.OriginalValues.GetFieldType(originalIdx),
                            //    change.CurrentValues.GetValue(originalIdx),
                            //    change.OriginalValues.EntityType.Name.GetName(originalIdx),
                            //    change.IsRelationship,
                            //    change.State,
                                //string.Join(",", change..EntityKeyValues.Select(v => string.Join(" = ", v.Key, v.Value))));
                        }
                    }

                }
                else if (change.State == EntityState.Deleted)
                {
                    // log deleted
                }
            }
            // don't forget to save
            return 0;//base.SaveChanges();
        }
    }
}
