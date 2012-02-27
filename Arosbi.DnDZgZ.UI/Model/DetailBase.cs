namespace Arosbi.DnDZgZ.UI.Model
{
    public abstract class DetailBase
    {
        public string Title { get; set; }

        public string Id { get; set; }

        public string Service { get; set; }

        public string[][] Items { get; set; }
    }
}