using System;
using Foundation;
using TSC.iOS.Cells;
using TSC.Common.ViewModels;
using UIKit;
using TSC.Common.Models;

namespace TSC.iOS
{
    public partial class WeatherListViewController
        : BaseViewController<WeatherListViewModel>, IUITableViewDataSource, IUITableViewDelegate
    {
        public WeatherListViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.NavigationItem.SetRightBarButtonItem(
                new UIBarButtonItem("Add Favourite"
                , UIBarButtonItemStyle.Plain
                , async (sender, args) =>
                {
                    await ViewModel.AddFavoriteCommand.ExecuteAsync(null);
                    TableView.ReloadData();
                })
            , true);

            TableView.WeakDataSource = TableView.WeakDelegate = this;
            ViewModel.ReloadAction = () => BeginInvokeOnMainThread(TableView.ReloadData);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public nint RowsInSection(UITableView tableView, nint section)
        {
            if (ViewModel.WeatherRequests != null)
                return ViewModel.WeatherRequests.Count;
            else return 0;
        }

        [Export("numberOfSectionsInTableView:")]
        public nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }


        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var weatherRequest = ViewModel.WeatherRequests[indexPath.Row];

            var key = CityCell.Key;

            if (weatherRequest is LocationWeatherRequest)
            {
                key = LocationCell.Key;
            }

            BaseWeatherRequestCell cell = tableView.DequeueReusableCell(key, indexPath) as BaseWeatherRequestCell;

            cell.SetData(weatherRequest);

            return cell;
        }

        [Export("tableView:didSelectRowAtIndexPath:")]
        public void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            PerformSegue(typeof(WeatherDetailViewController).Name, tableView.CellAt(indexPath));
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            WeatherDetailViewController destination = segue.DestinationViewController as WeatherDetailViewController;
            destination.ViewModel.Request = (sender as BaseWeatherRequestCell).GetData();
            base.PrepareForSegue(segue, sender);
        }
    }
}

