using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Tut2Proj.Models;

namespace Tut2Proj.Services
{
    public class MyJsonSerializer : ISerializer
    {
        // private Json serializeStudent;
        // private XmlSerializer serializerActiveStudies;
        public void SerializeStudents(IEnumerable<Student> students, FileStream writer)
        {
            System.Console.WriteLine("students serialization in progress...");
            var jsonString = JsonSerializer.Serialize(students);
            File.WriteAllText(writer.Name, jsonString);
        }

        public void SerializeActiveStudies(IEnumerable<ActiveStudies> activeStudies, FileStream writer)
        {
            System.Console.WriteLine("activeStudies serialization in progress...");
            var jsonString = JsonSerializer.Serialize(activeStudies);
            File.WriteAllText(writer.Name, jsonString);
        }
    }
}