using upload.Application.UseCases.Users.UploadProfilePhoto;
using upload.Domain.Storage;
using upload.Infrastructure.Storage;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Util.Store;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<IUploadProfilePhotoUseCase, UploadProfilePhotoUseCase>();
builder.Services.AddScoped<IStorageService>(option => {

    var clientId = builder.Configuration.GetValue<string>("CloudStorage:ClientId");
    var clientSecret = builder.Configuration.GetValue<string>("CloudStorage:ClientSecret");

    var apiCodeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
    {
        ClientSecrets = new ClientSecrets
        {
            ClientId = clientId,
            ClientSecret = clientSecret
        },
        Scopes = [Google.Apis.Drive.v3.DriveService.Scope.Drive],
        DataStore = new FileDataStore("YoutubeFlutter"),
    });
    return new GoogleDriveStorageService(apiCodeFlow);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();