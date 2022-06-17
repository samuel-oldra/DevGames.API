namespace DevGames.API.Models
{
    public record AddCommentInputModel(
        string Title,
        string Description,
        string User);
}