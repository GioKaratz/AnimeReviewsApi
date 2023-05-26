using AnimeReviewsData.Contracts;
using AnimeReviewsData.Data;
using AnimeReviewsData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeReviewsData.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AnimeReviewDbContext _db;

        public ReviewRepository(AnimeReviewDbContext db)
        {
            _db = db;
        }
        public bool CreateReview(Review review)
        {
            _db.Add(review);
            return Save();
        }

        public bool DeleteReview(Review review)
        {
            _db.Remove(review);
            return Save();
        }

        public bool DeleteReviews(List<Review> reviews)
        {
            _db.RemoveRange(reviews);
            return Save();
        }

        public Review GetReview(int reviewId)
        {
            return _db.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();
        }

        public List<Review> GetReviews()
        {
            return _db.Reviews.ToList();
        }

        public List<Review> GetReviewsOfAnime(int animeId)
        {
            return _db.Reviews.Where(r => r.Anime.Id == animeId).ToList();
        }

        public bool ReviewExists(int reviewId)
        {
            return _db.Reviews.Any(r => r.Id == reviewId);
        }

        public bool Save()
        {
            var save = _db.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool UpdateReview(Review review)
        {
            _db.Update(review);
            return Save();
        }
    }
}
