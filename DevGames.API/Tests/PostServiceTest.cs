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
    public class PostServiceTest
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
            Assert.NotNull(posts);

            posts.ShouldNotBeNull();

            postRepositoryMock.Verify(pr => pr.GetAllByBoard(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void GetById()
        {
            // Arrange
            var addPost = new Fixture().Create<Post>();

            var postRepositoryMock = new Mock<IPostRepository>();

            var postService = new PostService(postRepositoryMock.Object);

            // Act
            var addedPost = postService.Add(addPost);
            var post = postService.GetById(addedPost.BoardId, addedPost.Id);

            // Assert
            Assert.NotNull(addedPost);

            addedPost.ShouldNotBeNull();

            postRepositoryMock.Verify(pr => pr.Add(It.IsAny<Post>()), Times.Once);
            postRepositoryMock.Verify(pr => pr.GetById(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void Add()
        {
            // Arrange
            var addPost = new Fixture().Create<Post>();

            var postRepositoryMock = new Mock<IPostRepository>();

            var postService = new PostService(postRepositoryMock.Object);

            // Act
            var addedPost = postService.Add(addPost);

            // Assert
            Assert.NotNull(addedPost);
            Assert.Equal(addedPost.Title, addPost.Title);
            Assert.Equal(addedPost.Description, addPost.Description);
            Assert.Equal(addedPost.BoardId, addPost.BoardId);
            Assert.Equal(addedPost.CreatedAt, addPost.CreatedAt);

            addedPost.ShouldNotBeNull();
            addedPost.Title.ShouldBe(addPost.Title);
            addedPost.Description.ShouldBe(addPost.Description);
            addedPost.BoardId.ShouldBe(addPost.BoardId);
            addedPost.CreatedAt.ShouldBe(addPost.CreatedAt);

            postRepositoryMock.Verify(pr => pr.Add(It.IsAny<Post>()), Times.Once);
        }

        [Fact]
        public void AddComment()
        {
            // Arrange
            var addPost = new Fixture().Create<Post>();
            var addCommentInputModel = new Fixture().Create<AddCommentInputModel>();

            var postRepositoryMock = new Mock<IPostRepository>();

            var postService = new PostService(postRepositoryMock.Object);

            // Act
            var addedPost = postService.Add(addPost);
            var addedComment = postService.AddComment(addedPost.Id, addCommentInputModel);

            // Assert
            Assert.NotNull(addedPost);
            Assert.NotNull(addedComment);
            Assert.Equal(addedComment.Title, addCommentInputModel.Title);
            Assert.Equal(addedComment.Description, addCommentInputModel.Description);
            Assert.Equal(addedComment.User, addCommentInputModel.User);
            Assert.Equal(addedComment.PostId, addedPost.Id);

            addedPost.ShouldNotBeNull();
            addedComment.ShouldNotBeNull();
            addedComment.Title.ShouldBe(addCommentInputModel.Title);
            addedComment.Description.ShouldBe(addCommentInputModel.Description);
            addedComment.User.ShouldBe(addCommentInputModel.User);
            addedComment.PostId.ShouldBe(addedPost.Id);

            postRepositoryMock.Verify(pr => pr.Add(It.IsAny<Post>()), Times.Once);
            postRepositoryMock.Verify(pr => pr.AddComment(It.IsAny<Comment>()), Times.Once);
        }

        [Fact]
        public void PostExists()
        {
            // Arrange
            var addPost = new Fixture().Create<Post>();

            var postRepositoryMock = new Mock<IPostRepository>();

            var postService = new PostService(postRepositoryMock.Object);

            // Act
            var addedPost = postService.Add(addPost);
            var postExists = postService.PostExists(addedPost.BoardId, addedPost.Id);

            // Assert
            Assert.NotNull(addedPost);

            addedPost.ShouldNotBeNull();

            // TODO: Verify if post exists
            // Assert.True(postExists);
            // postExists.ShouldBe(true);

            postRepositoryMock.Verify(pr => pr.Add(It.IsAny<Post>()), Times.Once);
            postRepositoryMock.Verify(pr => pr.PostExists(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }
    }
}