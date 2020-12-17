using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using ModManager.API.Data.DataClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModManager.API {
    public class Manager {

        public List<Modpack> Packs = new();

        public string McPath;

        public Manager(string minecraftPath)
            => McPath = minecraftPath;
        public void LoadIndex() {
            var path = $"{McPath}/modmanager/index.json";
            var file = new FileInfo(path);
            if (!file.Exists) {
                using var stream = file.CreateText();
                stream.Write(JsonConvert.SerializeObject(Packs));
            }
            var data = File.ReadAllText(path);
            Packs = JsonConvert.DeserializeObject<List<Modpack>>(data);
        }

        public void UpdateIndex() {
            var path = $"{McPath}/modmanager/index.json";
            File.WriteAllText(path,JsonConvert.SerializeObject(Packs, Formatting.Indented));
        }

        public void LoadModpack(Modpack modpack) {
            ZipUtils.RecursiveDelete($"{McPath}/mods");
            ZipUtils.RecursiveDelete($"{McPath}/config");
            ZipUtils.UnzipIntoDir(modpack.resource.Path, McPath);
        }
    }
}
