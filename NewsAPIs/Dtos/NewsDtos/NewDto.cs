namespace NewsAPIs.Dtos.NewsDtos
{
    public class NewDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? Image { get; set; }

        public DateTime PublicationDate { get; set; }
        public int AuthorId { get; set; }
    }
}
