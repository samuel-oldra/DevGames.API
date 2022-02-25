using DevGames.API.Entities;
using DevGames.API.Models;

namespace DevGames.API.Services
{
    public interface IBoardService
    {
        IEnumerable<Board> GetAll();

        Board? GetById(int id);

        Board Add(AddBoardInputModel model);

        void Update(Board board, UpdateBoardInputModel model);
    }
}