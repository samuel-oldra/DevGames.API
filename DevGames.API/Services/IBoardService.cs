using DevGames.API.Entities;

namespace DevGames.API.Services
{
    public interface IBoardService
    {
        IEnumerable<Board> GetAll();

        Board? GetById(int id);

        void Add(Board board);

        void Update(Board board);
    }
}