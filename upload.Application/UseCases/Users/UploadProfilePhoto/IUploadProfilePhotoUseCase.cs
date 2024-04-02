using Microsoft.AspNetCore.Http;

namespace upload.Application.UseCases.Users.UploadProfilePhoto
{
    public interface IUploadProfilePhotoUseCase
    {
        public void Execute(IFormFile file);
    }
}