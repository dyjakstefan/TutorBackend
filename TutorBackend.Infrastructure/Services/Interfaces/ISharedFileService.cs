using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorBackend.Core.Dto;

namespace TutorBackend.Infrastructure.Services.Interfaces
{
    public interface ISharedFileService
    {
        Task<Guid> UploadFile(MemoryStream stream, string fileName, string username, Guid userId);

        Task<FileDto> DownloadFile(Guid fileId, Guid userId);
    }
}
