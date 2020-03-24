using System.Xml;
using System.Xml.Serialization;
using System;
using System.Text.Json.Serialization;

namespace Tut2Proj.Models
{
    [XmlType("student")]
    public class Student
    {
        [XmlAttribute("indexNumber")]
        [JsonPropertyName("indexNumber")]
        public string SNumber { get; set; }

        [XmlElement("fname")]
        [JsonPropertyName("fname")]
        public string FName { get; set; }

        [XmlElement("lname")]
        [JsonPropertyName("lname")]
        public string LName { get; set; }

        [XmlElement("birthdate")]
        [JsonPropertyName("birthdate")]
        public string Birthdate { get; set; }

        [XmlElement("email")]
        [JsonPropertyName("email")]
        public string EmailAddress { get; set; }

        [XmlElement("mothersName")]
        [JsonPropertyName("mothersName")]
        public string MothersName { get; set; }

        [XmlElement("fathersName")]
        [JsonPropertyName("fathersName")]
        public string FathersName { get; set; }

        [XmlElement("studies")]
        [JsonPropertyName("studies")]
        public Studies HisStudies { get; set; }

        // for returning error info into log.txt
        public string GetInfo()
        {
            return FName + "," + LName + "," + HisStudies.Name + "," + HisStudies.Mode + "," + SNumber + "," + Birthdate + "," + EmailAddress + "," + MothersName + "," + FathersName;
        }
    }
}
