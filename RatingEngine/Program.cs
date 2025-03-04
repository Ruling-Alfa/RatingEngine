using FastEndpoints;
using Scalar.AspNetCore;
using RatingEngine.DomainServices.Configurations;
using Microsoft.EntityFrameworkCore;
using RatingEngine.Configurations;
using System.Globalization;
using Infra.Configurations;

namespace RatingEngine;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateSlimBuilder(args);

        builder.Services.AddFastEndpoints();
        builder.AddServiceDefaults();
        builder.Services.AddOpenApi();
        builder.Services.AddOpenTelemetry();
        //builder.Services.AddScalarApiReference();
        builder.Services.AddDomainLayerLayer(builder.Configuration);
        var app = builder.Build();

        app.UseInfraLayer();

        if (!app.Environment.IsProduction())
        {
            app.MapOpenApi();
            app.MapScalarApiReference(_ => _.Servers = []);
        }

        await app.EnableMigrationTirgger();
        app.UseFastEndpoints();
        await app.RunAsync();
    }
}