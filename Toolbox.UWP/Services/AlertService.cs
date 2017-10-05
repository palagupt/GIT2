using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Portable.Services;

namespace Toolbox.UWP.Services
{
    public class AlertService : IAlertService
    {
        public Task DisplayAsync(string title, string message = null, string cancelButton = null)
        {
            throw new NotImplementedException();
        }

        public Task<string> DisplayInputEntryAsync(string title, string message = null, string actionButton = null, string cancelButton = null, string hint = null, Func<string, bool> validator = null)
        {
            throw new NotImplementedException();
        }
    }
}
