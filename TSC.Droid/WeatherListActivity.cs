
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TSC.Common;
using TSC.Common.Services;
using Toolbox.Droid.Services;
using Toolbox.Portable.Services;
using Android.Text;
using TSC.Common.ViewModels;
using TSC.Common.Models;
using System.Threading.Tasks;

namespace TSC.Droid
{
    [Activity(Label = "WeatherListActivity", MainLauncher = true, Theme = "@style/MyTheme", Icon = "@mipmap/icon")]
    public class WeatherListActivity : BaseActivity<WeatherListViewModel>
    {
        ListView _listView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var p = SQLite.SQLiteException.New(SQLite.SQLite3.Result.Row, "");

            SetContentView(Resource.Layout.WeatherList);

            _listView = FindViewById<ListView>(Resource.Id.WeatherListView);
            _listView.ItemClick += OnListViewItemClick;

            Bootstrap.Begin(() => new LocationService(this), () => new AlertService(this), () =>
            {
                FileSystem.RegisterService(new FileSystemService());
                Database.Initialize();
            });

            _listView.ChoiceMode = ChoiceMode.Single;

            var arrayAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, ViewModel.WeatherRequests);
            _listView.Adapter = arrayAdapter;

            var favButton = FindViewById<Button>(Resource.Id.addFavButton);
            favButton.Click += (sender, e) =>
            {
                ViewModel.AddFavoriteCommand.Execute(null);
            };

            ViewModel.ReloadAction = () =>
            {
                RunOnUiThread(() => _listView.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, ViewModel.WeatherRequests));
            };

            Task.Run(async () => await ViewModel.InitAsync());
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _listView.ItemClick -= OnListViewItemClick;
        }

        void OnListViewItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var intent = new Intent(this, typeof(WeatherDetailActivity));
            var city = ViewModel.WeatherRequests[e.Position];

            if (city is LocationWeatherRequest)
            {
                var location = city as LocationWeatherRequest;
                intent.PutExtra("location", true);
                intent.PutExtra("lat", location.Position.Latitude);
                intent.PutExtra("lon", location.Position.Longitude);
            }

            intent.PutExtra("city", city.ToString());
            StartActivity(intent);
        }
    }
}

