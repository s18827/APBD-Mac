using System.Xml;
using System.Xml.Serialization;
using System;

namespace Tut2Proj
{
    public class Studies
    {
        public Studies(string Name, string Mode)
        {
            this.Name = Name;
            this.Mode = Mode;
        }

        public Studies() { } // for serialization

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("mode")]
        public string Mode { get; set; }

        //public int NumOfPpl { get; set; }
    }
}