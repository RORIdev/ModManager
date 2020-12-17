using NStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace ModManager.TUI.Drawables {
    public class TextCheckBox : Window {
        public event EventHandler<int> OkClicked;
        Label lbl;
        RadioGroup input;
        Button ok;
        public TextCheckBox(string question, params ustring[] labels) : base("TextCheckBox") {
            var str = "";

            X = Pos.Center();
            Y = Pos.Center();
            Width = Dim.Percent(40);
            Height = Dim.Percent(20);
            ColorScheme = Colors.Menu;
            lbl = new(question) {
                Width = Dim.Fill(),
                Y = 1,
                TextAlignment = TextAlignment.Centered
            };

            input = new(labels) {
                Width = Dim.Fill(),
                Y = 3
            };

            ok = new("OK") {
                X = Pos.Center(),
                Y = 5
            };

            ok.Clicked += () => {
                OkClicked?.Invoke(this, input.SelectedItem);
            };

            Add(lbl, input, ok);
        }

        public new void SetFocus()
            => input.SetFocus();
    }
}
