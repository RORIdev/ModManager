using ModManager.API;
using ModManager.API.Data.DataClasses;
using ModManager.TUI.Drawables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModManager.TUI.Contexts {
    public class ModpackContext {
        public Func<Modpack> CurrentModpack { get; init; }
        public List<Modpack> Modpacks { get; init;  }
        public ScrollableList<Modpack> DRAW_Modpacks { get; init; }

        internal Manager manager;
        public ModpackContext(Manager manager) {
            this.manager = manager;

            DRAW_Modpacks = new(
                    manager.Packs,
                    new(0, 0, 50, 10),
                    "Modpacks Available"
                );

            Modpacks = manager.Packs ?? new();

            CurrentModpack = () => Modpacks[DRAW_Modpacks.SelectedItem];
        }

    }
}
