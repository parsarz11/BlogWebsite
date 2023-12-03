using WeblogApp.BlogData.Entities;
using WeblogApp.Data.Entities;
using WeblogApp.Data.Repositories.Blog;
using WeblogApp.Model;

namespace WeblogApp.Services.Blog
{
    public interface IBlogServices
    {
        public void AddBlog(BlogDTO blog);
        public void DeleteBlog(int Id);
        public List<BlogEntity> GetBlogList();
        public void UpdateBlog(BlogEntity blog);
        public BlogEntity FindBlogById(int id);
        public void AddCategoriesToBlog(int blogId, int categoryId);
        public List<BlogCategory> GetBlogCategories();


    }
}
