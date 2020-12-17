using NStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace ModManager.TUI.Drawables {
    public class Toolbox : Window {
        static Dictionary<Button,bool> buttons = new();
        public Toolbox(Rect frame, ustring title = null) : base(frame, title) {
            
        }
        public void AddButton(Button button) {
            buttons.Add(button,false);
            Add(button);
        }
        public void AddButtons(params Button[] bts) {
            foreach(var button in bts) {
                buttons.Add(button, false);
                Add(button);
            }
        }
        public void RemoveButton(Button button) {
            buttons.Remove(button);
            Remove(button);
        }
        public void HideButton(Button button) {
            if (!buttons.ContainsKey(button))
                AddButton(button);
            buttons[button] = true;
            Remove(button);
        }
        public void ShowButton(Button button) {
            if (!buttons.ContainsKey(button))
                AddButton(button);
            buttons[button] = false;
        }
    }
}
