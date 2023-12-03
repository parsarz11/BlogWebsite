namespace WeblogApp.Data.Entities
{
    public class PhotoFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] Photo { get; set; }
    }
}
