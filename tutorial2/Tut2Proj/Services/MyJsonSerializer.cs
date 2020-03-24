using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Tut2Proj.Models;

namespace Tut2Proj.Services
{
    public class MyJsonSerializer : ISerializer
    {
        public void SerializeStudents(IEnumerable<Student> students, FileStream writer)
        {
            System.Console.WriteLine("students serialization in progress...");
            var jsonString = JsonSerializer.Serialize(students);
            var bytes = Encoding.ASCII.GetBytes(jsonString);
            writer.Write(bytes);
        }

        public void SerializeActiveStudies(IEnumerable<ActiveStudies> activeStudies, FileStream writer)
        {
            System.Console.WriteLine("activeStudies serialization in progress...");
            var jsonString = JsonSerializer.Serialize(activeStudies);
            var bytes = Encoding.ASCII.GetBytes(jsonString);
            writer.Write(bytes);
        }
    }
}