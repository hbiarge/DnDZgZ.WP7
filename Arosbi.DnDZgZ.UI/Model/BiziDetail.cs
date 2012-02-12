// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BiziDetail.cs" company="Arosbi">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the BiziItem type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Arosbi.DnDZgZ.UI.Model
{
    /// <summary>
    /// Defines the detail info for a bizi station.
    /// </summary>
    public class BiziDetail : DetailBase
    {
        /// <summary>
        /// Gets the number of Bizis to take.
        /// </summary>
        public string Bizis
        {
            get
            {
                return this.Items[0][0];
            }
        }

        /// <summary>
        /// Gets the number of free parking.
        /// </summary>
        public string Aparcamientos
        {
            get
            {
                return this.Items[1][0];
            }
        }
    }
}