using DentRec.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DentRec.Infrastructure.SeedData
{
    public static class SeedData
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (!context.Dentists.Any())
            {
                var dentistsData = await File.ReadAllTextAsync(path + @"/SeedData/dentists.json");

                var dentists = JsonSerializer.Deserialize<List<Dentist>>(dentistsData);

                if (dentists == null) return;

                context.Dentists.AddRange(dentists);

                await context.SaveChangesAsync();
            }

            if (!context.Procedures.Any())
            {
                var proceduresData = await File.ReadAllTextAsync(path + @"/SeedData/procedures.json");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() }
                };

                var procedures = JsonSerializer.Deserialize<List<Procedure>>(proceduresData, options);

                if (procedures == null) return;

                context.Procedures.AddRange(procedures);

                await context.SaveChangesAsync();
            }
        }
    }
}
