using AutoMapper;
using DevGames.API.Entities;
using DevGames.API.Models;
using DevGames.API.Persistence.Repositories;

namespace DevGames.API.Services
{
    public class BoardService : IBoardService
    {
        private readonly IMapper mapper;

        private readonly IBoardRepository boardRepository;

        public BoardService(
            IMapper mapper,
            IBoardRepository boardRepository)
        {
            this.mapper = mapper;
            this.boardRepository = boardRepository;
        }

        public IEnumerable<Board> GetAll() =>
            boardRepository.GetAll();

        public Board? GetById(int id) =>
            boardRepository.GetById(id);

        public Board Add(AddBoardInputModel model)
        {
            var board = mapper.Map<Board>(model);

            boardRepository.Add(board);

            return board;
        }

        public void Update(Board board, UpdateBoardInputModel model)
        {
            board.Update(model.Description, model.Rules);

            boardRepository.Update(board);
        }
    }
}