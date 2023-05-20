namespace AnimeReviewsData.Model
{
    public class AnimeAuthor
    {
        public int AnimeId { get; set; }
        public int AuthorId { get; set; }
        public Anime Anime { get; set; }
        public Author Author { get; set; }
    }
}