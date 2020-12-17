using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace ModManager.TUI.Drawables {
    class YesNoBox  : Window {
        public event EventHandler YesClicked;
        public event EventHandler NoClicked;
        Label lbl;
        Button yes;
        Button no;

        public YesNoBox(string label) : base() {
            X = Pos.Center();
            Y = Pos.Center();
            Width = Dim.Percent(40);
            Height = Dim.Percent(20);
            ColorScheme = Colors.Error;

            lbl = new(label) {
                Width = Dim.Fill(),
                Y = 1,
                TextAlignment = TextAlignment.Centered
            };

            yes = new("Y_es") {
                X = Pos.Percent(30),
                Y = 5
            };

            no = new("N_o") {
                X = Pos.Percent(60),
                Y = 5
            };

            yes.Clicked += () => {
                YesClicked?.Invoke(this, null);
            };

            no.Clicked += () => {
                NoClicked?.Invoke(this, null);
            };

            Add(lbl, yes, no);

        }
    }
}
