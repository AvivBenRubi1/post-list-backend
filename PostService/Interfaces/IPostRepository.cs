using PostService.Dtos;
using PostService.Models;

namespace PostService.Interfaces
{
    public enum Filter{
        Author,
        Content,
        Title
    }
    public interface IPostRepository
    {
        Task<int> Count();
        Task<bool> IsExist(int id);

        Task <Post?>CreateAsync(CreatePostDto createPost);
        Task<Post?>DeleteAsync(int id);
        Task<Post?> UpdateAsync(int id, UpdatePostDto updatePost);
        Task<Post?> ReadAsync(int id);
        Task<IEnumerable<Post>> GetAllAsync();

        Task<IEnumerable<Post>> GetByFilter(string search, string filter);

        Task<int> Like(int id);
    }
}
