using RatingEngine.AppHost.Configurations;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.RatingEngine>("ratingengine")
    .WithScalar();

builder.Build().Run();
