using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
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

    //public class EFConfigProvider : ConfigurationProvider
    //{
    //    public EFConfigProvider(Action<DbContextOptionsBuilder> optionsAction)
    //    {
    //        OptionsAction = optionsAction;
    //    }

    //    Action<DbContextOptionsBuilder> OptionsAction { get; }

    //    // Load config data from EF DB.
    //    public override void Load()
    //    {
    //        var builder = new DbContextOptionsBuilder<ConfigurationContext>();
    //        OptionsAction(builder);

    //        using (var dbContext = new ConfigurationContext(builder.Options))
    //        {
    //            dbContext.Database.EnsureCreated();
    //            Data = !dbContext.Values.Any()
    //                ? CreateAndSaveDefaultValues(dbContext)
    //                : dbContext.Values.ToDictionary(c => c.Id, c => c.Value);
    //        }
    //    }

    //    private static IDictionary<string, string> CreateAndSaveDefaultValues(
    //        ConfigurationContext dbContext)
    //    {
    //        var configValues = new Dictionary<string, string>
    //        {
    //            { "key1", "value_from_ef_1" },
    //            { "key2", "value_from_ef_2" }
    //        };
    //        dbContext.Values.AddRange(configValues
    //            .Select(kvp => new ConfigurationValue { Id = kvp.Key, Value = kvp.Value })
    //            .ToArray());
    //        dbContext.SaveChanges();
    //        return configValues;
    //    }
    //}

    //public static class EntityFrameworkExtensions
    //{
    //    public static IConfigurationBuilder AddEntityFrameworkConfig(
    //        this IConfigurationBuilder builder, Action<DbContextOptionsBuilder> setup)
    //    {
    //        return builder.Add(new EFConfigSource(setup));
    //    }
    //}

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        var builder = new ConfigurationBuilder()
    //            .SetBasePath(Directory.GetCurrentDirectory())
    //            .AddJsonFile("appsettings.json");

    //        var connectionStringConfig = builder.Build();

    //        var config = new ConfigurationBuilder()
    //            .SetBasePath(Directory.GetCurrentDirectory())
    //            // Add "appsettings.json" to bootstrap EF config.
    //            .AddJsonFile("appsettings.json")
    //            // Add the EF configuration provider, which will override any
    //            // config made with the JSON provider.
    //            //.AddEntityFrameworkConfig(options =>
    //            //    options.UseSqlServer(connectionStringConfig.GetConnectionString(
    //            //        "DefaultConnection"))
    //            //)
    //            .Build();

    //        Console.WriteLine("QuickAuditDatabase={0}", config.GetSection("ConnectionStrings")["QuickAuditDatabase"]);
    //        Console.WriteLine();

    //        Console.WriteLine("Press a key...");
    //        Console.ReadKey();
    //    }
    //}
}
