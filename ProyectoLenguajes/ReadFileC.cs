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
            bool verificar = false;
            for (int i = 0; i<T_Lineas.Count();i++)
            {
                if (WordIncorrect(T_Lineas[i], "SETS"))
                {
                    i++; //diferent the word SETS
                    while (WordIncorrect(T_Lineas[i], "TOKENS") != true && verificar == false)
                    {
                        if (EmptyLines(T_Lineas[i]))
                        {
                            Sets.Add(T_Lineas[i]); //add item at list 
                            i++;
                            if (i == (T_Lineas.Count() - 1))
                                verificar = true;
                        }
                        else
                        {
                            i++;
                        }
                    }
                    verificar = true;
                    i = (T_Lineas.Count()-1); //end bucle
                }
                if (i == (T_Lineas.Count() - 1) & verificar == false) //last iteration
                {
                    Sets.Add("ERROR");
                }
            }
            return Sets; //send data only SETS
        }
        //tested completed

        //method for separate section tokens
        public List<string> SplitTokens(string[] T_Lineas)
        {
            bool verificar = false;
            for (int i = 0; i < T_Lineas.Count(); i++)
            {
                if (WordIncorrect(T_Lineas[i], "TOKENS"))
                {
                    i++; //diferent the word SETS
                    while ( WordIncorrect(T_Lineas[i],"ACTIONS") != true && verificar == false)
                    {
                        if (EmptyLines(T_Lineas[i]))
                        {
                            Tokens.Add(T_Lineas[i]); //add item at list 
                            i++;
                            if (i == (T_Lineas.Count() - 1))
                                verificar = true;
                        }
                        else
                        {
                            i++;
                        }
                    }
                    verificar = true;
                    i = T_Lineas.Count(); //end bucle
                }
                if (i == (T_Lineas.Count() - 1) & verificar == false) //last iteration
                {
                    Tokens.Add("ERROR");
                }
            }
            return Tokens;//send data only TOKENS
        }
        //tested completed

        //method for separate section actions
        public List<string> SplitActions(string[] T_Lineas)
        {
            bool verificar = false, v1 = false, v2 = false, v3 = false;

            for (int i = 0; i < T_Lineas.Count(); i++)
            {
                if (WordIncorrect(T_Lineas[i], "ACTIONS"))
                {
                    i++; //diferent the word SETS
                    while (WordIncorrect(T_Lineas[i], "ERROR =") != true && verificar == false)
                    {
                        //They are not part of regular expressions
                        if (!T_Lineas[i].Contains("RESERVADAS()") && !T_Lineas[i].Contains("{") && !T_Lineas[i].Contains("}") && EmptyLines(T_Lineas[i]))
                        {
                            Actions.Add(T_Lineas[i]); //add item at list 
                            i++;
                            if (i == (T_Lineas.Count() - 1))
                                verificar = true;
                        }
                        else
                        {
                            if (T_Lineas[i].Contains("RESERVADAS()"))
                            {
                                v1 = true;
                            }
                            else if (T_Lineas[i].Contains("{") && v1==true)
                            {
                                v2 = true;
                            }
                            else if (T_Lineas[i].Contains("}") && v2 == true)
                            {
                                v3 = true;
                            }
                            if (i == (T_Lineas.Count() - 1))
                                verificar = true;
                            i++;
                        }                      
                    }
                    verificar = true;
                    i = (T_Lineas.Count()-1); //end bucle
                }
                if (i == (T_Lineas.Count()-1) & verificar == false) //last iteration
                {
                    Actions.Clear();
                    Actions.Add("ERROR");
                    return Actions;
                }
                if ((v1 == false || v2 == false || v3 == false)  && i == (T_Lineas.Count() - 1))
                {
                    Actions.Clear();
                    Actions.Add("ERROR");
                    return Actions;
                }
            }
            return Actions;//send data only TOKENS
        }
        //tested completed
        
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
                        if (EmptyLines(T_Lineas[i]))
                        {
                            Error.Add(T_Lineas[i]); //add item at list 
                            i++;
                        }
                        else
                        {
                            i++;
                        }
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

        //method for delete empty lines
        public bool EmptyLines(string line)
        {
            //Contains characteres
            char[] c = line.ToArray();
            //bucle while character differt a space
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i].ToString() != " ")
                {
                    return true; //yes the line is not empty
                }
            }
            return false; // yes the line is empty
        }

    }
}
