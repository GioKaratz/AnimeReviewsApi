using Microsoft.EntityFrameworkCore;
using AnimeReviewsData.Data;
using AnimeReviewsData.Model;
using Microsoft.AspNetCore.OpenApi;
using AnimeReviewsData.Contracts;
using AutoMapper;
using AnimeReviewsApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AnimeReviewsApi.Endpoints;

public static class ReviewEndpoints
{
    public static void MapReviewEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Review").WithTags(nameof(Review));

        group.MapGet("/", (IReviewRepository repo, IMapper mapper) =>
        {
            var reviews = mapper.Map<List<ReviewDto>>(repo.GetReviews());
            return Results.Ok(reviews);
        })
        .WithName("GetAllReviews")
        .WithOpenApi()
        .Produces<List<ReviewDto>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", (int id, IReviewRepository repo, IMapper mapper) =>
        {
            if (!repo.ReviewExists(id))
                return Results.NotFound();

            var review = mapper.Map<ReviewDto>(repo.GetReview(id));
            
            return Results.Ok(review);
        })
        .WithName("GetReviewById")
        .WithOpenApi()
        .Produces<ReviewDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapGet("anime/{animeid}", (int id, [FromQuery] int animeId, IReviewRepository repo, IMapper mapper) =>
        {
            var reviews = mapper.Map<List<ReviewDto>>(repo.GetReviewsOfAnime(animeId));

            return Results.Ok(reviews);
        })
        .WithName("GetReviewOfAnime")
        .WithOpenApi()
        .Produces<ReviewDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id}", (int id, ReviewDto reviewDto, IReviewRepository repo, IMapper mapper) =>
        {
            if ( reviewDto == null)
                return Results.BadRequest();

            if (id != reviewDto.Id)
                return Results.BadRequest();

            if (!repo.ReviewExists(id))
                return Results.BadRequest();

            var reviewMap = mapper.Map<Review>(reviewDto);

            if (!repo.UpdateReview(reviewMap))
                return Results.StatusCode(StatusCodes.Status500InternalServerError);

            return Results.NoContent();
        })
        .WithName("UpdateReview")
        .WithOpenApi()
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", (ReviewDto reviewDto, [FromQuery] int animeId, [FromQuery] int reviewerId, 
            IReviewRepository repo, IMapper mapper, IAnimeRepository animeRepo, IReviewerRepository reviewerRepo) =>
        {
            if (reviewDto == null)
                return Results.BadRequest();

            var reviews = repo.GetReviews()
                .Where(r => r.Title.Trim().ToUpper() == reviewDto.Title.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (reviews != null)
                return Results.StatusCode(StatusCodes.Status422UnprocessableEntity);

            var reviewMap = mapper.Map<Review>(reviewDto);
            reviewMap.Anime = animeRepo.GetAnime(animeId);
            reviewMap.Reviewer = reviewerRepo.GetReviewer(reviewerId);

            if (!repo.CreateReview(reviewMap))
                return Results.StatusCode(StatusCodes.Status500InternalServerError);

            return Results.Ok("Successfully created");
        })
        .WithName("CreateReview")
        .WithOpenApi()
        .Produces<ReviewDto>(StatusCodes.Status201Created);

        group.MapDelete("/{id}", (int id, IReviewRepository repo) =>
        {
            if (!repo.ReviewExists(id))
                return Results.NotFound();

            var reviewToDelete = repo.GetReview(id);

            if (!repo.DeleteReview(reviewToDelete))
                return Results.NotFound();

            return Results.NoContent();
        })
        .WithName("DeleteReview")
        .WithOpenApi()
        .Produces<Review>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
