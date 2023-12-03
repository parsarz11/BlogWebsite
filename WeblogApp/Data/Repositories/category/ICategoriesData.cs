using WeblogApp.Data.Entities;
using WeblogApp.Data.Entities;
namespace WeblogApp.Data.Repositories.Category
{
    public interface ICategoriesData
    {
        public List<Entities.Category> GetCategory();
        public void AddCategory(Entities.Category category);
        public void RemoveCategory(int categoryId);
        public void UpdateCategory(Entities.Category category);
        public Entities.Category GetCategoryById(int categoryId);

        public void AddblogCategories(int blogId, List<string> categories);
    }
}
