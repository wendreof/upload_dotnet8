using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Drive.v3;
using Microsoft.AspNetCore.Http;
using upload.Domain.Entities;
using upload.Domain.Storage;

namespace upload.Infrastructure.Storage;

public class GoogleDriveStorageService : IStorageService
{
    private readonly GoogleAuthorizationCodeFlow _auth;

    public GoogleDriveStorageService(GoogleAuthorizationCodeFlow googleAuthorizationCodeFlow)
    {
        _auth = googleAuthorizationCodeFlow;
    }

    public string Upload(IFormFile file, User user)
    {
        var credential = new UserCredential(_auth, user.Email, new TokenResponse(){
            AccessToken = user.AccessToken,
            RefreshToken = user.RefreshToken,
        });

        var service = new DriveService(new Google.Apis.Services.BaseClientService.Initializer(){
             ApplicationName = "YoutubeFlutter",
             HttpClientInitializer = credential
        });

        var command = service.Files.Create(new Google.Apis.Drive.v3.Data.File
        {
            Name = file.FileName,
            MimeType = file.ContentType,
        }, file.OpenReadStream(), file.ContentType);

        var response = command.Upload();
        command.Fields = "id";

        if(response.Status is not Google.Apis.Upload.UploadStatus.Completed or Google.Apis.Upload.UploadStatus.NotStarted)
            throw response.Exception;

        return command.ResponseBody.Id;
    }
}
