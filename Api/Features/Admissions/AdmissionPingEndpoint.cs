namespace Api.Features.Admissions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

public static class AdmissionPingEndpoint
{
    public static void MapAdmissionPingEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/admissions");

        group.MapGet("/ping", Ping)
            .WithName("PingAdmission")
            .WithSummary("Health check for admissions endpoints");
    }

    private static IResult Ping()
    {
        return Results.Ok(new { Message = "Pong", TimestampUtc = DateTime.UtcNow });
    }
}
