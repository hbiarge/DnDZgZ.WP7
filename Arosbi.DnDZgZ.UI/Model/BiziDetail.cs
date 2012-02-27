namespace Arosbi.DnDZgZ.UI.Model
{
    public class BiziDetail : DetailBase
    {
        public string Bizis
        {
            get
            {
                return this.items[0][0];
            }
        }

        public string Aparcamientos
        {
            get
            {
                return this.items[1][0];
            }
        }
    }
}