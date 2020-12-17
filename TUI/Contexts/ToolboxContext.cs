using ModManager.API;
using ModManager.API.Data.DataClasses;
using ModManager.TUI.Drawables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace ModManager.TUI.Contexts {
    public class ToolboxContext {
        public Toolbox DRAW_Toolbox { get; init; }
        Button Edit { get; init; }
        Button Create { get; init; }
        Button Delete { get; init; }
        Button Load { get; init; }

        internal View Super => DRAW_Toolbox.SuperView;

        internal ModpackContext ctx;
        public ToolboxContext(ModpackContext context) {
            ctx = context;

            DRAW_Toolbox = new(
                    new(60, 0, 50, 10),
                    "Toolbox"
                );

            #region Buttons
            Edit = new("Edit") { // [ Edit ]
                Width = 8
            };

            Create = new($"Create") {
                Y = 2
            };

            Delete = new("Delete") { // [ Delete ]
                Width = 10,
                Y = 1
            };

            Load = new($"Load {context.CurrentModpack()}") {
                Y = 3
            };
            #endregion

            #region Buttons.Clicked
            Edit.Clicked += EditLogic;
            Delete.Clicked += DeleteLogic;
            Create.Clicked += CreateLogic;
            Load.Clicked += LoadLogic;
            #endregion

            ctx.DRAW_Modpacks.ItemChanged += UpdateGraphics;

            DRAW_Toolbox.Add(Edit, Delete, Create, Load);
        }

        private void LoadLogic()
            => ctx.manager.LoadModpack(ctx.CurrentModpack());
        private void CreateLogic() {
            var (name, version, loader) = ("", "", ModLoader.NULL);
            Modpack pack;

            TextCheckBox tcb = new("Please select the modloader", "FABRIC", "FORGE");
            Super.Add(tcb);
            tcb.OkClicked += (_, i) => {
                loader = (ModLoader)i;
                tcb.SuperView.Remove(tcb);
                var zip = ZipUtils.ZipCurrentModpack(ctx.manager.McPath);
                pack = new(loader, version, name, zip);
                ctx.Modpacks.Add(pack);
                ctx.manager.UpdateIndex();
            };

            TextInputBox tib2 = new("Please type: Minecraft Version");
            Super.Add(tib2);
            tib2.SetFocus();
            tib2.OkClicked += (_, str) => {
                version = str;
                Super.Remove(tib2);
            };

            TextInputBox tib = new("Please type : Name of modpack");
            Super.Add(tib);
            tib.SetFocus();
            tib.OkClicked += (_, str) => {
                name = str;
                Super.Remove(tib);
            };
        }
        private void DeleteLogic() {
            var pack = ctx.CurrentModpack();
            YesNoBox check = new($"Do you want to delete : {pack}");
            Super.Add(check);
            check.YesClicked += (_,_) => {
                Super.Remove(check);
                File.Delete(pack.resource.Path);
                ctx.manager.Packs.Remove(pack);
                ctx.manager.UpdateIndex();
                ctx.DRAW_Modpacks.Refresh();
                OKBox ok = new($"Sucessfully deleted {pack}");
                Super.Add(ok);
                ok.OkClicked += (_, _) => Super.Remove(ok);
            };
            check.NoClicked += (_, _) => {
                Super.Remove(check);
            };
        }
        private void EditLogic()
        {
        }

        private void UpdateGraphics(object sender, ListViewItemEventArgs e) {
            Load.Text = $"Load {ctx.CurrentModpack()}";
        }

    }
}
