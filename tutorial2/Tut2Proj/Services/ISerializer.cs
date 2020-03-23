using System.Collections.Generic;
using System.IO;
using Tut2Proj.Models;

namespace Tut2Proj.Services
{
    public interface ISerializer
    {
        public void SerializeStudents(IEnumerable<Student> students, FileStream writer);
        public void SerializeActiveStudies(IEnumerable<ActiveStudies> activeStudies, FileStream writer);

    }
}