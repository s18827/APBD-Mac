using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System;

namespace Tut2Proj
{
    [XmlType(TypeName="studies")]
    public class ActiveStudies
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "numberOfStudents")]
        public int NumOfStud { get; set; }
    }
}