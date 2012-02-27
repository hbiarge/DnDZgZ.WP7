namespace Arosbi.DnDZgZ.UI.Model
{
    using System;
    using System.Device.Location;

    public class PushpinModel
    {
        public GeoCoordinate Location { get; set; }

        public Uri Icon { get; set; }

        public string Id { get; set; }
    }
}
