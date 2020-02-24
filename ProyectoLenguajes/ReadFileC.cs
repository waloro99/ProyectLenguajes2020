using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProyectoLenguajes
{
    public class ReadFileC
    {
        public string[] ReadFile(string PathF)
        {
            string NameFile = @"" + PathF; //path the file
            StreamReader read = new StreamReader(NameFile); //reader
            var txtAll = read.ReadToEnd(); //have all text
            string[] T_Lineas = txtAll.Split(new[] { "\n\r", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries); //split line

            return T_Lineas; //test function
        }

        //method for separate section sets
        public string[] SplitSets(string [] T_Lineas)
        {
            foreach (var item in T_Lineas)
            {
                if (Word(item, "SETS"))
                {
                    return null;
                }

            }
            return null;
        }

        //method for separate section tokens
        public string[] SplitTokens(string[] T_Lineas)
        {

            return null;
        }

        //method for separate section actions
        public string[] SplitActions(string[] T_Lineas)
        {

            return null;
        }

        //method for search specific section
        public bool Word(string word, string CompareWord)
        {
            if (word == CompareWord)
            {
                return true;
            }
            return false;
        }




    }
}
