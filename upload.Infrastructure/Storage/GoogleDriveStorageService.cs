using Microsoft.AspNetCore.Http;
using upload.Domain.Entities;
using upload.Domain.Storage;

namespace upload.Infrastructure.Storage;

public class GoogleDriveStorageService : IStorageService
{
    public string Upload(IFormFile file, User user)
    {
        throw new NotImplementedException();
    }
}
