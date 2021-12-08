using AutoFixture;
using DevGames.API.Entities;
using DevGames.API.Models;
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
        public void GetAll()
        {
            // Arrange
            var boardRepositoryMock = new Mock<IBoardRepository>();

            var boardService = new BoardService(boardRepositoryMock.Object);

            // Act
            var boards = boardService.GetAll();

            // Assert
            boardRepositoryMock.Verify(br => br.GetAll(), Times.Once);
        }

        [Fact]
        public void GetById()
        {
            // Arrange
            var boardId = new Fixture().Create<int>();

            var boardRepositoryMock = new Mock<IBoardRepository>();

            var boardService = new BoardService(boardRepositoryMock.Object);

            // Act
            var board = boardService.GetById(boardId);

            // Assert
            boardRepositoryMock.Verify(br => br.GetById(It.IsAny<int>()), Times.Once);
        }

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

        [Fact]
        public void Update()
        {
            // Arrange
            var board = new Fixture().Create<Board>();
            var updateBoardInputModel = new Fixture().Create<UpdateBoardInputModel>();

            var boardRepositoryMock = new Mock<IBoardRepository>();

            var boardService = new BoardService(boardRepositoryMock.Object);

            // Act
            var addedBoard = boardService.Add(board);
            var updatedBoard = boardService.Update(addedBoard, updateBoardInputModel);

            // Assert
            Assert.Equal(updatedBoard.GameTitle, board.GameTitle);
            Assert.Equal(updatedBoard.Description, board.Description);
            Assert.Equal(updatedBoard.Rules, board.Rules);

            updatedBoard.GameTitle.ShouldBe(board.GameTitle);
            updatedBoard.Description.ShouldBe(board.Description);
            updatedBoard.Rules.ShouldBe(board.Rules);

            boardRepositoryMock.Verify(br => br.Update(It.IsAny<Board>()), Times.Once);
        }

        [Fact]
        public void BoardExists()
        {
            // Arrange
            var boardId = new Fixture().Create<int>();

            var boardRepositoryMock = new Mock<IBoardRepository>();

            var boardService = new BoardService(boardRepositoryMock.Object);

            // Act
            var boardExists = boardService.BoardExists(boardId);

            // Assert
            boardRepositoryMock.Verify(br => br.BoardExists(It.IsAny<int>()), Times.Once);
        }
    }
}