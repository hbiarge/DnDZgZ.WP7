namespace Arosbi.DnDZgZ.UI.Tests.Tests.ViewModels
{
    using Arosbi.DnDZgZ.UI.Infrastructure;
    using Arosbi.DnDZgZ.UI.Services.Fakes;
    using Arosbi.DnDZgZ.UI.Tests.Tests.Mocks;
    using Arosbi.DnDZgZ.UI.ViewModel;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BusesViewModelTests
    {
        [TestMethod]
        public void ZooIn_Incrementa_Zoom()
        {
            var navigationService = new NavigationServiceMock();
            var jsonSerializer = new JsonSerializer();
            var repository = new FakeRepository(jsonSerializer);
            var sut = new BusesViewModel(navigationService, repository);
            var actualZoom = sut.Zoom;

            sut.ZoomInCommand.Execute(null);

            Assert.AreEqual(actualZoom + 1, sut.Zoom);
        }

        [TestMethod]
        public void No_Puede_Hacer_ZooIn_En_MaxZoom()
        {
            var navigationService = new NavigationServiceMock();
            var jsonSerializer = new JsonSerializer();
            var repository = new FakeRepository(jsonSerializer);
            var sut = new BusesViewModel(navigationService, repository)
                {
                    Zoom = 20.0
                };

            Assert.IsFalse(sut.ZoomInCommand.CanExecute(null));
        }

        [TestMethod]
        public void ZooOut_decrementa_Zoom()
        {
            var navigationService = new NavigationServiceMock();
            var jsonSerializer = new JsonSerializer();
            var repository = new FakeRepository(jsonSerializer);
            var sut = new BusesViewModel(navigationService, repository);
            var actualZoom = sut.Zoom;

            sut.ZoomOutCommand.Execute(null);

            Assert.AreEqual(actualZoom - 1.0, sut.Zoom);
        }

        [TestMethod]
        public void No_Puede_Hacer_Zooout_En_MinZoom()
        {
            var navigationService = new NavigationServiceMock();
            var jsonSerializer = new JsonSerializer();
            var repository = new FakeRepository(jsonSerializer);
            var sut = new BusesViewModel(navigationService, repository)
            {
                Zoom = 15.0
            };

            Assert.IsFalse(sut.ZoomOutCommand.CanExecute(null));
        }
    }
}
