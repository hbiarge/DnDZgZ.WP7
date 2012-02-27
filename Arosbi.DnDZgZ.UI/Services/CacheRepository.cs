namespace Arosbi.DnDZgZ.UI.Services
{
    using System;
    using System.Collections.Generic;

    using Arosbi.DnDZgZ.UI.Model;

    using WP7Contrib.Caching;

    public class CacheRepository : IRepository
    {
        private const string BusesKey = "Buses";
        private const string BizisKey = "Bizis";
        private const string WifisKey = "Wifis";

        private readonly TimeSpan cacheDuration = TimeSpan.FromDays(3);

        private readonly IRepository repository;

        private readonly ICacheProvider cacheProvider;

        public CacheRepository(IRepository repository, ICacheProvider cacheProvider)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            if (cacheProvider == null)
            {
                throw new ArgumentNullException("cacheProvider");
            }

            this.repository = repository;
            this.cacheProvider = cacheProvider;
        }

        public void GetBusStops(Action<IEnumerable<BusStop>> callback)
        {
            var buses = this.cacheProvider.Get<string, IEnumerable<BusStop>>(BusesKey);

            if (buses == null)
            {
                this.repository.GetBusStops(b =>
                    {
                        this.cacheProvider.Add(BusesKey, b, cacheDuration);
                        callback(b);
                    });
            }
            else
            {
                callback(buses);
            }
        }

        public void GetBiziStations(Action<IEnumerable<BiziStation>> callback)
        {
            var bizis = this.cacheProvider.Get<string, IEnumerable<BiziStation>>(BizisKey);

            if (bizis == null)
            {
                this.repository.GetBiziStations(b =>
                {
                    this.cacheProvider.Add(BizisKey, b, cacheDuration);
                    callback(b);
                });
            }
            else
            {
                callback(bizis);
            }
        }

        public void GetWifiHotSpots(Action<IEnumerable<WifiHotSpot>> callback)
        {
            var wifis = this.cacheProvider.Get<string, IEnumerable<WifiHotSpot>>(WifisKey);

            if (wifis == null)
            {
                this.repository.GetWifiHotSpots(w =>
                {
                    this.cacheProvider.Add(WifisKey, w, cacheDuration);
                    callback(w);
                });
            }
            else
            {
                callback(wifis);
            }
        }

        public void GetBusStopDetails(string id, Action<BusStopDetail> callback)
        {
            // Las llamadas a los detalles no se cachean
            this.repository.GetBusStopDetails(id, callback);
        }

        public void GetBiziStationDetails(string id, Action<BiziStationDetail> callback)
        {
            // Las llamadas a los detalles no se cachean
            this.repository.GetBiziStationDetails(id, callback);
        }
    }
}