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
        public int CountLinesFile; //lines read in the file
        public List<string> Sets = new List<string>(); //save data the sets
        public List<string> Tokens = new List<string>(); //save data the tokens
        public List<string> Actions = new List<string>(); //save data the actions
        public List<string> Error = new List<string>(); //save data the error

        public string[] ReadFile(string PathF)
        {
            string NameFile = @"" + PathF; //path the file
            StreamReader read = new StreamReader(NameFile); //reader
            var txtAll = read.ReadToEnd(); //have all text
            string[] T_Lineas = txtAll.Split(new[] { "\n\r", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries); //split line
            CountLinesFile = T_Lineas.Count();
            return T_Lineas; //test function
        }

        //method for separate section sets
        public List<string> SplitSets(string [] T_Lineas)
        {
            for(int i = 0; i<T_Lineas.Count();i++)
            {
                if (WordIncorrect(T_Lineas[i], "SETS"))
                {
                    i++; //diferent the word SETS
                    while (T_Lineas[i] != "TOKENS")
                    {
                        Sets.Add(T_Lineas[i]); //add item at list 
                        i++; 
                    }
                    i = T_Lineas.Count(); //end bucle
                }

            }
            return Sets; //send data only SETS
        }

        //method for separate section tokens
        public List<string> SplitTokens(string[] T_Lineas)
        {
            for (int i = 0; i < T_Lineas.Count(); i++)
            {
                if (WordIncorrect(T_Lineas[i], "TOKENS"))
                {
                    i++; //diferent the word SETS
                    while ( WordIncorrect(T_Lineas[i],"ACTIONS") != true)
                    {
                        Tokens.Add(T_Lineas[i]); //add item at list 
                        i++;
                    }
                    i = T_Lineas.Count(); //end bucle
                }
            }
            return Tokens;//send data only TOKENS
        }

        //method for separate section actions
        public List<string> SplitActions(string[] T_Lineas)
        {
            bool verificar = false;
            for (int i = 0; i < T_Lineas.Count(); i++)
            {
                if (WordIncorrect(T_Lineas[i], "ACTIONS"))
                {
                    i++; //diferent the word SETS
                    while (i < T_Lineas.Count())
                    {
                        Actions.Add(T_Lineas[i]); //add item at list 
                        i++;
                    }
                    verificar = true;
                }
                if (i == (T_Lineas.Count()-1) & verificar == false) //last iteration
                {
                    Actions.Add("ERROR EN SECCION ACTIONS");
                }
            }
            return Actions;//send data only TOKENS
        }

        //method for separate section error
        public List<string> SplitError(string[] T_Lineas)
        {
            bool verificar = false;
            for (int i = 0; i < T_Lineas.Count(); i++)
            {
                if (WordIncorrect(T_Lineas[i], "ERROR ="))
                {
                   // i++; //diferent the word SETS
                    while (i < T_Lineas.Count())
                    {
                        Error.Add(T_Lineas[i]); //add item at list 
                        i++;
                    }
                    verificar = true;
                }
                if (i == (T_Lineas.Count() - 1) & verificar == false) //last iteration
                {
                    Error.Add("ERROR");
                }
            }
            return Error;//send data only TOKENS
        }

        //method for search specific section
        public bool WordIncorrect(string a, string b)
        {
            bool verificar;
            verificar = a.Contains(b); // YES , verificar = true
            return verificar;
        }



    }
}
