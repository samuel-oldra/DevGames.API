using AutoFixture;
using DevGames.API.Entities;
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
    }
}