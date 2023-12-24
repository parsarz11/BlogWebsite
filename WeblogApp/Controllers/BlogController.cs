using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeblogApp.BlogData.Entities;
using WeblogApp.Data.Entities;
using WeblogApp.Data.Repositories.Photo;
using WeblogApp.Model;
using WeblogApp.Services.Blog;
using WeblogApp.Services.Photo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using WeblogApp.Model.DTOs;
using System.Globalization;

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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult UpdateBlog(UpdateBlog blog)
        {


            _blogServices.UpdateBlog(blog);
            return Ok();
        }


        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult DeleteBlog(int id)
        {
            _blogServices.DeleteBlog(id);
            return Ok();
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult AddBLog(CreateBlogDTO blog)
        {

            _blogServices.AddBlog(blog);
            return Ok();
        }



        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult UploadPhoto([FromForm]FileUploadModel photo)
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

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult AddBlogByFile([FromForm]FileUploadModel uploadModel)
        {
            
            _blogServices.AddBlogByFile(uploadModel);
            return Ok();
        }
    }
}
