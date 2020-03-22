using System.Linq;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Text.Json;

namespace Tut2Proj
{
    class ExportData
    {
        // TO RUN:
        // xml serialization:
        // dotnet run "/Users/azyl/Git-Uni/APBD-Mac/tutorial2/inputData.csv" "/Users/azyl/Git-Uni/APBD-Mac/tutorial2/outputData.xml" xml
        // json serialization:
        // dotnet run "/Users/azyl/Git-Uni/APBD-Mac/tutorial2/inputData.csv" "/Users/azyl/Git-Uni/APBD-Mac/tutorial2/outputData.json" json

        static void Main(string[] args)
        {
            var list = new List<Student>();
            FileInfo inputFile = new FileInfo(@args[0]);
            using (var stream = new StreamReader(inputFile.OpenRead()))
            {
                string line = null;
                while ((line = stream.ReadLine()) != null)
                {
                    string[] student = line.Split(',');
                    list.Add(new Student
                    {
                        FName = student[0],
                        LName = student[1],
                        Studies = Studies.StudiesResolver(student[2], student[3]),
                        SNumber = student[4],
                        Birthdate = student[5],
                        EmailAddress = student[6],
                        MothersName = student[7],
                        FathersName = student[8]
                    });
                }
                //list = GetCountOfStudies(hashSet).Keys.ToList();
                FileStream writer = new FileStream(@args[1], FileMode.Create);
                if (args[2] == "xml")
                {
                    XmlSerializer xmlSerializerStud = new XmlSerializer(typeof(List<Student>), new XmlRootAttribute("university"));
                    xmlSerializerStud.Serialize(writer, list);
                    XmlSerializer xmlSerializerAStudies = new XmlSerializer(typeof(List<ActiveStudies>), new XmlRootAttribute("activeStudies"));
                    var activeStudies = convertToSet(Studies.fieldOfStudyNumOfPpl);
                    xmlSerializerAStudies.Serialize(writer, activeStudies);
                }
                if (args[2] == "json")
                {
                    var jsonString = JsonSerializer.Serialize(list);
                    File.WriteAllText(@args[1], jsonString);
                }
            }
        }
        // converts map of active fields of study and ppl attending them to list of Studies
        private static IEnumerable<ActiveStudies> convertToSet(Dictionary<string, int> dictionary)
        {
            var activeStudiesList = new List<ActiveStudies>();
            foreach (var VARIABLE in dictionary)
            {
                activeStudiesList.Add(new ActiveStudies
                {
                    Name = VARIABLE.Key,
                    NumOfStud = VARIABLE.Value
                });
            }
            return activeStudiesList;
        }
        // TODO:
        // problem with already existing students (log.txtg)
        // json structure improvement
        // error handling
    }
}
