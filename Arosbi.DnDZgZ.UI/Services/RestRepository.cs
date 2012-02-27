namespace Arosbi.DnDZgZ.UI.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    using Arosbi.DnDZgZ.UI.Model;

    using WP7Contrib.Communications;

    public class RestRepository : IRepository
    {
        private readonly ISerializer serializer;

        private const string FetchUri = @"http://www.dndzgz.com/fetch?service={0}";

        private const string PointUri = @"http://www.dndzgz.com/point?service={0}&id={1}";

        public RestRepository(ISerializer serializer)
        {
            if (serializer == null)
            {
                throw new ArgumentNullException("serializer");
            }

            this.serializer = serializer;
        }

        public void GetBuses(Action<IEnumerable<BusServicePoint>> callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException("callback");
            }

            var uri = new Uri(string.Format(FetchUri, "bus"));
            this.GetData(uri, callback);
        }

        public void GetBizis(Action<IEnumerable<BiziServicePoint>> callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException("callback");
            }

            var uri = new Uri(string.Format(FetchUri, "bizi"));
            this.GetData(uri, callback);
        }

        public void GetWifis(Action<IEnumerable<WifiServicePoint>> callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException("callback");
            }

            var uri = new Uri(string.Format(FetchUri, "wifi"));
            this.GetData(uri, callback);
        }

        public void GetBusDetails(string id, Action<BusDetail> callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException("callback");
            }

            var uri = new Uri(string.Format(PointUri, "bus", id));
            this.GetData(uri, callback);
        }

        public void GetBiziDetails(string id, Action<BiziDetail> callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException("callback");
            }

            var uri = new Uri(string.Format(PointUri, "bizi", id));
            this.GetData(uri, callback);
        }

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
