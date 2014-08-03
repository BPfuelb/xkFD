using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace xkfd
{
    public class DateiHandler
    {
        string inhalt = "";
        
        public string ReadFileConfig(String fileName)
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

        public void WriteFileConfig(String sLines)
        {
            StreamWriter myFile = new StreamWriter("");
            myFile.Write(sLines);
            myFile.Close();
        }


    }
}
