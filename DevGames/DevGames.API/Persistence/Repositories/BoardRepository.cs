using DevGames.API.Entities;

namespace DevGames.API.Persistence.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private readonly DevGamesContext context;

        public BoardRepository(DevGamesContext context)
        {
            this.context = context;
        }

        public void Add(Board board)
        {
            context.Boards.Add(board);
            context.SaveChanges();
        }

        public IEnumerable<Board> GetAll()
        {
            return context.Boards.ToList();
        }

        public Board GetById(int id)
        {
            return context.Boards.SingleOrDefault(b => b.Id == id);
        }

        public void Update(Board board)
        {
            context.Boards.Update(board);
            context.SaveChanges();
        }
    }
}