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
            var studiesList = new List<Studies>();

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
                        Studies = new Studies(student[2],student[3]),
                        SNumber = student[4],
                        Birthdate = student[5],
                        EmailAddress = student[6],
                        MothersName = student[7],
                        FathersName = student[8]
                    });
                    // if (!studiesList.Exists(Studies.Name, student[2]))
                    // {
                    //     studiesList.Add(new Studies(student[2], student[3]));
                    // }else
                    // {
                    //     Studies.NumOfPpl++;
                    // }
                }
                FileStream writer = new FileStream(@args[1], FileMode.Create);
                if (args[2] == "xml")
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Student>), new XmlRootAttribute("university"));
                    xmlSerializer.Serialize(writer, list);
                }
                if (args[2] == "json")
                {
                    var jsonString = JsonSerializer.Serialize(list);
                    File.WriteAllText(@args[1], jsonString);
                }
            }
        }
        // TODO:
        // add resources management (using(){})
        // problem with already existing students (log.txtg)
        // problem with already existing studiesField & adding ppl to them
        // json structure improvement
        // error handling
    }
}
