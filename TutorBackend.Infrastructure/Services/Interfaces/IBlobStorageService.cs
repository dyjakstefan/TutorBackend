using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBackend.Infrastructure.Services.Interfaces
{
    public interface IBlobStorageService
    {
        Task UploadFile(MemoryStream stream, string filename, string containerId);

        Task<Stream> DownloadFile(string filename, string containerId);
    }
}
