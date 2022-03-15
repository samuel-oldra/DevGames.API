namespace DevGames.API.Entities
{
    public class Post
    {
        public int Id { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public int BoardId { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public List<Comment> Comments { get; private set; }

        public Post(string title, string description, int boardId)
        {
            Title = title;
            Description = description;
            BoardId = boardId;

            CreatedAt = DateTime.Now;
            Comments = new List<Comment>();
        }

        public void AddCommet(Comment comment)
            => Comments.Add(comment);
    }
}