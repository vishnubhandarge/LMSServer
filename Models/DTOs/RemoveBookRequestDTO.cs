using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models.DTOs
{
    public class RemoveBookRequestDTO
    {
        [Required]
        public int Id{ get; set; }
    }
}
