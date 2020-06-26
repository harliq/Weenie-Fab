using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WeenieFab
{
    class FileOps
    {
        public void GetLists()
        {
            


        }

        private void IntegerLists(string filepath)
        {
            filepath = "Int32Properties.txt";

            var intergerListFile = File.ReadAllLines(filepath);
            var intgerPropList = new List<string>(intergerListFile);

        }

    }
}
