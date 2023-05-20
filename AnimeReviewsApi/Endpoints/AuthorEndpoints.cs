using Microsoft.EntityFrameworkCore;
using AnimeReviewsData.Data;
using AnimeReviewsData.Model;
using Microsoft.AspNetCore.OpenApi;

namespace AnimeReviewsApi.Endpoints;

public static class AuthorEndpoints
{
    public static void MapAuthorEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Author").WithTags(nameof(Author));

        group.MapGet("/", async (AnimeReviewDbContext db) =>
        {
            return await db.Authors.ToListAsync();
        })
        .WithName("GetAllAuthors")
        .WithOpenApi()
        .Produces<List<Author>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", async (int id, AnimeReviewDbContext db) =>
        {
            return await db.Authors.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Author model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetAuthorById")
        .WithOpenApi()
        .Produces<Author>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id}", async (int id, Author author, AnimeReviewDbContext db) =>
        {
            var affected = await db.Authors
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, author.Id)
                  .SetProperty(m => m.FirstName, author.FirstName)
                  .SetProperty(m => m.LastName, author.LastName)
                  .SetProperty(m => m.BirthDate, author.BirthDate)
                );

            return affected == 1 ? Results.Ok() : Results.NotFound();
        })
        .WithName("UpdateAuthor")
        .WithOpenApi()
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", async (Author author, AnimeReviewDbContext db) =>
        {
            db.Authors.Add(author);
            await db.SaveChangesAsync();
            return Results.Created($"/api/Author/{author.Id}", author);
        })
        .WithName("CreateAuthor")
        .WithOpenApi()
        .Produces<Author>(StatusCodes.Status201Created);

        group.MapDelete("/{id}", async (int id, AnimeReviewDbContext db) =>
        {
            var affected = await db.Authors
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();

            return affected == 1 ? Results.Ok() : Results.NotFound();
        })
        .WithName("DeleteAuthor")
        .WithOpenApi()
        .Produces<Author>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
