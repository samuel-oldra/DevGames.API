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
            var board = new Fixture().Create<Board>();

            var boardRepositoryMock = new Mock<IBoardRepository>();

            var boardService = new BoardService(boardRepositoryMock.Object);

            // Act
            var addedBoard = boardService.Add(board);

            // Assert
            Assert.Equal(addedBoard.GameTitle, board.GameTitle);
            Assert.Equal(addedBoard.Description, board.Description);
            Assert.Equal(addedBoard.Rules, board.Rules);

            addedBoard.GameTitle.ShouldBe(board.GameTitle);
            addedBoard.Description.ShouldBe(board.Description);
            addedBoard.Rules.ShouldBe(board.Rules);

            boardRepositoryMock.Verify(br => br.Add(It.IsAny<Board>()), Times.Once);
        }
    }
}