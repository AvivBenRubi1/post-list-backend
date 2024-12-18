using PostService.Dtos;
using PostService.Models;

namespace PostService.Mappers
{
    public static class PostMapper
    {
        public static Post FromCreatePostDtoToPost(this CreatePostDto createPostDto)
        {
            return new Post()
            {
                Title = createPostDto.Title,
                Author = createPostDto.Author,
                Content = createPostDto.Content,
            };
        }
    }
}
