using ModManager.API;

using ModManager.TUI.Contexts;

using Terminal.Gui;

namespace ModManager {
    class Program {
        static void Main(string[] args) {
            Manager manager = new(@"C:\Users\roridev\AppData\Roaming\.minecraft");
            manager.LoadIndex();

            Application.Init();

            var top = Application.Top;
            Window window = new("ModManager") {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            ModpackContext mctx = new(manager);
            ToolboxContext tctx = new(mctx);

            window.Add(mctx.DRAW_Modpacks, tctx.DRAW_Toolbox);

            top.Add(window);
            Application.Run();
        }
    }
}
