using AnimeReviewsData.Contracts;
using AnimeReviewsData.Data;
using AnimeReviewsData.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeReviewsData.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly AnimeReviewDbContext _db;

        public ReviewerRepository(AnimeReviewDbContext db)
        {
            _db = db;
        }
        public bool CreateReviewer(Reviewer reviewer)
        {
            _db.Add(reviewer);
            return Save();
        }

        public bool DeleteReviewer(Reviewer reviewer)
        {
            _db.Remove(reviewer);
            return Save();
        }

        public Reviewer GetReviewer(int reviewerId)
        {
            return _db.Reviewers.Where(r => r.Id == reviewerId).Include(q => q.Reviews).FirstOrDefault();
        }

        public List<Reviewer> GetReviewers()
        {
            return _db.Reviewers.ToList();
        }

        public List<Review> GetReviewsByReviewer(int reviewerId)
        {
            return _db.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList();
        }

        public bool ReviewerExists(int reviewerId)
        {
            return _db.Reviewers.Any(r => r.Id == reviewerId);
        }

        public bool Save()
        {
            var save = _db.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool UpdateReviewer(Reviewer reviewer)
        {
            _db.Update(reviewer);
            return Save();
        }
    }
}
