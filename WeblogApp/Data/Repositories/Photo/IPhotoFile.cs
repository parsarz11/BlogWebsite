using WeblogApp.Data.Entities;
using WeblogApp.Model;

namespace WeblogApp.Data.Repositories.Photo
{
    public interface IPhotoFile
    {
        public List<PhotoFile> DownloadPhotos();
        public bool UploadPhoto(FileUploadModel photoModel);
    }
}
