﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeblogApp.Data.Entities;
using WeblogApp.Data.Repositories.Category;
using WeblogApp.Services.Blog;
using WeblogApp.Services.category;

namespace WeblogApp.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;
        private readonly IBlogServices _blogServices;
        public CategoryController(ICategoryServices categoryData, IBlogServices blogServices)
        {

            _categoryServices = categoryData;
            _blogServices = blogServices;
        }


        [HttpGet]
        public IActionResult Getcategories()
        {

            return Ok(_categoryServices);
        }

        [HttpPost]
        public IActionResult AddCategory(Category blogCategory) 
        {
            _categoryServices.AddCategory(blogCategory);

            return Ok();
        }

        [HttpPost]
        public IActionResult EditCategory(Category blogCategory)
        {
            _categoryServices.EditCategory(blogCategory);
            return Ok();
        }
        [HttpGet]
        public IActionResult DeleteCategory(int id)
        {
            _categoryServices.DeleteCategory(id);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetCategoryById(int id)
        {
            var result = _categoryServices.FindCategoryById(id);
           return Ok(result);
        }

        [HttpPost]
        public IActionResult AddCategoriesToBlog(List<string> categories,int blogId = 0)
        {

            return Ok();
        }

        [HttpGet]
        public IActionResult FindCategoriesByBLog(int blogId)
        {
            var result = _categoryServices.FindCategoryByBlogs(blogId);
            return Ok(result);
        }
    }
}