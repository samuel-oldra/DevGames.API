namespace DevGames.API.Entities
{
    public class Post
    {
        public Post(int id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;

            CreatedAt = DateTime.Now;
            Comments = new List<Comment>();
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public List<Comment> Comments { get; private set; }

        public void AddCommet(Comment comment)
        {
            Comments.Add(comment);
        }
    }
}