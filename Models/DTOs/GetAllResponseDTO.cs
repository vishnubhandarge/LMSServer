
namespace LMS.Models.DTOs
{
    public class GetAllResponseDTO
    {
        public string Title { get; set; }
        public string CategoryType { get; set; }
        public string AuthorName { get; set; }
        public string PublicationName { get; set; }
        public string ISBN { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
