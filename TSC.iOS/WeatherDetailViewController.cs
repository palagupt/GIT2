using System;
using TSC.Common.ViewModels;
using Toolbox.Portable.Mvvm;
using UIKit;

namespace TSC.iOS
{
    public partial class WeatherDetailViewController
        : BaseViewController<WeatherDetailViewModel>
    {
        public WeatherDetailViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            ViewModel.WatchProperty(nameof(ViewModel.Weather), HandleWeatherChanged);

            ViewModel.WatchProperty(nameof(ViewModel.BackgroundPath), HandleBackgroundChanged);

            base.ViewDidLoad();
        }

        void HandleWeatherChanged()
        {
            Title = ViewModel.Weather.Name;
            DescriptionLabel.Text = ViewModel.Weather.Description;
            CurrentTemperatureLabel.Text = ViewModel.Weather.CurrentTemperature;
            UnitLabel.Text = ViewModel.TemperatureUnitString;
            HighTemperatureLabel.Text = ViewModel.HighTemperature;
            LowTemperatureLabel.Text = ViewModel.LowTemperature;
            Thermometer.Temperature = (int)ViewModel.CurrentTemperature;

            UIView.Animate(0.5, () =>
            {
                CurrentTemperatureLabel.Alpha = 1;
                UnitLabel.Alpha = 1;
            });
        }

        void HandleBackgroundChanged()
        {
            BackgroundImageView.Image = UIImage.FromFile(ViewModel.BackgroundPath);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

