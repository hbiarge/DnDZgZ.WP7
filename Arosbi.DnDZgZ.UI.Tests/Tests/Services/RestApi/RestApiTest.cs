namespace Arosbi.DnDZgZ.UI.Tests.Tests.Services.RestApi
{
    using System.Collections.Generic;

    using Arosbi.DnDZgZ.UI.Model;
    using Arosbi.DnDZgZ.UI.Services;
    using Arosbi.DnDZgZ.UI.Services.RestApi;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [Ignore]
    [TestClass]
    public class RestApiTest
    {
        [TestMethod]
        public void GetBuses_devuelve_informacion()
        {
            var sut = InitializeSut();

            sut.GetBuses(buses =>
                {
                    Assert.IsNotNull(buses);
                    var listaBuses = new List<BusServicePoint>(buses);
                    CollectionAssert.AllItemsAreNotNull(listaBuses);
                });
        }

        [TestMethod]
        public void GetBizis_devuelve_informacion()
        {
            var sut = InitializeSut();

            sut.GetBizis(bizis =>
            {
                Assert.IsNotNull(bizis);
                var listaBizis = new List<BiziServicePoint>(bizis);
                CollectionAssert.AllItemsAreNotNull(listaBizis);
            });
        }

        [TestMethod]
        public void GetWifis_devuelve_informacion()
        {
            var sut = InitializeSut();

            sut.GetWifis(wifis =>
            {
                Assert.IsNotNull(wifis);
                var listaWifis = new List<WifiServicePoint>(wifis);
                CollectionAssert.AllItemsAreNotNull(listaWifis);
            });
        }

        [TestMethod]
        public void GetBusDetails_devuelve_informacion()
        {
            var sut = InitializeSut();

            sut.GetBusDetails(
                "3062",
                Assert.IsNotNull);
        }

        [TestMethod]
        public void GetBiziDetails_devuelve_informacion()
        {
            var sut = InitializeSut();

            sut.GetBiziDetails(
                "101",
                Assert.IsNotNull);
        }

        private static RestRepository InitializeSut()
        {
            var jsonSerializer = new JsonSerializer();
            var sut = new RestRepository(jsonSerializer);
            return sut;
        }
    }
}
