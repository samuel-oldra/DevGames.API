using DevGames.API.Entities;
using DevGames.API.Persistence.Repositories;

namespace DevGames.API.Services
{
    public class BoardService : IBoardService
    {
        private readonly IBoardRepository boardRepository;

        public BoardService(IBoardRepository boardRepository) =>
            this.boardRepository = boardRepository;

        public IEnumerable<Board> GetAll() =>
            boardRepository.GetAll();

        public Board? GetById(int id) =>
            boardRepository.GetById(id);

        public void Add(Board board) =>
            boardRepository.Add(board);

        public void Update(Board board) =>
            boardRepository.Update(board);
    }
}