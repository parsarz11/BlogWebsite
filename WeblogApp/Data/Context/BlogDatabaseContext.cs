using Microsoft.EntityFrameworkCore;
using WeblogApp.BlogData.Entities;
using WeblogApp.Data.Entities;

namespace WeblogApp.BlogData.Context
{
    public class BlogDatabaseContext : DbContext
    {
        public BlogDatabaseContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<BlogEntity> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PhotoFile> PhotoFiles { get; set; }
        public DbSet<BlogCategory>  blogsCategories { get; set; }
    }
}
