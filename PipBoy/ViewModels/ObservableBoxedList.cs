using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using PipBoy.Protocol;

namespace PipBoy.ViewModels
{
    public class ObservableBoxedList<T> : ObservableCollection<T>
    {
        private readonly Box box;
        private Dictionary<int, T> ids = new Dictionary<int, T>();
        private readonly Func<Box, T> factory;

        public ObservableBoxedList(Box box, Func<Box, T> factory)
        {
            this.box = box;
            this.factory = factory;

            this.box.PropertyChanged += Box_PropertyChanged;
            this.Box_PropertyChanged(box, new PropertyChangedEventArgs("Value"));
        }

        private void Box_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Debug.Assert(e.PropertyName == "Value");

            this.Clear();
            var newValue = this.box.Value as List<Box>;
            if (newValue == null)
            {
                return;
            }

            foreach (var box in newValue)
            {
                this.Add(this.factory(box));
            }
        }
    }
}
