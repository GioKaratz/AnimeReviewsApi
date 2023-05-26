using AnimeReviewsData.Model;
using AnimeReviewsData.Contracts;
using AutoMapper;
using AnimeReviewsApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AnimeReviewsApi.Endpoints;

public static class AnimeEndpoints
{
    public static void MapAnimeEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Anime").WithTags(nameof(Anime));

        group.MapGet("/", (IAnimeRepository repo, IMapper mapper) =>
        {
            var animes = mapper.Map<List<AnimeDto>>(repo.GetAnimes());

            return animes;
        })
        .WithName("GetAllAnimes")
        .WithOpenApi()
        .Produces<List<AnimeDto>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", (int id, IAnimeRepository repo, IMapper mapper) =>
        {
            return repo.GetAnime(id)
                is Anime model
                    ? Results.Ok(mapper.Map<AnimeDto>(model))
                    : Results.NotFound();
        })
        .WithName("GetAnimeById")
        .WithOpenApi()
        .Produces<AnimeDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapGet("{animeId}/rating", (int id, IAnimeRepository repo, IMapper mapper) =>
        {
            if (!repo.AnimeExists(id))
                return Results.NotFound();

            var rating = repo.GetAnimeRating(id);

            return Results.Ok(rating);
        })
        .WithName("GetAnimeRating")
        .WithOpenApi()
        .Produces<Anime>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id}", (int id, [FromQuery] int authorId, [FromQuery] int catId, AnimeDto animeDto, IAnimeRepository repo, IMapper mapper) =>
        {
            if (animeDto == null)
                return Results.BadRequest();

            if (id != animeDto.Id)
                return Results.BadRequest();

            if (repo.AnimeExists(id))
                return Results.NotFound();

            var animeMap = mapper.Map<Anime>(animeDto);

            if (!repo.UpdateAnime(authorId, catId, animeMap))
                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            
            return Results.NoContent();
        })
        .WithName("UpdateAnime")
        .WithOpenApi()
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", (AnimeDto animeDto, [FromQuery] int authorId, [FromQuery] int catId, IAnimeRepository repo, IMapper mapper) =>
        {
            if (animeDto == null)
                return Results.BadRequest();

            var anime = repo.GetAnimes()
                .Where(a => a.Name.Trim().ToUpper() == animeDto.Name.Trim().ToUpper())
                .FirstOrDefault();

            if (anime != null)
            {
                return Results.StatusCode(StatusCodes.Status422UnprocessableEntity);
            }

            var animeMap = mapper.Map<Anime>(animeDto);

            if (!repo.CreateAnime(authorId, catId, animeMap))
                return Results.StatusCode(StatusCodes.Status500InternalServerError);

            return Results.NoContent();
        })
        .WithName("CreateAnime")
        .WithOpenApi()
        .Produces<Anime>(StatusCodes.Status201Created);

        group.MapDelete("/{id}", (int id, IAnimeRepository repo, IReviewRepository reviewRepo) =>
        {
            if (!repo.AnimeExists(id))
                return Results.NotFound();

            var reviewsToDelete = reviewRepo.GetReviewsOfAnime(id);
            var animeToDelete = repo.GetAnime(id);

            if(!reviewRepo.DeleteReviews(reviewsToDelete.ToList()))
                return Results.BadRequest();

            if(!repo.DeleteAnime(animeToDelete))
                return Results.NotFound();

            return Results.NoContent();
        })
        .WithName("DeleteAnime")
        .WithOpenApi()
        .Produces<Anime>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
