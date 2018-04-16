using System.Collections;
using System.IO;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace DeviceDetectorNET.Yaml
{
    public class YamlParser<T> : IParser<T>
        where T : class, IEnumerable// IParseLibrary
    {
        public T ParseFile(string file)
        {
            using (var r = new StreamReader(file))
            {
                return Parse(r);
            }
        }

        private static T Parse(StreamReader r)
        {
            var deserializer = new DeserializerBuilder().Build();
            var parser = new YamlDotNet.Core.Parser(r);

            // Consume the stream start event "manually"
            parser.Expect<StreamStart>();

            while (parser.Accept<DocumentStart>())
                // Deserialize the document
            {
                return deserializer.Deserialize<T>(parser);
            }
            return null;
        }

        public T ParseStream(Stream stream)
        {
            using (var r = new StreamReader(stream))
            {
                return Parse(r);
            }
        }
    }
}
