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
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets Subtittle.
        /// </summary>
        public string Subtitle { get; set; }

        /// <summary>
        /// Gets or sets Lon.
        /// </summary>
        public double Lon { get; set; }

        /// <summary>
        /// Gets or sets Lat.
        /// </summary>
        public double Lat { get; set; }

        /// <summary>
        /// Gets or sets Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Title.
        /// </summary>
        public string Title { get; set; }
    }
}