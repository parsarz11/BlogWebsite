using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeblogApp.BlogData.Entities;
using WeblogApp.Data.Entities;
using WeblogApp.Data.Repositories.Photo;
using WeblogApp.Model;
using WeblogApp.Services.Blog;
using WeblogApp.Services.Photo;
using static System.Net.WebRequestMethods;

namespace WeblogApp.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class BlogController : ControllerBase
    {

        private readonly IBlogServices _blogServices;
        private readonly IPhotoServices _photoServices;
        public BlogController(IBlogServices blogServices, IPhotoServices photoFile)
        {
            _blogServices = blogServices;
            this._photoServices = photoFile;
        }


        [HttpGet]
        public IActionResult AllBlogs() 
        {
            var blogs = _blogServices.GetBlogList();
            return Ok(blogs);
        }




        [HttpPost]
        public IActionResult UpdateBlog(BlogEntity blog)
        {
            _blogServices.UpdateBlog(blog);
            return Ok();
        }


        [HttpGet]
        public IActionResult DeleteBlog(int id)
        {
            _blogServices.DeleteBlog(id);
            return Ok();
        }


        [HttpPost]
        public IActionResult AddBLog(BlogDTO blog)
        {

            _blogServices.AddBlog(blog);
            return Ok();
        }



        [HttpPost]
        public async Task<IActionResult> UploadPhoto([FromForm]FileUploadModel photo)
        {
            bool isUploaded = _photoServices.PhotoUpload(photo);
            return Ok(isUploaded);
        }

        [HttpGet]
        public IActionResult DownloadPhotoById(int id)
        {
            var photo = _photoServices.FindPhotoById(id);
            var content = new System.IO.MemoryStream(photo.Photo);

            return File(photo.Photo, "image/jpeg");
        }

        

        [HttpGet]
        public IActionResult GetBlogById(int id)
        {
            return Ok(_blogServices.FindBlogById(id));

        }
        
    }
}
