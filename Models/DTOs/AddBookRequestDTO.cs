using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models.DTOs
{
    public class AddBookRequestDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string AuthorName { get; set; }
        [Required]
        public string PublicationName { get; set; }
        [Required]
        [MinLength(13), MaxLength(13)]
        public string ISBN { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Now.Date;
    }
}
