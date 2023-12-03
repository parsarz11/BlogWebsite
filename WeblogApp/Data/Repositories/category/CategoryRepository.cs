using System.Reflection.Metadata;
using WeblogApp.BlogData.Context;
using WeblogApp.Data.Entities;
using WeblogApp.Data.Repositories.Blog;
using WeblogApp.Data.Repositories.Category;
using WeblogApp.Exceptions;

namespace WeblogApp.Data.Repositories.category
{
    public class CategoryRepository : ICategoriesData
    {
        private readonly BlogDatabaseContext _blogDbContext;

        public CategoryRepository(BlogDatabaseContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        public void AddCategory(Entities.Category category)
        {

            if (category == null)
            {
                throw new Exception("data is null");
            }
            else
            {
                _blogDbContext.Categories.Add(category);
                _blogDbContext.SaveChanges();
            }
        }

        public List<Entities.Category> GetCategory()
        {
            var allCategories = _blogDbContext.Categories.ToList();

            if (allCategories.Count() == 0)
            {
                throw new NotFoundException("there is no Category");
            }
            else
            {
               
                return allCategories;
            }


        }

        public void RemoveCategory(int categoryId)
        {
            var SelectedCategory = _blogDbContext.Categories.Where(x=> x.Id == categoryId).FirstOrDefault();

            if (SelectedCategory != null)
            {
                _blogDbContext.Categories.Remove(SelectedCategory);
                _blogDbContext.SaveChanges();
            }
            else
            {
                throw new Exception("In Remove category Selected item is null");
            }

        }

        public void UpdateCategory(Entities.Category category)
        {
            
            _blogDbContext.Categories.Update(category);
            _blogDbContext.SaveChanges();
        }

        Entities.Category ICategoriesData.GetCategoryById(int categoryId)
        {
            return _blogDbContext.Categories.Where(x => x.Id == categoryId).FirstOrDefault();

        }





        public void AddblogCategories(int blogId, List<string> categories)
        {
            

            foreach (var item in categories)
            {
                int categoryId = _blogDbContext.Categories.Where(x => x.Name == item).Select(y => y.Id).FirstOrDefault();

                BlogCategory blogCategory = new BlogCategory() 
                { 
                    blogId = blogId,
                    categoryId = categoryId
                };
                
                _blogDbContext.blogsCategories.Add(blogCategory);
                _blogDbContext.SaveChanges();
            }
            
        }

    }
}
