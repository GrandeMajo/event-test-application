using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

namespace WpfApplication1
{
    class Serializer
    {
        public static byte[] ObjectSerialize(object obj)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(memoryStream, obj);
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Flush();
                return bytes;
            }
        }

        public static object ObjectDeserialize(byte[] serializedObject)
        {
            using (MemoryStream memoryStream = new MemoryStream(serializedObject))
            {
                memoryStream.Position = 0;
                BinaryFormatter bf = new BinaryFormatter();
                return bf.Deserialize(memoryStream);
            }
        }
    }
}
