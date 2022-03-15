using DevGames.API.Entities;
using DevGames.API.Models;
using DevGames.API.Persistence.Repositories;

namespace DevGames.API.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository postRepository;

        public PostService(IPostRepository postRepository)
            => this.postRepository = postRepository;

        public IEnumerable<Post> GetAllByBoard(int boardId)
            => postRepository.GetAllByBoard(boardId);

        public Post? GetById(int boardId, int id)
            => postRepository.GetById(boardId, id);

        public Post Add(Post post)
        {
            postRepository.Add(post);

            return post;
        }

        public Comment AddComment(int postId, AddCommentInputModel model)
        {
            var comment = new Comment(model.Title, model.Description, model.User, postId);

            postRepository.AddComment(comment);

            return comment;
        }

        public bool PostExists(int boardId, int id)
            => postRepository.PostExists(boardId, id);
    }
}