using System;
using System.Collections.Generic;
using System.IO;
using Tut2Proj.Models;

namespace Tut2Proj.Services
{
    public class StudentErrorHandler
    {
        public StreamWriter writer;

        public StudentErrorHandler(string errorFilePath)
        {
            writer = new StreamWriter(errorFilePath);
        }

        public bool ContainRepeatValue(int personNo, IEnumerable<Student> students, Student st)
        {
            foreach (var student in students) // do this without loop and goto ?
            {
                if ((st.SNumber == student.SNumber) || (st.FName == student.FName) || (st.LName == student.LName))
                {
                    System.Console.WriteLine("Repetetive value found...");
                    System.Console.WriteLine("\tWriting error info + student info to log.txt...");
                    writer.WriteLine("----------------------------------------------------------------------------");
                    writer.WriteLine("Message : Repetetive value of identifier argument in person No.: " + personNo + ":\n\t" + st.GetInfo());
                    return true;
                }
            }
            return false;
        }

        public bool ContainsEmptyField(int personNo, string[] student, Student st)
        {
            foreach (var field in student) // do this without loop and goto ?
            {
                if (field == "")
                {
                    System.Console.WriteLine("Empty value found...");
                    System.Console.WriteLine("\tWriting error info + student info to log.txt...");
                    writer.WriteLine("----------------------------------------------------------------------------");
                    writer.WriteLine("Message : Empty value of argument in person No.: " + personNo + ":\n\t" + st.GetInfo());
                    return true;
                }
            }
            return false;
        }
        public void WriteErrorToLog(Exception ex, int personNo)
        {
            while (ex != null)
            {
                System.Console.WriteLine("Missing argument found...");
                System.Console.WriteLine("\tWriting error info to log.txt...");
                writer.WriteLine("----------------------------------------------------------------------------");
                writer.WriteLine(ex.GetType().FullName);
                writer.WriteLine("Message : " + "Mising argument in person No.: " + personNo);
                writer.WriteLine("StackTrace : " + ex.StackTrace);
                ex = ex.InnerException;
            }
        }

        public void CloseLogWriter()
        {
            writer.Close();
        }

        // public override ClearLogFile()
        // {

        // }
    }
}