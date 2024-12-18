using PostService.Dtos;
using PostService.Interfaces;
using PostService.Models;
using PostService.Mappers;
namespace PostService.Repository
{
    public class PostRepository : IPostRepository
    {
        private ICustomJsonHelper _customJsonHelper;

        public PostRepository(ICustomJsonHelper customJsonHelper)
        {
            _customJsonHelper = customJsonHelper;
        }

        public async  Task<int> Count()
        {
            return (await GetAllAsync()).Count();
        }

        public async Task<Post?> CreateAsync(CreatePostDto createPost)
        {
            var allItems = await GetAllAsync();
            Post post = createPost.FromCreatePostDtoToPost();
            post.Id = allItems.Count();
            allItems = allItems.Append(post);
            await _customJsonHelper.Write(allItems);
            return post;
        }

        public async Task<Post?> DeleteAsync(int id)
        {
            var allItems = await GetAllAsync();
            var item = allItems.FirstOrDefault(p => p.Id == id);
            await _customJsonHelper.Write(allItems.Where(p => p.Id != id));
            return item;
        }

        public async Task<int> Dislike(int id)
        {
            var post = await ReadAsync(id);
            int updatedLikes = 0;
            if (post != null)
            {
                updatedLikes = post.Likes - 1;
                await UpdateAsync(id, new UpdatePostDto { Likes = updatedLikes });
            }
            return updatedLikes;
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _customJsonHelper.Read<Post>();
        }

        public async Task<IEnumerable<Post>> GetByFilter(string search, Filter filter)
        {
            var allItems = await GetAllAsync();
            return filter switch
            {
                Filter.Content => allItems.Where(p => p.Content.Contains(search)),
                Filter.Author => allItems.Where(p => p.Author.Contains(search)),
                _ => allItems.Where(p => p.Title.Contains(search)),
            };
        }

        public async Task<bool> IsExist(int id)
        {
            var items = await GetAllAsync();
                if (items.FirstOrDefault((p) => p.Id == id) == null)
                    return false;
                return true;
        }

        public async Task<int> Like(int id)
        {
            var post = await ReadAsync(id);
            int updatedLikes = 0;
            if (post != null)
            {
                updatedLikes = post.Likes + 1;
                await UpdateAsync(id, new UpdatePostDto { Likes = updatedLikes });
            }
            return updatedLikes;
        }

        public async Task<Post?> ReadAsync(int id)
        {
            var items = await GetAllAsync();
            return items.FirstOrDefault((x) => x.Id == id);
        }

        public async Task<Post?> UpdateAsync(int id, UpdatePostDto updatePost)
        {
            var allItems = await GetAllAsync();
            Post? post = null;
            foreach(var item in allItems)
            {
                if(item.Id == id)
                {
                    post = item;
                    if (!string.IsNullOrEmpty(updatePost.Content))
                    {
                        item.Content = updatePost.Content;
                    }
                    if (!string.IsNullOrEmpty(updatePost.Author))
                    {
                        item.Author = updatePost.Author;
                    }
                    if (!string.IsNullOrEmpty(updatePost.Title))
                    {
                        item.Title = updatePost.Title;
                    }
                    if (updatePost.Likes != null)
                    {
                        item.Likes = (int)updatePost.Likes;
                    }
                }
            }
            await _customJsonHelper.Write(allItems);
            return post;
        }

    }
}
