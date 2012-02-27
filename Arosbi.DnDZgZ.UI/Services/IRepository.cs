namespace Arosbi.DnDZgZ.UI.Services
{
    using System;
    using System.Collections.Generic;

    using Arosbi.DnDZgZ.UI.Model;

    public interface IRepository
    {
        void GetBuses(Action<IEnumerable<BusServicePoint>> callback);

        void GetBizis(Action<IEnumerable<BiziServicePoint>> callback);

        void GetWifis(Action<IEnumerable<WifiServicePoint>> callback);

        void GetBusDetails(string id, Action<BusDetail> callback);

        void GetBiziDetails(string id, Action<BiziDetail> callback);
    }
}