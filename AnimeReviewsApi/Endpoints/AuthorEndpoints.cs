using AnimeReviewsData.Model;
using AnimeReviewsData.Contracts;
using AutoMapper;
using AnimeReviewsApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AnimeReviewsApi.Endpoints;

public static class AuthorEndpoints
{
    public static void MapAuthorEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Author").WithTags(nameof(Author));

        group.MapGet("/", (IAuthorRepository repo, IMapper mapper) =>
        {
            var authors = mapper.Map<List<AuthorDto>>(repo.GetAuthors());
            return authors;
        })
        .WithName("GetAllAuthors")
        .WithOpenApi()
        .Produces<List<AuthorDto>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", (int id, IAuthorRepository repo, IMapper mapper) =>
        {
            if (!repo.AuthorExists(id))
                return Results.NotFound();

            var author = mapper.Map<AuthorDto>(repo.GetAuthor(id));

            return Results.Ok(author);
        })
        .WithName("GetAuthorById")
        .WithOpenApi()
        .Produces<AuthorDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapGet("{authorId}/anime", (int id, IAuthorRepository repo, IMapper mapper) =>
        {
            if (!repo.AuthorExists(id))
                return Results.NotFound();

            var animes = mapper.Map<List<AnimeDto>>(repo.GetAnimeByAuthor(id));

            return Results.Ok(animes);
        })
        .WithName("GetAnimeByAuthor")
        .WithOpenApi()
        .Produces<AuthorDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id}", (int id, AuthorDto authorDto, IAuthorRepository repo, IMapper mapper) =>
        {
            if (authorDto == null)
                return Results.BadRequest();

            if (id != authorDto.Id)
                return Results.BadRequest();

            if (!repo.AuthorExists(id))
                return Results.BadRequest();

            var authorMap = mapper.Map<Author>(authorDto);

            if(!repo.UpdateAuthor(authorMap))
                return Results.StatusCode(StatusCodes.Status500InternalServerError);

            return Results.NoContent();
            
        })
        .WithName("UpdateAuthor")
        .WithOpenApi()
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", (AuthorDto authorDto, IAuthorRepository repo, IMapper mapper) =>
        {
            if (authorDto == null)
                return Results.BadRequest();

            var author = repo.GetAuthors()
                .Where(au => au.LastName.Trim().ToUpper() == authorDto.LastName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (author != null)
                return Results.StatusCode(StatusCodes.Status422UnprocessableEntity);

            var authorMap = mapper.Map<Author>(authorDto);

            if (!repo.CreateAuthor(authorMap))
                return Results.BadRequest();

            return Results.NoContent();
        })
        .WithName("CreateAuthor")
        .WithOpenApi()
        .Produces<Author>(StatusCodes.Status201Created);

        group.MapDelete("/{id}", (int id, IAuthorRepository repo) =>
        {
            if (!repo.AuthorExists(id))
                return Results.NotFound();

            var authorToDelete = repo.GetAuthor(id);

            if (!repo.DeleteAuthor(authorToDelete))
                return Results.BadRequest();

            return Results.NoContent();
        })
        .WithName("DeleteAuthor")
        .WithOpenApi()
        .Produces<Author>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
