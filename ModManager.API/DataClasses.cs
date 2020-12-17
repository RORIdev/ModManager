using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModManager.API.Data.DataClasses {
    public record Modpack (ModLoader modLoader, string version, string name, ZipResource resource) {
        public override string ToString() =>
            $"{name} - {version} ({modLoader})";
    }
    public enum ModLoader {
        FABRIC, FORGE, NULL
    }
    public record ZipResource (string Path);

}
