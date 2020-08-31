using System.ComponentModel.DataAnnotations;

namespace DwitterApp.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public int Likes { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}