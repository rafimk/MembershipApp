﻿using Membership.Application.Commands.Memberships.MembershipPeriods;
using Membership.Application.Security;
using Membership.Core.Abstractions;
using Membership.Core.Contracts.Users;
using Membership.Core.Entities.Memberships.MembershipPeriods;
using Membership.Core.Entities.Memberships.Professions;
using Membership.Core.Entities.Memberships.Qualifications;
using Membership.Core.Entities.Memberships.RegisteredOrganizations;
using Membership.Core.Entities.Memberships.WelfareSchemes;
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
                State.Create(Guid.Parse("E50C3216-A1B5-46CF-8386-185D3BCF11CF"), "FUJAIRAH", "FUJ", _clock.Current()),
                State.Create(Guid.Parse("B4A729EB-E004-4E53-8C03-220D4B4F9B12"), "SHARJAH", "SHJ",  _clock.Current()),
                State.Create(Guid.Parse("C4613305-D19A-4719-931A-58D7D5853A41"), "ABU DHABI", "AUH", _clock.Current()),
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
                Area.Create(Guid.Parse("03e0d6a8-41c4-42c9-a80d-2b184ab91a62"), "AL QUOWA",Guid.Parse("b9be4f9e-1ef0-4ef6-b4b3-7015f689532b"), _clock.Current()),
                Area.Create(Guid.Parse("0b9ff92e-758f-420b-9b10-c78f2a821522"), "JAMMIYA",Guid.Parse("b6d27754-b617-47fa-b8cb-e23c92ec2ad0"), _clock.Current()),
                Area.Create(Guid.Parse("13cbc723-bbef-4686-92f2-7026d18deec1"), "AL KHAIL",Guid.Parse("353be5ca-ea67-47ea-9e78-e3e8b55a8a15"), _clock.Current()),
                Area.Create(Guid.Parse("18fe66d6-4303-4633-9501-893333311d63"), "KHORFAKKAN",Guid.Parse("e50c3216-a1b5-46cf-8386-185d3bcf11cf"), _clock.Current()),
		        Area.Create(Guid.Parse("B64415AE-97E1-42C1-9952-55283810F8F7"), "KALBA",Guid.Parse("e50c3216-a1b5-46cf-8386-185d3bcf11cf"), _clock.Current()),
                Area.Create(Guid.Parse("1d7d27f7-b6b2-4aef-b72f-98d52db02399"), "OLD SANAYIYYA",Guid.Parse("b6d27754-b617-47fa-b8cb-e23c92ec2ad0"), _clock.Current()),
                Area.Create(Guid.Parse("2e07b808-7645-4ff0-bc7b-29a6d9895e00"), "SOUHAN",Guid.Parse("b9be4f9e-1ef0-4ef6-b4b3-7015f689532b"), _clock.Current()),
                Area.Create(Guid.Parse("3472b53d-0ef9-4251-b291-190b35cd280b"), "DUBAI",Guid.Parse("d6c29acf-2c83-446f-bfa2-70c914218969"), _clock.Current()),
                Area.Create(Guid.Parse("3d0630fa-6742-4b87-a48c-b0c6096e2583"), "BANIYAS",Guid.Parse("c4613305-d19a-4719-931a-58d7d5853a41"), _clock.Current()),
                Area.Create(Guid.Parse("4141a690-4730-488d-bd08-88392276a5cb"), "FUJAIRAH",Guid.Parse("e50c3216-a1b5-46cf-8386-185d3bcf11cf"), _clock.Current()),
                Area.Create(Guid.Parse("43ffa5c5-1d27-482c-8e67-40069afb92a4"), "AJMAN",Guid.Parse("6c0c8ac8-8c1e-43ad-8399-f66e2d53a9c9"), _clock.Current()),
                Area.Create(Guid.Parse("52f0a04f-1071-4e9d-a819-2d07ee3763d9"), "KARAN",Guid.Parse("353be5ca-ea67-47ea-9e78-e3e8b55a8a15"), _clock.Current()),
                Area.Create(Guid.Parse("6441bcb4-e503-4723-a2c3-bd08983cac30"), "MARIDH",Guid.Parse("353be5ca-ea67-47ea-9e78-e3e8b55a8a15"), _clock.Current()),
                Area.Create(Guid.Parse("9073bb7b-5e09-4fd7-80a4-1d874bc78dc3"), "MUSAFFAH",Guid.Parse("c4613305-d19a-4719-931a-58d7d5853a41"), _clock.Current()),
                Area.Create(Guid.Parse("920060e9-d813-4278-a1e3-5a6064f86636"), "SHARJAH",Guid.Parse("b4a729eb-e004-4e53-8c03-220d4b4f9b12"), _clock.Current()),
                Area.Create(Guid.Parse("9241704c-5d10-4004-88df-c4e3e1c57925"), "NEW SANAYIYYA",Guid.Parse("b6d27754-b617-47fa-b8cb-e23c92ec2ad0"), _clock.Current()),
                Area.Create(Guid.Parse("97856452-46ee-4a51-a6c9-dfc065ec73dd"), "ABU DHABI",Guid.Parse("c4613305-d19a-4719-931a-58d7d5853a41"), _clock.Current()),
                Area.Create(Guid.Parse("a1c223a3-9570-4e50-b92a-795a6e696d21"), "RAS AL KHAIMAH",Guid.Parse("353be5ca-ea67-47ea-9e78-e3e8b55a8a15"), _clock.Current()),
                Area.Create(Guid.Parse("a37c8a63-0c43-4ab0-ab72-7073825f7381"), "RAMS",Guid.Parse("353be5ca-ea67-47ea-9e78-e3e8b55a8a15"), _clock.Current()),
                Area.Create(Guid.Parse("a8242176-5398-4484-9d87-0587165e37db"), "SALAMA",Guid.Parse("b6d27754-b617-47fa-b8cb-e23c92ec2ad0"), _clock.Current()),
                Area.Create(Guid.Parse("bfc7d8a2-fe94-4d7a-a9e7-67b3676af267"), "SHAM",Guid.Parse("353be5ca-ea67-47ea-9e78-e3e8b55a8a15"), _clock.Current()),
                Area.Create(Guid.Parse("d48d1a9f-18e0-47bf-b8fc-598901276f92"), "AL AIN",Guid.Parse("b9be4f9e-1ef0-4ef6-b4b3-7015f689532b"), _clock.Current()),
                Area.Create(Guid.Parse("d6ffd518-0aac-43c1-9a12-a1ca59fcc796"), "DIBBA",Guid.Parse("e50c3216-a1b5-46cf-8386-185d3bcf11cf"), _clock.Current()),
                Area.Create(Guid.Parse("d75c20be-05fe-43f3-8d24-1fdd8a54ed05"), "MASAFI",Guid.Parse("e50c3216-a1b5-46cf-8386-185d3bcf11cf"), _clock.Current()),
                Area.Create(Guid.Parse("df2f9a09-b0c9-459d-8fe6-98ef2e8dc8e3"), "UMM AL QUWAIN",Guid.Parse("b6d27754-b617-47fa-b8cb-e23c92ec2ad0"), _clock.Current()),
                Area.Create(Guid.Parse("e051961c-359b-44e5-b2ec-58dc200378dc"), "BASAR",Guid.Parse("b6d27754-b617-47fa-b8cb-e23c92ec2ad0"), _clock.Current()),
                Area.Create(Guid.Parse("e38e9f88-38c7-4755-a768-cf8bacc0e81a"), "FALAJ AL MUALLA",Guid.Parse("b6d27754-b617-47fa-b8cb-e23c92ec2ad0"), _clock.Current()),
                Area.Create(Guid.Parse("ecf6ad62-fc9b-4ace-852a-8c19608c717f"), "AL KHATHAM",Guid.Parse("c4613305-d19a-4719-931a-58d7d5853a41"), _clock.Current()),
                Area.Create(Guid.Parse("f5e7525b-1d75-4f51-b102-1a2a980197cf"), "SHAHAMA",Guid.Parse("c4613305-d19a-4719-931a-58d7d5853a41"), _clock.Current()),
                Area.Create(Guid.Parse("fb2ea6fb-16de-4f71-a3d8-faebe7cfe1ad"), "ALHEAR",Guid.Parse("b9be4f9e-1ef0-4ef6-b4b3-7015f689532b"), _clock.Current())
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
                Profession.Create(Guid.Parse("08f701a3-781a-40ad-9973-8999bcb4b35a"), "SECRETARY", _clock.Current()),
                Profession.Create(Guid.Parse("0f4b6b59-381e-404f-8a34-4ff9f4f17cc0"), "PRO", _clock.Current()),
                Profession.Create(Guid.Parse("0fdf91c3-fedf-4880-9013-97671b3e5d9d"), "COOK", _clock.Current()),
                Profession.Create(Guid.Parse("163cccfa-4f57-423e-8e01-2f68dea1c3c6"), "ENGINEER-COMPUTER", _clock.Current()),
                Profession.Create(Guid.Parse("2b6bde06-c105-447f-9ea0-cfe594834126"), "SECURITYGUARD", _clock.Current()),
                Profession.Create(Guid.Parse("2d1b493d-7900-4ba5-bde4-84d490db0e92"), "DRAFTSMAN", _clock.Current()),
                Profession.Create(Guid.Parse("2db1d4ce-715f-4727-9771-34c0538d83fc"), "CHEF", _clock.Current()),
                Profession.Create(Guid.Parse("4ef61457-49eb-4c84-9fe4-28ae53c919b4"), "SYSTEMADMINISTRATOR", _clock.Current()),
                Profession.Create(Guid.Parse("6aef1d9b-9839-4285-addb-19133cfd8a95"), "JOURNALIST", _clock.Current()),
                Profession.Create(Guid.Parse("6cfebedd-ce1b-4cd2-bc97-7d0a80478867"), "TYPIST", _clock.Current()),
                Profession.Create(Guid.Parse("72cab49a-2062-48f3-8d4e-382881f386c4"), "DRIVER-HEAVYVEHICLE", _clock.Current()),
                Profession.Create(Guid.Parse("8420f24e-b2dd-44b8-887a-4c16bdc96aef"), "WAITER", _clock.Current()),
                Profession.Create(Guid.Parse("89b90dbe-84b6-4f00-94e6-51720d4ecf56"), "ENGINEER-MECHANICAL", _clock.Current()),
                Profession.Create(Guid.Parse("99a55dbe-4cfe-4ff6-8e6c-8eb5c9022887"), "DOCTOR", _clock.Current()),
                Profession.Create(Guid.Parse("9c125ec5-1274-4536-a74c-6643bf35c943"), "BUTCHER", _clock.Current()),
                Profession.Create(Guid.Parse("9c150285-d96f-4ea0-a4a3-440502af09b0"), "VIDEOGRAPHER", _clock.Current()),
                Profession.Create(Guid.Parse("9fd112c1-db2f-4a6a-b587-04e84deb2bf8"), "BUILDER", _clock.Current()),
                Profession.Create(Guid.Parse("a26834fe-adb9-4f38-9410-29f2e9f347f2"), "SALESWOMAN", _clock.Current()),
                Profession.Create(Guid.Parse("ac288549-ab52-419e-b976-b769dc24749e"), "DENTIST", _clock.Current()),
                Profession.Create(Guid.Parse("accb64c5-6244-4a92-aec3-c9d646763919"), "PHYSICIAN", _clock.Current()),
                Profession.Create(Guid.Parse("addb7a07-053a-4265-81ab-dabc0c95c170"), "SALESEXECUTIVE", _clock.Current()),
                Profession.Create(Guid.Parse("af058d8b-ab61-495c-ae35-05eb41ac342a"), "ARCHITECT", _clock.Current()),
                Profession.Create(Guid.Parse("afaac11f-4389-4ff9-b2ba-11d32463baac"), "MANAGER", _clock.Current()),
                Profession.Create(Guid.Parse("b150a12f-7fbd-425d-b73a-d085d845a277"), "JEWELER", _clock.Current()),
                Profession.Create(Guid.Parse("bb6811cf-a864-42a1-bb99-d7f4f78a8c0f"), "BUSINESS", _clock.Current()),
                Profession.Create(Guid.Parse("bc086056-6c43-48bf-a9a3-1aa9202f00a8"), "HR-ADMIN", _clock.Current()),
                Profession.Create(Guid.Parse("bda002ef-8948-42cc-8e62-259ac7dd6e1d"), "UNSKLILLEDLABOUR", _clock.Current()),
                Profession.Create(Guid.Parse("bf4cbd6b-1dad-4b18-9ae4-c1229438f9f6"), "SUPERVISOR", _clock.Current()),
                Profession.Create(Guid.Parse("bffd45ce-cdea-48ef-9ac9-f112344dd6ad"), "TAILOR", _clock.Current()),
                Profession.Create(Guid.Parse("c50a8342-ac8e-40a4-b8db-6b875e8577c0"), "BARBER", _clock.Current()),
                Profession.Create(Guid.Parse("c58884f4-1596-4cb6-bb16-a5c3d713fe60"), "SKILLEDLABOUR", _clock.Current()),
                Profession.Create(Guid.Parse("c5bc05d7-ecb1-4426-a73e-8fe464bfaa2a"), "SURGEON", _clock.Current()),
                Profession.Create(Guid.Parse("c90241e6-6500-4587-8142-ec17bdcd386c"), "LECTURER", _clock.Current()),
                Profession.Create(Guid.Parse("ccd46a81-0803-474d-a330-256b294eaa5b"), "OFFICEBOY", _clock.Current()),
                Profession.Create(Guid.Parse("ce26ab12-38f6-4ce3-a16b-6509bbfd059b"), "INVESTOR", _clock.Current()),
                Profession.Create(Guid.Parse("d27a3714-410c-4332-899e-1c165362816b"), "CASHIER", _clock.Current()),
                Profession.Create(Guid.Parse("d3109548-6387-45eb-8897-8f10ad6598ca"), "PHOTOGRAPHER", _clock.Current()),
                Profession.Create(Guid.Parse("d3e5b074-cf83-471f-8c89-b3181ba20504"), "DRIVER-LIGHTVEHICLE", _clock.Current()),
                Profession.Create(Guid.Parse("d6d44ea8-2225-4c4b-9c7b-4da4af6eed93"), "ELECTRICIAN", _clock.Current()),
                Profession.Create(Guid.Parse("d8eece34-8b9b-4ded-9f86-5276cefa3180"), "TRADER", _clock.Current()),
                Profession.Create(Guid.Parse("d9a42337-02ca-4038-81af-5c79edb6a40d"), "SHOPKEEPER", _clock.Current()),
                Profession.Create(Guid.Parse("dcc20f80-fc4e-49dc-a0bc-bf18b3882ff1"), "JUDGE", _clock.Current()),
                Profession.Create(Guid.Parse("e2ed2d07-3d01-4dbb-ae7b-6368c34e84da"), "ACCOUNTANT", _clock.Current()),
                Profession.Create(Guid.Parse("e2febb04-f615-4181-9d9b-14169c5f5158"), "ENGINEER-CIVIL", _clock.Current()),
                Profession.Create(Guid.Parse("e8cd7aa2-3bed-46df-bce0-f0da68ac8e35"), "PLUMBER", _clock.Current()),
                Profession.Create(Guid.Parse("ef83c83d-b769-4e61-94a9-b4ddde649ec5"), "NURSE", _clock.Current()),
                Profession.Create(Guid.Parse("efc965c2-21a2-4a47-9769-beaf896380fb"), "ENGINEER", _clock.Current()),
                Profession.Create(Guid.Parse("f0bc47c5-e6f4-450d-9118-a983319d677d"), "PROJECTMANAGER", _clock.Current()),
                Profession.Create(Guid.Parse("f42c9c10-317b-44ce-b344-90c2f2af7ca2"), "EDITOR", _clock.Current()),
                Profession.Create(Guid.Parse("f441c27e-e0ea-4e24-a503-9a5f6eee1496"), "MESSENGER", _clock.Current()),
                Profession.Create(Guid.Parse("f529ac0a-5094-43d4-947a-a3e415999d82"), "DRIVINGINSTRUCTOR", _clock.Current()),
                Profession.Create(Guid.Parse("f5e3c868-84e8-47b6-a5ed-c7c491258fe4"), "LAWYER", _clock.Current()),
                Profession.Create(Guid.Parse("f62f7f9d-5a9a-4dea-9f8a-7993da581e02"), "PROGRAMMER", _clock.Current()),
                Profession.Create(Guid.Parse("f70eecdc-df41-43d8-a96b-6bc2fe3702dc"), "AUDITOR", _clock.Current()),
                Profession.Create(Guid.Parse("fb256e70-d629-467a-ad1c-cdc7b1171f2d"), "TEACHER", _clock.Current()),
                Profession.Create(Guid.Parse("fc7a5dd0-237c-4ea1-8229-3eb51898957e"), "ADMIN", _clock.Current()),
                Profession.Create(Guid.Parse("fc9368fe-56e8-4717-8c1d-c19b04a028a7"), "PAINTER", _clock.Current())
            };            
            await dbContext.Professions.AddRangeAsync(professions, cancellationToken);
        }

        if (!(await dbContext.Qualifications.AnyAsync(cancellationToken)))
        {
            var qualifications = new List<Qualification>
            {
                Qualification.Create(Guid.Parse("0de682b5-b491-4623-889b-5ba9c72aea53"), "OTHERS (N/A)", _clock.Current()),
                Qualification.Create(Guid.Parse("A69D8042-662D-4CE2-AAB3-0E8759260AFD"), "PRIMARY", _clock.Current()),
                Qualification.Create(Guid.Parse("B828BA9E-FEE6-44C1-B9C3-89EF49BEFF7B"), "SECONDARY", _clock.Current()),
                Qualification.Create(Guid.Parse("15a66750-cb44-4268-bbd9-b9bc1bc11c23"), "PLUS TWO / PRE-UNIVERSITY COURSE", _clock.Current()),
                Qualification.Create(Guid.Parse("3b90e6f8-86f6-4065-a751-4142d43937c5"), "DIPLOMA", _clock.Current()),
                Qualification.Create(Guid.Parse("45e7f4c7-4551-463a-89ba-460602c4ee81"), "GRADUATE DEGREE", _clock.Current()),
                Qualification.Create(Guid.Parse("8e159a00-2954-471b-9a38-0736c5d9029c"), "PROFESSIONAL DEGREE", _clock.Current()),
                Qualification.Create(Guid.Parse("71a23ce0-36bd-42bf-b43c-c8d589a92519"), "MASTER DEGREE", _clock.Current()),
                Qualification.Create(Guid.Parse("7ba442e9-5485-446f-861a-bec8cee780af"), "DOCTORATE DEGREE", _clock.Current())
            };
            
            await dbContext.Qualifications.AddRangeAsync(qualifications, cancellationToken);
        }
        
        if (!(await dbContext.WelfareSchemes.AnyAsync(cancellationToken)))
        {
            var welfareSchemes = new List<WelfareScheme>
            {
                WelfareScheme.Create(Guid.Parse("8b08b3f0-6b8f-40f1-9a27-b8582392a1ad"), "DUBAI KMCC Welfare Scheme", _clock.Current()),
                WelfareScheme.Create(Guid.Parse("F4503A80-4EAC-4AEB-AD45-D8D495C2B649"), "ABUDHABI KMCC Care", _clock.Current()),
                WelfareScheme.Create(Guid.Parse("213B073D-BAD8-4CE4-B8A6-3A64F4D8B21A"), "SHARJAH KMCC - FAMILY CARE", _clock.Current()),
                WelfareScheme.Create(Guid.Parse("6C4550E8-EDF1-44E2-B727-9392764160BC"), "Pratheekasha Kozhikkode", _clock.Current()),
                WelfareScheme.Create(Guid.Parse("146023A0-CEDD-42DF-BE17-929138939C48"), "Kadappuram Panchayath Welfare Scheme", _clock.Current())
            };
            
            await dbContext.WelfareSchemes.AddRangeAsync(welfareSchemes, cancellationToken);
        }
        
        if (!(await dbContext.RegisteredOrganizations.AnyAsync(cancellationToken)))
        {
            var registeredOrganizations = new List<RegisteredOrganization>
            {
                RegisteredOrganization.Create(Guid.Parse("7f16542d-df41-44a1-b7a6-bac0c480d366"), "ABUDHABI INDIAN ISLAMIC CENTRE", _clock.Current()),
                RegisteredOrganization.Create(Guid.Parse("a78b37ba-6a3e-4dc5-88f5-2040c66acc39"), "ABUDHABI INDIAN SOCIAL CENTRE", _clock.Current()),
                RegisteredOrganization.Create(Guid.Parse("CD8F44F7-B9E8-454A-AFF2-9CDFFFE8CE61"), "ABUDHABI MALYALI SAMAJAM", _clock.Current()),
                RegisteredOrganization.Create(Guid.Parse("6DD72A10-7713-414D-B4A3-82DBD3161342"), "ABUDHABI KERALA SOCIAL CENTRE", _clock.Current()),
                RegisteredOrganization.Create(Guid.Parse("30BACBC9-9F5C-4FFC-9611-CDB2A0340570"), "FUJAIRAH INDIAN SOCIAL CLUB", _clock.Current()),
		        RegisteredOrganization.Create(Guid.Parse("6C3DC828-D3A3-43B4-9B89-732BA8F30217"), "KALBA INDIAN SOCIAL CLUB", _clock.Current()),
		        RegisteredOrganization.Create(Guid.Parse("8A29CFAB-98DA-4E49-B29A-6ED135780913"), "KHORFAKKAN INDIAN SOCIAL CLUB", _clock.Current()),
                RegisteredOrganization.Create(Guid.Parse("8CC583A9-905D-4249-A3BD-39E9FC722A03"), "SHARJA INDIAN ASSOACIATION", _clock.Current()),
                RegisteredOrganization.Create(Guid.Parse("3A32DAD9-B942-4CA9-976D-A11DD9D46474"), "AJMAN INDIAN ASSOCIATION", _clock.Current()),
                RegisteredOrganization.Create(Guid.Parse("52F10D30-640B-4F94-AE82-3994BA0F6B1D"), "AJMAN INDIAN SOCIAL CENTRE", _clock.Current()),
                RegisteredOrganization.Create(Guid.Parse("43414897-0CC1-487D-8843-4F516B69E396"), "AL AIN INDIAN SOCIAL CENTRE", _clock.Current()),
                RegisteredOrganization.Create(Guid.Parse("B3049D55-91BB-4718-9745-1A302A35BC1D"), "RAK INDIAN ASSOCIATION", _clock.Current()),
                RegisteredOrganization.Create(Guid.Parse("F5A6C45E-0385-49C5-94CD-A75DB6644D0E"), "RAK KERALA SAMAJAM", _clock.Current()),
                RegisteredOrganization.Create(Guid.Parse("4B41CB4F-7D2E-4CE4-B6E7-A52790B1B628"), "RAK INDIAN RELIEF COMMUNITEE", _clock.Current()),
		        RegisteredOrganization.Create(Guid.Parse("F4480683-A143-4C52-AC8D-85AB442D98AA"), "RAK JAMEYYATHUL IMAMIL BUKHARI", _clock.Current()),
                RegisteredOrganization.Create(Guid.Parse("A3E46BCF-5E92-45D8-9155-271C4E2F7203"), "RAK INDIAN BUSINESS AND PROFESSIONAL COUNCIL", _clock.Current())
            };
            
            await dbContext.RegisteredOrganizations.AddRangeAsync(registeredOrganizations, cancellationToken);
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
            };

            var membershipPeriod = MembershipPeriod.Create(contract.MembershipPeriodId, contract.Start,
                contract.End, _clock.Current());
            
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