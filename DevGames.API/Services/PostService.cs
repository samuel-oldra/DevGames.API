using DevGames.API.Entities;
using DevGames.API.Persistence.Repositories;

namespace DevGames.API.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository postRepository;

        public PostService(IPostRepository postRepository) =>
            this.postRepository = postRepository;

        public IEnumerable<Post> GetAllByBoard(int boardId) =>
            postRepository.GetAllByBoard(boardId);

        public Post? GetById(int boardId, int id) =>
            postRepository.GetById(boardId, id);

        public void Add(Post post) =>
            postRepository.Add(post);

        public void AddComment(Comment comment) =>
            postRepository.AddComment(comment);

        public bool PostExists(int boardId, int id) =>
            postRepository.PostExists(boardId, id);
    }
}