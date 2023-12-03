namespace WeblogApp.BlogData.Entities
{
    public class BlogEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Article { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public int PhotoId { get; set; }
    }
}
