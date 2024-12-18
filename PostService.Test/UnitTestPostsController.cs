using Microsoft.AspNetCore.Mvc;
using PostService.Controllers;
using PostService.Dtos;
using PostService.Interfaces;
using PostService.Models;

namespace PostService.Test
{
    public class UnitTestPostsController
    {
            private readonly Mock<IPostRepository> _postRepositoryMock;
            private readonly PostController _controller;

            public PostsControllerTests()
            {
                _postRepositoryMock = new Mock<IPostRepository>();
                _controller = new PostsController(_postRepositoryMock.Object);
            }

            [Fact]
            public async Task Create_ReturnsOk_WhenPostIsCreated()
            {
                // Arrange
                var createPostDto = new CreatePostDto { Title = "Test", Content = "Content" };
                var createdPost = new Post { Id = 1, Title = "Test", Content = "Content" };
                _postRepositoryMock.Setup(r => r.CreateAsync(createPostDto)).ReturnsAsync(createdPost);

                // Act
                var result = await _controller.Create(createPostDto);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Equal(createdPost, okResult.Value);
            }

            [Fact]
            public async Task Delete_ReturnsNotFound_WhenPostDoesNotExist()
            {
                // Arrange
                int postId = 1;
                _postRepositoryMock.Setup(r => r.DeleteAsync(postId)).ReturnsAsync((Post)null);

                // Act
                var result = await _controller.Delete(postId);

                // Assert
                Assert.IsType<NotFoundResult>(result);
            }

            [Fact]
            public async Task Delete_ReturnsOk_WhenPostIsDeleted()
            {
                // Arrange
                int postId = 1;
                var deletedPost = new Post { Id = postId, Title = "Test", Content = "Content" };
                _postRepositoryMock.Setup(r => r.DeleteAsync(postId)).ReturnsAsync(deletedPost);

                // Act
                var result = await _controller.Delete(postId);

                // Assert
                Assert.IsType<OkResult>(result);
            }

            [Fact]
            public async Task Read_ReturnsNotFound_WhenPostDoesNotExist()
            {
                // Arrange
                int postId = 1;
                _postRepositoryMock.Setup(r => r.ReadAsync(postId)).ReturnsAsync((Post)null);

                // Act
                var result = await _controller.Read(postId);

                // Assert
                Assert.IsType<NotFoundResult>(result);
            }

            [Fact]
            public async Task Read_ReturnsOk_WhenPostExists()
            {
                // Arrange
                int postId = 1;
                var post = new Post { Id = postId, Title = "Test", Content = "Content" };
                _postRepositoryMock.Setup(r => r.ReadAsync(postId)).ReturnsAsync(post);

                // Act
                var result = await _controller.Read(postId);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Equal(post, okResult.Value);
            }

            [Fact]
            public async Task GetAll_ReturnsOkWithPosts()
            {
                // Arrange
                var posts = new List<Post>
        {
            new Post { Id = 1, Title = "Test1", Content = "Content1" },
            new Post { Id = 2, Title = "Test2", Content = "Content2" }
        };
                _postRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(posts);

                // Act
                var result = await _controller.GetAll();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Equal(posts, okResult.Value);
            }

            [Fact]
            public async Task Update_ReturnsNotFound_WhenPostDoesNotExist()
            {
                // Arrange
                int postId = 1;
                var updatePostDto = new UpdatePostDto { Title = "Updated Title", Content = "Updated Content" };
                _postRepositoryMock.Setup(r => r.UpdateAsync(postId, updatePostDto)).ReturnsAsync((Post)null);

                // Act
                var result = await _controller.Update(postId, updatePostDto);

                // Assert
                Assert.IsType<NotFoundResult>(result);
            }

            [Fact]
            public async Task Update_ReturnsOk_WhenPostIsUpdated()
            {
                // Arrange
                int postId = 1;
                var updatePostDto = new UpdatePostDto { Title = "Updated Title", Content = "Updated Content" };
                var updatedPost = new Post { Id = postId, Title = "Updated Title", Content = "Updated Content" };
                _postRepositoryMock.Setup(r => r.UpdateAsync(postId, updatePostDto)).ReturnsAsync(updatedPost);

                // Act
                var result = await _controller.Update(postId, updatePostDto);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Equal(updatedPost, okResult.Value);
            }

            [Fact]
            public async Task GetBy_ReturnsOkWithFilteredPosts()
            {
                // Arrange
                string search = "Test";
                var filter = Filter.Author;
                var posts = new List<Post>
        {
            new Post { Id = 1, Title = "Test1", Content = "Content1" },
            new Post { Id = 2, Title = "Test2", Content = "Content2" }
        };
                _postRepositoryMock.Setup(r => r.GetByFilter(search, filter)).ReturnsAsync(posts);

                // Act
                var result = await _controller.GetBy(search, filter);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Equal(posts, okResult.Value);
            }
    }
}