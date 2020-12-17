using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace ModManager.TUI.Drawables {
    public class ScrollableList<T> : Window {
        public ListView data;
        internal List<T> _source;
        public int SelectedItem => data.SelectedItem;
        public event EventHandler<ListViewItemEventArgs> ItemChanged;
        public ScrollableList(List<T> DataSource, Rect frame ,string Title = null) : base(frame, Title) {
            _source = DataSource;
            data = new(getInnerBound(), DataSource);

            data.SelectedItemChanged += (e) => {
                ItemChanged?.Invoke(this, e);
            };

            Add(data);
        }

        public void Refresh()
            => data.SetSource(_source);
        internal Rect getInnerBound()
            => new(Bounds.Location, new(Bounds.Width - 1, Bounds.Height - 1));
    }
}
