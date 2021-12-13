using DevGames.API.Entities;
using DevGames.API.Models;
using DevGames.API.Persistence.Repositories;

namespace DevGames.API.Services
{
    public class BoardService : IBoardService
    {
        private readonly IBoardRepository boardRepository;

        public BoardService(IBoardRepository boardRepository)
            => this.boardRepository = boardRepository;

        public IEnumerable<Board> GetAll()
            => boardRepository.GetAll();

        public Board? GetById(int id)
            => boardRepository.GetById(id);

        public Board Add(Board board)
        {
            boardRepository.Add(board);

            return board;
        }

        public Board Update(Board board, UpdateBoardInputModel model)
        {
            board.Update(model.Description, model.Rules);

            boardRepository.Update(board);

            return board;
        }

        public bool BoardExists(int id)
            => boardRepository.BoardExists(id);
    }
}