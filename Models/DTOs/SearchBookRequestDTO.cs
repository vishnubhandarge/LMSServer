using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models.DTOs
{
    public class SearchBookRequestDTO
    {
        public bool? Author {get; set; }
        public bool? Title {get; set; }
        public bool? ISBN {get; set; }
        [Required]
        public  string Input { get; set; }
    }
}
