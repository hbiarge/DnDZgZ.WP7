namespace Arosbi.DnDZgZ.UI.Services
{
    using System;
    using System.IO;

    using Newtonsoft.Json;

    using WP7Contrib.Communications;

    public class JsonSerializer : ISerializer
    {
        public Stream Serialize(object graph)
        {
            throw new NotImplementedException();
        }

        public string ContentType
        {
            get
            {
                return "application/json";
            }
        }

        public T Deserialize<T>(Stream stream) where T : class
        {
            var result = default(T);

            if (stream != null && stream.Length > 0L)
            {
                var text = new StreamReader(stream);
                result = JsonConvert.DeserializeObject<T>(text.ReadToEnd());
                stream.Position = 0L;
            }
            return result;
        }
    }
}
