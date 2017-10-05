using System;

using Foundation;
using TSC.Common.Models;
using UIKit;

namespace TSC.iOS.Cells
{
    public abstract class BaseWeatherRequestCell : UITableViewCell
    {

        protected BaseWeatherRequestCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public abstract void SetData(WeatherRequest request);

        public abstract WeatherRequest GetData();
    }
}
