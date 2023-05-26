using AnimeReviewsData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeReviewsData.Contracts
{
    public interface IAnimeRepository
    {
        List<Anime> GetAnimes();
        Anime GetAnime(int id);
        Anime GetAnime(string name);
        decimal GetAnimeRating(int animeId);
        bool AnimeExists(int animeId);
        bool CreateAnime(int authorId, int categoryId, Anime anime);
        bool UpdateAnime(int uthorId, int categoryId, Anime anime);
        bool DeleteAnime(Anime anime);
        bool Save();
    }
}
