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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AnimeReviewDbContext _db;

        public CategoryRepository(AnimeReviewDbContext db)
        {
            _db = db;
        }
        public bool CategoryExists(int categoryId)
        {
            return _db.Categories.Any(c => c.Id == categoryId);
        }

        public bool CreateCategory(Category category)
        {
            _db.Add(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _db.Remove(category);
            return Save();
        }

        public List<Anime> GetAnimeByCategory(int categoryId)
        {
            return _db.AnimeCategories.Where(ac => ac.Category.Id == categoryId).Select(a => a.Anime).ToList();
        }

        public List<Category> GetCategories()
        {
            return _db.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return _db.Categories.Where(c => c.Id == id).FirstOrDefault();
        }

        public bool Save()
        {
            var save = _db.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
            _db.Update(category);
            return Save();
        }
    }
}
