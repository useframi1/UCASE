using System.Text.Json;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class Seed
{
    public static async Task SeedGovernorates(DataContext context)
    {
        if (await context.Governorates.AnyAsync()) return;

        var governoratesData = await File.ReadAllTextAsync("Data/GovernorateSeedData.json");

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var governorates = JsonSerializer.Deserialize<List<Governorate>>(governoratesData, options);

        foreach (var governorate in governorates)
        {
            context.Governorates.Add(governorate);
        }

        await context.SaveChangesAsync();
    }
}
