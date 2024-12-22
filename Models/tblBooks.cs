using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class tblBooks
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string CategoryType { get; set; }
        public string AuthorName { get; set; }
        public string PublicationName { get; set; }
        public string ISBN { get; set; }
        public string CreatedBy { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
    }
}
