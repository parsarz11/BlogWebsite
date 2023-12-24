using WeblogApp.Data.Entities;
using WeblogApp.Model;

namespace WeblogApp.Data.Repositories.Photo
{
    public interface IPhotoFile
    {
        List<PhotoFile> DownloadPhotos();
        Task<bool> UploadPhoto(FileUploadModel photoModel);
    }
}
