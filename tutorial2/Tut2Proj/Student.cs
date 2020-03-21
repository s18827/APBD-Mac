using System.Xml;
using System.Xml.Serialization;
using System;

namespace Tut2Proj
{
    public class Student
    {
        [XmlAttribute(AttributeName = "indexNumber")]
        public string SNumber { get; set; }

        [XmlElement(ElementName = "fname")]
        public string FName { get; set; }

        [XmlElement(ElementName = "lname")]
        public string LName { get; set; }

        [XmlElement(ElementName = "birthdate")]
        public string Birthdate { get; set; }

        [XmlElement(ElementName = "email")]
        public string EmailAddress { get; set; }

        [XmlElement(ElementName = "morhersName")]
        public string MothersName { get; set; }

        [XmlElement(ElementName = "fathersName")]
        public string FathersName { get; set; }

        [XmlElement(ElementName = "studies")]
        public Studies Studies { get; set; }

    }
}
