using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System;

namespace Tut2Proj.Models
{
    public class Studies
    {
        public static Dictionary<string, int> fieldOfStudyNumOfPpl = new Dictionary<string, int>();
        public Studies(string name, string mode)
        {
            this.Name = name;
            this.Mode = mode;
        }
        public Studies() { } // for serialization

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("mode")]
        public string Mode { get; set; }

        public static void AddToOrActivateStudies(Studies studies) // could be done better
        {
            if (!fieldOfStudyNumOfPpl.ContainsKey(studies.Name))
            {
                fieldOfStudyNumOfPpl.Add(studies.Name, 1);
            }
            else
            {
                fieldOfStudyNumOfPpl[studies.Name]++;
            }
        }
    }
}