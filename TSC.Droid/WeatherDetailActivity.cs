
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using TSC.Common.Models;
using TSC.Common.ViewModels;
using TSC.Common.Droid.Views;

namespace TSC.Droid
{
    [Activity(Label = "WeatherDetailActivity", ParentActivity = typeof(WeatherListActivity))]
    public class WeatherDetailActivity : BaseActivity<WeatherDetailViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            var city = Intent.GetStringExtra("city");
            this.Title = city;

            if (Intent.GetBooleanExtra("location", false))
            {
                var lat = Intent.GetDoubleExtra("lat", -99);
                var lon = Intent.GetDoubleExtra("lon", -99);
                ViewModel.Request = new LocationWeatherRequest() { Position = new Position { Latitude = lat, Longitude = lon } };
            }
            else
            {
                ViewModel.Request = new CityWeatherRequest() { City = new City { Name = city } };
            }

            ViewModel.InitAsync();

            ViewModel.WatchProperty(nameof(ViewModel.Weather), HandleWeatherChanged);

            ViewModel.WatchProperty(nameof(ViewModel.BackgroundPath), HandleBackgroundChanged);

            SetContentView(Resource.Layout.WeatherDetail);
        }


        void HandleWeatherChanged()
        {
            var descriptionLabel = FindViewById<TextView>(Resource.Id.descriptionLabel);
            descriptionLabel.Text = ViewModel.Weather.Description;

            var currentTemperatureLabel = FindViewById<TextView>(Resource.Id.currentTemperatureLabel);
            currentTemperatureLabel.Text = ViewModel.Weather.CurrentTemperature;

            var unitLabel = FindViewById<TextView>(Resource.Id.unitLabel);
            unitLabel.Text = ViewModel.TemperatureUnitString;

            var highTemperatureLabel = FindViewById<TextView>(Resource.Id.highTemperatureLabel);
            highTemperatureLabel.Text = ViewModel.HighTemperature;

            var lowTemperatureLabel = FindViewById<TextView>(Resource.Id.lowTemperatureLabel);
            lowTemperatureLabel.Text = ViewModel.LowTemperature;

            var thermometerView = FindViewById<ThermometerView>(Resource.Id.thermometerView);
            thermometerView.Temperature = (int)ViewModel.CurrentTemperature;
        }

        async void HandleBackgroundChanged()
        {
            var backgroundImageView = FindViewById<ImageView>(Resource.Id.backgroundImage);
            backgroundImageView.SetScaleType(ImageView.ScaleType.CenterCrop);

            var imageBitmap = await BitmapFactory.DecodeFileAsync(ViewModel.BackgroundPath);
            backgroundImageView.SetImageBitmap(imageBitmap);
        }
    }
}
