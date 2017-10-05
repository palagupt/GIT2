using TSC.Common.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace TSC.UWP.Views
{
    public sealed partial class WeatherListPage : Page
    {
        public WeatherListViewModel ViewModel {
            get { return DataContext as WeatherListViewModel; }
        }
        public WeatherListPage()
        {
            this.InitializeComponent();
            DataContext = new WeatherListViewModel();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await ViewModel.InitAsync();
        }
    }
}
