namespace Arosbi.DnDZgZ.UI.Tests.Tests.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    using Arosbi.DnDZgZ.UI.Model;
    using Arosbi.DnDZgZ.UI.Services.Fakes;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using WP7Contrib.Communications;

    [TestClass]
    public class JsonSerializeTest
    {
        [TestMethod]
        public void Puede_Leer_Json_Buses()
        {
            var data = FakeData.GetBusesData();
            var sut = new JsonContractSerializer();
            var buffer = Encoding.UTF8.GetBytes(data);

            var buses = sut.Deserialize<IEnumerable<BusServicePoint>>(new MemoryStream(buffer));

            Assert.IsNotNull(buses);
            var listaBuses = new List<BusServicePoint>(buses);
            CollectionAssert.AllItemsAreNotNull(listaBuses);

            var primerBus = listaBuses[0];
            Assert.AreEqual("Líneas: 20, 23, 42, 43, 50, CI1", primerBus.subtitle);
            Assert.AreEqual("Poste 3062", primerBus.name);
            Assert.AreEqual("Poste 3062", primerBus.title);
            Assert.AreEqual(-0.890923840283, primerBus.lon);
            Assert.AreEqual(41.6719478775, primerBus.lat);
            Assert.AreEqual("3062", primerBus.id);
        }

        [TestMethod]
        public void Puede_Leer_Json_Bizis()
        {
            var data = FakeData.GetBizisData();
            var sut = new JsonContractSerializer();
            var buffer = Encoding.UTF8.GetBytes(data);

            var bizis = sut.Deserialize<IEnumerable<BiziServicePoint>>(new MemoryStream(buffer));

            Assert.IsNotNull(bizis);
            var listaBizis = new List<BiziServicePoint>(bizis);
            CollectionAssert.AllItemsAreNotNull(listaBizis);

            var primeraBizi = listaBizis[0];
            Assert.AreEqual("Doctor Iranzo con Escultor Benllure", primeraBizi.subtitle);
            Assert.AreEqual("Parada 101", primeraBizi.name);
            Assert.AreEqual("Parada 101", primeraBizi.title);
            Assert.AreEqual(-0.893717288, primeraBizi.lon);
            Assert.AreEqual(41.659247007, primeraBizi.lat);
            Assert.AreEqual("101", primeraBizi.id);
        }

        [TestMethod]
        public void Puede_Leer_Json_Wifis()
        {
            var data = FakeData.GetWifisData();
            var sut = new JsonContractSerializer();
            var buffer = Encoding.UTF8.GetBytes(data);

            var wifis = sut.Deserialize<IEnumerable<WifiServicePoint>>(new MemoryStream(buffer));

            Assert.IsNotNull(wifis);
            var listaWifis = new List<WifiServicePoint>(wifis);
            CollectionAssert.AllItemsAreNotNull(listaWifis);

            var primeraWifi = listaWifis[0];
            Assert.AreEqual(string.Empty, primeraWifi.subtitle);
            Assert.AreEqual("Calle Delicias", primeraWifi.name);
            Assert.AreEqual("Calle Delicias", primeraWifi.title);
            Assert.AreEqual(-0.904700526828, primeraWifi.lon);
            Assert.AreEqual(41.6502224678, primeraWifi.lat);
            Assert.AreEqual(string.Empty, primeraWifi.id);
        }

        [TestMethod]
        public void Puede_Leer_Json_Detalle_Bus()
        {
            var data = FakeData.GetDetalleBusData();
            var sut = new JsonContractSerializer();
            var buffer = Encoding.UTF8.GetBytes(data);

            var detalles = sut.Deserialize<BusDetail>(new MemoryStream(buffer));

            Assert.IsNotNull(detalles);
        }

        [TestMethod]
        public void Puede_Leer_Json_Detalle_Bizi()
        {
            var data = FakeData.GetDetalleBiziData();
            var sut = new JsonContractSerializer();
            var buffer = Encoding.UTF8.GetBytes(data);

            var detalles = sut.Deserialize<BiziDetail>(new MemoryStream(buffer));

            Assert.IsNotNull(detalles);
        }
    }
}
