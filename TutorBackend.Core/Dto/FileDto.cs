using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBackend.Core.Dto
{
    public class FileDto
    {
        public Stream Stream { get; set; }

        public string FileName { get; set; }

        public string ContentType
        {
            get
            {
                var parts = FileName.Split(".");
                return mimeTypes[parts[^1]];
            }

        }

        private readonly Dictionary<string, string> mimeTypes = new()
        {  
            {"txt", "text/plain"},  
            {"pdf", "application/pdf"},  
            {"doc", "application/vnd.ms-word"},  
            {"docx", "application/vnd.ms-word"},  
            {"xls", "application/vnd.ms-excel"},  
            {"xlsx", "application/vnd.openxmlformats  officedocument.spreadsheetml.sheet"},  
            {"png", "image/png"},  
            {"jpg", "image/jpeg"},  
            {"jpeg", "image/jpeg"},  
            {"gif", "image/gif"},  
            {"csv", "text/csv"}  
        };
    }
}
