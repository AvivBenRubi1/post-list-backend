using System.ComponentModel.DataAnnotations;

namespace PostService.Dtos
{
    public class CreatePostDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 2)]
        public string Content { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Author { get; set; }
    }
}
