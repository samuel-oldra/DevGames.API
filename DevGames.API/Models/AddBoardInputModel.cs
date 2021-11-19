namespace DevGames.API.Models
{
    public record AddBoardInputModel(
        string GameTitle,
        string Description,
        string Rules);
}