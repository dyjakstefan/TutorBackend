using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBackend.Infrastructure.Settings
{
    public class BlobStorageSettings
    {
        public const string BlobStorage = "BlobStorage";

        public string ConnectionString { get; set; }
    }
}
