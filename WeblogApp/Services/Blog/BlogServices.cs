using WeblogApp.BlogData.Entities;
using WeblogApp.Data.Entities;
using WeblogApp.Data.Repositories.Blog;

using WeblogApp.Exceptions;
using WeblogApp.Model;
using WeblogApp.Services.Photo;

namespace WeblogApp.Services.Blog
{
    public class BlogServices : IBlogServices
    {
        private readonly IBlogsData _BlogData;
        private readonly IPhotoServices _PhotoServices;

        public BlogServices(IBlogsData blogData, IPhotoServices photoServices)
        {
            _BlogData = blogData;
            _PhotoServices = photoServices;
        }

        public void AddBlog(BlogDTO blog)
        {
            int photoId = _PhotoServices.FindPhotoIdByName(blog.photoName);

            BlogEntity blogEntity = new BlogEntity()
            {
                            
                Name = blog.Name,
                Article = blog.Article,
                Author = blog.Author,
                Title = blog.Title,
                PhotoId= photoId,
            
            };
            
            _BlogData.AddBlog(blogEntity);


        }

        public void DeleteBlog(int Id)
        {
            _BlogData.DeleteBlog(Id);
        }

        public List<BlogEntity> GetBlogList()
        {
            return _BlogData.GetBlogs();
        }

        public void UpdateBlog(BlogEntity blog)
        {
            _BlogData.UpdateBlog(blog);
        }


        public BlogEntity FindBlogById(int id)
        {
            var blog = _BlogData.GetBlogs().Where(x=>x.Id == id).FirstOrDefault();
            return blog;
        }

        public void AddCategoriesToBlog(int blogId, int categoryId) 
        {
            _BlogData.AddCategoryToBlog(blogId, categoryId);
        }

        public List<BlogCategory> GetBlogCategories()
        {
            return _BlogData.GetBlogCategories();
        }



    }
}
