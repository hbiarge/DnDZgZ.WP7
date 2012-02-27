namespace Arosbi.DnDZgZ.UI.Tests.Tests.Services
{
    using System.Collections.Generic;

    using Arosbi.DnDZgZ.UI.Model;
    using Arosbi.DnDZgZ.UI.Services;
    using Arosbi.DnDZgZ.UI.Services.Fakes;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using WP7Contrib.Caching;
    using WP7Contrib.Communications;

    [TestClass]
    public class CacheRepositoryTest
    {
        private static FakeRepository innerRepository;

        [TestMethod]
        public void GetBuses_Cachea_Llamada()
        {
            var sut = InitializeSut();

            sut.GetBusStops(Assert.IsNotNull);
            Assert.AreEqual(1, innerRepository.TimesGetBusesHasBeenCalled);
            sut.GetBusStops(Assert.IsNotNull);
            Assert.AreEqual(1, innerRepository.TimesGetBusesHasBeenCalled);
        }

        [TestMethod]
        public void GetBizis_Cachea_Llamada()
        {
            var sut = InitializeSut();

            sut.GetBiziStations(Assert.IsNotNull);
            Assert.AreEqual(1, innerRepository.TimesGetBizisHasBeenCalled);
            sut.GetBiziStations(Assert.IsNotNull);
            Assert.AreEqual(1, innerRepository.TimesGetBizisHasBeenCalled);
        }

        [TestMethod]
        public void GetWifis_Cachea_Llamada()
        {
            var sut = InitializeSut();

            sut.GetWifiHotSpots(Assert.IsNotNull);
            Assert.AreEqual(1, innerRepository.TimesGetWifisHasBeenCalled);
            sut.GetWifiHotSpots(Assert.IsNotNull);
            Assert.AreEqual(1, innerRepository.TimesGetWifisHasBeenCalled);
        }

        [TestMethod]
        public void GetBusDetails_NO_Cachea_Llamada()
        {
            var sut = InitializeSut();

            sut.GetBusStopDetails("3062", Assert.IsNotNull);
            Assert.AreEqual(1, innerRepository.TimesGetBusDetailsHasBeenCalled);
            sut.GetBusStopDetails("3062", Assert.IsNotNull);
            Assert.AreEqual(2, innerRepository.TimesGetBusDetailsHasBeenCalled);
        }

        [TestMethod]
        public void GetBiziDetails_NO_Cachea_Llamada()
        {
            var sut = InitializeSut();

            sut.GetBiziStationDetails("101", Assert.IsNotNull);
            Assert.AreEqual(1, innerRepository.TimesGetBiziDetailsHasBeenCalled);
            sut.GetBiziStationDetails("101", Assert.IsNotNull);
            Assert.AreEqual(2, innerRepository.TimesGetBiziDetailsHasBeenCalled);
        }

        private static CacheRepository InitializeSut()
        {
            var jsonSerializer = new JsonContractSerializer();
            innerRepository = new FakeRepository(jsonSerializer);
            var memCache = new InMemoryCacheProvider();
            var sut = new CacheRepository(innerRepository, memCache);
            return sut;
        }
    }
}
