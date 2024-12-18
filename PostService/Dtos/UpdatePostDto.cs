using System.ComponentModel.DataAnnotations;

namespace PostService.Dtos
{
    public class UpdatePostDto
    {
        //[StringLength(100, MinimumLength = 2)]
        public string Title { get; set; }

        //[StringLength(1000, MinimumLength = 2)]
        public string Content { get; set; }

        //[StringLength(100, MinimumLength = 2)]
        public string Author { get; set; }

        public int? Likes { get; set; }
    }
}
