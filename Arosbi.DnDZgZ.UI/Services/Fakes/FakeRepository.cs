// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FakeRepository.cs" company="Arosbi">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the FakeRepository type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Arosbi.DnDZgZ.UI.Services.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    using Arosbi.DnDZgZ.UI.Model;

    using WP7Contrib.Communications;

    /// <summary>
    /// Fake IRepository for UI design and test.
    /// </summary>
    public class FakeRepository : IRepository
    {
        /// <summary>
        /// Stores the JsonSerializer.
        /// </summary>
        private readonly ISerializer serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeRepository"/> class.
        /// </summary>
        /// <param name="serializer">
        /// The json serializer.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// If the JsonSerializer is null.
        /// </exception>
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

        /// <summary>
        /// Get all the bus stations.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public void GetBuses(Action<IEnumerable<BusServicePoint>> callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException("callback");
            }

            this.TimesGetBusesHasBeenCalled += 1;

            var buffer = Encoding.UTF8.GetBytes(FakeData.GetBusesData());
            using (var ms = new MemoryStream(buffer))
            {
                var buses = this.serializer.Deserialize<IEnumerable<BusServicePoint>>(ms);
                callback(buses);
            }
        }

        /// <summary>
        /// Get all the bizi stations.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public void GetBizis(Action<IEnumerable<BiziServicePoint>> callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException("callback");
            }

            this.TimesGetBizisHasBeenCalled += 1;

            var buffer = Encoding.UTF8.GetBytes(FakeData.GetBizisData());
            using (var ms = new MemoryStream(buffer))
            {
                var bizis = this.serializer.Deserialize<IEnumerable<BiziServicePoint>>(ms);
                callback(bizis);
            }
        }

        /// <summary>
        /// Get all wifi hotspots.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public void GetWifis(Action<IEnumerable<WifiServicePoint>> callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException("callback");
            }

            this.TimesGetWifisHasBeenCalled += 1;

            var buffer = Encoding.UTF8.GetBytes(FakeData.GetWifisData());
            using (var ms = new MemoryStream(buffer))
            {
                var wifis = this.serializer.Deserialize<IEnumerable<WifiServicePoint>>(ms);
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
            if (callback == null)
            {
                throw new ArgumentNullException("callback");
            }

            this.TimesGetBusDetailsHasBeenCalled += 1;

            var buffer = Encoding.UTF8.GetBytes(FakeData.GetDetalleBusData());
            using (var ms = new MemoryStream(buffer))
            {
                var busDetail = this.serializer.Deserialize<BusDetail>(ms);
                callback(busDetail);
            }
        }

        /// <summary>
        /// Get the details (cycles remaining) from a certain bizi station.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="callback">The callback.</param>
        public void GetBiziDetails(string id, Action<BiziDetail> callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException("callback");
            }

            this.TimesGetBiziDetailsHasBeenCalled += 1;

            var buffer = Encoding.UTF8.GetBytes(FakeData.GetDetalleBiziData());
            using (var ms = new MemoryStream(buffer))
            {
                var biziDetail = this.serializer.Deserialize<BiziDetail>(ms);
                callback(biziDetail);
            }
        }
    }
}