using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ProyectoLenguajes.Class;

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

        //method to know read line for line the section sets
        public string ReadSets(List<string> Set, List<Token> tokens, string ER,string ER2)
        {
            //recorre lista
            foreach (var item in Set)
            {
                char[] c = item.ToArray();
                bool space1 = false, space2 = false;
                char temp = ' ';

                for (int i = 0; i < c.Length; i++)
                {
                    if(i != c.Length-1)
                        temp = c[i + 1];

                    if (space1 == false)
                    {
                        if (!Is_Token(c[i].ToString(),tokens,"a") && (Is_Token(temp.ToString(), tokens, "a")|| Is_Token(temp.ToString(), tokens, "f")))
                        {
                            if(!Is_Token(c[i].ToString(), tokens, "f") && (Is_Token(temp.ToString(), tokens, "f") || Is_Token(c[i].ToString(), tokens, "a")))
                                return i + item;
                        }
                        else if (Is_Token(c[i].ToString(), tokens, "f"))
                        {
                            space1 = true;
                        }
                    }
                    else if (space1 == true && space2 == false)
                    {

                        if ((!Is_Token(c[i].ToString(), tokens, "f") || !Is_Token(c[i].ToString(), tokens, "g")) && (Is_Token(temp.ToString(), tokens, "g") || Is_Token(temp.ToString(), tokens, "f")))
                        {
                            if(!Is_Token(c[i].ToString(), tokens, "g"))
                                return i + item;
                            //if (Is_Token(c[i].ToString(), tokens, "g") && Is_Token(temp.ToString(), tokens, "f"))
                            //    space2 = true;
                        }
                        else if (Is_Token(c[i].ToString(), tokens, "g") && !Is_Token(temp.ToString(), tokens, "f"))
                        {
                            return i + item;
                        }
                        if (Is_Token(c[i].ToString(), tokens, "g") && Is_Token(temp.ToString(), tokens, "f"))
                        {
                            space2 = true;
                        }
                    }
                    else if (space2 == true)
                    {
                        if (Is_Token(c[i].ToString(), tokens, "h"))
                        {
                            if( i != c.Length-1)
                                i++;

                            if (!Is_Token(c[i].ToString(), tokens, "a"))
                            {
                                if (!Is_Token(c[i].ToString(), tokens, "c"))
                                {
                                    if (!Is_Token(c[i].ToString(), tokens, "i"))
                                    {
                                        if (!Is_Token(c[i].ToString(), tokens, "b"))
                                        {
                                            return i + item;
                                        }                                    
                                    }
                                }
                            }
                            if (i != c.Length - 1)
                                i++;

                            if(!Is_Token(c[i].ToString(), tokens, "h"))
                            {
                                return i + item;
                            }

                            if (i != c.Length - 1)
                                i++;

                            if (Is_Token(c[i].ToString(), tokens, "j"))
                            {
                                if (i != c.Length - 1)
                                    i++;
                                if (!Is_Token(c[i].ToString(), tokens, "j"))
                                {
                                    return i + item;
                                }
                            }
                            else if (!Is_Token(c[i].ToString(), tokens, "k"))
                            {
                                if(i != c.Length - 1)
                                    return i + item;
                            }

                        }
                        else if (Is_Token(c[i].ToString(), tokens, "d")) // C
                        {
                            if (i != c.Length - 1)
                                i++;

                            if (Is_Token(c[i].ToString(), tokens, "d")) // H
                            {
                                if (i != c.Length - 1)
                                    i++;

                                if (Is_Token(c[i].ToString(), tokens, "d"))  // R
                                {
                                    if (i != c.Length - 1)
                                        i++;

                                    if (Is_Token(c[i].ToString(), tokens, "l")) // (
                                    {
                                        bool v = false;
                                        while (v == false)
                                        {
                                            if (i != c.Length - 1)
                                                i++;
                                            if (!Is_Token(c[i].ToString(), tokens, "c") && !Is_Token(c[i].ToString(), tokens, "m"))
                                                return i + item;
                                            else if (Is_Token(c[i].ToString(), tokens, "m"))
                                                v = true;
                                            else if (i == c.Length - 1 && !Is_Token(c[i].ToString(), tokens, "m"))
                                                return i + item;
                                        }
                                        if (i != c.Length - 1)
                                            i++;
                                        if (Is_Token(c[i].ToString(), tokens, "j"))
                                        {
                                            if (i != c.Length - 1)
                                                i++;
                                            if (!Is_Token(c[i].ToString(), tokens, "j"))
                                            {
                                                return i + item;
                                            }
                                        }
                                        else if (!Is_Token(c[i].ToString(), tokens, "f"))
                                        {
                                            if(i != c.Length - 1)
                                                return i + item;
                                        }

                                    }
                                    else
                                    {
                                        return i + item;
                                    }
                                }
                                else
                                {
                                    return i + item;
                                }
                            }
                            else
                            {
                                return i + item;
                            }
                        }
                        else if(!Is_Token(c[i].ToString(), tokens, "f"))
                        {
                            return i + item;
                        }
                    }
                }
            }

            return "GG";//file correct
        }

        //method to know read line for line the section error
        public string ReadTokens(List<string> Token, List<Token> tokens, string ER)
        {
            //recorre lista
            foreach (var item in Token)
            {
                char[] t = item.ToArray();
                char[] t2 ;
                char temp = ' ';
                int cont = 0;
                if (t[0] != 'T')
                {                   
                    for (int i = 0; i < t.Length; i++)
                    {
                        if (t[i] == 'T')
                        {
                            while (i < t.Length)
                            {
                                cont++;
                                i++;
                            }
                        }
                    }
                    t2 = new char[cont];
                    int x = 0;
                    for (int i = 0; i < t.Length; i++)
                    {
                        if (t[i] == 'T')
                        {
                            while (i < t.Length)
                            {
                                t2[x] = t[i];
                                i++;
                                x++;
                            }
                        }
                    }
                }
                else
                {
                    t2 = t;
                }

                bool space1 = false, space2 = false, space3 = false;
                for (int i = 0; i < t2.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            if (t2[i] != 'T')
                                return i + item;
                            break;
                        case 1:
                            if (t2[i] != 'O')
                                return i + item;
                            break;
                        case 2:
                            if (t2[i] != 'K')
                                return i + item;
                            break;
                        case 3:
                            if (t2[i] != 'E')
                                return i + item;
                            break;
                        case 4:
                            if (t2[i] != 'N')
                                return i + item;
                            break;
                        case 5:
                            if (t2[i] != ' ')
                                return i + item;
                            break;
                        default:
                            //BEFORE space
                            if(i != t2.Length-1 )
                                 temp = t2[i + 1];
                            if (space1 == false)
                            {
                                if (!Is_Token(t2[i].ToString(), tokens, "f") && Is_Token(temp.ToString(), tokens, "c"))
                                {
                                    return i + item;
                                }
                                else if (Is_Token(t2[i].ToString(), tokens, "f") && Is_Token(temp.ToString(), tokens, "c"))
                                {
                                    space1 = true;
                                }
                                else if (Is_Token(t2[i].ToString(), tokens, "c") && Is_Token(temp.ToString(), tokens, "g"))
                                {
                                    space1 = true;
                                    space2 = true;
                                }
                            }
                            else if (space1 == true && space2 == false)
                            {
                                if (!Is_Token(t2[i].ToString(), tokens, "c") && Is_Token(temp.ToString(), tokens, "f"))
                                {
                                    if(!Is_Token(temp.ToString(), tokens, "g"))
                                        if(!Is_Token(temp.ToString(), tokens, "c"))
                                            return i + item;
                                }
                                else if (Is_Token(t2[i].ToString(), tokens, "c") && Is_Token(temp.ToString(), tokens, "f"))//space
                                {
                                    space2 = true;
                                }
                                else if (Is_Token(t2[i].ToString(), tokens, "c") && Is_Token(temp.ToString(), tokens, "g")) //=
                                {
                                    space2 = true;
                                }
                            }
                            else if (space2 == true && space3 == false)
                            {
                                if ((!Is_Token(t2[i].ToString(), tokens, "g") && Is_Token(t[i].ToString(), tokens, "f")) && Is_Token(temp.ToString(), tokens, "f"))
                                {
                                    if(!Is_Token(t2[i].ToString(), tokens, "f"))
                                        return i + item;
                                }
                                else if (Is_Token(t2[i].ToString(), tokens, "g") && Is_Token(temp.ToString(), tokens, "f")) //=
                                {
                                    space3 = true;
                                }
                            }
                            else if (space3 == true)
                            {
                                if (Is_Token(t2[i].ToString(), tokens, "h"))
                                {
                                    if (!Is_Token(t2[i+1].ToString(), tokens, "a"))
                                    {
                                        if (!Is_Token(t2[i + 1].ToString(), tokens, "i"))
                                        {
                                            if (!Is_Token(t2[i + 1].ToString(), tokens, "p"))
                                            {
                                                i++;
                                                return i + item;
                                            }                                          
                                        }                                      
                                    }
                                    if ((i+2 < t2.Length))
                                    {
                                        if(!Is_Token(t2[i + 2].ToString(), tokens, "h"))
                                        {
                                            return i + item;
                                        }
                                    }
                                    else if(i == t2.Length-2)
                                    {
                                        return i + item;
                                    }
                                    i = i + 2;
                                }
                                else if (Is_Token(t2[i].ToString(), tokens, "l"))
                                {
                                    bool bandera1 = false;
                                    while (bandera1 == false)
                                    {
                                        if (i == t2.Length)
                                        {
                                            return i + item;
                                        }
                                        else if (Is_Token(t2[i].ToString(), tokens, "m"))
                                        {
                                            bandera1 = true;
                                        }                                       
                                        else if (!Is_Token(t2[i].ToString(), tokens, "f"))
                                        {
                                            if (!Is_Token(t2[i].ToString(), tokens, "a"))
                                            {
                                                if (!Is_Token(t2[i].ToString(), tokens, "n"))
                                                {
                                                    return i + item;
                                                }
                                            }
                                        }
                                        i++;
                                    }
                                }
                                else
                                {
                                    if (!Is_Token(t2[i].ToString(), tokens, "f"))
                                    {
                                        if (!Is_Token(t2[i].ToString(), tokens, "a"))
                                        {
                                            if (!Is_Token(t2[i].ToString(), tokens, "n"))
                                            {
                                                if (t2[i] != '{')
                                                {
                                                    if (t2[i] != '}')
                                                    {
                                                        return i + item;
                                                    }                                                   
                                                }                                               
                                            }
                                        }
                                    }
                                }
                            }

                            break;
                    }
                }
            }
            return "GG";//file correct
        }


        //method to know read line for line the section action
        public string ReadAction(List<string> Action, List<Token> tokens, string ER)
        {
            foreach (var item in Action)
            {
                char[] a = item.ToArray();
                char temp=' ';
                int space1 = 0; // 0=false, 1=true,2=ignorar
                int space2 = 0; // 0=false, 1=true,2=ignorar
                int comilla1 = 0; // 0=false, 1=true,2=ignorar
                int comilla2 = 0; // 0=false, 1=true,2=ignorar


                for (int i = 0; i < a.Length; i++)
                {
                    if (i != (a.Length - 1))
                    {
                        temp = a[i + 1];
                    }
                    if (a[i] == ' ' && space1 == 0)
                        space1 = 1;
                    if (a[i] == ' ' && space1 > 1)
                        space2 = 1;
                    if (a[i].ToString() == "'" && comilla1 == 0)
                        comilla1 = 1;
                    if (a[i].ToString() == "'" && comilla1 > 1)
                        comilla2 = 1;
                    if (a[i] != '\t')
                    {
                        if (space1 == 0)
                        {
                            if (!Is_Token(a[i].ToString(), tokens, "c") && Is_Token(temp.ToString(), tokens, "c"))
                            {
                                return i + item;
                            }
                            else if (!Is_Token(a[i].ToString(), tokens, "c") && Is_Token(temp.ToString(), tokens, "f"))
                            {
                                return i + item;
                            }
                        }

                        if (space1 == 1)
                        {
                            if (!Is_Token(a[i].ToString(), tokens, "f") && Is_Token(temp.ToString(), tokens, "g"))
                            {
                                return i + item;
                            }
                            space1++;
                        }
                        if (a[i] == '=' && space1 > 1)
                        {
                            if (!Is_Token(a[i].ToString(), tokens, "g") && Is_Token(temp.ToString(), tokens, "f"))
                            {
                                return i + item;
                            }
                        }
                        if (space2 == 1)
                        {
                            if (!Is_Token(a[i].ToString(), tokens, "f") && Is_Token(temp.ToString(), tokens, "h"))
                            {
                                return i + item;
                            }
                            space2++;
                        }

                        if (space2 > 1)
                        {
                            if (comilla1 == 1)
                            {
                                if (!Is_Token(a[i].ToString(), tokens, "h") && Is_Token(temp.ToString(), tokens, "a"))
                                {
                                    return i + item;
                                }
                                comilla1++;
                            }

                        }

                        if (comilla1 > 1)
                        {
                            if (comilla1 > 2 && comilla2 != 1)
                            {
                                if (!Is_Token(a[i].ToString(), tokens, "a") && Is_Token(temp.ToString(), tokens, "a"))
                                {
                                    return i + item;
                                }
                                else if (!Is_Token(a[i].ToString(), tokens, "a") && Is_Token(temp.ToString(), tokens, "h"))
                                {
                                    return i + item;
                                }
                            }
                            if (comilla2 == 1)
                            {
                                if (!Is_Token(a[i].ToString(), tokens, "h") && Is_Token(temp.ToString(), tokens, "f"))
                                {
                                    return i + item;
                                }
                            }
                            comilla1++;
                        }
                    }
        
                }
            }
            return "GG";//file correct
        }

        //method to know read line for line the section error
        public string ReadError(List<string> Error, List<Token> tokens, string ER)
        {
            //recorre lista
            foreach (var item in Error)
            {
                char[] e = item.ToArray();

                for (int i = 0; i < e.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            if (e[i] != 'E')
                                return i + item;
                            break;
                        case 1:
                            if (e[i] != 'R')
                                return i + item;
                            break;
                        case 2:
                            if (e[i] != 'R')
                                return i + item;
                            break;
                        case 3:
                            if (e[i] != 'O')
                                return i + item;
                            break;
                        case 4:
                            if (e[i] != 'R')
                                return i + item;
                            break;
                        case 5:
                            if (e[i] != ' ')
                                return i + item;
                            break;
                        case 6:
                            if (e[i] != '=')
                                return i + item;
                            break;
                        case 7:
                            if (e[i] != ' ')
                                return i + item;
                            break;
                        default:
                            if (!Is_Token(e[i].ToString(),tokens,"c"))
                                if(e[i] != ' ')
                                return i + item;
                            break;
                    }
                }

            }

            return "GG";//file correct
        }




        //method for comparation with values the token
        public bool Is_Token(string c1, List<Token> c2,string res)
        {
            //res = name the token
            foreach (var item in c2)
            {
                if (res == item.Name)
                {
                    foreach (var x in item.Values)
                    {
                        if (c1 == x.ToString())
                        {
                            return true;
                        }                       
                    }
                    return false;
                }
            }
            return false;
        }

        public string Only_Token(string ER)
        {
            char[] res = ER.ToArray();
            string ans = "";
            for (int i = 0; i < res.Length; i++)
            {
                if (res[i] == '+' || res[i] == '|' || res[i] == '*' || res[i] == '?' || res[i] == ' ')
                {
                }
                else
                {
                    ans += ans + res[i];
                }
            }
            return ans;
        }


    }
}
