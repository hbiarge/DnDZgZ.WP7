namespace Arosbi.DnDZgZ.UI.Tests.Tests.ViewModels
{
    using System;
    using System.Device.Location;
    using System.Reactive.Linq;

    using Arosbi.DnDZgZ.UI.Tests.Mocks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using WP7Contrib.Services.Location;

    public class Dummy
    {
        private readonly ILocationService locationService;

        public Dummy(ILocationService locationService)
        {
            if (locationService == null)
            {
                throw new ArgumentNullException("locationService");
            }

            this.locationService = locationService;

            var currentSubscriber = locationService.Location()
                .Subscribe(location => { this.Coordinate = location; });
        }

        public GeoCoordinate Coordinate { get; private set; }
    }

    [TestClass]
    public class JsonSerializeTest
    {
        [TestMethod]
        public void Puede_Leer_Json_Buses()
        {
            var changedLocation = new GeoCoordinate(2, 2);
            var service = new LocationServiceWp7Mock();
            var sut = new Dummy(service);

            Assert.IsNull(sut.Coordinate);

            service.ChangeLocation(changedLocation);

            Assert.AreEqual(changedLocation, sut.Coordinate);
        }
    }
}
