using AutoFixture;
using DevGames.API.Entities;
using DevGames.API.Persistence.Repositories;
using DevGames.API.Services;
using Moq;
using Shouldly;
using Xunit;

namespace DevGames.API.Tests
{
    public class BoardTests
    {
        [Fact]
        public void Add()
        {
            // Arrange
            var addBoard = new Fixture().Create<Board>();

            //var mapperMock = new Mock<IMapper>();
            var boardRepositoryMock = new Mock<IBoardRepository>();

            var boardService = new BoardService(boardRepositoryMock.Object);

            // Act
            var result = boardService.Add(addBoard);

            //var result2 = boardService.GetById(result.Id);
            //Assert.Equal(addBoard.GameTitle, result2.GameTitle); // Board?

            // Assert
            Assert.Equal(addBoard.GameTitle, result.GameTitle);
            Assert.Equal(addBoard.Description, result.Description);
            Assert.Equal(addBoard.Rules, result.Rules);

            result.GameTitle.ShouldBe(addBoard.GameTitle);
            result.Description.ShouldBe(addBoard.Description);
            result.Rules.ShouldBe(addBoard.Rules);

            boardRepositoryMock.Verify(br => br.Add(It.IsAny<Board>()), Times.Once);
        }
    }
}