namespace Arosbi.DnDZgZ.UI.Tests.Tests.ViewModels
{
    using System;

    using Arosbi.DnDZgZ.UI.Tests.Mocks;
    using Arosbi.DnDZgZ.UI.ViewModel;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NorthernLights;

    [TestClass]
    public class MainViewModelTests
    {
        [TestMethod]
        public void Puede_Navegar_A_Buses()
        {
            var navigationService = new NavigationServiceMock();
            var sut = new MainViewModel(() => navigationService);
            var expectedUri = new Uri("/BusesPage.xaml", UriKind.Relative);

            sut.AutobusesCommand.Execute(null);

            Assert.AreEqual(expectedUri, navigationService.LastNavigateUri);
        }

        [TestMethod]
        public void Puede_Navegar_A_Bizis()
        {
            var navigationService = new NavigationServiceMock();
            var sut = new MainViewModel(() => navigationService);
            var expectedUri = new Uri("/BizisPage.xaml", UriKind.Relative);

            sut.BiziCommand.Execute(null);

            Assert.AreEqual(expectedUri, navigationService.LastNavigateUri);
        }

        [TestMethod]
        public void Puede_Navegar_A_Wifis()
        {
            var navigationService = new NavigationServiceMock();
            var sut = new MainViewModel(() => navigationService);
            var expectedUri = new Uri("/WifisPage.xaml", UriKind.Relative);

            sut.WifiCommand.Execute(null);

            Assert.AreEqual(expectedUri, navigationService.LastNavigateUri);
        }
    }
}
