using System.Xml.Linq;
using WeblogApp.BlogData.Context;
using WeblogApp.Data.Entities;
using WeblogApp.Model;

namespace WeblogApp.Data.Repositories.Photo
{
    public class PhotoRepository : IPhotoFile
    {
        private readonly BlogDatabaseContext _blogDbContext;

        public PhotoRepository(BlogDatabaseContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        public List<PhotoFile> DownloadPhotos()
        {
            var photoList = _blogDbContext.PhotoFiles.ToList();
            return photoList;
        }

        public bool UploadPhoto(FileUploadModel photoModel)
        {
            var photos = DownloadPhotos();
            bool isExist = photos.Any(x => x.FileName == photoModel.FileName);
            if (isExist)
            {
                return true;
            }

            var photoFile = new PhotoFile()
            {
                FileName = photoModel.FileName,
            };

            using (var stream = new MemoryStream())
            {
                photoModel.file.CopyTo(stream);
                photoFile.Photo = stream.ToArray();
            }

            _blogDbContext.PhotoFiles.AddAsync(photoFile);

            _blogDbContext.SaveChangesAsync();
            
            return true;
            
        }
    }
}
