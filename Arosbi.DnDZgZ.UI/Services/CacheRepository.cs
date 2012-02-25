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

        /// <summary>
        /// Get all the bus stations.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public void GetBuses(Action<IEnumerable<BusServicePoint>> callback)
        {
            var buses = this.cacheProvider.Get<string, IEnumerable<BusServicePoint>>(BusesKey);

            if (buses == null)
            {
                this.repository.GetBuses(b =>
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

        /// <summary>
        /// Get all the bizi stations.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public void GetBizis(Action<IEnumerable<BiziServicePoint>> callback)
        {
            var bizis = this.cacheProvider.Get<string, IEnumerable<BiziServicePoint>>(BizisKey);

            if (bizis == null)
            {
                this.repository.GetBizis(b =>
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

        /// <summary>
        /// Get all wifi hotspots.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public void GetWifis(Action<IEnumerable<WifiServicePoint>> callback)
        {
            var wifis = this.cacheProvider.Get<string, IEnumerable<WifiServicePoint>>(WifisKey);

            if (wifis == null)
            {
                this.repository.GetWifis(w =>
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

        /// <summary>
        /// Get the details (buses to arrive) from a certain bus station.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="callback">The callback.</param>
        public void GetBusDetails(string id, Action<BusDetail> callback)
        {
            // Las llamadas a los detalles no se cachean
            this.repository.GetBusDetails(id, callback);
        }

        /// <summary>
        /// Get the details (cycles remaining) from a certain bizi station.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="callback">The callback.</param>
        public void GetBiziDetails(string id, Action<BiziDetail> callback)
        {
            // Las llamadas a los detalles no se cachean
            this.repository.GetBiziDetails(id, callback);
        }
    }
}