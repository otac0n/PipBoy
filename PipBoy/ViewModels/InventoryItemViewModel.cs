using System.Collections.Generic;
using PipBoy.Protocol;
using ReactiveUI;

namespace PipBoy.ViewModels
{
    public class InventoryItemViewModel : ReactiveObject
    {
        private readonly ObservableAsPropertyHelper<string> text;

        public InventoryItemViewModel(Box box)
        {
            var properties = box.WhenAny(x => x.Value, x => x.Value as Dictionary<string, Box>);
            properties.ToBoxedProperty(this, x => x.Text, out text, b => b.Value as string, "text");
        }

        public string Text => this.text.Value;
    }
}
