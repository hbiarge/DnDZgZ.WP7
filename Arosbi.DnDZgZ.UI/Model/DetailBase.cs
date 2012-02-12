﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DetailBase.cs" company="Arosbi">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the Item type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Arosbi.DnDZgZ.UI.Model
{
    /// <summary>
    /// Defines the details info of a bus stop or a bizi station.
    /// </summary>
    public abstract class DetailBase
    {
        /// <summary>
        /// Gets or sets Title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets Service.
        /// </summary>
        public string Service { get; set; }

        /// <summary>
        /// Gets or sets Items.
        /// </summary>
        public string[][] Items { get; set; }
    }
}