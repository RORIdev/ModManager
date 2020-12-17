using System;

using Terminal.Gui;

namespace ModManager.TUI.Drawables {
    public class OKBox : Window {
        public event EventHandler OkClicked;
        Label lbl;
        Button ok;
        public OKBox(string question) : base("TextInputBox") {

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

            ok = new("OK") {
                X = Pos.Center(),
                Y = 5
            };

            ok.Clicked += () => {
                OkClicked?.Invoke(this, null);
            };

            Add(lbl, ok);
        }
}}
