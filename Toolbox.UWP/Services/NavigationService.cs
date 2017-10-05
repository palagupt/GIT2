using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Portable.Services;
using Windows.UI.Xaml.Controls;

namespace Toolbox.UWP.Services
{
    public class NavigationService: INavigationService
    {
        IDictionary<string, Type> pageTypeDictionary = new Dictionary<string, Type>();
        private Frame frame;
        public NavigationService(Frame frame, IDictionary<string, Type> pageTypeDictionary)
        {
            this.frame = frame;
            this.pageTypeDictionary = pageTypeDictionary;
        }

        public void Navigate(string pageToken, object param=null)
        {
            Type t = GetRegisterType(pageToken);
            if (t != null)
                frame.Navigate(t, param);
        }

        public void RegisterType<T>(string token)
        {
            if (!pageTypeDictionary.ContainsKey(token))
            {
                pageTypeDictionary.Add(token, typeof(T));
            }
        }

        public void DeRegisterType(string token)
        {
            if (!pageTypeDictionary.ContainsKey(token))
            {
                pageTypeDictionary.Remove(token);
            }
        }

        public Type GetRegisterType(string token)
        {
            Type t = null;
            pageTypeDictionary.TryGetValue(token, out t);
            return t;
        }
    }
}
