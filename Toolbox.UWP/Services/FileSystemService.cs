using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Portable.Services;
using Windows.Foundation.Metadata;
using Windows.Storage;

namespace Toolbox.UWP.Services
{
    public class FileSystemService : IFileSystemService
    {
        private readonly IFileStorageService documentStorage;
        private readonly IFileStorageService settingsStorage;
        private readonly IFileStorageService tempStorage;

        public FileSystemService()
        {
            var applicationSupportPath = ApplicationData.Current.LocalFolder.Path;
            var documentDirectoryPath = Path.Combine(applicationSupportPath, "Documents");

            // Note: When running in debug mode (most likely in the Simulator) we print out the
            // path to the documents directory, since it can change based on the simulator chosen.
            // Apple uses cryptic GUID's in the paths, thus this helps to find the directories 
            // quicker.
#if DEBUG
            //Logger.Debug($"Documents Directory Path: {documentDirectoryPath}");
#endif

            documentStorage = new FileStorageService(documentDirectoryPath);
            settingsStorage = new FileStorageService(Path.Combine(applicationSupportPath, "Settings"));
            tempStorage = new FileStorageService(Path.Combine(applicationSupportPath, "Temp"));
        }

        public IFileStorageService DocumentStorage => documentStorage;

        public IFileStorageService SettingsStorage => settingsStorage;

        public IFileStorageService TempStorage => tempStorage;

        public string GetTempFileName()
        {
            return Path.GetTempFileName();
        }
    }
}
