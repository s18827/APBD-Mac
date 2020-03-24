using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Text.Json;
using Tut2Proj.Models;
using Tut2Proj.Services;

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
            var students = ReadData(@args[0]);
            var activeStudies = ConvertToList(Studies.fieldOfStudyNumOfPpl);
            SerializeData(@args[1], @args[2], students, activeStudies);
        }

        private static IEnumerable<Student> ReadData(string inputPath)
        {
            if (!Regex.IsMatch(inputPath, "((?:[a-zA-Z]\\:){0,1}(?:[\\/][\\w.]+){1,})")) throw new ArgumentException("The path is incorrect");
            FileInfo inputFile = new FileInfo(inputPath);
            if (!inputFile.Exists) throw new FileNotFoundException("File does not exist");
            var studentList = FillListWithStudents(inputFile);
            return studentList;
        }

        private static IEnumerable<Student> FillListWithStudents(FileInfo inputFile)
        {
            var list = new List<Student>();
            string errFilePath = "log.txt";
            StudentErrorHandler errHandler = new StudentErrorHandler(errFilePath);
            using (var stream = new StreamReader(inputFile.OpenRead()))
            {
                int personCount = 0;
                string line = null;
                while ((line = stream.ReadLine()) != null)
                {
                    string[] student = line.Split(',');
                    personCount++;
                    try // add exception info to the file
                    {
                        Studies studies = new Studies
                        {
                            Name = student[2],
                            Mode = student[3]
                        };
                        Student st = new Student
                        {
                            FName = student[0],
                            LName = student[1],
                            HisStudies = new Studies(student[2], student[3]),
                            SNumber = student[4],
                            Birthdate = student[5],
                            EmailAddress = student[6],
                            MothersName = student[7],
                            FathersName = student[8]
                        };
                        if (errHandler.ContainsEmptyField(personCount, student, st)) goto REPEAT;
                        if (errHandler.ContainRepeatValue(personCount, list, st)) goto REPEAT;
                        Studies.AddToOrActivateStudies(st.HisStudies);
                        list.Add(st);
                    }
                    catch (System.IndexOutOfRangeException ex)
                    {
                        errHandler.WriteErrorToLog(ex, personCount);
                    };
                REPEAT: continue;
                }
            }
            errHandler.CloseLogWriter();
            return list;
        }

        // private static bool ContainRepeatValue()
        // {
        //     using (StreamWriter writer = new StreamWriter(errFilePath, true))
        //     {
        //         foreach (var field in student) // do this without loop and goto ?
        //         {
        //             if (field == "")
        //             {
        //                 System.Console.WriteLine("Empty value found...");
        //                 System.Console.WriteLine("Writing error info + student info to log.txt...");
        //                 writer.WriteLine("----------------------------------------------------------------------------");
        //                 writer.WriteLine("Message : Empty value of argument in person No.: " + personNo + ":\n\t" + st.GetInfo());
        //                 return true;
        //             }
        //         }
        //         return false;
        //     }
        // }
        // private static bool ContainsEmptyField(int personNo, string errFilePath, string[] student, Student st)
        // {
        //     using (StreamWriter writer = new StreamWriter(errFilePath, true))
        //     {
        //         foreach (var field in student) // do this without loop and goto ?
        //         {
        //             if (field == "")
        //             {
        //                 System.Console.WriteLine("Empty value found...");
        //                 System.Console.WriteLine("Writing error info + student info to log.txt...");
        //                 writer.WriteLine("----------------------------------------------------------------------------");
        //                 writer.WriteLine("Message : Empty value of argument in person No.: " + personNo + ":\n\t" + st.GetInfo());
        //                 return true;
        //             }
        //         }
        //         return false;
        //     }
        // }
        // private static void WriteErrorToLog(Exception ex, int personNo, string errFilePath)
        // {
        //     using (StreamWriter writer = new StreamWriter(errFilePath, true))
        //     {
        //         while (ex != null)
        //         {
        //             System.Console.WriteLine("Missing argument found...");
        //             System.Console.WriteLine("Writing error info to log.txt...");
        //             writer.WriteLine("----------------------------------------------------------------------------");
        //             writer.WriteLine(ex.GetType().FullName);
        //             writer.WriteLine("Message : " + "Mising argument in person No: " + personNo);
        //             writer.WriteLine("StackTrace : " + ex.StackTrace);
        //             ex = ex.InnerException;
        //         }
        //     }
        // }

        private static ISerializer SelectSerializer(string format)
        {
            ISerializer serializer;
            switch (format)
            {
                case "xml":
                    {
                        System.Console.WriteLine("XML serialization selected");
                        serializer = new MyXmlSerializer();
                        break;
                    }
                case "json":
                    {
                        System.Console.WriteLine("JSON serialization selected");
                        serializer = new MyJsonSerializer();
                        break;
                    }
                default: throw new ArgumentException("format not supported");
            }
            return serializer;
        }

        private static void SerializeData(string outputPath, string format, IEnumerable<Student> sList, IEnumerable<ActiveStudies> asList)
        {
            FileStream writer = new FileStream(outputPath, FileMode.Create);
            var selectedSerializer = SelectSerializer(format);
            selectedSerializer.SerializeStudents(sList, writer);
            selectedSerializer.SerializeActiveStudies(asList, writer);
        }

        // converts map of active studies and number of atendees to list of ActiveStudies
        private static IEnumerable<ActiveStudies> ConvertToList(Dictionary<string, int> dictionary)
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

    }
}
