using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace QuickAudit.Domain
{
    public class QuickAuditDbContext : DbContext
    {
        public DbSet<AuditEntry> AuditEntries { get; set; }
        public DbSet<AuditEntryProperty> AuditEntryProperties { get; set; }
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("Person");

            modelBuilder.Entity<AuditEntry>().ToTable("AuditEntry");
            modelBuilder.Entity<AuditEntryProperty>().ToTable("AuditEntryProperty");

            modelBuilder.Entity<AuditEntry>()
                .Property(e => e.EntityTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<AuditEntry>()
                .Property(e => e.StateName)
                .IsUnicode(false);

            modelBuilder.Entity<AuditEntry>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);
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

            var auditEntries = new List<AuditEntry>();

            foreach (var change in changes)
            {
                var type = change.Entity.GetType();

                var auditEntry = new AuditEntry
                {
                    CreatedBy = "SYSTEM",
                    CreatedDate = DateTime.Now,
                    StateName = change.State.ToString(),
                    State = (int) change.State,
                    EntityTypeName = type.Name
                };

                var originalValues = Entry(change.Entity).OriginalValues;
                var currentValues = Entry(change.Entity).CurrentValues;

                foreach (var property in originalValues.Properties)
                {
                    var original = originalValues[property.Name];
                    var current = currentValues[property.Name];


                    auditEntry.AuditEntryProperties.Add(new AuditEntryProperty
                    {
                        OldValue = original?.ToString() ?? "",
                        NewValue = current?.ToString() ?? "",
                        PropertyName = property.Name
                    });
                }

                auditEntries.Add(auditEntry);
            }

            auditEntries.ForEach(a => AuditEntries.Add(a));

            return base.SaveChanges();
        }
    }
}
