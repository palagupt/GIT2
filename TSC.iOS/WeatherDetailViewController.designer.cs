// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace TSC.iOS
{
    [Register ("WeatherDetailViewController")]
    partial class WeatherDetailViewController
    {
        [Outlet]
        UIKit.UIImageView BackgroundImageView { get; set; }


        [Outlet]
        UIKit.UILabel CurrentTemperatureLabel { get; set; }


        [Outlet]
        UIKit.UILabel DescriptionLabel { get; set; }


        [Outlet]
        UIKit.UILabel HighTemperatureLabel { get; set; }


        [Outlet]
        UIKit.UILabel LocationLabel { get; set; }


        [Outlet]
        UIKit.UILabel LowTemperatureLabel { get; set; }


        [Outlet]
        UIKit.UILabel UnitLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        TSC.Common.iOS.Views.ThermometerView Thermometer { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (BackgroundImageView != null) {
                BackgroundImageView.Dispose ();
                BackgroundImageView = null;
            }

            if (CurrentTemperatureLabel != null) {
                CurrentTemperatureLabel.Dispose ();
                CurrentTemperatureLabel = null;
            }

            if (DescriptionLabel != null) {
                DescriptionLabel.Dispose ();
                DescriptionLabel = null;
            }

            if (HighTemperatureLabel != null) {
                HighTemperatureLabel.Dispose ();
                HighTemperatureLabel = null;
            }

            if (LowTemperatureLabel != null) {
                LowTemperatureLabel.Dispose ();
                LowTemperatureLabel = null;
            }

            if (Thermometer != null) {
                Thermometer.Dispose ();
                Thermometer = null;
            }

            if (UnitLabel != null) {
                UnitLabel.Dispose ();
                UnitLabel = null;
            }
        }
    }
}