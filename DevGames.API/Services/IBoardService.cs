using DevGames.API.Entities;
using DevGames.API.Models;

namespace DevGames.API.Services
{
    public interface IBoardService
    {
        IEnumerable<Board> GetAll();

        Board? GetById(int id);

        Board Add(Board board);

        void Update(Board board, UpdateBoardInputModel model);
    }
}