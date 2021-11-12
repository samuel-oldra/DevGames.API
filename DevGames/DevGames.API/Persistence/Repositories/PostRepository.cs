using DevGames.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevGames.API.Persistence.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DevGamesContext context;

        public PostRepository(DevGamesContext context) => this.context = context;

        public IEnumerable<Post> GetAllByBoard(int boardId) =>
            context.Posts.Where(p => p.BoardId == boardId);

        public Post GetById(int id) =>
            context.Posts.Include(p => p.Comments).SingleOrDefault(p => p.Id == id);

        public void Add(Post post)
        {
            context.Posts.Add(post);
            context.SaveChanges();
        }

        public void AddComment(Comment comment)
        {
            context.Comments.Add(comment);
            context.SaveChanges();
        }

        public bool PostExists(int postId) =>
            context.Posts.Any(p => p.Id == postId);
    }
}