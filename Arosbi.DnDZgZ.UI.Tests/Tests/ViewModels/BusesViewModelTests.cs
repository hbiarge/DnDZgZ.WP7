namespace Arosbi.DnDZgZ.UI.Tests.Tests.ViewModels
{
    using System.Device.Location;

    using Arosbi.DnDZgZ.UI.Services.Fakes;
    using Arosbi.DnDZgZ.UI.Tests.Mocks;
    using Arosbi.DnDZgZ.UI.ViewModel;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using WP7Contrib.Communications;

    [TestClass]
    public class BusesViewModelTests
    {
        private static LocationServiceWp7Mock locationService;

        [TestMethod]
        public void Carga_Buses_Al_Entrar()
        {
            BusesViewModel sut = GetSut();

            Assert.IsNotNull(sut.BusStops);
            Assert.AreNotEqual(0, sut.BusStops.Count);
            CollectionAssert.AllItemsAreNotNull(sut.BusStops);
        }

        [TestMethod]
        public void Zoom_Por_Defecto_Al_Entrar()
        {
            BusesViewModel sut = GetSut();

            Assert.AreEqual(BusesViewModel.DefaultZoomLevel, sut.ZoomLevel);
        }

        [TestMethod]
        public void DefaultLocation_Si_Null_Location_Al_Entrar()
        {
            BusesViewModel sut = GetSut();

            Assert.AreEqual(ViewModelLocator.DefaultLocation, sut.Center);
        }

        [TestMethod]
        public void LocationService_Location_Al_Entrar()
        {
            var expectedLocation = new GeoCoordinate(1, 1);
            BusesViewModel sut = GetSut(GeoPositionStatus.Ready, expectedLocation);

            Assert.AreEqual(expectedLocation, sut.Center);
        }

        [TestMethod]
        public void Center_Se_Actualiza_Con_Nueva_Posicion()
        {
            var newLocation = new GeoCoordinate(25, 25);
            BusesViewModel sut = GetSut(GeoPositionStatus.Ready, newLocation);

            Assert.AreEqual(newLocation, sut.Center);
        }

        [TestMethod]
        public void ZooIn_Incrementa_Zoom()
        {
            BusesViewModel sut = GetSut();
            var actualZoom = sut.ZoomLevel;

            sut.ZoomInCommand.Execute(null);

            Assert.AreEqual(actualZoom + 1, sut.ZoomLevel);
        }

        [TestMethod]
        public void No_Puede_Hacer_ZooIn_En_MaxZoom()
        {
            BusesViewModel sut = GetSut();
            sut.ZoomLevel = BusesViewModel.MaxZoomLevel;

            Assert.IsFalse(sut.ZoomInCommand.CanExecute(null));
        }

        [TestMethod]
        public void ZooOut_decrementa_Zoom()
        {
            BusesViewModel sut = GetSut();
            var actualZoom = sut.ZoomLevel;

            sut.ZoomOutCommand.Execute(null);

            Assert.AreEqual(actualZoom - 1.0, sut.ZoomLevel);
        }

        [TestMethod]
        public void No_Puede_Hacer_Zooout_En_MinZoom()
        {
            BusesViewModel sut = GetSut();
            sut.ZoomLevel = BusesViewModel.MinZoomLevel;

            Assert.IsFalse(sut.ZoomOutCommand.CanExecute(null));
        }

        [TestMethod]
        public void Puede_Obtener_Detalle_Parada()
        {
            BusesViewModel sut = GetSut();

            sut.PushpinCommand.Execute("fake");

            Assert.IsNotNull(sut.CurrentBusStopDetail);
            Assert.AreEqual(true, sut.ShowBusStopDetail);
        }

        [TestMethod]
        public void Obtener_Detalle_Parada_Abre_Popup()
        {
            BusesViewModel sut = GetSut();

            sut.PushpinCommand.Execute("fake");

            Assert.AreEqual(true, sut.ShowBusStopDetail);
        }

        [TestMethod]
        public void ClosePopup_Cierra_Popup()
        {
            BusesViewModel sut = GetSut();
            sut.ShowBusStopDetail = true;

            sut.ClosePopupCommand.Execute(null);

            Assert.AreEqual(false, sut.ShowBusStopDetail);
        }

        private static BusesViewModel GetSut(
            GeoPositionStatus positionStatus = GeoPositionStatus.Disabled,
            GeoCoordinate location = null)
        {
            locationService = new LocationServiceWp7Mock();
            var jsonSerializer = new JsonContractSerializer();
            var repository = new FakeRepository(jsonSerializer);
            var vm = new BusesViewModel(locationService, repository);

            if (positionStatus == GeoPositionStatus.Ready)
            {
                locationService.ChangeStatus(positionStatus);
                locationService.ChangeLocation(location);
            }

            return vm;
        }
    }
}
