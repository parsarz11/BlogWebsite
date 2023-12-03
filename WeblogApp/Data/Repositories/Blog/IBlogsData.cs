using WeblogApp.BlogData.Entities;
using WeblogApp.Data.Entities;

namespace WeblogApp.Data.Repositories.Blog
{
    public interface IBlogsData
    {
        public List<BlogEntity> GetBlogs();
        public void AddBlog(BlogEntity blog);
        public void UpdateBlog(BlogEntity blog);
        public void DeleteBlog(int blogId);
        public void AddCategoryToBlog(int blogId, int categoryId);
        public List<BlogCategory> GetBlogCategories();
    }
}
