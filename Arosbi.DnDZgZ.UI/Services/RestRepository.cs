// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RestRepository.cs" company="Arosbi">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the RestDataAccess type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Arosbi.DnDZgZ.UI.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    using Arosbi.DnDZgZ.UI.Model;

    using WP7Contrib.Communications;

    /// <summary>
    /// Implementation of <see cref="IRepository"/> that fetches the info from a public REST API.
    /// </summary>
    public class RestRepository : IRepository
    {
        /// <summary>
        /// Stores the instance of the Json Deserializer.
        /// </summary>
        private readonly ISerializer serializer;

        /// <summary>
        /// URI to get the stations info.
        /// </summary>
        private const string FetchUri = @"http://www.dndzgz.com/fetch?service={0}";

        /// <summary>
        /// URI to get details for a station.
        /// </summary>
        private const string PointUri = @"http://www.dndzgz.com/point?service={0}&id={1}";

        /// <summary>
        /// Initializes a new instance of the <see cref="RestRepository"/> class.
        /// </summary>
        /// <param name="serializer">The json serializer.</param>
        /// <exception cref="ArgumentNullException">If json serializer is null.</exception>
        public RestRepository(ISerializer serializer)
        {
            if (serializer == null)
            {
                throw new ArgumentNullException("serializer");
            }

            this.serializer = serializer;
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

            var uri = new Uri(string.Format(FetchUri, "bus"));
            this.GetData(uri, callback);
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

            var uri = new Uri(string.Format(FetchUri, "bizi"));
            this.GetData(uri, callback);
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

            var uri = new Uri(string.Format(FetchUri, "wifi"));
            this.GetData(uri, callback);
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

            var uri = new Uri(string.Format(PointUri, "bus", id));
            this.GetData(uri, callback);
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

            var uri = new Uri(string.Format(PointUri, "bizi", id));
            this.GetData(uri, callback);
        }

        /// <summary>
        /// Get data from the specified service.
        /// </summary>
        /// <param name="uri">The uri to get.</param>
        /// <param name="callback">The callback.</param>
        /// <typeparam name="T">The generic Type.</typeparam>
        /// <exception cref="InvalidOperationException">If http call don't return with 200 OK.</exception>
        private void GetData<T>(Uri uri, Action<T> callback) where T : class
        {
            var request = new WebClient { AllowReadStreamBuffering = true };
            request.OpenReadCompleted += (sender, args) =>
             {
                 if (args.Error != null)
                 {
                     throw new InvalidOperationException("No se han podido recuperar los datos", args.Error);
                 }

                 var data = serializer.Deserialize<T>(args.Result);
                 callback(data);
             };

            request.OpenReadAsync(uri);
        }
    }
}
