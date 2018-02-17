using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using QuickAudit.Domain;

namespace QuickAudit.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var db = new QuickAuditDbContext())
            {
                var persons = db.Persons.ToList();
                foreach (var person in persons)
                {
                    Console.WriteLine($"{person.FirstName} {person.LastName}");
                    if (person == db.Persons.First())
                    {
                        person.LastName += "!";
                    }
                }

                db.Persons.Add(new Person
                {
                    FirstName = "Jamie",
                    LastName = "Yup"
                });
                db.SaveChanges();
            }

            Console.WriteLine("Press a key...");
            Console.ReadKey();
        }
    }

    public static class ChangeTrackerExtensions
    {
        public static void DisplayTrackedEntities(this ChangeTracker changeTracker)
        {
            Console.WriteLine("");

            var entries = changeTracker.Entries();
            foreach (var entry in entries)
            {
                var type = entry.Entity.GetType();
                Console.WriteLine("Entity Name: {0}", type.Name);
               
                Console.WriteLine("Status: {0}", entry.State);
            }
            Console.WriteLine("");
            Console.WriteLine("---------------------------------------");
        }
    }
}
