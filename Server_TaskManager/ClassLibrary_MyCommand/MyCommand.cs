using System.Runtime.Serialization;

namespace ClassLibrary_MyCommand
{
    [Serializable]
    [DataContract]
    public class MyCommand
    {
        [DataMember]
        public string NameOfCommand { get; set; }
        [DataMember]
        public int IdProcess { get; set; }
        [DataMember]
        public string Path { get; set; }
        [DataMember]
        public bool CommandResult { get; set; }

        public override string ToString()
        {
            return NameOfCommand;
        }

        public MyCommand() { CommandResult = false; }
    }
}
