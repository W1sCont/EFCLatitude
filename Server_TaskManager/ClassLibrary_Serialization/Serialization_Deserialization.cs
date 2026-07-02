using System.Runtime.Serialization.Json;
namespace ClassLibrary_Serialization
{
    public class Serialization_Deserialization
    {
        public byte[] SerializeObj<T>(T obj)
        {
            try
            {
                using MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(T));
                json.WriteObject(ms, obj);
                byte[] result = ms.ToArray();
                return result;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return null; }
        }
        public T DeserializeObj<T>(byte[] jsonData, int bytesRec)
        {
            try
            {
                using MemoryStream ms = new MemoryStream(jsonData, 0, bytesRec);
                DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(T));
                T result = (T)json.ReadObject(ms);
                return result;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return default(T); }
        }
    }
}
