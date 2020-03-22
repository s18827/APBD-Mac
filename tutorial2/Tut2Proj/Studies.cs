using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System;

namespace Tut2Proj
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

        // public static Dictionary<string, int> FieldOfStudyNumOfPpl { 
        //     get { return fieldOfStudyNumOfPpl; }
        //     set { fieldOfStudyNumOfPpl = new Dictionary<string, int>(); }
        // }
        //public static int NumOfPpl { get; set; }
        public static Studies StudiesResolver(string studiesName, string studiesMode)
        {
            if (!fieldOfStudyNumOfPpl.ContainsKey(studiesName))
            {
                fieldOfStudyNumOfPpl.Add(studiesName, 1);
            }
            else
            {
                fieldOfStudyNumOfPpl[studiesName]++;
            }
            return new Studies(studiesName, studiesMode);
        }

        

    }
}