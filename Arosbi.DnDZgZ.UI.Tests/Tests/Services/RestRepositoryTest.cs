namespace Arosbi.DnDZgZ.UI.Tests.Tests.Services
{
    using System.Collections.Generic;

    using Arosbi.DnDZgZ.UI.Model;
    using Arosbi.DnDZgZ.UI.Services;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using WP7Contrib.Communications;

    [Ignore]
    [TestClass]
    public class RestRepositoryTest
    {
        [TestMethod]
        public void GetBuses_devuelve_informacion()
        {
            var sut = InitializeSut();

            sut.GetBusStops(buses =>
                {
                    Assert.IsNotNull(buses);
                    var listaBuses = new List<BusStop>(buses);
                    CollectionAssert.AllItemsAreNotNull(listaBuses);
                });
        }

        [TestMethod]
        public void GetBizis_devuelve_informacion()
        {
            var sut = InitializeSut();

            sut.GetBiziStations(bizis =>
            {
                Assert.IsNotNull(bizis);
                var listaBizis = new List<BiziStation>(bizis);
                CollectionAssert.AllItemsAreNotNull(listaBizis);
            });
        }

        [TestMethod]
        public void GetWifis_devuelve_informacion()
        {
            var sut = InitializeSut();

            sut.GetWifiHotSpots(wifis =>
            {
                Assert.IsNotNull(wifis);
                var listaWifis = new List<WifiHotSpot>(wifis);
                CollectionAssert.AllItemsAreNotNull(listaWifis);
            });
        }

        [TestMethod]
        public void GetBusDetails_devuelve_informacion()
        {
            var sut = InitializeSut();

            sut.GetBusStopDetails(
                "3062",
                Assert.IsNotNull);
        }

        [TestMethod]
        public void GetBiziDetails_devuelve_informacion()
        {
            var sut = InitializeSut();

            sut.GetBiziStationDetails(
                "101",
                Assert.IsNotNull);
        }

        private static RestRepository InitializeSut()
        {
            var jsonSerializer = new JsonContractSerializer();
            var sut = new RestRepository(jsonSerializer);
            return sut;
        }
    }
}
