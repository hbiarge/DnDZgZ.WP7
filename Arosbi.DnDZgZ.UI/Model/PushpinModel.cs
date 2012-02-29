namespace Arosbi.DnDZgZ.UI.Model
{
    using System;
    using System.Device.Location;

    public class PushpinModel : IEquatable<PushpinModel>
    {
        public GeoCoordinate Location { get; set; }

        public Uri Icon { get; set; }

        public string Id { get; set; }

        public bool Equals(PushpinModel other)
        {
            return this.Id.Equals(other.Id);
        }
    }
}
