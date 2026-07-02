using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace ClassLibrary_MyProcess
{
    [Serializable]
    [DataContract]
    public class MyProcess
    {
        [DataMember]
        public int ProcessId { get; set; }
        [DataMember]
        public string ProcessName { get; set; }
        public MyProcess() { }
        public MyProcess(string name, int id)
        {
            ProcessName = name;
            ProcessId = id;
        } 
        public override string ToString()
        {
            return $"{ProcessName} - {ProcessId}";
        }
    }
}
