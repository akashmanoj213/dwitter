using System.ComponentModel.DataAnnotations;

namespace DwitterApp.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Username { get; set; }
    }
}