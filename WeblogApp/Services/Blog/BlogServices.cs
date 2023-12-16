using System.Globalization;
using System.Reflection.Metadata;
using WeblogApp.BlogData.Entities;
using WeblogApp.Data.Entities;
using WeblogApp.Data.Repositories.Blog;

using WeblogApp.Exceptions;
using WeblogApp.Model;
using WeblogApp.Model.DTOs;
using WeblogApp.Services.category;
using WeblogApp.Services.Photo;

namespace WeblogApp.Services.Blog
{
    public class BlogServices : IBlogServices
    {
        private readonly IBlogsData _BlogData;
        private readonly IPhotoServices _PhotoServices;
        private readonly ICategoryServices _CategoryServices;
        public BlogServices(IBlogsData blogData, IPhotoServices photoServices, ICategoryServices categoryServices)
        {
            _BlogData = blogData;
            _PhotoServices = photoServices;
            _CategoryServices = categoryServices;
        }

        public void AddBlog(CreateBlogDTO blog)
        {
            int photoId = _PhotoServices.FindPhotoIdByName(blog.photoName);

            BlogEntity blogEntity = new BlogEntity()
            {
                            
                
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

        public List<BLogDTO> GetBlogList()
        {
            var blogList = _BlogData.GetBlogs();
            List<BLogDTO> blogs = new List<BLogDTO>();
            
            PersianCalendar pc = new PersianCalendar();
            
            foreach (var item in blogList)
            {
                var persianDate = string.Format("{0}/{1}/{2}", pc.GetYear(item.Date), pc.GetMonth(item.Date), pc.GetDayOfMonth(item.Date)) + "  "  + string.Format("{0}:{1}", pc.GetHour(item.Date), pc.GetMinute(item.Date));
                

                blogs.Add(new BLogDTO()
                {
                    Id = item.Id,
                    Article = item.Article,
                    Author =item.Author,
                    Title = item.Title,
                    Date = persianDate,
                    PhotoId = item.PhotoId,
                });
            }
            return blogs;
        }

        public void UpdateBlog(UpdateBlog blog)
        {
            var blogs = _BlogData.GetBlogs().Where(x=>x.Id == blog.Id).FirstOrDefault();
            var blogEntity = new BlogEntity()
            {
                Id = blog.Id,
                Article = blog.Article,
                Author = blog.Author,
                Title = blog.Title,      
                Date = blogs.Date,
                PhotoId = blogs.PhotoId,
            };
            _BlogData.UpdateBlog(blogEntity);
        }


        public BLogDTO FindBlogById(int id)
        {
            var blog = GetBlogList().Where(x=>x.Id == id).FirstOrDefault();
            return blog;
        }

        public void AddCategoriesToBlog(int blogId, int categoryId) 
        {
            _BlogData.AddCategoryToBlog(blogId, categoryId);
        }


        public void AddBlogByFile(FileUploadModel fileUploadModel)
        {

            Byte[] file;
            using (var stream = new MemoryStream())
            {
                fileUploadModel.file.CopyTo(stream);
                file = stream.ToArray();
            }

            string blogString = System.Text.Encoding.Default.GetString(file);

            
            var splitedBlog = blogString.Split(",,", StringSplitOptions.TrimEntries);


            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (var item in splitedBlog)
            {
                //Console.WriteLine(item);

                int titleIndex = item.IndexOf("=");
                if (titleIndex == -1)
                {
                    continue;
                }

                string key = item.Substring(0, titleIndex);
                string value = item.Substring(titleIndex + 1);
                dictionary.Add(key, value);

            }

            CreateBlogDTO blogDTO = new CreateBlogDTO();
            var splitedCategories = new List<string>();

            foreach (var item in dictionary)
            {
                if (item.Key.ToLower() == "title")
                {
                    blogDTO.Title = item.Value;
                }
                if (item.Key.ToLower() == "article")
                {
                    blogDTO.Article = item.Value;
                }
                if (item.Key.ToLower() == "author")
                {
                    blogDTO.Author = item.Value;
                }
                if(item.Key.ToLower() == "photoname")
                {
                    blogDTO.photoName = item.Value;
                }
                if (item.Key.ToLower() == "categories")
                {
                    splitedCategories = item.Value.Split(",", StringSplitOptions.TrimEntries).ToList();
                    
                }
            }

            AddBlog(blogDTO);

            _CategoryServices.AddCategoriesToBlog(0, splitedCategories);
        }
    }
}
