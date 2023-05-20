using Microsoft.EntityFrameworkCore;
using AnimeReviewsData.Data;
using AnimeReviewsData.Model;
using Microsoft.AspNetCore.OpenApi;

namespace AnimeReviewsApi.Endpoints;

public static class AnimeEndpoints
{
    public static void MapAnimeEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Anime").WithTags(nameof(Anime));

        group.MapGet("/", async (AnimeReviewDbContext db) =>
        {
            return await db.Anime.ToListAsync();
        })
        .WithName("GetAllAnimes")
        .WithOpenApi()
        .Produces<List<Anime>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", async (int id, AnimeReviewDbContext db) =>
        {
            return await db.Anime.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Anime model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetAnimeById")
        .WithOpenApi()
        .Produces<Anime>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id}", async (int id, Anime anime, AnimeReviewDbContext db) =>
        {
            var affected = await db.Anime
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, anime.Id)
                  .SetProperty(m => m.Name, anime.Name)
                  .SetProperty(m => m.CreateDate, anime.CreateDate)
                );

            return affected == 1 ? Results.Ok() : Results.NotFound();
        })
        .WithName("UpdateAnime")
        .WithOpenApi()
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", async (Anime anime, AnimeReviewDbContext db) =>
        {
            db.Anime.Add(anime);
            await db.SaveChangesAsync();
            return Results.Created($"/api/Anime/{anime.Id}", anime);
        })
        .WithName("CreateAnime")
        .WithOpenApi()
        .Produces<Anime>(StatusCodes.Status201Created);

        group.MapDelete("/{id}", async (int id, AnimeReviewDbContext db) =>
        {
            var affected = await db.Anime
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();

            return affected == 1 ? Results.Ok() : Results.NotFound();
        })
        .WithName("DeleteAnime")
        .WithOpenApi()
        .Produces<Anime>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
