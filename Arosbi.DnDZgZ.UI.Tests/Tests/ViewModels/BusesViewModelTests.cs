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
        private static LocationServiceMock locationService;

        [TestMethod]
        public void LocationService_Arranca_Al_Entrar()
        {
            BusesViewModel sut = GetSut();

            Assert.AreEqual(true, locationService.IsStarted);
        }

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

            Assert.AreEqual(BusesViewModel.DefaultZoomLevel, sut.Zoom);
        }

        [TestMethod]
        public void DefaultLocation_Si_Null_Location_Al_Entrar()
        {
            BusesViewModel sut = GetSut(CurrentLocationType.NullLocation);

            Assert.AreEqual(ViewModelLocator.DefaultLocation, sut.Center);
        }

        [TestMethod]
        public void LocationService_Location_Al_Entrar()
        {
            BusesViewModel sut = GetSut();

            Assert.AreEqual(locationService.CurrentLocation.Latitude, sut.Center.Latitude);
            Assert.AreEqual(locationService.CurrentLocation.Longitude, sut.Center.Longitude);
        }

        [TestMethod]
        public void Center_Se_Actualiza_Con_Nueva_Posicion()
        {
            BusesViewModel sut = GetSut();
            var newLocation = new GeoCoordinate(25, 25);
            locationService.ChangeLocation(newLocation);

            Assert.AreEqual(newLocation, sut.Center);
        }

        [TestMethod]
        public void ZooIn_Incrementa_Zoom()
        {
            BusesViewModel sut = GetSut();
            var actualZoom = sut.Zoom;

            sut.ZoomInCommand.Execute(null);

            Assert.AreEqual(actualZoom + 1, sut.Zoom);
        }

        [TestMethod]
        public void No_Puede_Hacer_ZooIn_En_MaxZoom()
        {
            BusesViewModel sut = GetSut();
            sut.Zoom = BusesViewModel.MaxZoomLevel;

            Assert.IsFalse(sut.ZoomInCommand.CanExecute(null));
        }

        [TestMethod]
        public void ZooOut_decrementa_Zoom()
        {
            BusesViewModel sut = GetSut();
            var actualZoom = sut.Zoom;

            sut.ZoomOutCommand.Execute(null);

            Assert.AreEqual(actualZoom - 1.0, sut.Zoom);
        }

        [TestMethod]
        public void No_Puede_Hacer_Zooout_En_MinZoom()
        {
            BusesViewModel sut = GetSut();
            sut.Zoom = BusesViewModel.MinZoomLevel;

            Assert.IsFalse(sut.ZoomOutCommand.CanExecute(null));
        }

        [TestMethod]
        public void Puede_Obtener_Detalle_Parada()
        {
            BusesViewModel sut = GetSut();

            sut.PushpinCommand.Execute("fake");

            Assert.IsNotNull(sut.CurrentBusInfo);
            Assert.AreEqual(true, sut.IsPopupOpen);
        }

        [TestMethod]
        public void Obtener_Detalle_Parada_Abre_Popup()
        {
            BusesViewModel sut = GetSut();

            sut.PushpinCommand.Execute("fake");

            Assert.AreEqual(true, sut.IsPopupOpen);
        }

        [TestMethod]
        public void ClosePopup_Cierra_Popup()
        {
            BusesViewModel sut = GetSut();
            sut.IsPopupOpen = true;

            sut.ClosePopupCommand.Execute(null);

            Assert.AreEqual(false, sut.IsPopupOpen);
        }

        private static BusesViewModel GetSut(CurrentLocationType locationType = CurrentLocationType.DefaultLocation)
        {
            locationService = new LocationServiceMock(locationType);
            var jsonSerializer = new JsonContractSerializer();
            var repository = new FakeRepository(jsonSerializer);
            return new BusesViewModel(locationService, repository);
        }
    }
}
