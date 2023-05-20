namespace AnimeReviewsData.Model
{
    public class Anime
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public List<Review> Reviews { get; set; }
        public List<AnimeAuthor> AnimeAuthors { get; set; }
        public List<AnimeCategory> AnimeCategories { get; set; }
    }
}