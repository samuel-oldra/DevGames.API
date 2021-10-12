using AutoMapper;
using DevGames.API.Entities;
using DevGames.API.Models;

namespace DevGames.API.Mappers
{
    public class BoardMapper : Profile
    {
        public BoardMapper()
        {
            // CreateMap<ObjetoOrigem, ObjetoDestino>();
            CreateMap<AddBoardInputModel, Board>();
        }
    }
}