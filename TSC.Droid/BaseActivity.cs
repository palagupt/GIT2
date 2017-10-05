using System;
using Android.App;
using Toolbox.Portable.Mvvm;
namespace TSC.Droid
{
    public abstract class BaseActivity<T> : Activity where T : BaseViewModel, new()
    {
        private T _viewModel;

        public T ViewModel
        {
            get
            {
                return _viewModel ?? (_viewModel = new T());
            }
            set
            {
                _viewModel = value;
            }
        }
    }
}