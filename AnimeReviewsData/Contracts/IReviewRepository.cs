using AnimeReviewsData.Model;

namespace AnimeReviewsData.Contracts
{
    public interface IReviewRepository
    {
        List<Review> GetReviews();
        Review GetReview(int reviewId);
        List<Review> GetReviewsOfAnime(int animeId);
        bool ReviewExists(int reviewId);
        bool CreateReview(Review review);
        bool UpdateReview(Review review);
        bool DeleteReview(Review review);
        bool DeleteReviews(List<Review> reviews);
        bool Save();
    }
}
