using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostService.Dtos;
using PostService.Interfaces;
using PostService.Models;

namespace PostService.Controllers
{
    [Route("posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpPatch]
        [Route("like/{id:int}")]
        public async Task<IActionResult> Like(int id)
        {
            return Ok(await _postRepository.Like(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostDto createPost)
        {
            //try
            //{
            return Ok(await _postRepository.CreateAsync(createPost));

            //catch (Exception ex)
            //{
            //    return StatusCode(500, ex.Message);
            //}
        }

        //[HttpDelete]
        [HttpDelete("{id:int}")]
        //[Route("{id:int}")] // Both will work
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var post = await _postRepository.DeleteAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Read([FromRoute] int id)
        {
            var post = await _postRepository.ReadAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _postRepository.GetAllAsync());
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePostDto updatePost)
        {

            var post = await _postRepository.UpdateAsync(id, updatePost);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpGet("get-by")]
        public async Task<IActionResult> GetBy([FromQuery] string filter, [FromQuery] string search = "")
        {
            return Ok(await _postRepository.GetByFilter(search, filter));
        }

    }
}
