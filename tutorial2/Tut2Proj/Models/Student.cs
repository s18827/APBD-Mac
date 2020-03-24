using System.Xml;
using System.Xml.Serialization;
using System;

namespace Tut2Proj.Models
{
    [XmlType(TypeName = "student")]
    public class Student
    {
        [XmlAttribute(AttributeName = "indexNumber")]
        public string SNumber { get; set; }

        [XmlElement(ElementName = "fname")]
        // [JsonPropertyName("fname")]
        public string FName { get; set; }

        [XmlElement(ElementName = "lname")]
        public string LName { get; set; }

        [XmlElement(ElementName = "birthdate")]
        public string Birthdate { get; set; }

        [XmlElement(ElementName = "email")]
        public string EmailAddress { get; set; }

        [XmlElement(ElementName = "mothersName")]
        public string MothersName { get; set; }

        [XmlElement(ElementName = "fathersName")]
        public string FathersName { get; set; }

        [XmlElement(ElementName = "studies")]
        public Studies HisStudies { get; set; }

        // for returning error info into log.txt
        public string GetInfo()
        {
            return FName + "," + LName + "," + HisStudies.Name + "," + HisStudies.Mode + "," + SNumber + "," + Birthdate + "," + EmailAddress + "," + MothersName + "," + FathersName;
        }
    }
}
