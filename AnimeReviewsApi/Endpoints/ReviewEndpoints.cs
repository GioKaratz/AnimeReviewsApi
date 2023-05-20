using Microsoft.EntityFrameworkCore;
using AnimeReviewsData.Data;
using AnimeReviewsData.Model;
using Microsoft.AspNetCore.OpenApi;

namespace AnimeReviewsApi.Endpoints;

public static class ReviewEndpoints
{
    public static void MapReviewEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Review").WithTags(nameof(Review));

        group.MapGet("/", async (AnimeReviewDbContext db) =>
        {
            return await db.Reviews.ToListAsync();
        })
        .WithName("GetAllReviews")
        .WithOpenApi()
        .Produces<List<Review>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", async (int id, AnimeReviewDbContext db) =>
        {
            return await db.Reviews.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Review model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetReviewById")
        .WithOpenApi()
        .Produces<Review>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id}", async (int id, Review review, AnimeReviewDbContext db) =>
        {
            var affected = await db.Reviews
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, review.Id)
                  .SetProperty(m => m.Title, review.Title)
                  .SetProperty(m => m.Text, review.Text)
                  .SetProperty(m => m.Rating, review.Rating)
                );

            return affected == 1 ? Results.Ok() : Results.NotFound();
        })
        .WithName("UpdateReview")
        .WithOpenApi()
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", async (Review review, AnimeReviewDbContext db) =>
        {
            db.Reviews.Add(review);
            await db.SaveChangesAsync();
            return Results.Created($"/api/Review/{review.Id}", review);
        })
        .WithName("CreateReview")
        .WithOpenApi()
        .Produces<Review>(StatusCodes.Status201Created);

        group.MapDelete("/{id}", async (int id, AnimeReviewDbContext db) =>
        {
            var affected = await db.Reviews
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();

            return affected == 1 ? Results.Ok() : Results.NotFound();
        })
        .WithName("DeleteReview")
        .WithOpenApi()
        .Produces<Review>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
