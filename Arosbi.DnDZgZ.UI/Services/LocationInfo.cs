namespace Arosbi.DnDZgZ.UI.Services
{
    using System.Device.Location;

    public class LocationInfo
    {
        public LocationInfo()
        {
            this.CurrentLocation = null;
            this.LocationStatus = GeoPositionStatus.Disabled;
        }

        public GeoCoordinate CurrentLocation { get; set; }

        public GeoPositionStatus LocationStatus { get; set; }
    }
}
