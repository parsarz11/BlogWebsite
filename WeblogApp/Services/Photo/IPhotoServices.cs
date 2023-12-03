using WeblogApp.Data.Entities;
using WeblogApp.Model;

namespace WeblogApp.Services.Photo
{
    public interface IPhotoServices
    {
        public bool PhotoUpload(FileUploadModel fileUploadModel);
        public PhotoFile FindPhotoById(int id);
        public int FindPhotoIdByName(string name);
    }
}
