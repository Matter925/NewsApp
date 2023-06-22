
using NewsMVC.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsMVC.ViewModels
{
    public class NewDto
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public DateTime publicationDate { get; set; }
        public DateTime creationDate { get; set; }
        public int authorId { get; set; }
        public author author { get; set; }
    }
}
