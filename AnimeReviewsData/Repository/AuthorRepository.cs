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
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AnimeReviewDbContext _db;

        public AuthorRepository(AnimeReviewDbContext db)
        {
            _db = db;
        }
        public bool AuthorExists(int authorId)
        {
            return _db.Authors.Any(a => a.Id == authorId);
        }

        public bool CreateAuthor(Author author)
        {
            _db.Authors.Add(author);
            return Save();
        }

        public bool DeleteAuthor(Author author)
        {
            _db.Remove(author);
            return Save();
        }

        public List<Anime> GetAnimeByAuthor(int authorId)
        {
            return _db.AnimeAuthors.Where(au => au.Author.Id == authorId).Select(a => a.Anime).ToList();
        }

        public Author GetAuthor(int authorId)
        {
            return _db.Authors.Where(au => au.Id == authorId).FirstOrDefault();
        }

        public Author GetAuthorOfAnAnime(int animeId)
        {
            return _db.AnimeAuthors.Where(a => a.AnimeId == animeId).Select(au => au.Author).FirstOrDefault();
        }

        public List<Author> GetAuthors()
        {
            return _db.Authors.ToList();
        }

        public bool Save()
        {
            var save = _db.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool UpdateAuthor(Author author)
        {
            _db.Update(author);
            return Save();
        }
    }
}
