using System.Collections.Generic;
using System.IO;

namespace Tut2Proj
{
    public interface ISerializer
    {
        public void SerializeStudents(IEnumerable<Student> students, FileStream writer);
        public void SerializeActiveStudies(IEnumerable<ActiveStudies> activeStudies, FileStream writer);

    }
}