using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace WpfApplication1
{
    class Serializer
    {
        public static byte[] ObjectSerialize(object obj)
        {
            try
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
            catch (SerializationException se)
            {
                return null;
            }
            //catch (Exception e)
            //{
            //    return null;
            //}
        }

        public static object ObjectDeserialize(byte[] serializedObject)
        {
            try
            {
                using (MemoryStream memoryStream = new MemoryStream(serializedObject))
                {
                    memoryStream.Position = 0;
                    BinaryFormatter bf = new BinaryFormatter();
                    return bf.Deserialize(memoryStream);
                }
            }
            catch (SerializationException se)
            {
                return null;
            }
        }
    }
}
