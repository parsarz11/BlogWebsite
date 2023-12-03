using WeblogApp.Data.Entities;
using WeblogApp.Data.Repositories.Photo;
using WeblogApp.Model;

namespace WeblogApp.Services.Photo
{
    public class PhotoServices:IPhotoServices
    {
        private readonly IPhotoFile _photoFile;

        public PhotoServices(IPhotoFile photoFile)
        {
            _photoFile = photoFile;
        }


        public PhotoFile FindPhotoById(int id)
        {
            var photos = _photoFile.DownloadPhotos();
            var filteredById = photos.Find(x => x.Id == id);

            return filteredById;
        }

        public bool PhotoUpload(FileUploadModel fileUploadModel)
        {
            bool isUpploaded = _photoFile.UploadPhoto(fileUploadModel);
            return isUpploaded;
        }
        public int FindPhotoIdByName(string name)
        {
            var photos = _photoFile.DownloadPhotos();
            var photoId = photos.Where(x=>x.FileName == name).FirstOrDefault().Id;
            return photoId;
        }
    }
}
