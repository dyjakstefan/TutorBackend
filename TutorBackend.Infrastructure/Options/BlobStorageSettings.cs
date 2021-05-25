using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBackend.Infrastructure.Options
{
    public class BlobStorageSettings
    {
        public const string BlobStorage = "BlobStorage";

        public string ConnectionString { get; set; }
    }
}
