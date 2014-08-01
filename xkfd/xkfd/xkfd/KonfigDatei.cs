using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace xkfd
{
    class KonfigDatei
    {
        string inhalt = "";
        string fileName = "config.txt";

        public string ReadFile()
        {
            if (File.Exists(fileName))
            {
                StreamReader myFile = new StreamReader(fileName, System.Text.Encoding.Default);
                inhalt = myFile.ReadToEnd();
                myFile.Close();
            }
            else
            {
                File.Create(fileName).Dispose();
                StreamWriter myFile = new StreamWriter(fileName);
                myFile.Write("0");
                myFile.Close();
                inhalt = "0";
                
            }
            return inhalt;
        }

        public void WriteFile(String sLines)
        {
            StreamWriter myFile = new StreamWriter(fileName);
            myFile.Write(sLines);
            myFile.Close();
        }
    }
}
