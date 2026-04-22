using TunnelBlockCalculator;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Render and most PaaS providers expose the public port through the PORT
// environment variable. Bind to it when present, otherwise default to 8080.
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://0.0.0.0:{port}");

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors();

// POST /api/volume — accepts the three dimensions of the tunnel block
// (length, width, wallHeight) and returns the computed volume along with
// a breakdown of the rectangular base and the semi-cylindrical roof.
app.MapPost("/api/volume", (TunnelBlockRequest request) =>
{
    if (request.Length <= 0 || request.Width <= 0 || request.WallHeight < 0)
    {
        return Results.BadRequest(new
        {
            error = "Length and width must be greater than zero, and wall height cannot be negative."
        });
    }

    var result = VolumeCalculator.Compute(request.Length, request.Width, request.WallHeight);
    return Results.Ok(result);
});

// Health endpoint used by Render / Docker health checks.
app.MapGet("/api/health", () => Results.Ok(new { status = "ok" }));

app.Run();

public record TunnelBlockRequest(double Length, double Width, double WallHeight);
