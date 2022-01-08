using DevGames.API.Entities;

namespace DevGames.API.Persistence
{
    public class DevGamesContext
    {
        public DevGamesContext()
        {
            Boards = new List<Board>();
        }

        public List<Board> Boards { get; private set; }
    }
}