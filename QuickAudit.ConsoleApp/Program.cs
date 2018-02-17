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
                }
            }

            Console.WriteLine("Press a key...");
            Console.ReadKey();
        }
    }
}
