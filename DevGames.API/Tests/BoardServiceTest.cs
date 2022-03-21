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
    public class BoardServiceTest
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
            Assert.NotNull(boards);

            boards.ShouldNotBeNull();

            boardRepositoryMock.Verify(br => br.GetAll(), Times.Once);
        }

        [Fact]
        public void GetById()
        {
            // Arrange
            var addBoard = new Fixture().Create<Board>();

            var boardRepositoryMock = new Mock<IBoardRepository>();

            var boardService = new BoardService(boardRepositoryMock.Object);

            // Act
            var addedBoard = boardService.Add(addBoard);
            var board = boardService.GetById(addedBoard.Id);

            // Assert
            Assert.NotNull(addedBoard);

            addedBoard.ShouldNotBeNull();

            boardRepositoryMock.Verify(br => br.Add(It.IsAny<Board>()), Times.Once);
            boardRepositoryMock.Verify(br => br.GetById(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void Add()
        {
            // Arrange
            var addBoard = new Fixture().Create<Board>();

            var boardRepositoryMock = new Mock<IBoardRepository>();

            var boardService = new BoardService(boardRepositoryMock.Object);

            // Act
            var addedBoard = boardService.Add(addBoard);

            // Assert
            Assert.NotNull(addedBoard);
            Assert.Equal(addedBoard.GameTitle, addBoard.GameTitle);
            Assert.Equal(addedBoard.Description, addBoard.Description);
            Assert.Equal(addedBoard.Rules, addBoard.Rules);

            addedBoard.ShouldNotBeNull();
            addedBoard.GameTitle.ShouldBe(addBoard.GameTitle);
            addedBoard.Description.ShouldBe(addBoard.Description);
            addedBoard.Rules.ShouldBe(addBoard.Rules);

            boardRepositoryMock.Verify(br => br.Add(It.IsAny<Board>()), Times.Once);
        }

        [Fact]
        public void Update()
        {
            // Arrange
            var addBoard = new Fixture().Create<Board>();
            var updateBoardInputModel = new Fixture().Create<UpdateBoardInputModel>();

            var boardRepositoryMock = new Mock<IBoardRepository>();

            var boardService = new BoardService(boardRepositoryMock.Object);

            // Act
            var addedBoard = boardService.Add(addBoard);
            var updatedBoard = boardService.Update(addedBoard, updateBoardInputModel);

            // Assert
            Assert.NotNull(addedBoard);
            Assert.NotNull(updatedBoard);
            Assert.Equal(updatedBoard.GameTitle, addBoard.GameTitle);
            Assert.Equal(updatedBoard.Description, updateBoardInputModel.Description);
            Assert.Equal(updatedBoard.Rules, updateBoardInputModel.Rules);

            addedBoard.ShouldNotBeNull();
            updatedBoard.ShouldNotBeNull();
            updatedBoard.GameTitle.ShouldBe(addBoard.GameTitle);
            updatedBoard.Description.ShouldBe(updateBoardInputModel.Description);
            updatedBoard.Rules.ShouldBe(updateBoardInputModel.Rules);

            boardRepositoryMock.Verify(br => br.Add(It.IsAny<Board>()), Times.Once);
            boardRepositoryMock.Verify(br => br.Update(It.IsAny<Board>()), Times.Once);
        }

        [Fact]
        public void BoardExists()
        {
            // Arrange
            var addBoard = new Fixture().Create<Board>();

            var boardRepositoryMock = new Mock<IBoardRepository>();

            var boardService = new BoardService(boardRepositoryMock.Object);

            // Act
            var addedBoard = boardService.Add(addBoard);
            var boardExists = boardService.BoardExists(addedBoard.Id);

            // Assert
            Assert.NotNull(addedBoard);

            addedBoard.ShouldNotBeNull();

            // TODO: Verify if board exists
            // Assert.True(boardExists);
            // boardExists.ShouldBe(true);

            boardRepositoryMock.Verify(br => br.Add(It.IsAny<Board>()), Times.Once);
            boardRepositoryMock.Verify(br => br.BoardExists(It.IsAny<int>()), Times.Once);
        }
    }
}