using AnimeReviewsData.Model;
using AnimeReviewsData.Contracts;
using AutoMapper;
using AnimeReviewsApi.DTOs;

namespace AnimeReviewsApi.Endpoints;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Category").WithTags(nameof(Category));

        group.MapGet("/", (ICategoryRepository repo, IMapper mapper) =>
        {
            var categories = mapper.Map<List<CategoryDto>>(repo.GetCategories());
            return Results.Ok(categories);
        })
        .WithName("GetAllCategorys")
        .WithOpenApi()
        .Produces<List<CategoryDto>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", (int id, ICategoryRepository repo, IMapper mapper) =>
        {
            if (!repo.CategoryExists(id))
                return Results.NotFound();

            var category = mapper.Map<CategoryDto>(repo.GetCategory(id));

            return Results.Ok(category);
        })
        .WithName("GetCategoryById")
        .WithOpenApi()
        .Produces<CategoryDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapGet("/{categoryId}/anime", (int id, ICategoryRepository repo, IMapper mapper) =>
        {
            var animes = mapper.Map<List<AnimeDto>>(repo.GetAnimeByCategory(id));

            return Results.Ok(animes);
        })
        .WithName("GetAnimeByCategoryById")
        .WithOpenApi()
        .Produces<Anime>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id}", (int id, CategoryDto categoryDto, ICategoryRepository repo, IMapper mapper) =>
        {
            if (categoryDto == null)
                return Results.BadRequest();

            if (id != categoryDto.Id)
                return Results.BadRequest();

            if (!repo.CategoryExists(id))
                return Results.NotFound();

            var categoryMap = mapper.Map<Category>(categoryDto);

            if (!repo.UpdateCategory(categoryMap))
                return Results.StatusCode(StatusCodes.Status500InternalServerError);

            return Results.NoContent();
        })
        .WithName("UpdateCategory")
        .WithOpenApi()
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);


        group.MapPost("/", (CategoryDto categoryDto, ICategoryRepository repo, IMapper mapper) =>
        {
            if (categoryDto == null)
                return Results.BadRequest();

            var category = repo.GetCategories()
                .Where(c => c.Name.Trim().ToUpper() == categoryDto.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (category != null)
                return Results.StatusCode(StatusCodes.Status422UnprocessableEntity);

            var categoryMap = mapper.Map<Category>(categoryDto);

            if (!repo.CreateCategory(categoryMap))
                return Results.StatusCode(StatusCodes.Status500InternalServerError);

            return Results.NoContent();

        })
        .WithName("CreateCategory")
        .WithOpenApi()
        .Produces<CategoryDto>(StatusCodes.Status201Created);

        group.MapDelete("/{id}", async (int id, ICategoryRepository repo, IMapper mapper) =>
        {
            if (!repo.CategoryExists(id))
                return Results.NotFound();

            var categoryToDelete = repo.GetCategory(id);

            if (!repo.DeleteCategory(categoryToDelete))
                return Results.BadRequest();

            return Results.NoContent();
        })
        .WithName("DeleteCategory")
        .WithOpenApi()
        .Produces<Category>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
