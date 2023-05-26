using AnimeReviewsData.Model;

namespace AnimeReviewsData.Contracts
{
    public interface IAuthorRepository
    {
        List<Author> GetAuthors();
        Author GetAuthor(int authorId);
        Author GetAuthorOfAnAnime(int animeId);
        List<Anime> GetAnimeByAuthor(int authorId);
        bool AuthorExists(int authorId);
        bool CreateAuthor(Author author);
        bool UpdateAuthor(Author author);
        bool DeleteAuthor(Author author);
        bool Save();
    }
}
