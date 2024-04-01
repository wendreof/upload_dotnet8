using FileTypeChecker;
using Microsoft.AspNetCore.Http;
using upload.Domain.Entities;
using upload.Domain.Storage;

namespace upload.Application.UseCases.Users.UploadProfilePhoto
{
    public class UploadProfilePhotoUseCase
    {
        private readonly IStorageService _storageService;
        public  UploadProfilePhotoUseCase(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public void Execute(IFormFile file)
        {
            var fileStream = file.OpenReadStream();
            var isImage = FileTypeValidator.IsImage(fileStream);

            if (!isImage)
                throw new Exception("File is not an image");

            _storageService.Upload(file, GetUserFromDatabase());
        }

        private static User GetUserFromDatabase() => new()
        {
            Id = 1,
            Name = "John Doe",
            Email = "teste@email.com"
        };
    }    
}