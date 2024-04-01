using Microsoft.AspNetCore.Http;
using upload.Domain.Entities;

namespace upload.Domain.Storage;

public interface IStorageService
{
    string Upload(IFormFile file, User user);

}
