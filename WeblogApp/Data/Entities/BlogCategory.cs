using Microsoft.EntityFrameworkCore;

namespace WeblogApp.Data.Entities
{
    
    public class BlogCategory
    {
        public int Id { get; set; }
        public int blogId {  get; set; }
        public int categoryId { get; set; }
    }
}
