namespace AnimeReviewsData.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AnimeCategory> AnimeCategories { get; set; }
    }
}