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
    public class AnimeRepository : IAnimeRepository
    {
        private readonly AnimeReviewDbContext _db;

        public AnimeRepository(AnimeReviewDbContext db)
        {
            _db = db;
        }
        public bool AnimeExists(int animeId)
        {
            return _db.Anime.Any(a => a.Id == animeId);
        }

        public bool CreateAnime(int authorId, int categoryId, Anime anime)
        {
            var animeAuthorEntity = _db.Authors.Where(a => a.Id == authorId).FirstOrDefault();
            var category = _db.Categories.Where(a => a.Id == categoryId).FirstOrDefault();

            var animeAuthor = new AnimeAuthor()
            {
                Author = animeAuthorEntity,
                Anime = anime,
            };
            _db.Add(animeAuthor);

            var animeCategory = new AnimeCategory()
            {
                Category = category,
                Anime = anime,
            };
            _db.Add(animeCategory);
            _db.Add(anime);

            return Save();
        }

        public bool DeleteAnime(Anime anime)
        {
            _db.Remove(anime);
            return Save();
        }

        public Anime GetAnime(int id)
        {
            return _db.Anime.Where(a => a.Id == id).FirstOrDefault();
        }

        public Anime GetAnime(string name)
        {
            return _db.Anime.Where(a => a.Name == name).FirstOrDefault();
        }

        public decimal GetAnimeRating(int animeId)
        {
            var review = _db.Reviews.Where(a => a.Anime.Id == animeId);

            if ( review.Count() <= 0)
                return 0;

            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public List<Anime> GetAnimes()
        {
            return _db.Anime.OrderBy(a => a.Id).ToList();
        }

        public bool Save()
        {
            var saved = _db.SaveChanges();
            return saved > 0 ? true: false;
        }

        public bool UpdateAnime(int uthorId, int categoryId, Anime anime)
        {
            _db.Update(anime);
            return Save();
        }
    }
}
