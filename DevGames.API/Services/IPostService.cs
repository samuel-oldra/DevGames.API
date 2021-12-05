using DevGames.API.Entities;
using DevGames.API.Models;

namespace DevGames.API.Services
{
    public interface IPostService
    {
        IEnumerable<Post> GetAllByBoard(int boardId);

        Post? GetById(int boardId, int id);

        Post Add(Post post);

        Comment AddComment(int postId, AddCommentInputModel model);

        bool PostExists(int boardId, int id);
    }
}