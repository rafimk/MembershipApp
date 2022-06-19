using Membership.Application.Commands.Memberships.MembershipPeriods;
using Membership.Application.Security;
using Membership.Core.Abstractions;
using Membership.Core.Contracts.Users;
using Membership.Core.Entities.Memberships.MembershipPeriods;
using Membership.Core.Entities.Memberships.Professions;
using Membership.Core.Entities.Memberships.Qualifications;
using Membership.Core.Entities.Nationalities;
using Membership.Core.Entities.Users;
using Membership.Core.ValueObjects;
using Membership.Infrastructure.DAL.DataSeed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Membership.Infrastructure.DAL;

internal sealed class DatabaseInitializer : IHostedService
{
    // Service locator "anti-pattern" (but it depends) :)
    private readonly IServiceProvider _serviceProvider;
    private readonly IPasswordManager _passwordManager;
    private readonly IClock _clock;

    public DatabaseInitializer(IServiceProvider serviceProvider, 
        IPasswordManager passwordManager, IClock clock)
    {
        _serviceProvider = serviceProvider;
        _passwordManager = passwordManager;
        _clock = clock;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<MembershipDbContext>();
        await dbContext.Database.MigrateAsync(cancellationToken);

        if (!(await dbContext.States.AnyAsync(cancellationToken)))
        {
            var states = new List<State>
            {
                State.Create(Guid.Parse("E50C3216-A1B5-46CF-8386-185D3BCF11CF"), "FUJAIRAH", "FHJ", _clock.Current()),
                State.Create(Guid.Parse("B4A729EB-E004-4E53-8C03-220D4B4F9B12"), "SHARJAH", "SHJ",  _clock.Current()),
                State.Create(Guid.Parse("C4613305-D19A-4719-931A-58D7D5853A41"), "ABU DHABI", "ADH", _clock.Current()),
                State.Create(Guid.Parse("B9BE4F9E-1EF0-4EF6-B4B3-7015F689532B"), "AL AIN", "ALN", _clock.Current()),
                State.Create(Guid.Parse("D6C29ACF-2C83-446F-BFA2-70C914218969"), "DUBAI", "DXB", _clock.Current()),
                State.Create(Guid.Parse("B6D27754-B617-47FA-B8CB-E23C92EC2AD0"), "UMM AL QUWAIN", "UAQ", _clock.Current()),
                State.Create(Guid.Parse("353BE5CA-EA67-47EA-9E78-E3E8B55A8A15"), "RAS AL KHAIMAH", "RAK", _clock.Current()),
                State.Create(Guid.Parse("6C0C8AC8-8C1E-43AD-8399-F66E2D53A9C9"), "AJMAN", "AJM",_clock.Current()),
            };

            await dbContext.States.AddRangeAsync(states, cancellationToken);
        }

        if (!(await dbContext.Areas.AnyAsync(cancellationToken)))
        {
            var areas = new List<Area>
            {
                Area.Create(Guid.Parse("3472B53D-0EF9-4251-B291-190B35CD280B"), "DUBAI",Guid.Parse("D6C29ACF-2C83-446F-BFA2-70C914218969"), _clock.Current()),
                Area.Create(Guid.Parse("9073BB7B-5E09-4FD7-80A4-1D874BC78DC3"), "MUSAFFAH", Guid.Parse("C4613305-D19A-4719-931A-58D7D5853A41"), _clock.Current()),
                Area.Create(Guid.Parse("43FFA5C5-1D27-482C-8E67-40069AFB92A4"), "AJMAN", Guid.Parse("6C0C8AC8-8C1E-43AD-8399-F66E2D53A9C9"),  _clock.Current()),
                Area.Create(Guid.Parse("D48D1A9F-18E0-47BF-B8FC-598901276F92"), "AL AIN", Guid.Parse("B9BE4F9E-1EF0-4EF6-B4B3-7015F689532B"),  _clock.Current()),
                Area.Create(Guid.Parse("920060E9-D813-4278-A1E3-5A6064F86636"), "SHARJAH", Guid.Parse("B4A729EB-E004-4E53-8C03-220D4B4F9B12"), _clock.Current()),
                Area.Create(Guid.Parse("19D83AD7-B5BC-47B8-997B-5CDD0F5363C4"), "AWEER", Guid.Parse("D6C29ACF-2C83-446F-BFA2-70C914218969"), _clock.Current()),
                Area.Create(Guid.Parse("A1C223A3-9570-4E50-B92A-795A6E696D21"), "RAS AL KHAIMAH", Guid.Parse("353BE5CA-EA67-47EA-9E78-E3E8B55A8A15"), _clock.Current()),
                Area.Create(Guid.Parse("4141A690-4730-488D-BD08-88392276A5CB"), "FUJAIRAH", Guid.Parse("E50C3216-A1B5-46CF-8386-185D3BCF11CF"), _clock.Current()),
                Area.Create(Guid.Parse("DF2F9A09-B0C9-459D-8FE6-98EF2E8DC8E3"), "UMM AL QUWAIN", Guid.Parse("B6D27754-B617-47FA-B8CB-E23C92EC2AD0"), _clock.Current()),
                Area.Create(Guid.Parse("3D0630FA-6742-4B87-A48C-B0C6096E2583"), "BANIYAS", Guid.Parse("C4613305-D19A-4719-931A-58D7D5853A41"), _clock.Current()),
                Area.Create(Guid.Parse("97856452-46EE-4A51-A6C9-DFC065EC73DD"), "ABU DHABI", Guid.Parse("C4613305-D19A-4719-931A-58D7D5853A41"), _clock.Current())
            };

            await dbContext.Areas.AddRangeAsync(areas, cancellationToken);
        }

        if (!(await dbContext.Districts.AnyAsync(cancellationToken)))
        {
            var districts = new List<District>
            {
                District.Create(Guid.Parse("30BA1C7F-4BD7-43D9-9F62-07C377FEB667"), "KASARAGOD", _clock.Current()),
                District.Create(Guid.Parse("666EB6D3-5977-43D3-8C2B-1124DD068988"), "THIRUVANANTHAPURAM", _clock.Current()),
                District.Create(Guid.Parse("16B81A9E-DCCA-49AE-8B28-12C2BB8A85FA"), "MALAPPURAM", _clock.Current()),
                District.Create(Guid.Parse("9D3B85D9-4F02-4AE7-A531-131D61E29F46"), "THRISSUR", _clock.Current()),
                District.Create(Guid.Parse("D8B0F759-79AD-4567-8BF8-3349D4C63EC3"), "WAYANAD", _clock.Current()),
                District.Create(Guid.Parse("D777BC44-BBA1-4BE2-8F00-3A447A6F2BFE"), "PATHANAMTHITTA", _clock.Current()),
                District.Create(Guid.Parse("CF5E40D1-6F85-4540-9CB6-5AD7D7980141"), "ALAPPUZHA", _clock.Current()),
                District.Create(Guid.Parse("42DDC8E9-D836-4C06-BE71-5E44E003BF0C"), "KOLLAM", _clock.Current()),
                District.Create(Guid.Parse("B14BB553-D564-40F0-ACC8-6ECB27D7CFA0"), "IDUKKI", _clock.Current()),
                District.Create(Guid.Parse("E18E56D4-41F3-4874-9C59-790262041BF2"), "PALAKKAD", _clock.Current()),
                District.Create(Guid.Parse("A945211D-4D57-451B-9FEB-A4585138DF82"), "KANNUR", _clock.Current()),
                District.Create(Guid.Parse("9CFE0129-1FC0-40AB-B899-AB8BF4B6110D"), "KOTTAYAM", _clock.Current()),
                District.Create(Guid.Parse("E903D35E-6BFA-495D-A988-DA7C36EF072A"), "KOZHIKODE", _clock.Current()),
                District.Create(Guid.Parse("8E8E52B9-8605-4537-ABEC-FA2A3492BE65"), "ERNAKULAM", _clock.Current())
            };

            await dbContext.Districts.AddRangeAsync(districts, cancellationToken);
        }

        if (!(await dbContext.Professions.AnyAsync(cancellationToken)))
        {
            var professions = new List<Profession>            
            {                
                Profession.Create(Guid.NewGuid(), "ACCOUNTANT", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "ACTOR", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "ACTRESS", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "AIR TRAFFIC CONTROLLER", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "ARCHITECT", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "ARTIST", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "ATTORNEY", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "BANKER", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "BARTENDER", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "BARBER", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "BOOKKEEPER", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "BUILDER", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "BUSINESSMAN", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "BUSINESSWOMAN", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "BUSINESSPERSON", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "BUTCHER", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "CARPENTER", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "CASHIER", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "CHEF", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "COACH", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "DENTAL HYGIENIST", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "DENTIST", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "DESIGNER", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "DEVELOPER", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "DIETICIAN", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "DOCTOR", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "ECONOMIST", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "EDITOR", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "ELECTRICIAN", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "ENGINEER", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "FARMER", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "FILMMAKER", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "FISHERMAN", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "FLIGHT ATTENDANT", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "JEWELER", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "JUDGE", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "LAWYER", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "MECHANIC", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "MUSICIAN", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "NUTRITIONIST", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "NURSE", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "OPTICIAN", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "PAINTER", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "PHARMACIST", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "PHOTOGRAPHER", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "PHYSICIAN", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "PHYSICIAN'S ASSISTANT", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "PILOT", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "PLUMBER", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "POLICE OFFICER", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "POLITICIAN", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "PROFESSOR", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "PROGRAMMER", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "PSYCHOLOGIST", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "RECEPTIONIST", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "SALESMAN", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "SALESPERSON", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "SALESWOMAN", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "SECRETARY", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "SINGER", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "SURGEON", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "TEACHER", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "THERAPIST", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "TRANSLATOR", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "TRANSLATOR", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "UNDERTAKER", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "VETERINARIAN", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "VIDEOGRAPHER", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "WAITER", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "WAITRESS", _clock.Current()),
                Profession.Create(Guid.NewGuid(), "WRITER", _clock.Current()),
            };            
            await dbContext.Professions.AddRangeAsync(professions, cancellationToken);
        }

        if (!(await dbContext.Qualifications.AnyAsync(cancellationToken)))
        {
            var qualifications = new List<Qualification>
            {
                Qualification.Create(Guid.NewGuid(), "DIPLOMAS", _clock.Current()),
                Qualification.Create(Guid.NewGuid(), "THREE YEAR BACHELOR PASS DEGREES", _clock.Current()),
                Qualification.Create(Guid.NewGuid(), "FOUR YEAR BACHELOR PASS DEGREES", _clock.Current()),
                Qualification.Create(Guid.NewGuid(), "FOUR YEAR BACHELOR HONOURS DEGREES", _clock.Current()),
                Qualification.Create(Guid.NewGuid(), "ONE YEAR BACHELOR HONOURS DEGREES", _clock.Current()),
                Qualification.Create(Guid.NewGuid(), "DOUBLE BACHELOR DEGREES", _clock.Current()),
                Qualification.Create(Guid.NewGuid(), "GRADUATE ENTRY BACHELOR DEGREES", _clock.Current()),
                Qualification.Create(Guid.NewGuid(), "GRADUATE CERTIFICATES", _clock.Current()),
                Qualification.Create(Guid.NewGuid(), "GRADUATE DIPLOMAS", _clock.Current()),
                Qualification.Create(Guid.NewGuid(), "MASTERS BY COURSEWORK DEGREES", _clock.Current()),
                Qualification.Create(Guid.NewGuid(), "MASTERS BY RESEARCH DEGREES", _clock.Current()),
                Qualification.Create(Guid.NewGuid(), "DOCTORAL DEGREES BY THESIS", _clock.Current()),
                Qualification.Create(Guid.NewGuid(), "HIGHER DOCTORAL DEGREES", _clock.Current()),
                Qualification.Create(Guid.NewGuid(), "HONORARY DOCTORAL DEGREES", _clock.Current()),
                Qualification.Create(Guid.NewGuid(), "SECONDARY SCHOOL LEAVING CERTIFICATE", _clock.Current()),
                Qualification.Create(Guid.NewGuid(), "PLUS TWO / PRE-UNIVERSITY COURSE", _clock.Current()),
                Qualification.Create(Guid.NewGuid(), "OTHERS (N/A)", _clock.Current()),
            };
            
            await dbContext.Qualifications.AddRangeAsync(qualifications, cancellationToken);
        }
        
        if (!(await dbContext.Users.AnyAsync(cancellationToken)))
        {
            var securedPassword = _passwordManager.Secure("admin@123");
            
            var adminUser = new UserCreateContract
            {
                Id = Guid.NewGuid(),
                FullName = "admin",
                Email = "admin@admin.com",
                MobileNumber = "0505550444",
                AlternativeContactNumber = "0505550444",
                Designation = "admin",
                PasswordHash = securedPassword,
                Role = UserRole.CentralCommitteeAdmin(),
                CascadeId = null,
                CreatedAt = _clock.Current(),
            };
 
            await dbContext.Users.AddAsync(User.Create(adminUser), cancellationToken);
        }
        
        if (!(await dbContext.MembershipPeriods.AnyAsync(cancellationToken)))
        {
            var contract = new CreateMembershipPeriod
            {
                MembershipPeriodId = Guid.NewGuid(),
                Start = new DateTimeOffset(2022,1,1,0,0,0, TimeSpan.Zero),
                End = new DateTimeOffset(2022,12,31,23,59,59,999, TimeSpan.Zero),
                RegistrationUntil = new DateTimeOffset(2022,12,31,23,59,59,999, TimeSpan.Zero),
            };

            var membershipPeriod = MembershipPeriod.Create(contract.MembershipPeriodId, contract.Start,
                contract.End, contract.RegistrationUntil, _clock.Current());
            
            membershipPeriod.Activate();
 
            await dbContext.MembershipPeriods.AddAsync(membershipPeriod, cancellationToken);
        }

        if (!(await dbContext.Mandalams.AnyAsync(cancellationToken)))
        {
            var mandalams = MandalamDataSeed.GetSeedData();

            var mandalamList = new List<Mandalam>();
            
            foreach (var item in mandalams)
            {
                var mandalam = Mandalam.Create(item.Id, item.Name, item.DistrictId, _clock.Current());
                mandalamList.Add(mandalam);
            }
            
            await dbContext.Mandalams.AddRangeAsync(mandalamList, cancellationToken);
        }
        
        if (!(await dbContext.Panchayats.AnyAsync(cancellationToken)))
        {
            var panchayats = PanchayathDataSeed.GetSeedData();

            var panchayatList = new List<Panchayat>();
            
            foreach (var item in panchayats)
            {
                var panchayat = Panchayat.Create(item.Id, item.Name, item.MandalamId, _clock.Current());
                panchayatList.Add(panchayat);
            }
            
            await dbContext.Panchayats.AddRangeAsync(panchayatList, cancellationToken);
        }

        await dbContext.SaveChangesAsync(cancellationToken);    
    }    
        
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}