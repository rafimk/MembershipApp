using Membership.Core.Abstractions;
using Membership.Core.Entities.Nationalities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Membership.Infrastructure.DAL;

internal sealed class DatabaseInitializer : IHostedService
{
    // Service locator "anti-pattern" (but it depends) :)
    private readonly IServiceProvider _serviceProvider;
    private readonly IClock _clock;

    public DatabaseInitializer(IServiceProvider serviceProvider, IClock clock)
    {
        _serviceProvider = serviceProvider;
        _clock = clock;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<MembershipDbContext>();
        await dbContext.Database.MigrateAsync(cancellationToken);

        if (await !dbContext.States.AnyAsync(cancellationToken))
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
                State.Create(Guid.Parse("6C0C8AC8-8C1E-43AD-8399-F66E2D53A9C9"), "AJMAN", "AJM"_clock.Current()),
            };

            await dbContext.States.AddRangeAsync(states, cancellationToken);
        }

        if (await !dbContext.Areas.AnyAsync(cancellationToken))
        {
            var areas = new List<Area>
            {
                Area.Create(Guid.Parse("3472B53D-0EF9-4251-B291-190B35CD280B"), "DUBAI",Guid.Parse("D6C29ACF-2C83-446F-BFA2-70C914218969", _clock.Current()),
                Area.Create(Guid.Parse("9073BB7B-5E09-4FD7-80A4-1D874BC78DC3"), "MUSAFFAH", Guid.Parse("C4613305-D19A-4719-931A-58D7D5853A41" _clock.Current()),
                Area.Create(Guid.Parse("43FFA5C5-1D27-482C-8E67-40069AFB92A4"), "AJMAN", Guid.Parse("6C0C8AC8-8C1E-43AD-8399-F66E2D53A9C9",  _clock.Current()),
                Area.Create(Guid.Parse("D48D1A9F-18E0-47BF-B8FC-598901276F92"), "AL AIN", Guid.Parse("B9BE4F9E-1EF0-4EF6-B4B3-7015F689532B",  _clock.Current()),
                Area.Create(Guid.Parse("920060E9-D813-4278-A1E3-5A6064F86636"), "SHARJAH", Guid.Parse("B4A729EB-E004-4E53-8C03-220D4B4F9B12", _clock.Current()),
                Area.Create(Guid.Parse("19D83AD7-B5BC-47B8-997B-5CDD0F5363C4"), "AWEER", Guid.Parse("D6C29ACF-2C83-446F-BFA2-70C914218969", _clock.Current()),
                Area.Create(Guid.Parse("A1C223A3-9570-4E50-B92A-795A6E696D21"), "RAS AL KHAIMAH", Guid.Parse("353BE5CA-EA67-47EA-9E78-E3E8B55A8A15", _clock.Current()),
                Area.Create(Guid.Parse("4141A690-4730-488D-BD08-88392276A5CB"), "FUJAIRAH", Guid.Parse("E50C3216-A1B5-46CF-8386-185D3BCF11CF", _clock.Current()),
                Area.Create(Guid.Parse("DF2F9A09-B0C9-459D-8FE6-98EF2E8DC8E3"), "UMM AL QUWAIN", Guid.Parse("B6D27754-B617-47FA-B8CB-E23C92EC2AD0", _clock.Current()),
                Area.Create(Guid.Parse("3D0630FA-6742-4B87-A48C-B0C6096E2583"), "BANIYAS", Guid.Parse("C4613305-D19A-4719-931A-58D7D5853A41", _clock.Current()),
                Area.Create(Guid.Parse("97856452-46EE-4A51-A6C9-DFC065EC73DD"), "ABU DHABI", Guid.Parse("C4613305-D19A-4719-931A-58D7D5853A41", _clock.Current())
            };

            await dbContext.Areas.AddRangeAsync(areas, cancellationToken);
        }

        if (await !dbContext.Districts.AnyAsync(cancellationToken))
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

        if (await !dbContext.Mandalams.AnyAsync(cancellationToken))
        {
            var mandalams = new List<Mandalam>
            {
                Mandalam.Create(Guid.Parse("701A1D5C-5A67-4084-A2E4-00561661568F"), "KOVALAM", Guid.Parse("666EB6D3-5977-43D3-8C2B-1124DD068988", _clock.Current()),
                Mandalam.Create(Guid.Parse("09E0ED2A-8EF1-4CDB-A1BE-05A2C788C9D3"), "DHARMADAM", Guid.Parse("A945211D-4D57-451B-9FEB-A4585138DF82", _clock.Current()),
                Mandalam.Create(Guid.Parse("6D6B622F-6E71-49A2-958E-06E1002A9E48"), "IDUKKI", Guid.Parse("B14BB553-D564-40F0-ACC8-6ECB27D7CFA0", _clock.Current()),
                Mandalam.Create(Guid.Parse("0D0718BE-E5B8-4432-8DF5-09A42B269E53"), "KANNUR", Guid.Parse("A945211D-4D57-451B-9FEB-A4585138DF82", _clock.Current()),
                Mandalam.Create(Guid.Parse("C97B8253-E044-4A27-ABD8-0B00D4B46BBA"), "IRINJALAKKUDA", Guid.Parse("9D3B85D9-4F02-4AE7-A531-131D61E29F46", _clock.Current()),
                Mandalam.Create(Guid.Parse("D5DFEE97-259C-4BDC-BFEF-0CC3F2816BE3"), "PEERUMADE", Guid.Parse("B14BB553-D564-40F0-ACC8-6ECB27D7CFA0", _clock.Current()),
                Mandalam.Create(Guid.Parse("5B696BC8-4C3F-40AE-89EA-0D5AC0F3916A"), "TALIPARAMBA", Guid.Parse("A945211D-4D57-451B-9FEB-A4585138DF82", _clock.Current()),
                Mandalam.Create(Guid.Parse("48F25C3F-FAD6-4ECB-A010-0D67C659CF7D"), "IRIKKUR", Guid.Parse("A945211D-4D57-451B-9FEB-A4585138DF82", _clock.Current()),
                Mandalam.Create(Guid.Parse("E078B7D2-D7BC-48AC-88E8-0EB165EFAC72"), "NATTIKA", Guid.Parse("9D3B85D9-4F02-4AE7-A531-131D61E29F46", _clock.Current()),
                Mandalam.Create(Guid.Parse("E565FDA7-A024-4668-A918-11FE8D6C670B"), "KUNNATHUR", Guid.Parse("42DDC8E9-D836-4C06-BE71-5E44E003BF0C", _clock.Current()),
                Mandalam.Create(Guid.Parse("4E8FCC80-CE17-42C4-B311-13389DA105ED"), "KONGAD", Guid.Parse("E18E56D4-41F3-4874-9C59-790262041BF2", _clock.Current()),
                Mandalam.Create(Guid.Parse("453E7556-EF98-493E-A869-14615EC13CB4"), "CHELAKKARA", Guid.Parse("9D3B85D9-4F02-4AE7-A531-131D61E29F46", _clock.Current()),
                Mandalam.Create(Guid.Parse("C1E6C1C7-2FD0-41A3-8AA3-1561AF040D25"), "VENGARA", Guid.Parse("16B81A9E-DCCA-49AE-8B28-12C2BB8A85FA", _clock.Current()),
                Mandalam.Create(Guid.Parse("86ECB0FC-5D66-4EBB-ABCE-15895DB10F2C"), "PAYYANNUR", Guid.Parse("A945211D-4D57-451B-9FEB-A4585138DF82", _clock.Current()),
                Mandalam.Create(Guid.Parse("720F67EF-D1D9-46BF-B298-162527B12ECA"), "NENMARA", Guid.Parse("E18E56D4-41F3-4874-9C59-790262041BF2", _clock.Current()),
                Mandalam.Create(Guid.Parse("8FFA2BA6-6546-4186-B450-19A8781068AB"), "VATTIYOORKAVU", Guid.Parse("666EB6D3-5977-43D3-8C2B-1124DD068988", _clock.Current()),
                Mandalam.Create(Guid.Parse("48025018-BF9D-412A-9F8C-1C60E54AEFCC"), "KOTHAMANGALAM", Guid.Parse("8E8E52B9-8605-4537-ABEC-FA2A3492BE65", _clock.Current()),
                Mandalam.Create(Guid.Parse("639447B8-FC3F-4FF0-8172-1D576E78141D"), "ERANAKULAM", Guid.Parse("8E8E52B9-8605-4537-ABEC-FA2A3492BE65", _clock.Current()),
                Mandalam.Create(Guid.Parse("697D40A9-D74B-4570-BF5D-1DAC0336DCD1"), "OTTAPALAM", Guid.Parse("E18E56D4-41F3-4874-9C59-790262041BF2", _clock.Current()),
                Mandalam.Create(Guid.Parse("3F60F853-110C-4E64-9161-1F219F02ADE5"), "THIRUVANANTHAPURAM", Guid.Parse("666EB6D3-5977-43D3-8C2B-1124DD068988", _clock.Current()),
                Mandalam.Create(Guid.Parse("05C0B239-06F0-4522-A14D-1F9AC11648B9"), "PARASSALA", Guid.Parse("666EB6D3-5977-43D3-8C2B-1124DD068988", _clock.Current()),
                Mandalam.Create(Guid.Parse("755AC8E1-F48F-428F-8033-222AF554BF80"), "ELATHUR", Guid.Parse("E903D35E-6BFA-495D-A988-DA7C36EF072A", _clock.Current()),
                Mandalam.Create(Guid.Parse("7F64EA28-3A8B-4EA6-829C-227AC74CEE00"), "AZHIKODE", Guid.Parse("A945211D-4D57-451B-9FEB-A4585138DF82", _clock.Current()),
                Mandalam.Create(Guid.Parse("6B5890F3-6C9B-42A1-8916-28FAA5679B0E"), "NEMOM", Guid.Parse("666EB6D3-5977-43D3-8C2B-1124DD068988", _clock.Current()),
                Mandalam.Create(Guid.Parse("A359C8E4-DAD2-4B64-A813-2DFD652CE2BD"), "MAVELIKARA", Guid.Parse("CF5E40D1-6F85-4540-9CB6-5AD7D7980141", _clock.Current()),
                Mandalam.Create(Guid.Parse("F80C6D21-DBAA-4794-B4D9-30CCAC0F012C"), "THIRUVALLA", Guid.Parse("D777BC44-BBA1-4BE2-8F00-3A447A6F2BFE", _clock.Current()),
                Mandalam.Create(Guid.Parse("C8985C0F-1E2B-4B6B-9E2C-316CFC66D128"), "ADOOR", Guid.Parse("D777BC44-BBA1-4BE2-8F00-3A447A6F2BFE", _clock.Current()),
                Mandalam.Create(Guid.Parse("95746E48-2FE6-467A-A1BA-31D34166D097"), "VAIKOM", Guid.Parse("9CFE0129-1FC0-40AB-B899-AB8BF4B6110D", _clock.Current()),
                Mandalam.Create(Guid.Parse("5DE28FCF-F8B6-4017-83FB-32AF2568983A"), "KALPETTA", Guid.Parse("D8B0F759-79AD-4567-8BF8-3349D4C63EC3", _clock.Current()),
                Mandalam.Create(Guid.Parse("A4854ABA-07BC-47D1-831F-34D0346D2848"), "TARUR", Guid.Parse("E18E56D4-41F3-4874-9C59-790262041BF2", _clock.Current()),", 
                Mandalam.Create(Guid.Parse("9596AC5C-1850-40AE-951B-370C1B5DDE52"), "PUTHUKKAD", Guid.Parse("9D3B85D9-4F02-4AE7-A531-131D61E29F46", _clock.Current()),
                Mandalam.Create(Guid.Parse("EBC6DA9D-3E36-408C-946C-37D560FC399A"), "CHATHANNUR", Guid.Parse("42DDC8E9-D836-4C06-BE71-5E44E003BF0C", _clock.Current()),
                Mandalam.Create(Guid.Parse("60B4B5BA-FE98-4C35-BB11-38DD045A58B9"), "BEYPORE", Guid.Parse("E903D35E-6BFA-495D-A988-DA7C36EF072A", _clock.Current()),
                Mandalam.Create(Guid.Parse("4B26CFBD-058A-4C1E-A41B-3A3F35CDE8BA"), "PERUMBAVOOR", Guid.Parse("8E8E52B9-8605-4537-ABEC-FA2A3492BE65", _clock.Current()),
                Mandalam.Create(Guid.Parse("639FA70F-348A-42B1-B776-3CED1558AE40"), "VYPEN", Guid.Parse("8E8E52B9-8605-4537-ABEC-FA2A3492BE65", _clock.Current()),
                Mandalam.Create(Guid.Parse("051842B7-7396-45D5-90A1-3F3CFC900EFE"), "MANKADA", Guid.Parse("16B81A9E-DCCA-49AE-8B28-12C2BB8A85FA", _clock.Current()),
                Mandalam.Create(Guid.Parse("462FB4AF-904F-41F9-A025-416B98E24FD1"), "THAVANUR", Guid.Parse("16B81A9E-DCCA-49AE-8B28-12C2BB8A85FA", _clock.Current()),
                Mandalam.Create(Guid.Parse("5CA4C754-C0C9-4064-8C55-42D706F485D0"), "KUNNAMANGALAM", Guid.Parse("E903D35E-6BFA-495D-A988-DA7C36EF072A", _clock.Current()),
                Mandalam.Create(Guid.Parse("87FC14A4-07B5-4B64-B7D7-433D60D96E03"), "THRIPUNITHURA", Guid.Parse("8E8E52B9-8605-4537-ABEC-FA2A3492BE65", _clock.Current()),
                Mandalam.Create(Guid.Parse("F787FF16-1696-4E12-8B4B-43BEFCCBEED0"), "UDUMBANCHOLA", Guid.Parse("B14BB553-D564-40F0-ACC8-6ECB27D7CFA0", _clock.Current()),
                Mandalam.Create(Guid.Parse("8ECB2E5C-DE2B-4FEC-A9F9-455D171AD89C"), "VARKALA", Guid.Parse("666EB6D3-5977-43D3-8C2B-1124DD068988", _clock.Current()),
                Mandalam.Create(Guid.Parse("F6CBC556-59D9-49DC-A869-48929DC6D260"), "VALLIKKUNNU", Guid.Parse("16B81A9E-DCCA-49AE-8B28-12C2BB8A85FA", _clock.Current()),
                Mandalam.Create(Guid.Parse("52BFE459-3C0D-4077-B3C3-48D8D63DFB32"), "OLLUR", Guid.Parse("9D3B85D9-4F02-4AE7-A531-131D61E29F46", _clock.Current()),
                Mandalam.Create(Guid.Parse("BAFBC9D6-9127-4901-B6B8-4900FB1D0CAA"), "TANUR", Guid.Parse("16B81A9E-DCCA-49AE-8B28-12C2BB8A85FA", _clock.Current()),
                Mandalam.Create(Guid.Parse("7CB01A8F-EF3B-4639-9473-490436966DCD"), "PALAKKAD", Guid.Parse("E18E56D4-41F3-4874-9C59-790262041BF2", _clock.Current()),
                Mandalam.Create(Guid.Parse("D00B5876-CB55-4C4D-988B-49ECF54160E9"), "KADUTHURUTHY", Guid.Parse("9CFE0129-1FC0-40AB-B899-AB8BF4B6110D", _clock.Current()),
                Mandalam.Create(Guid.Parse("DB367F27-4043-4CA4-89F4-4A1FA6265E0F"), "KUTTANAD", Guid.Parse("CF5E40D1-6F85-4540-9CB6-5AD7D7980141", _clock.Current()),
                Mandalam.Create(Guid.Parse("D2A32913-7D15-4978-BA4D-4C68A23274E9"), "THODUPUZHA", Guid.Parse("B14BB553-D564-40F0-ACC8-6ECB27D7CFA0", _clock.Current()),
                Mandalam.Create(Guid.Parse("EDEE1562-911C-454A-98FC-4F77727D264A"), "MATTANNUR", Guid.Parse("A945211D-4D57-451B-9FEB-A4585138DF82", _clock.Current()),
                Mandalam.Create(Guid.Parse("5E752333-B663-402B-A651-5062648F706F"), "MANNARKAD", Guid.Parse("E18E56D4-41F3-4874-9C59-790262041BF2", _clock.Current()),
                Mandalam.Create(Guid.Parse("8EDB7168-C5CB-4A17-B804-55F1827DADF9"), "TIRURANGADI", Guid.Parse("16B81A9E-DCCA-49AE-8B28-12C2BB8A85FA", _clock.Current()),
                Mandalam.Create(Guid.Parse("F00D3681-3D0C-4534-95FB-595193AAE566"), "CHERTHALA", Guid.Parse("CF5E40D1-6F85-4540-9CB6-5AD7D7980141", _clock.Current()),
                Mandalam.Create(Guid.Parse("596459A1-3669-4D7D-9B2A-59E640302CB2"), "ERANAD", Guid.Parse("16B81A9E-DCCA-49AE-8B28-12C2BB8A85FA", _clock.Current()),
                Mandalam.Create(Guid.Parse("19856442-23EA-4F70-987A-5ADD0C2A877F"), "KONDOTTY", Guid.Parse("16B81A9E-DCCA-49AE-8B28-12C2BB8A85FA", _clock.Current()),
                Mandalam.Create(Guid.Parse("F7371A52-F9D6-4BE1-9E7B-5FB6F48E06A0"), "SULTHANBATHERY", Guid.Parse("D8B0F759-79AD-4567-8BF8-3349D4C63EC3", _clock.Current()),
                Mandalam.Create(Guid.Parse("8393C08F-DE8B-4608-8182-62081145AA13"), "CHITTUR", Guid.Parse("E18E56D4-41F3-4874-9C59-790262041BF2", _clock.Current()),
                Mandalam.Create(Guid.Parse("1DA3763B-2CE6-4460-8F2F-631416C8B03A"), "ARUVIKKARA", Guid.Parse("666EB6D3-5977-43D3-8C2B-1124DD068988", _clock.Current()),
                Mandalam.Create(Guid.Parse("3A17B599-B8A2-415A-AA52-673A004526E7"), "KUNNAMKULAM", Guid.Parse("9D3B85D9-4F02-4AE7-A531-131D61E29F46", _clock.Current()),
                Mandalam.Create(Guid.Parse("E13AB25D-E18C-40EC-B721-674EB68965B6"), "MALAPPURAM", Guid.Parse("16B81A9E-DCCA-49AE-8B28-12C2BB8A85FA", _clock.Current()),
                Mandalam.Create(Guid.Parse("EA1B0313-D0A8-425B-BA67-6773F1ED08EB"), "PERINTHALMANNA", Guid.Parse("16B81A9E-DCCA-49AE-8B28-12C2BB8A85FA", _clock.Current()),
                Mandalam.Create(Guid.Parse("0978CE5C-4DCA-4606-BE5F-681E992AAE82"), "THALASSERY", Guid.Parse("A945211D-4D57-451B-9FEB-A4585138DF82", _clock.Current()),
                Mandalam.Create(Guid.Parse("A0359F27-B012-4116-BE8A-68A059793B45"), "CHANGANASSERY", Guid.Parse("9CFE0129-1FC0-40AB-B899-AB8BF4B6110D", _clock.Current()),
                Mandalam.Create(Guid.Parse("FFB0855C-78C0-4522-930A-6B1A473A9212"), "KOTTAYAM", Guid.Parse("9CFE0129-1FC0-40AB-B899-AB8BF4B6110D", _clock.Current()),
                Mandalam.Create(Guid.Parse("1CEA3125-AE45-4987-9721-6C8C7CEF35AB"), "KOCHI", Guid.Parse("8E8E52B9-8605-4537-ABEC-FA2A3492BE65", _clock.Current()),
                Mandalam.Create(Guid.Parse("6AF6ADC9-6C19-4B13-9F5F-723AD2D02B74"), "PIRAVOM", Guid.Parse("8E8E52B9-8605-4537-ABEC-FA2A3492BE65", _clock.Current()),
                Mandalam.Create(Guid.Parse("2992EBA3-8E08-4760-8A2A-755E133DF0A6"), "KUTHUPARAMBA", Guid.Parse("A945211D-4D57-451B-9FEB-A4585138DF82", _clock.Current()),
                Mandalam.Create(Guid.Parse("147A406B-D01B-45AF-8CF9-76CB68F28DD9"), "KATTAKKADA", Guid.Parse("666EB6D3-5977-43D3-8C2B-1124DD068988", _clock.Current()),
                Mandalam.Create(Guid.Parse("9A92F2E3-C959-49F3-9523-77F2E2E0C1A3"), "MANALUR", Guid.Parse("9D3B85D9-4F02-4AE7-A531-131D61E29F46", _clock.Current()),
                Mandalam.Create(Guid.Parse("C26AC7AE-FF82-43F9-B7D8-786C2469C229"), "NEDUMANGAD", Guid.Parse("666EB6D3-5977-43D3-8C2B-1124DD068988", _clock.Current()),
                Mandalam.Create(Guid.Parse("AF94A03C-AB90-4A81-B203-7C109B976C44"), "THRISSUR", Guid.Parse("9D3B85D9-4F02-4AE7-A531-131D61E29F46", _clock.Current()),
                Mandalam.Create(Guid.Parse("C9FCA197-BFB3-48AF-B3EC-7C15D886001F"), "PERAMBRA", Guid.Parse("E903D35E-6BFA-495D-A988-DA7C36EF072A", _clock.Current()),
                Mandalam.Create(Guid.Parse("78E412D9-0882-45FF-944F-7CA4D21B849B"), "POONJAR", Guid.Parse("9CFE0129-1FC0-40AB-B899-AB8BF4B6110D", _clock.Current()),
                Mandalam.Create(Guid.Parse("52B3926F-B506-4020-8DF9-7DE8195768A0"), "VADAKARA", Guid.Parse("E903D35E-6BFA-495D-A988-DA7C36EF072A", _clock.Current()),
                Mandalam.Create(Guid.Parse("5D9C162E-B84F-4289-ACD2-8234D3CA94AF"), "DEVIKULAM", Guid.Parse("B14BB553-D564-40F0-ACC8-6ECB27D7CFA0", _clock.Current()),
                Mandalam.Create(Guid.Parse("A9A6B1A3-158C-47CD-9C85-829F72926FB1"), "KASARAGOD", Guid.Parse("30BA1C7F-4BD7-43D9-9F62-07C377FEB667", _clock.Current()),
                Mandalam.Create(Guid.Parse("FBF42551-A856-4F9E-98E2-84F79F4CC78D"), "ARANMULA", Guid.Parse("D777BC44-BBA1-4BE2-8F00-3A447A6F2BFE", _clock.Current()),
                Mandalam.Create(Guid.Parse("091AF9C9-681C-422E-AAA2-85D68B6E5406"), "THRIKKAKARA", Guid.Parse("8E8E52B9-8605-4537-ABEC-FA2A3492BE65", _clock.Current()),
                Mandalam.Create(Guid.Parse("96B33B7D-D234-4F91-B2E9-888A3873DC8B"), "THRITHALA", Guid.Parse("E18E56D4-41F3-4874-9C59-790262041BF2", _clock.Current()),
                Mandalam.Create(Guid.Parse("239F2F5D-996A-4EF3-BCA9-8ADA456A8DFA"), "KOTTARAKKARA", Guid.Parse("42DDC8E9-D836-4C06-BE71-5E44E003BF0C", _clock.Current()),
                Mandalam.Create(Guid.Parse("A0C42EB2-1095-40B4-A42E-8AEB32DE9F13"), "UDMA", Guid.Parse("30BA1C7F-4BD7-43D9-9F62-07C377FEB667", _clock.Current()),
                Mandalam.Create(Guid.Parse("668CB2B6-A52C-45AD-8D9A-8B41D2E64310"), "ALATHUR", Guid.Parse("E18E56D4-41F3-4874-9C59-790262041BF2", _clock.Current()),
                Mandalam.Create(Guid.Parse("7E905DD0-8684-44E2-AF73-8E03F383E6AB"), "NADAPURAM", Guid.Parse("E903D35E-6BFA-495D-A988-DA7C36EF072A", _clock.Current()),
                Mandalam.Create(Guid.Parse("53933A70-2FFB-4CD2-82ED-902BE397286C"), "NEYYATTINKARA", Guid.Parse("666EB6D3-5977-43D3-8C2B-1124DD068988", _clock.Current()),
                Mandalam.Create(Guid.Parse("8061E945-3CE0-4E30-81AD-93AA9009380D"), "WANDOOR", Guid.Parse("16B81A9E-DCCA-49AE-8B28-12C2BB8A85FA", _clock.Current()),
                Mandalam.Create(Guid.Parse("264EFBF9-EA4A-4415-AD9A-93C6836FAD7B"), "VAMANAPURAM", Guid.Parse("666EB6D3-5977-43D3-8C2B-1124DD068988", _clock.Current()),
                Mandalam.Create(Guid.Parse("B5C8F817-AFD0-479E-9AB2-9796275F2F45"), "CHADAYAMANGALAM", Guid.Parse("42DDC8E9-D836-4C06-BE71-5E44E003BF0C", _clock.Current()),
                Mandalam.Create(Guid.Parse("A4F82B25-E382-454B-AD02-9B1BC024A915"), "MANANTHAVADY", Guid.Parse("D8B0F759-79AD-4567-8BF8-3349D4C63EC3", _clock.Current()),
                Mandalam.Create(Guid.Parse("D0B2147D-413C-4A17-A8B1-9CE7270D397D"), "KARUNAGAPPALLY", Guid.Parse("42DDC8E9-D836-4C06-BE71-5E44E003BF0C", _clock.Current()),
                Mandalam.Create(Guid.Parse("A2E87639-5A3C-4CB4-8F6A-9CF0BF76F4DB"), "PUTHUPPALLY", Guid.Parse("9CFE0129-1FC0-40AB-B899-AB8BF4B6110D", _clock.Current()),
                Mandalam.Create(Guid.Parse("CEFFD94A-B63F-4723-B4C0-9E3D56EC1E12"), "KAYAMKULAM", Guid.Parse("CF5E40D1-6F85-4540-9CB6-5AD7D7980141", _clock.Current()),
                Mandalam.Create(Guid.Parse("449560DD-4AB8-40A7-AE51-A0AC24A55B05"), "THIRUVAMBADY", Guid.Parse("E903D35E-6BFA-495D-A988-DA7C36EF072A", _clock.Current()),
                Mandalam.Create(Guid.Parse("8C8100C1-6A08-4D47-A5D6-A15C65C083FB"), "CHAVARA", Guid.Parse("42DDC8E9-D836-4C06-BE71-5E44E003BF0C", _clock.Current()),
                Mandalam.Create(Guid.Parse("0EFB46EB-1872-492D-BC92-A2976079DD8D"), "AROOR", Guid.Parse("CF5E40D1-6F85-4540-9CB6-5AD7D7980141", _clock.Current()),
                Mandalam.Create(Guid.Parse("A41B186B-D8F7-4C5B-8F91-A81FEBC8345A"), "KOLLAM", Guid.Parse("42DDC8E9-D836-4C06-BE71-5E44E003BF0C", _clock.Current()),
                Mandalam.Create(Guid.Parse("0CC0F6B5-89F4-437B-A449-A8F6069B40D8"), "ALAPPUZHA", Guid.Parse("CF5E40D1-6F85-4540-9CB6-5AD7D7980141", _clock.Current()),
                Mandalam.Create(Guid.Parse("E3DFDC74-0200-4D1C-8902-A9D567EE2479"), "KUNNATHUNAD", Guid.Parse("8E8E52B9-8605-4537-ABEC-FA2A3492BE65", _clock.Current()),
                Mandalam.Create(Guid.Parse("E7EA3998-90E1-4D30-A64A-AB4B60A5066C"), "PALA", Guid.Parse("9CFE0129-1FC0-40AB-B899-AB8BF4B6110D", _clock.Current()),
                Mandalam.Create(Guid.Parse("5BBC9E4E-5ECA-4131-8686-AC18EA6891F6"), "KALLIASSERI", Guid.Parse("A945211D-4D57-451B-9FEB-A4585138DF82", _clock.Current()),
                Mandalam.Create(Guid.Parse("F1203BFA-9A0B-4D3A-90F9-AFAA2D1C4296"), "KUNDARA", Guid.Parse("42DDC8E9-D836-4C06-BE71-5E44E003BF0C", _clock.Current()),
                Mandalam.Create(Guid.Parse("8954F3B9-957D-4577-9C81-B1F5E9D1AB7F"), "MUVATTUPUZHA", Guid.Parse("8E8E52B9-8605-4537-ABEC-FA2A3492BE65", _clock.Current()),
                Mandalam.Create(Guid.Parse("117150C1-AF46-42A0-901C-B3C0CDBA4E00"), "KODUVALLY", Guid.Parse("E903D35E-6BFA-495D-A988-DA7C36EF072A", _clock.Current()),
                Mandalam.Create(Guid.Parse("4C51035F-ECDB-49B8-B075-B653F14B1D54"), "CHENGANNUR", Guid.Parse("CF5E40D1-6F85-4540-9CB6-5AD7D7980141", _clock.Current()),
                Mandalam.Create(Guid.Parse("05B6D650-5174-43B2-A485-B676C729C2EC"), "KAZHAKKOOTTAM", Guid.Parse("666EB6D3-5977-43D3-8C2B-1124DD068988", _clock.Current()),
                Mandalam.Create(Guid.Parse("9AFDE89E-AC0D-4FDB-A21D-B7538149AEB1"), "ERAVIPURAM", Guid.Parse("42DDC8E9-D836-4C06-BE71-5E44E003BF0C", _clock.Current()),
                Mandalam.Create(Guid.Parse("B930A76B-7278-48B9-B579-B8648ADAA503"), "PATHANAPURAM", Guid.Parse("42DDC8E9-D836-4C06-BE71-5E44E003BF0C", _clock.Current()),
                Mandalam.Create(Guid.Parse("32708C79-B7D8-475D-964F-BBC57EC43FEF"), "KOTTAKKAL", Guid.Parse("16B81A9E-DCCA-49AE-8B28-12C2BB8A85FA", _clock.Current()),
                Mandalam.Create(Guid.Parse("4E86D61C-B8CC-49DB-857B-BF42F952CDB3"), "KANJIRAPPALLY", Guid.Parse("9CFE0129-1FC0-40AB-B899-AB8BF4B6110D", _clock.Current()),
                Mandalam.Create(Guid.Parse("32B53C9A-623E-410D-A8E3-BF8497168ABE"), "KUTTIADI", Guid.Parse("E903D35E-6BFA-495D-A988-DA7C36EF072A", _clock.Current()),
                Mandalam.Create(Guid.Parse("042B16FB-0509-4CCF-A93D-C000F567ECA0"), "ANGAMALY", Guid.Parse("8E8E52B9-8605-4537-ABEC-FA2A3492BE65", _clock.Current()),
                Mandalam.Create(Guid.Parse("D943840E-E1DA-49FC-A05F-C349DB2AED9D"), "GURUVAYOOR", Guid.Parse("9D3B85D9-4F02-4AE7-A531-131D61E29F46", _clock.Current()),
                Mandalam.Create(Guid.Parse("2D66A85A-103D-4D1A-914E-C396D7E69EFE"), "PATTAMBI", Guid.Parse("E18E56D4-41F3-4874-9C59-790262041BF2", _clock.Current()),
                Mandalam.Create(Guid.Parse("8994E7A9-CB95-4FA6-8403-C8F62387C0AC"), "SHORNUR", Guid.Parse("E18E56D4-41F3-4874-9C59-790262041BF2", _clock.Current()),
                Mandalam.Create(Guid.Parse("41DC37AA-8E58-40E5-9F0B-C949B17889CB"), "KANHANGAD", Guid.Parse("30BA1C7F-4BD7-43D9-9F62-07C377FEB667", _clock.Current()),
                Mandalam.Create(Guid.Parse("A10585F1-A3D3-403B-8243-CAE4964B1367"), "AMBALAPUZHA", Guid.Parse("CF5E40D1-6F85-4540-9CB6-5AD7D7980141", _clock.Current()),
                Mandalam.Create(Guid.Parse("836E1ED2-2CA2-432D-9345-CBF9AB6F74A6"), "ATTINGAL", Guid.Parse("666EB6D3-5977-43D3-8C2B-1124DD068988", _clock.Current()),
                Mandalam.Create(Guid.Parse("FADA7507-AF5B-4B44-AB76-CD11DB72D449"), "TRIKARIPUR", Guid.Parse("30BA1C7F-4BD7-43D9-9F62-07C377FEB667", _clock.Current()),
                Mandalam.Create(Guid.Parse("9527645E-EE5F-4547-B513-D0B593150B34"), "ETTUMANOOR", Guid.Parse("9CFE0129-1FC0-40AB-B899-AB8BF4B6110D", _clock.Current()),
                Mandalam.Create(Guid.Parse("FC2A2D45-703C-4D08-988D-D0E29C2536B8"), "MANJERI", Guid.Parse("16B81A9E-DCCA-49AE-8B28-12C2BB8A85FA", _clock.Current()),
                Mandalam.Create(Guid.Parse("9ED14B7E-AFF5-41CE-B4C9-D21086384D1C"), "PONNANI", Guid.Parse("16B81A9E-DCCA-49AE-8B28-12C2BB8A85FA", _clock.Current()),
                Mandalam.Create(Guid.Parse("901266E3-BB06-4DFA-AEFF-D72B00D0E1D4"), "MALAMPUZHA", Guid.Parse("E18E56D4-41F3-4874-9C59-790262041BF2", _clock.Current()),
                Mandalam.Create(Guid.Parse("961075CA-1499-4325-856B-D7FA5D309128"), "NILAMBUR", Guid.Parse("16B81A9E-DCCA-49AE-8B28-12C2BB8A85FA", _clock.Current()),
                Mandalam.Create(Guid.Parse("051151E8-8179-4FCA-B3D7-DD945FCF5EAB"), "KOZHIKODE NORTH", Guid.Parse("E903D35E-6BFA-495D-A988-DA7C36EF072A", _clock.Current()),
                Mandalam.Create(Guid.Parse("64C2F61D-409F-4FFF-B210-DF9583E04E56"), "PARAVUR", Guid.Parse("8E8E52B9-8605-4537-ABEC-FA2A3492BE65", _clock.Current()),
                Mandalam.Create(Guid.Parse("4813207D-A5C3-447D-A086-E006D057BD0C"), "KAIPAMANGALAM", Guid.Parse("9D3B85D9-4F02-4AE7-A531-131D61E29F46", _clock.Current()),
                Mandalam.Create(Guid.Parse("625DC4C8-CFD4-40DC-A0B6-E03F7E5AE02A"), "CHIRAYINKEEZHU", Guid.Parse("666EB6D3-5977-43D3-8C2B-1124DD068988", _clock.Current()),
                Mandalam.Create(Guid.Parse("D5DAE918-9EC6-4AC3-B3C8-E14899D32F86"), "RANNI", Guid.Parse("D777BC44-BBA1-4BE2-8F00-3A447A6F2BFE", _clock.Current()),
                Mandalam.Create(Guid.Parse("885AD600-D0DC-42BA-B26A-E2D27050EF42"), "ALUVA", Guid.Parse("8E8E52B9-8605-4537-ABEC-FA2A3492BE65", _clock.Current()),
                Mandalam.Create(Guid.Parse("7DE712FC-70B7-479D-8B85-E409E8E169F7"), "CHALAKKUDY", Guid.Parse("9D3B85D9-4F02-4AE7-A531-131D61E29F46", _clock.Current()),
                Mandalam.Create(Guid.Parse("356DBA13-D67F-46A1-80AE-E6F42F4B1735"), "KALAMASSERY", Guid.Parse("8E8E52B9-8605-4537-ABEC-FA2A3492BE65", _clock.Current()),
                Mandalam.Create(Guid.Parse("3E2F85A7-3D84-4CB9-A4C5-E7DABF18793B"), "QUILANDY", Guid.Parse("E903D35E-6BFA-495D-A988-DA7C36EF072A", _clock.Current()),
                Mandalam.Create(Guid.Parse("FAF78EB3-8072-4724-8FF1-E8D1806B5D3F"), "WADAKKANCHERY", Guid.Parse("9D3B85D9-4F02-4AE7-A531-131D61E29F46", _clock.Current()),
                Mandalam.Create(Guid.Parse("F8B6A2B9-F8CC-4912-9E31-EA459A47D442"), "BALUSSERI", Guid.Parse("E903D35E-6BFA-495D-A988-DA7C36EF072A", _clock.Current()),
                Mandalam.Create(Guid.Parse("DBB13401-2B42-4807-9CC8-EB0BEE5B7528"), "PERAVOOR", Guid.Parse("A945211D-4D57-451B-9FEB-A4585138DF82", _clock.Current()),
                Mandalam.Create(Guid.Parse("A0990F64-A8FC-4FFB-8E01-EE301C96309E"), "KONNI", Guid.Parse("D777BC44-BBA1-4BE2-8F00-3A447A6F2BFE", _clock.Current()),
                Mandalam.Create(Guid.Parse("6DD70747-1A2C-46AD-983A-EF0125993872"), "KOZHIKODE SOUTH", Guid.Parse("E903D35E-6BFA-495D-A988-DA7C36EF072A", _clock.Current()),
                Mandalam.Create(Guid.Parse("742ED83F-214D-4B37-8AFF-F0A29F03DE13"), "KODUNGALLUR", Guid.Parse("9D3B85D9-4F02-4AE7-A531-131D61E29F46", _clock.Current()),
                Mandalam.Create(Guid.Parse("87428F87-50BB-4C62-93DE-F7A153080CD5"), "MANJESHWAR", Guid.Parse("30BA1C7F-4BD7-43D9-9F62-07C377FEB667", _clock.Current()),
                Mandalam.Create(Guid.Parse("2983A6D5-E69E-4B89-B8F4-F7AC542D4831"), "PUNALUR", Guid.Parse("42DDC8E9-D836-4C06-BE71-5E44E003BF0C", _clock.Current()),
                Mandalam.Create(Guid.Parse("A036D8CB-A417-4C71-A69A-F9C077AF5FDE"), "HARIPAD", Guid.Parse("CF5E40D1-6F85-4540-9CB6-5AD7D7980141", _clock.Current()),
                Mandalam.Create(Guid.Parse("9845091B-CAA7-4B67-A60A-FAD8C25194F1"), "TIRUR", Guid.Parse("16B81A9E-DCCA-49AE-8B28-12C2BB8A85FA", _clock.Current())
            };

            await dbContext.Mandalams.AddRangeAsync(mandalams, cancellationToken);
        }

        if (await !dbContext.Professions.AnyAsync(cancellationToken))
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

        if (await !dbContext.Qualifications.AnyAsync(cancellationToken))
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
            }
            await dbContext.Qualifications.AddRangeAsync(qualifications, cancellationToken);
        }

        if (await !dbContext.Panchayaths.AnyAsync(cancellationToken))
        {
            var panchayaths = MandalamSeed.Get()
            await dbContext.Panchayaths.AddRangeAsync(panchayaths, cancellationToken);        
        }                    
        await dbContext.SaveChangesAsync(cancellationToken);    
    }    
        
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTa;
}