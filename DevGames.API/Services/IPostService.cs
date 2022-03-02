using DevGames.API.Entities;

namespace DevGames.API.Services
{
    public interface IPostService
    {
        IEnumerable<Post> GetAllByBoard(int boardId);

        Post? GetById(int boardId, int id);

        Post Add(Post post);

        void AddComment(Comment comment);

        bool PostExists(int boardId, int id);
    }
}