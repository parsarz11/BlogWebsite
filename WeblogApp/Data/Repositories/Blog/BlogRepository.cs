using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WeblogApp.BlogData.Context;
using WeblogApp.BlogData.Entities;
using WeblogApp.Data.Entities;
using WeblogApp.Exceptions;

namespace WeblogApp.Data.Repositories.Blog
{
    public class BlogRepository : IBlogsData
    {
        private readonly BlogDatabaseContext _databaseContext;

        public BlogRepository(BlogDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void AddBlog(BlogEntity blog)
        {
            if (blog == null)
            {
                throw new Exception("data is null");
            }
            else
            {
            
                _databaseContext.Blogs.Add(blog);
                _databaseContext.SaveChanges();
            }
        }

        public void DeleteBlog(int blogId)
        {
            var Blogs = _databaseContext.Blogs.Where(b => b.Id == blogId).FirstOrDefault();

            if (Blogs == null)
            {
                throw new NotFoundException("Item with this Id doesn't exist");
            }
            else
            {
                _databaseContext.Blogs.Remove(Blogs);
                _databaseContext.SaveChanges();
            }
            
        }

        public List<BlogEntity> GetBlogs()
        {
            var blogs =  _databaseContext.Blogs.AsNoTracking().ToList();
            if (blogs.Count() == 0)
            {
                throw new NotFoundException("no blogs exist");
            }
            else
            {
               
                return blogs;
            }

        }

        public void UpdateBlog(BlogEntity blog)
        {
           
            _databaseContext.Blogs.Update(blog);
            _databaseContext.SaveChanges();
        }

        public List<BlogCategory> GetBlogCategories()
        {
            return _databaseContext.blogsCategories.ToList();
        }

        public void AddCategoryToBlog(int blogId,int categoryId)
        {
            var blogCategory = new BlogCategory()
            {
                blogId = blogId,
                categoryId = categoryId,
            };
            _databaseContext.blogsCategories.AddRange(blogCategory);
        }
    }
}
