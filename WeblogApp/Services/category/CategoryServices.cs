using WeblogApp.BlogData.Entities;
using WeblogApp.Data.Entities;
using WeblogApp.Data.Repositories.Blog;
using WeblogApp.Data.Repositories.Category;
using WeblogApp.Exceptions;
using WeblogApp.Services.Blog;

namespace WeblogApp.Services.category
{
    public class CategoryServices:ICategoryServices
    {
        private readonly IBlogsData _blogsData;
        private readonly ICategoriesData _categoryData;
        public CategoryServices(IBlogsData blogData,ICategoriesData categoriesData)
        {
            _blogsData = blogData;
            _categoryData = categoriesData;
        }


        
        public List<Category> FindCategoryByBlogs(int blogId)
        {

            var categoryIds = GetBlogCategories().Where(x => x.blogId == blogId).Select(y => y.categoryId).ToList();
            var categories = _categoryData.GetCategory();


            if (categoryIds.Count() == 0)
            {
                throw new NotFoundException();
            }


            var categoryList = new List<Category>();
            foreach (var item in categoryIds)
            {
                categoryList.Add(categories.Where(x => x.Id == item).FirstOrDefault());
            };


            return categoryList;
            
        }

        public Category FindCategoryById(int id)
        {
            return _categoryData.GetCategoryById(id);
        }

        public List<Category> GetCategories()
        {
            return _categoryData.GetCategory();
        }

        public void AddCategory(Category category)
        {
            _categoryData.AddCategory(category);
        }

        public void EditCategory(Category category)
        {
            _categoryData.UpdateCategory(category);
        }

        public void DeleteCategory(int id)
        {
            _categoryData.RemoveCategory(id);
        }

        public void AddCategoriesToBlog(int blogId,List<string> categories)
        {
            if (blogId == 0)
            {
                var lastBlogId = _blogsData.GetBlogs().Select(x => x.Id).LastOrDefault();
                blogId = lastBlogId;
                

            }
            bool isBlogExist = _blogsData.GetBlogs().Any(x => x.Id == blogId);

            if (!isBlogExist)
            {
                throw new NotFoundException("No Blogs found to add cateogry to");
            }
            _categoryData.AddblogCategories(blogId, categories);

                
        }



        public List<BlogEntity> FindBlogsByCategoryId(int categoryId)
        {

            var blogIds = GetBlogCategories().Where(x => x.categoryId == categoryId).Select(y => y.blogId).ToList();
            var blogs = _blogsData.GetBlogs();


            if (blogIds.Count() == 0)
            {
                throw new NotFoundException();
            }


            var blogList = new List<BlogEntity>();
            foreach (var item in blogIds)
            {
                blogList.Add(blogs.Where(x => x.Id == item).FirstOrDefault());
            };


            return blogs;

        }
        
        public List<BlogCategory> GetBlogCategories()
        {
            return _blogsData.GetBlogCategories();
        }
    }
}
