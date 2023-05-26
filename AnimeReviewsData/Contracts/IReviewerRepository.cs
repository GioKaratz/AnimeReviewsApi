using AnimeReviewsData.Model;

namespace AnimeReviewsData.Contracts
{
    public interface IReviewerRepository
    {
        List<Reviewer> GetReviewers();
        Reviewer GetReviewer(int reviewerId);
        List<Review> GetReviewsByReviewer(int reviewerId);
        bool ReviewerExists(int reviewerId);
        bool CreateReviewer(Reviewer reviewer);
        bool UpdateReviewer(Reviewer reviewer);
        bool DeleteReviewer(Reviewer reviewer);
        bool Save();
    }
}
