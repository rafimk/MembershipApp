using Membership.Application;
using Membership.Core;
using Membership.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "localhost:3000";

builder.Services
    .AddCors(options =>
    {
        options.AddPolicy(name: MyAllowSpecificOrigins,
            policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
    })
    .AddCore()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Host.UseSerilog((context, loggerConfiguration) =>
{
    loggerConfiguration.WriteTo
        .Console();
    // .WriteTo
    // .File("logs.txt")
    // .WriteTo
    // .Seq("http://localhost:5341");
});

builder.Services.AddHealthChecks();

var app = builder.Build();
app.MapHealthChecks("/health");
app.UseCors(MyAllowSpecificOrigins);
app.UseInfrastructure();
app.MapGet("/api", (IOptions<AppOptions> options) => Results.Ok(options.Value.Name));
//app.UseUsersApi();
app.Run();