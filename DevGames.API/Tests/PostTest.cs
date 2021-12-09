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
    public class PostTest
    {
        [Fact]
        public void GetAllByBoard()
        {
            // Arrange
            var boardId = new Fixture().Create<int>();

            var postRepositoryMock = new Mock<IPostRepository>();

            var postService = new PostService(postRepositoryMock.Object);

            // Act
            var posts = postService.GetAllByBoard(boardId);

            // Assert
            postRepositoryMock.Verify(pr => pr.GetAllByBoard(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void GetById()
        {
            // Arrange
            var boardId = new Fixture().Create<int>();
            var postId = new Fixture().Create<int>();

            var postRepositoryMock = new Mock<IPostRepository>();

            var postService = new PostService(postRepositoryMock.Object);

            // Act
            var post = postService.GetById(boardId, postId);

            // Assert
            postRepositoryMock.Verify(pr => pr.GetById(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void Add()
        {
            // Arrange
            var post = new Fixture().Create<Post>();

            var postRepositoryMock = new Mock<IPostRepository>();

            var postService = new PostService(postRepositoryMock.Object);

            // Act
            var addedPost = postService.Add(post);

            // Assert
            Assert.Equal(addedPost.Title, post.Title);
            Assert.Equal(addedPost.Description, post.Description);
            Assert.Equal(addedPost.BoardId, post.BoardId);
            Assert.Equal(addedPost.CreatedAt, post.CreatedAt);

            addedPost.Title.ShouldBe(post.Title);
            addedPost.Description.ShouldBe(post.Description);
            addedPost.BoardId.ShouldBe(post.BoardId);
            addedPost.CreatedAt.ShouldBe(post.CreatedAt);

            postRepositoryMock.Verify(pr => pr.Add(It.IsAny<Post>()), Times.Once);
        }

        [Fact]
        public void AddComment()
        {
            // Arrange
            var postId = new Fixture().Create<int>();
            var addCommentInputModel = new Fixture().Create<AddCommentInputModel>();

            var postRepositoryMock = new Mock<IPostRepository>();

            var postService = new PostService(postRepositoryMock.Object);

            // Act
            var addedComment = postService.AddComment(postId, addCommentInputModel);

            // Assert
            Assert.Equal(addedComment.Title, addCommentInputModel.Title);
            Assert.Equal(addedComment.Description, addCommentInputModel.Description);
            Assert.Equal(addedComment.User, addCommentInputModel.User);

            addedComment.Title.ShouldBe(addCommentInputModel.Title);
            addedComment.Description.ShouldBe(addCommentInputModel.Description);
            addedComment.User.ShouldBe(addCommentInputModel.User);

            postRepositoryMock.Verify(pr => pr.AddComment(It.IsAny<Comment>()), Times.Once);
        }

        [Fact]
        public void PostExists()
        {
            // Arrange
            var boardId = new Fixture().Create<int>();
            var postId = new Fixture().Create<int>();

            var postRepositoryMock = new Mock<IPostRepository>();

            var postService = new PostService(postRepositoryMock.Object);

            // Act
            var postExists = postService.PostExists(boardId, postId);

            // Assert
            postRepositoryMock.Verify(pr => pr.PostExists(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }
    }
}