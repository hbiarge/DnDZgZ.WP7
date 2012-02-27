namespace Arosbi.DnDZgZ.UI.Services.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    using Arosbi.DnDZgZ.UI.Model;

    using WP7Contrib.Communications;

    public class FakeRepository : IRepository
    {
        private readonly ISerializer serializer;

        public FakeRepository(ISerializer serializer)
        {
            if (serializer == null)
            {
                throw new ArgumentNullException("serializer");
            }

            this.serializer = serializer;
        }

        public int TimesGetBusesHasBeenCalled { get; private set; }
        public int TimesGetBizisHasBeenCalled { get; private set; }
        public int TimesGetWifisHasBeenCalled { get; private set; }
        public int TimesGetBusDetailsHasBeenCalled { get; private set; }
        public int TimesGetBiziDetailsHasBeenCalled { get; private set; }

        public void GetBusStops(Action<IEnumerable<BusStop>> callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException("callback");
            }

            this.TimesGetBusesHasBeenCalled += 1;

            var buffer = Encoding.UTF8.GetBytes(FakeData.GetBusesData());
            using (var ms = new MemoryStream(buffer))
            {
                var buses = this.serializer.Deserialize<IEnumerable<BusStop>>(ms);
                callback(buses);
            }
        }

        public void GetBiziStations(Action<IEnumerable<BiziStation>> callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException("callback");
            }

            this.TimesGetBizisHasBeenCalled += 1;

            var buffer = Encoding.UTF8.GetBytes(FakeData.GetBizisData());
            using (var ms = new MemoryStream(buffer))
            {
                var bizis = this.serializer.Deserialize<IEnumerable<BiziStation>>(ms);
                callback(bizis);
            }
        }

        public void GetWifiHotSpots(Action<IEnumerable<WifiHotSpot>> callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException("callback");
            }

            this.TimesGetWifisHasBeenCalled += 1;

            var buffer = Encoding.UTF8.GetBytes(FakeData.GetWifisData());
            using (var ms = new MemoryStream(buffer))
            {
                var wifis = this.serializer.Deserialize<IEnumerable<WifiHotSpot>>(ms);
                callback(wifis);
            }
        }

        public void GetBusStopDetails(string id, Action<BusStopDetail> callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException("callback");
            }

            this.TimesGetBusDetailsHasBeenCalled += 1;

            var buffer = Encoding.UTF8.GetBytes(FakeData.GetDetalleBusData());
            using (var ms = new MemoryStream(buffer))
            {
                var busDetail = this.serializer.Deserialize<BusStopDetail>(ms);
                callback(busDetail);
            }
        }

        public void GetBiziStationDetails(string id, Action<BiziStationDetail> callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException("callback");
            }

            this.TimesGetBiziDetailsHasBeenCalled += 1;

            var buffer = Encoding.UTF8.GetBytes(FakeData.GetDetalleBiziData());
            using (var ms = new MemoryStream(buffer))
            {
                var biziDetail = this.serializer.Deserialize<BiziStationDetail>(ms);
                callback(biziDetail);
            }
        }
    }
}