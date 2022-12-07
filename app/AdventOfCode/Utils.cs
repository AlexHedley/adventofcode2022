using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class Utils
    {
        public static string[] GetLines(string fileName)
        {
            return System.IO.File.ReadAllLines(fileName);
        }
    }
}
