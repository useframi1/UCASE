using System.Text;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace API.Data;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IDataRepository _dataRepository;
    private readonly IUniversityRepository _universityRepository;

    public UserRepository(DataContext context, IMapper mapper, IDataRepository dataRepository, IUniversityRepository universityRepository)
    {
        _context = context;
        _mapper = mapper;
        _dataRepository = dataRepository;
        _universityRepository = universityRepository;
    }

    public async Task<bool> EditProfileAsync(ProfileDto profileDto)
    {
        var user = await GetUserByEmailAsync(profileDto.Email);

        user.FirstName = profileDto.FirstName;
        user.LastName = profileDto.LastName;
        user.Dob = profileDto.Dob;
        user.Gender = profileDto.Gender;
        user.PhoneNo = profileDto.PhoneNo;
        user.Nationality = profileDto.Nationality;
        user.StartUni = profileDto.StartUni;
        user.GovName = profileDto.GovName;
        user.Area = profileDto.Area;
        user.AddressLine1 = profileDto.AddressLine1;
        user.AddressLine2 = profileDto.AddressLine2;

        Update(user);

        return await UpdatePreferredIndustriesAsync(profileDto.Email, profileDto.Industries)
            && await UpdatePreferredSubjectsAsync(profileDto.Email, profileDto.Subjects);
    }

    public async Task<MemberDto> GetMemberAsync(string email)
    {
        return await _context.Users
            .Where(u => u.Email == email)
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<MemberDto>> GetMembersAsync()
    {
        return await _context.Users
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public class Part
    {
        public string text { get; set; }
    }

    public class Content
    {
        public List<Part> parts { get; set; }
    }

    public class Candidate
    {
        public Content content { get; set; }
    }

    public class RootRequest
    {
        public List<Content> contents { get; set; }
    }

    public class RootResponse
    {
        public List<Candidate> candidates { get; set; }
    }

    public string CreateRequestBody(List<string> universities, List<string> subjects, List<string> industries)
    {
        var text = $"From these universities that are in egypt: {JsonConvert.SerializeObject(universities)} write a list in this format ['uni1', 'uni2', ...] for recommended universities for someone who is interested in subjects like {JsonConvert.SerializeObject(subjects)} and industries like {JsonConvert.SerializeObject(industries)}. also, only write the list and nothing else";

        var root = new RootRequest
        {
            contents =
        [
            new Content
            {
                parts =
                [
                    new Part { text = text }
                ]
            }
        ]
        };

        return JsonConvert.SerializeObject(root);
    }

    public async Task<IEnumerable<UniversityDto>> GetRecommendedUniversitiesAsync(string email)
    {
        var user = await GetMemberAsync(email);
        var universities = new List<string>(await _dataRepository.GetUniversityNamesAsync());
        var subjects = user.PreferredSubjects;
        var industries = user.PreferredIndustries;


        var subjectNames = new List<string>();
        foreach (var subject in subjects)
        {
            subjectNames.Add(subject.Subject);
        }

        var industryNames = new List<string>();
        foreach (var industry in industries)
        {
            industryNames.Add(industry.Industry);
        }

        var requestBody = CreateRequestBody(universities, subjectNames, industryNames);

        var client = new HttpClient();
        var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent?key=AIzaSyAJsuCzJxEbxbmDB6eKKryDzdau6A6c_6o", content);
        var responseString = await response.Content.ReadAsStringAsync();
        var root = JsonConvert.DeserializeObject<RootResponse>(responseString);
        var text = root.candidates[0].content.parts[0].text;
        if (text[0] != '[')
        {
            return new List<UniversityDto>();
        }
        var uniList = JsonConvert.DeserializeObject<List<string>>(text);
        var recommendedUniversities = new List<UniversityDto>();
        foreach (var uniName in uniList)
        {
            recommendedUniversities.Add(await _universityRepository.GetUniversityAsync(uniName));
        }
        return recommendedUniversities;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _context.Users
            .Include(x => x.Universities)
            .Include(x => x.PreferredIndustries)
            .Include(x => x.PreferredSubjects)
            .Include(x => x.Application)
            .ThenInclude(x => x.UniNames)
            .SingleOrDefaultAsync(x => x.Email == email);
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _context.Users
            .Include(x => x.Universities)
            .Include(x => x.PreferredIndustries)
            .Include(x => x.PreferredSubjects)
            .ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void Update(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
    }

    public async Task<bool> UpdateFavoriteUniversitiesAsync(string email, string[] universities)
    {
        var user = await GetUserByEmailAsync(email);
        user.Universities.Clear();

        foreach (var universityName in universities)
        {
            var uni = await _context.Universities.FindAsync(universityName);
            user.Universities.Add(uni);
        }

        await SaveAllAsync();

        return true;
    }

    public async Task<bool> UpdatePreferredIndustriesAsync(string email, string[] industries)
    {
        _context.PreferredIndustries.RemoveRange(_context.PreferredIndustries.Where(fu => fu.Email == email));

        foreach (var industryName in industries)
        {
            var industry = new PreferredIndustry
            {
                Email = email,
                Industry = industryName
            };

            _context.PreferredIndustries.Add(industry);
        }

        await SaveAllAsync();

        return true;
    }

    public async Task<bool> UpdatePreferredSubjectsAsync(string email, string[] subjects)
    {
        _context.PreferredSubjects.RemoveRange(_context.PreferredSubjects.Where(fu => fu.Email == email));

        foreach (var subjectName in subjects)
        {
            var subject = new PreferredSubject
            {
                Email = email,
                Subject = subjectName
            };

            _context.PreferredSubjects.Add(subject);
        }

        await SaveAllAsync();

        return true;
    }

    public async Task<bool> UpdateGuardianInfoAsync(GuardianInfoDto guardianInfoDto)
    {
        var user = await GetUserByEmailAsync(guardianInfoDto.Email);

        user.Application.GuardianName = guardianInfoDto.GuardianName;
        user.Application.GuardianEmail = guardianInfoDto.GuardianEmail;
        user.Application.GuardianNumber = guardianInfoDto.GuardianNumber;
        user.Application.GuardianProfession = guardianInfoDto.GuardianProfession;
        user.Application.GuardianCompany = guardianInfoDto.GuardianCompany;

        Update(user);

        await SaveAllAsync();

        return true;
    }

    public async Task<bool> UpdateEducationAsync(EducationDto educationDto)
    {
        var user = await GetUserByEmailAsync(educationDto.Email);

        user.Application.SchoolName = educationDto.SchoolName;
        user.Application.SchoolCountry = educationDto.SchoolCountry;
        user.Application.SchoolCity = educationDto.SchoolCity;
        user.Application.YearOfGraduation = educationDto.YearOfGraduation;

        Update(user);

        await SaveAllAsync();

        return true;
    }
}
