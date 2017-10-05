using System;

using Foundation;
using TSC.Common.Models;
using UIKit;

namespace TSC.iOS.Cells
{
    public partial class CityCell : BaseWeatherRequestCell
    {
        public static readonly NSString Key = new NSString("CityCell");

        CityWeatherRequest weatherRequest;

        static CityCell()
        {

        }

        protected CityCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override WeatherRequest GetData()
        {
            return weatherRequest;
        }

        public override void SetData(WeatherRequest request)
        {
            weatherRequest = request as CityWeatherRequest;
            CityNameLabel.Text = weatherRequest.City.Name;
        }

    }
}
