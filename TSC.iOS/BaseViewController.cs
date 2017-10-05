using System;
using Toolbox.Portable.Mvvm;
using UIKit;

namespace TSC.iOS
{
    public abstract class BaseViewController<T> : UIViewController where T : BaseViewModel, new()
    {
        public T ViewModel
        {
            get;
            set;
        }

        public BaseViewController(IntPtr handle) : base(handle)
        {
            ViewModel = new T();
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            await ViewModel.InitAsync();
        }
    }
}

