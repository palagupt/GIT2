using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TSC.Common.ViewModels;

namespace TSC.Common.Test
{
    [TestClass]
    public class WeatherListViewModelTest
    {
        [TestMethod]
        public void WeatherListCTOR()
        {
            WeatherListViewModel vm = new WeatherListViewModel();
            Assert.IsInstanceOfType(vm, typeof(WeatherListViewModel));
            Assert.IsNotNull(vm);
        }

        [TestInitialize]
        public void Setup()
        {
            Bootstrap.Begin(() => null, () => null, () => { });
        }
    }
}
