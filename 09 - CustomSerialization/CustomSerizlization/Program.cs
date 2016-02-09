using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace CustomSerizlization
{
    class Program
    {
        static void Main()
        {
            var c = new C() { A = "a", B = "b" };
            var ser = new DataContractSerializer(typeof(C));
            var fs = new FileStream("file.dat", FileMode.OpenOrCreate);
            ser.WriteObject(fs, c);
            fs.Close();
            fs = new FileStream("file.dat", FileMode.OpenOrCreate);
            var cc = (C)ser.ReadObject(fs);
            fs.Close();
        }

    }
    [Serializable]
    public class C
    {
        public string A { get; set; }
        public string B { get; set; }
        
        [OnSerializing]
        public void OnSerializing(StreamingContext context)
        {
            A = "on serializing";
        }

        [OnSerialized]
        public void OnSerialized(StreamingContext context)
        {
            A = "on serialized";
        }
    }
}
