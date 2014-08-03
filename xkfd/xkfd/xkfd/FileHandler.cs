using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace xkfd
{
    public class KonfigDatei
    {
        string inhalt = "";


        // Wins Lesen
        public string ReadFile(String fileName)
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
        // Wins Schreiben
        public void WriteFile(String sLines)
        {
            StreamWriter myFile = new StreamWriter("config.txt");
            myFile.Write(sLines);
            myFile.Close();
        }



        // Analyse Lesen
        public string ReadFileAnalyse(String fileName, Game1 game1)
        {
            if (File.Exists(fileName))
            {
                StreamReader myFile = new StreamReader(fileName, System.Text.Encoding.Default);
                game1.liedlaenge = int.Parse(myFile.ReadLine());
                myFile.ReadLine(); // Anzahl der Beats

                while(!myFile.EndOfStream)
                {
                    game1.liedWerte.Add(int.Parse(myFile.ReadLine()));
                }


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
    }
}
