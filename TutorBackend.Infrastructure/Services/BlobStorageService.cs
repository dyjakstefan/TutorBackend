using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using TutorBackend.Infrastructure.Settings;
using TutorBackend.Infrastructure.Services.Interfaces;

namespace TutorBackend.Infrastructure.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobStorageSettings settings;

        public BlobStorageService(IOptions<BlobStorageSettings> settings)
        {
            this.settings = settings.Value;
        }

        public async Task UploadFile(MemoryStream stream, string filename, string containerId)
        {
            var blobServiceClient = new BlobServiceClient(settings.ConnectionString);

            var container = blobServiceClient.GetBlobContainerClient(containerId);
            await container.CreateIfNotExistsAsync();

            var blobClient = container.GetBlobClient(filename);
            stream.Position = 0;
            await blobClient.UploadAsync(stream, true);
        }

        public async Task<Stream> DownloadFile(string filename, string containerId)
        {
            var blobServiceClient = new BlobServiceClient(settings.ConnectionString);

            var container = blobServiceClient.GetBlobContainerClient(containerId);

            if (!await container.ExistsAsync())
            {
                return null;
            }

            var blob = container.GetBlobClient(filename);

            if (!await blob.ExistsAsync())
            {
                return null;
            }

            var memoryStream = new MemoryStream();
            await blob.DownloadToAsync(memoryStream);

            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}
