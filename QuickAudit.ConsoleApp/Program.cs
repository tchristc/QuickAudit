using System;
using System.Linq;
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
}
