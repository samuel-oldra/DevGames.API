using DevGames.API.Entities;

namespace DevGames.API.Persistence.Repositories
{
    public interface IBoardRepository
    {
        IEnumerable<Board> GetAll();

        Board? GetById(int id);

        void Add(Board board);

        void Update(Board board);

        bool BoardExists(int id);
    }
}