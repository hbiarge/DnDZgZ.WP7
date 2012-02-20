// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServicePointBase.cs" company="Arosbi">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the ServicePointBase type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Arosbi.DnDZgZ.UI.Model
{
    /// <summary>
    /// Defines the common information of a location.
    /// </summary>
    public abstract class ServicePointBase
    {
        /// <summary>
        /// Gets or sets Id.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Gets or sets Subtittle.
        /// </summary>
        public string subtitle { get; set; }

        /// <summary>
        /// Gets or sets Lon.
        /// </summary>
        public double lon { get; set; }

        /// <summary>
        /// Gets or sets Lat.
        /// </summary>
        public double lat { get; set; }

        /// <summary>
        /// Gets or sets Name.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets Title.
        /// </summary>
        public string title { get; set; }
    }
}