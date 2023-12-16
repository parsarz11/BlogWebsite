using WeblogApp.BlogData.Entities;
using WeblogApp.Data.Entities;
using WeblogApp.Data.Repositories.Blog;
using WeblogApp.Model;
using WeblogApp.Model.DTOs;

namespace WeblogApp.Services.Blog
{
    public interface IBlogServices
    {
        public void AddBlog(CreateBlogDTO blog);
        public void DeleteBlog(int Id);
        public List<BLogDTO> GetBlogList();
        public void UpdateBlog(UpdateBlog blog);
        public BLogDTO FindBlogById(int id);
        public void AddCategoriesToBlog(int blogId, int categoryId);
        public void AddBlogByFile(FileUploadModel fileUploadModel);
    }
}
