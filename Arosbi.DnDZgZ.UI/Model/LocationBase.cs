namespace Arosbi.DnDZgZ.UI.Model
{
    public abstract class LocationBase
    {
        public string Id { get; set; }

        public string Subtitle { get; set; }

        public double Lon { get; set; }

        public double Lat { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }
    }
}