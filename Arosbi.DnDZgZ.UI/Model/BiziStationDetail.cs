namespace Arosbi.DnDZgZ.UI.Model
{
    public class BiziStationDetail : DetailBase
    {
        public string Bizis
        {
            get
            {
                return this.Items[0][0];
            }
        }

        public string Aparcamientos
        {
            get
            {
                return this.Items[1][0];
            }
        }
    }
}