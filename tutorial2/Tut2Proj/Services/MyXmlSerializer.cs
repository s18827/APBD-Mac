using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using Tut2Proj.Models;

namespace Tut2Proj.Services
{
    public class MyXmlSerializer : ISerializer
    {
        private XmlSerializer serializeStudent;
        private XmlSerializer serializerActiveStudies;

        // <activeStudies></activeStudies> should be inside <university></university> - how?
        public void SerializeStudents(IEnumerable<Student> students, FileStream writer)
        {
            System.Console.WriteLine("students serialization in progress...");
            serializeStudent = new XmlSerializer(typeof(List<Student>), new XmlRootAttribute("university"));
            serializeStudent.Serialize(writer, students);
        }

        public void SerializeActiveStudies(IEnumerable<ActiveStudies> activeStudies, FileStream writer)
        {
            System.Console.WriteLine("activeStudies serialization in progress...");
            serializerActiveStudies = new XmlSerializer(typeof(List<ActiveStudies>), new XmlRootAttribute("activeStudies"));
            serializerActiveStudies.Serialize(writer, activeStudies);
        }
    }
}