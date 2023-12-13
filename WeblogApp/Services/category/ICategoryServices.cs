using WeblogApp.BlogData.Entities;
using WeblogApp.Data.Entities;

namespace WeblogApp.Services.category
{
    public interface ICategoryServices
    {
        public List<Category> FindCategoryByBlogs(int blogId);
        public Category FindCategoryById(int id);
        public List<Category> GetCategories();
        public void AddCategory(Category category);
        public void EditCategory(Category category);
        public void DeleteCategory(int id);
        public void AddCategoriesToBlog(int blogId, List<string> categories);
        public List<BlogEntity> FindBlogsByCategoryId(int categoryId);
    }
}
