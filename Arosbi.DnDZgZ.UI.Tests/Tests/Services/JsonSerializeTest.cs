namespace Arosbi.DnDZgZ.UI.Tests.Tests.Services
{
    using System.Collections.Generic;

    using Arosbi.DnDZgZ.UI.Infrastructure;
    using Arosbi.DnDZgZ.UI.Model;
    using Arosbi.DnDZgZ.UI.Services;
    using Arosbi.DnDZgZ.UI.Services.Fakes;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class JsonSerializeTest
    {
        [TestMethod]
        public void Puede_Leer_Json_Buses()
        {
            var data = FakeData.GetBusesData();
            var sut = new JsonSerializer();

            var buses = sut.Deserialize<IEnumerable<BusServicePoint>>(data);

            Assert.IsNotNull(buses);
            var listaBuses = new List<BusServicePoint>(buses);
            CollectionAssert.AllItemsAreNotNull(listaBuses);

            var primerBus = listaBuses[0];
            Assert.AreEqual("Líneas: 20, 23, 42, 43, 50, CI1", primerBus.Subtitle);
            Assert.AreEqual("Poste 3062", primerBus.Name);
            Assert.AreEqual("Poste 3062", primerBus.Title);
            Assert.AreEqual(-0.890923840283, primerBus.Lon);
            Assert.AreEqual(41.6719478775, primerBus.Lat);
            Assert.AreEqual("3062", primerBus.Id);
        }

        [TestMethod]
        public void Puede_Leer_Json_Bizis()
        {
            var data = FakeData.GetBizisData();
            var sut = new JsonSerializer();

            var bizis = sut.Deserialize<IEnumerable<BiziServicePoint>>(data);

            Assert.IsNotNull(bizis);
            var listaBizis = new List<BiziServicePoint>(bizis);
            CollectionAssert.AllItemsAreNotNull(listaBizis);

            var primeraBizi = listaBizis[0];
            Assert.AreEqual("Doctor Iranzo con Escultor Benllure", primeraBizi.Subtitle);
            Assert.AreEqual("Parada 101", primeraBizi.Name);
            Assert.AreEqual("Parada 101", primeraBizi.Title);
            Assert.AreEqual(-0.893717288, primeraBizi.Lon);
            Assert.AreEqual(41.659247007, primeraBizi.Lat);
            Assert.AreEqual("101", primeraBizi.Id);
        }

        [TestMethod]
        public void Puede_Leer_Json_Wifis()
        {
            var data = FakeData.GetWifisData();
            var sut = new JsonSerializer();

            var wifis = sut.Deserialize<IEnumerable<WifiServicePoint>>(data);

            Assert.IsNotNull(wifis);
            var listaWifis = new List<WifiServicePoint>(wifis);
            CollectionAssert.AllItemsAreNotNull(listaWifis);

            var primeraWifi = listaWifis[0];
            Assert.AreEqual(string.Empty, primeraWifi.Subtitle);
            Assert.AreEqual("Calle Delicias", primeraWifi.Name);
            Assert.AreEqual("Calle Delicias", primeraWifi.Title);
            Assert.AreEqual(-0.904700526828, primeraWifi.Lon);
            Assert.AreEqual(41.6502224678, primeraWifi.Lat);
            Assert.AreEqual(string.Empty, primeraWifi.Id);
        }

        [TestMethod]
        public void Puede_Leer_Json_Detalle_Bus()
        {
            var data = FakeData.GetDetalleBusData();
            var sut = new JsonSerializer();

            var detalles = sut.Deserialize<BusDetail>(data);

            Assert.IsNotNull(detalles);
        }

        [TestMethod]
        public void Puede_Leer_Json_Detalle_Bizi()
        {
            var data = FakeData.GetDetalleBiziData();
            var sut = new JsonSerializer();

            var detalles = sut.Deserialize<BiziDetail>(data);

            Assert.IsNotNull(detalles);
        }
    }
}
