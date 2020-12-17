using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace ModManager.TUI.Drawables {
    public class TextInputBox : Window {
        public event EventHandler<string> OkClicked;
        Label lbl;
        TextField input;
        Button ok;
        public TextInputBox(string question) : base("TextInputBox") {
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

            input = new() {
                Width = Dim.Fill(),
                Y = 3
            };

            ok = new("OK") {
                X = Pos.Center(),
                Y = 5
            };

            ok.Clicked += () => {
                OkClicked?.Invoke(this,input.Text.ToString());
            };

            input.KeyUp += (k) => {
                if (k.KeyEvent.Key == Key.Enter) {
                    OkClicked?.Invoke(this, input.Text.ToString());
                }
            };

            Add(lbl, input, ok);
        }

        public new void SetFocus()
            => input.SetFocus();
    }
}
