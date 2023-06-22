using System.ComponentModel.DataAnnotations;

namespace NewsAPIs.Models
{
    public class New
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        
        public DateTime PublicationDate { get; set; }
        public DateTime CreationDate { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
