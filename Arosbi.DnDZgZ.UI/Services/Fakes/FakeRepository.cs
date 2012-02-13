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

    using Arosbi.DnDZgZ.UI.Infrastructure;
    using Arosbi.DnDZgZ.UI.Model;

    /// <summary>
    /// Fake IRepository for UI design and test.
    /// </summary>
    public class FakeRepository : IRepository
    {
        /// <summary>
        /// Stores the JsonSerializer.
        /// </summary>
        private readonly JsonSerializer jsonSerializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeRepository"/> class.
        /// </summary>
        /// <param name="jsonSerializer">
        /// The json serializer.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// If the JsonSerializer is null.
        /// </exception>
        public FakeRepository(JsonSerializer jsonSerializer)
        {
            if (jsonSerializer == null)
            {
                throw new ArgumentNullException("jsonSerializer");
            }

            this.jsonSerializer = jsonSerializer;
        }

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

            var buses = this.jsonSerializer.Deserialize<IEnumerable<BusServicePoint>>(FakeData.GetBusesData());
            callback(buses);
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

            var bizis = this.jsonSerializer.Deserialize<IEnumerable<BiziServicePoint>>(FakeData.GetBizisData());
            callback(bizis);
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

            var wifis = this.jsonSerializer.Deserialize<IEnumerable<WifiServicePoint>>(FakeData.GetWifisData());
            callback(wifis);
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

            var busDetail = this.jsonSerializer.Deserialize<BusDetail>(FakeData.GetDetalleBusData());
            callback(busDetail);
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

            var biziDetail = this.jsonSerializer.Deserialize<BiziDetail>(FakeData.GetDetalleBiziData());
            callback(biziDetail);
        }
    }
}