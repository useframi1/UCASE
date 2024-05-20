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

    public static async Task SeedSubjects(DataContext context)
    {
        if (await context.Subjects.AnyAsync()) return;

        var subjectsData = await File.ReadAllTextAsync("Data/SubjectsSeedData.json");

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var subjects = JsonSerializer.Deserialize<List<Subject>>(subjectsData, options);

        foreach (var subject in subjects)
        {
            context.Subjects.Add(subject);
        }

        await context.SaveChangesAsync();
    }

    public static async Task SeedIndustries(DataContext context)
    {
        if (await context.Industries.AnyAsync()) return;

        var industriesData = await File.ReadAllTextAsync("Data/IndustriesSeedData.json");

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var industries = JsonSerializer.Deserialize<List<Industry>>(industriesData, options);

        foreach (var industry in industries)
        {
            context.Industries.Add(industry);
        }

        await context.SaveChangesAsync();
    }

    public static async Task SeedUniversities(DataContext context)
    {
        if (await context.Universities.AnyAsync()) return;

        var universitiesData = await File.ReadAllTextAsync("Data/UniversitiesSeedData.json");

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var universities = JsonSerializer.Deserialize<List<University>>(universitiesData, options);

        foreach (var university in universities)
        {
            context.Universities.Add(university);
        }

        await context.SaveChangesAsync();
    }
}
