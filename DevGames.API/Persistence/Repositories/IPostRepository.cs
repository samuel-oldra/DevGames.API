using DevGames.API.Entities;

namespace DevGames.API.Persistence.Repositories
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetAllByBoard(int boardId);

        Post? GetById(int id);

        void Add(Post post);

        void AddComment(Comment comment);

        bool PostExists(int postId);
    }
}