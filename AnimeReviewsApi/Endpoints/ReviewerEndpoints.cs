using Microsoft.EntityFrameworkCore;
using AnimeReviewsData.Data;
using AnimeReviewsData.Model;
using Microsoft.AspNetCore.OpenApi;

namespace AnimeReviewsApi.Endpoints;

public static class ReviewerEndpoints
{
    public static void MapReviewerEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Reviewer").WithTags(nameof(Reviewer));

        group.MapGet("/", async (AnimeReviewDbContext db) =>
        {
            return await db.Reviewers.ToListAsync();
        })
        .WithName("GetAllReviewers")
        .WithOpenApi()
        .Produces<List<Reviewer>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", async (int id, AnimeReviewDbContext db) =>
        {
            return await db.Reviewers.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Reviewer model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetReviewerById")
        .WithOpenApi()
        .Produces<Reviewer>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id}", async (int id, Reviewer reviewer, AnimeReviewDbContext db) =>
        {
            var affected = await db.Reviewers
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, reviewer.Id)
                  .SetProperty(m => m.FirstName, reviewer.FirstName)
                  .SetProperty(m => m.LastName, reviewer.LastName)
                );

            return affected == 1 ? Results.Ok() : Results.NotFound();
        })
        .WithName("UpdateReviewer")
        .WithOpenApi()
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", async (Reviewer reviewer, AnimeReviewDbContext db) =>
        {
            db.Reviewers.Add(reviewer);
            await db.SaveChangesAsync();
            return Results.Created($"/api/Reviewer/{reviewer.Id}", reviewer);
        })
        .WithName("CreateReviewer")
        .WithOpenApi()
        .Produces<Reviewer>(StatusCodes.Status201Created);

        group.MapDelete("/{id}", async (int id, AnimeReviewDbContext db) =>
        {
            var affected = await db.Reviewers
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();

            return affected == 1 ? Results.Ok() : Results.NotFound();
        })
        .WithName("DeleteReviewer")
        .WithOpenApi()
        .Produces<Reviewer>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
