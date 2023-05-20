using Microsoft.EntityFrameworkCore;
using AnimeReviewsData.Data;
using AnimeReviewsData.Model;
using Microsoft.AspNetCore.OpenApi;

namespace AnimeReviewsApi.Endpoints;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Category").WithTags(nameof(Category));

        group.MapGet("/", async (AnimeReviewDbContext db) =>
        {
            return await db.Categories.ToListAsync();
        })
        .WithName("GetAllCategorys")
        .WithOpenApi()
        .Produces<List<Category>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", async (int id, AnimeReviewDbContext db) =>
        {
            return await db.Categories.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Category model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetCategoryById")
        .WithOpenApi()
        .Produces<Category>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id}", async (int id, Category category, AnimeReviewDbContext db) =>
        {
            var affected = await db.Categories
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, category.Id)
                  .SetProperty(m => m.Name, category.Name)
                );

            return affected == 1 ? Results.Ok() : Results.NotFound();
        })
        .WithName("UpdateCategory")
        .WithOpenApi()
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", async (Category category, AnimeReviewDbContext db) =>
        {
            db.Categories.Add(category);
            await db.SaveChangesAsync();
            return Results.Created($"/api/Category/{category.Id}", category);
        })
        .WithName("CreateCategory")
        .WithOpenApi()
        .Produces<Category>(StatusCodes.Status201Created);

        group.MapDelete("/{id}", async (int id, AnimeReviewDbContext db) =>
        {
            var affected = await db.Categories
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();

            return affected == 1 ? Results.Ok() : Results.NotFound();
        })
        .WithName("DeleteCategory")
        .WithOpenApi()
        .Produces<Category>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
