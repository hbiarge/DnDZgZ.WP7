namespace Arosbi.DnDZgZ.UI.Services
{
    using System;
    using System.Collections.Generic;

    using Arosbi.DnDZgZ.UI.Model;

    public interface IRepository
    {
        void GetBusStops(Action<IEnumerable<BusStop>> callback);

        void GetBiziStations(Action<IEnumerable<BiziStation>> callback);

        void GetWifiHotSpots(Action<IEnumerable<WifiHotSpot>> callback);

        void GetBusStopDetails(string id, Action<BusStopDetail> callback);

        void GetBiziStationDetails(string id, Action<BiziStationDetail> callback);
    }
}