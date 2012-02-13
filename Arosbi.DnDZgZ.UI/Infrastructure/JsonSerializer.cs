// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonSerializer.cs" company="Arosbi">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the JsonSerializer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Arosbi.DnDZgZ.UI.Infrastructure
{
    using JsonFx.Json;
    using JsonFx.Serialization;
    using JsonFx.Serialization.Resolvers;

    /// <summary>
    /// Deserialize Json data.
    /// </summary>
    public class JsonSerializer
    {
        /// <summary>
        /// Deserialize Json data.
        /// </summary>
        /// <param name="jsonString">The json string.</param>
        /// <typeparam name="T">Data type.</typeparam>
        /// <returns>Instace of data type.</returns>
        public T Deserialize<T>(string jsonString) where T : class
        {
            var conventionResolverStrategy =
                new ConventionResolverStrategy(ConventionResolverStrategy.WordCasing.Lowercase, "-");
            var dataWriterSettings = new DataReaderSettings(conventionResolverStrategy);
            var reader = new JsonReader(dataWriterSettings);
            var data = reader.Read<T>(jsonString);
            return data;
        }
    }
}
