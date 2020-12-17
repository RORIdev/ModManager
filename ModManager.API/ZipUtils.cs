using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using ModManager.API.Data.DataClasses;
using System;
using System.IO;
using System.Linq;

namespace ModManager.API {
    public static class ZipUtils {
        public static ZipResource ZipCurrentModpack(string minecraftPath) {
            var managerFolder = $"{minecraftPath}/modmanager";
            var modsFolder = $"{minecraftPath}/mods";
            var configFolder = $"{minecraftPath}/config";

            var filepath = $"{managerFolder}/{GenerateGibberish(20)}.zip";


            if (!Directory.Exists(modsFolder))
                return null;

            if (!Directory.Exists(configFolder))
                Directory.CreateDirectory(configFolder);

            if (!Directory.Exists(managerFolder))
                Directory.CreateDirectory(managerFolder);

            using var zipFs = new FileStream(path: filepath, FileMode.OpenOrCreate);
            using var zip = new ZipOutputStream(zipFs);

            RecursiveCopy(configFolder,zip);
            RecursiveCopy(modsFolder,zip);

            return new(filepath);
        }

        public static void UnzipIntoDir(string zipPath, string folderPath) {
            using var fs = new FileStream(zipPath, FileMode.Open);
            using var data = new ZipFile(fs);
            foreach (ZipEntry zipEntry in data) {
                if (!zipEntry.IsFile) {
                    continue;
                }

                string entryFileName = zipEntry.Name;

                byte[] buffer = new byte[4096];
                var zipStream = data.GetInputStream(zipEntry);

                string fullZipToPath = Path.Combine(folderPath, entryFileName);
                string directoryName = Path.GetDirectoryName(fullZipToPath);

                if (directoryName.Length > 0) {
                    Directory.CreateDirectory(directoryName);
                }

                using var streamWriter = File.Create(fullZipToPath);
                StreamUtils.Copy(zipStream, streamWriter, buffer);

            }
        }

        public static string GenerateGibberish(int length) {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static void RecursiveCopy(string folderDir, ZipOutputStream zipOutputStream , string parent = "" ) {
            var buffer = new byte[4096];
            var folder = Directory.CreateDirectory(folderDir);
            string combinedParentName;

            if (string.IsNullOrWhiteSpace(parent)) {
                combinedParentName = folder.Name;
            } else {
                combinedParentName = $"{parent}/{folder.Name}";
            }

            foreach (var file in folder.GetFiles()) {
                zipOutputStream.PutNextEntry(new($"{combinedParentName}/{file.Name}"));
                using var fs = file.OpenRead();
                StreamUtils.Copy(fs, zipOutputStream, buffer);
            }

            foreach(var subfolder in folder.GetDirectories()) {
                RecursiveCopy(subfolder.FullName, zipOutputStream, combinedParentName);
            }
        }

        /// <summary>
        /// Tis rm -rf. Use with caution, this shit has no safety whatsoever.
        /// </summary>
        /// <param name="folderDir"></param>
        public static void RecursiveDelete(string folderDir) {
            DirectoryInfo folder = new(folderDir);
            foreach(var subfolder in folder.GetDirectories()) {
                RecursiveDelete(subfolder.FullName);
            }

            foreach(var file in folder.GetFiles()) {
                file.Delete();
            }

            folder.Delete();
        }
    }
}
