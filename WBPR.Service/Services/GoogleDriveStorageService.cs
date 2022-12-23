using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBPR.Service.Interfaces;
using WBPR.Service.Models.Common;
using WBPR.Service.Models.Response;

namespace WBPR.Service.Services
{
    public class GoogleDriveStorageService : IStorageService
    {
        private readonly GoogleConfig googleConfig;
        private readonly IGoogleAuthProvider auth;

        public GoogleDriveStorageService(IGoogleAuthProvider auth)
        {
            //this.googleConfig = googleConfig;
            this.auth = auth;
        }

        private async Task<DriveService> CreateDriveService()
        {
            GoogleCredential credential = await auth.GetCredentialAsync();
            //UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
            //    new ClientSecrets
            //    {
            //        ClientId = googleConfig.ClientId,
            //        ClientSecret = googleConfig.ClientSecret,
            //    },
            //    new[] {
            //        DriveService.Scope.Drive,
            //        DriveService.Scope.DriveFile
            //    },
            //    "user",//如果只有一人, 可以使用任何固定字串, 例如: "user"
            //    CancellationToken.None,
            //    new FileDataStore("Drive.Api.Auth.Store")//用來儲存 Token 的目錄
            //).Result;

            return new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential
            });
        }

        public Task<MessageModel<bool>> Delete(string filepath, string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task<MessageModel<StorageGetRes>> Get(string filepath, string fileName)
        {
            string folderId = await GetFolderId(filepath);
            if (folderId != null)
            {
                var service = await CreateDriveService();
                var filesReq = service.Files.List();
                filesReq.Q = string.Format("parents in '{0}'", folderId);
                var files = await filesReq.ExecuteAsync();
                var target = files.Files.FirstOrDefault(x => x.Name == fileName);
                if (target != null)
                {
                    var req = service.Files.Get(target.Id);
                    req.MediaDownloader.ProgressChanged +=
                        progress =>
                        {
                            switch (progress.Status)
                            {
                                case DownloadStatus.Downloading:
                                    {
                                        Console.WriteLine(progress.BytesDownloaded);
                                        break;
                                    }
                                case DownloadStatus.Completed:
                                    {
                                        Console.WriteLine("Download complete.");
                                        break;
                                    }
                                case DownloadStatus.Failed:
                                    {
                                        Console.WriteLine("Download failed.");
                                        break;
                                    }
                            }
                        };
                    using MemoryStream ms = new MemoryStream();
                    req.Download(ms);
                    return new MessageModel<StorageGetRes>()
                    {
                        Success = true,
                        Msg = "Success, fileId: " + target.Id,
                        Data = new StorageGetRes()
                        {
                            Name = fileName,
                            Data = ms.ToArray()
                        }
                    };
                }
            }
            return new MessageModel<StorageGetRes>()
            {
                Success = false,
                Msg = "File not exist",
            };
        }

        public async Task<MessageModel<bool>> Save(string filepath, string fileName, byte[] bytes)
        {
            filepath = "WPBR_DATA";
            fileName = "WPBR_DATA_202212";

            string folderId = await GetFolderId(filepath);
            if (folderId == null)
            {
                folderId = await CreateFolder(filepath);
            }

            var service = await CreateDriveService();

            // Upload file photo.jpg on drive.
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = fileName,
                Parents = new List<string>
                    {
                        folderId
                    }
            };
            FilesResource.CreateMediaUpload request;
            // Create a new file on drive.
            using var stream = new MemoryStream();
            // Create a new file, with metadata and stream.
            request = service.Files.Create(fileMetadata, stream, "text/plain");
            request.Fields = "id";
            request.Upload();
            var file = request.ResponseBody;
            return new MessageModel<bool>
            {
                Success = true,
                Msg = "Yes:" + file.Id,
                Data = true
            };

        }

        private async Task<string> GetFolderId(string filepath)
        {
            var service = await CreateDriveService();
            var filesReq = service.Files.List();
            filesReq.Q = "mimeType = 'application/vnd.google-apps.folder'";
            var files = await filesReq.ExecuteAsync();
            var folder = files.Files.SingleOrDefault(x => x.Name == filepath);
            return folder == null ? null : folder.Id;
        }

        private async Task<string> CreateFolder(string filepath)
        {
            try
            {
                var service = await CreateDriveService();
                // File metadata
                var folderMetadata = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = filepath,
                    MimeType = "application/vnd.google-apps.folder"
                };
                // Create a new folder on drive.
                var folerReq = service.Files.Create(folderMetadata);
                folerReq.Fields = "id";
                var folder = folerReq.Execute();
                return folder.Id;
            }
            catch (Exception e)
            {
                // TODO(developer) - handle error appropriately
                if (e is AggregateException)
                {
                    Console.WriteLine("Credential Not found");
                }
                else
                {
                    throw;
                }
            }
            return null;
        }
    }
}
