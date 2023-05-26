using Microsoft.EntityFrameworkCore;
using AnimeReviewsData.Data;
using AnimeReviewsData.Model;
using Microsoft.AspNetCore.OpenApi;
using AnimeReviewsData.Contracts;
using AutoMapper;
using AnimeReviewsApi.DTOs;

namespace AnimeReviewsApi.Endpoints;

public static class ReviewerEndpoints
{
    public static void MapReviewerEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Reviewer").WithTags(nameof(Reviewer));

        group.MapGet("/", (IReviewerRepository repo, IMapper mapper) =>
        {
            var reviewers = mapper.Map<List<ReviewerDto>>(repo.GetReviewers());

            return Results.Ok(reviewers);
        })
        .WithName("GetAllReviewers")
        .WithOpenApi()
        .Produces<List<ReviewerDto>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", (int id, IReviewerRepository repo, IMapper mapper) =>
        {
            if (!repo.ReviewerExists(id))
                return Results.NotFound();

            var reviewer = mapper.Map<ReviewerDto>(repo.GetReviewer(id));

            return Results.Ok(reviewer);
        })
        .WithName("GetReviewerById")
        .WithOpenApi()
        .Produces<ReviewerDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapGet("{reviewerId}/reviews", (int id, IReviewerRepository repo, IMapper mapper) =>
        {
            if (!repo.ReviewerExists(id))
                return Results.NotFound();

            var reviews = mapper.Map<List<ReviewDto>>(repo.GetReviewsByReviewer(id));

            return Results.Ok(reviews);
        })
        .WithName("GetReviewsByReviewer")
        .WithOpenApi()
        .Produces<ReviewDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id}", (int id, ReviewerDto reviewerDto, IReviewerRepository repo, IMapper mapper) =>
        {
            if (reviewerDto == null)
                return Results.BadRequest();

            if (id != reviewerDto.Id) 
                return Results.BadRequest();

            if (!repo.ReviewerExists(id))
                return Results.NotFound();

            var reviewerMap = mapper.Map<Reviewer>(reviewerDto);

            if (!repo.UpdateReviewer(reviewerMap))
                return Results.StatusCode(StatusCodes.Status500InternalServerError);

            return Results.NoContent();
        })
        .WithName("UpdateReviewer")
        .WithOpenApi()
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", (ReviewerDto reviewerDto, IReviewerRepository repo, IMapper mapper) =>
        {
            if  (reviewerDto == null)
                return Results.BadRequest();

            var reviewers = repo.GetReviewers()
                .Where(r => r.LastName.Trim().ToUpper() == reviewerDto.LastName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (reviewers != null)
                return Results.StatusCode(StatusCodes.Status422UnprocessableEntity);

            var reviewerMap = mapper.Map<Reviewer>(reviewerDto);

            if (!repo.CreateReviewer(reviewerMap))
                return Results.StatusCode(StatusCodes.Status500InternalServerError);

            return Results.Ok("Successfully created");
        })
        .WithName("CreateReviewer")
        .WithOpenApi()
        .Produces<Reviewer>(StatusCodes.Status201Created);

        group.MapDelete("/{id}", (int id, IReviewerRepository repo) =>
        {
            if (!repo.ReviewerExists(id))
                return Results.NotFound();

            var reviewerToDelete = repo.GetReviewer(id);

            if (!repo.DeleteReviewer(reviewerToDelete))
                return Results.NotFound();

            return Results.NoContent();
        })
        .WithName("DeleteReviewer")
        .WithOpenApi()
        .Produces<Reviewer>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
