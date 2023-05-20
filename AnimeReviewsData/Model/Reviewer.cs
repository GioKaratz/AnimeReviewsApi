namespace AnimeReviewsData.Model
{
    public class Reviewer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Review> Reviews { get; set; }
    }
}