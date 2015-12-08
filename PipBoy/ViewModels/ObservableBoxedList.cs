// Copyright (c) John and Katie Gietzen. All rights reserved.

namespace PipBoy.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using PipBoy.Protocol;

    internal class ObservableBoxedList<T> : ObservableCollection<T>
    {
        private readonly Box box;
        private readonly Func<Box, T> factory;

        public ObservableBoxedList(Box box, Func<Box, T> factory)
        {
            this.box = box;
            this.factory = factory;

            this.box.PropertyChanged += this.Box_PropertyChanged;
            this.Box_PropertyChanged(box, new PropertyChangedEventArgs("Value"));
        }

        private void Box_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Debug.Assert(e.PropertyName == "Value", "Box expected to only ever raise property changed for its 'Value' property.");

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
