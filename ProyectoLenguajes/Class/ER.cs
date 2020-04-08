using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoLenguajes.Class
{
    public class ER
    {
        //Create ER for syntactic analysis but is first version, then more verifications
        public string CreateER(List<string> list)
        {
            string res = "(";
            string or = "|"; //symbol 
            //scroll list
            foreach (var item in list)
            {
                string aux = GetToken(item);
                res = res + aux + or;
            }
            //delete last or
            res = res.TrimEnd('|');
            res = res + ").#"; //end string
            res = LastDeleteSpace(res);
            return res;
        }

        public string Is_Correct_SETS(string c, List<string> sets)
        {
            char[] aux = c.ToArray();
            char comilla = Convert.ToChar("'");
            int inicial = 0, final = 0;
            string res = "";
            //scroll string
            for (int i = 0; i < aux.Length; i++)
            {
                if (Is_CapitalLetter(aux[i]) && i > 0)
                {
                    if (aux[i - 1] != comilla)
                    {
                        inicial = i;
                        while (Is_CapitalLetter(aux[i])) //while is true
                        {
                            i++;
                            if (!Is_CapitalLetter(aux[i]))
                                final = i - 1;
                        }
                        res = Is_SET(c,inicial,final,sets);
                        if (res != "GG")
                            i = aux.Length;
                    } //false case if --> Expamble 'A'
                }
            }

            if (res != "GG")
            {
                return "Palabra no encontrada en SETS: ' " + res + " '";
            }
            else
                return res;
        }

        //method for comparation SETS with words
        private string Is_SET(string c, int inicial, int final, List <string> name)
        {
            string res = "";
            char[] x = c.ToArray();
            //complet word
            for (int i = inicial; i < final+1; i++)
            {
                res = res + x[i];
            }

            //scroll list
            foreach (var item in name)
            {
                if (res == item)
                    return "GG";
            }
            return res;
        }


        //method for get if is capital letter
        private bool Is_CapitalLetter(char x)
        {
            string A = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
            char[] a = A.ToArray();

            for (int i = 0; i < a.Length; i++)
            {
                if (x == a[i])
                    return true;
            }
            return false;
        }

        //method for get only token the all string
        private string GetToken(string z)
        {
            string comilla = "'";
            char c = Convert.ToChar(comilla);
            string res = "";
            char[] y = z.ToArray();
            bool copy = false;
            for (int i = 0; i < y.Length; i++)
            {
                if (copy == true)
                {
                    // last item , contain name the methods the actions
                    if (y[i] == '{')
                    {
                        if (y[i - 1] != c)
                            i = y.Length - 1; //yes, exit bucle
                        else
                            res = res + y[i]; //case false
                    }
                    else
                        res = res + y[i]; //normal
                }
                if (y[i] == '=')
                    copy = true;
            }
            string aux = DeleteSpace(res);
            return aux;
        }

        //method for delete space in string in first and last position
        private string DeleteSpace(string c)
        {
            string res = "";
            char[] aux = c.ToArray();

            for (int i = 0; i < aux.Length; i++)
            {
                if (i == 0)
                {
                    if (aux[i] != ' ' && aux[i] != '\t')
                    {
                        res = res + aux[i];
                    }
                }
                else if (i == (aux.Length - 1))
                {
                    if (aux[i] != ' ' && aux[i] != '\t')
                    {
                        res = res + aux[i];
                    }
                }
                else
                {
                    res = res + aux[i];
                }
            }
            return res;
        }

        //method for delete space in last concatenation
        private string LastDeleteSpace(string c)
        {
            string res = "";
            char[] aux = c.ToArray();
            bool flag = false;

            for (int i = aux.Length-1; i > -1; i--)
            {
                if (i > (aux.Length-4))
                {
                    res = aux[i] + res;
                }
                else
                {
                    if (flag == false)
                    {
                        if (aux[i] != ' ')
                        {
                            flag = true;
                            res = aux[i] + res;
                        }
                    }
                    else if (flag == true)
                    {
                        res = aux[i] + res;
                    }       
                }
            }

            return res;
        }

    }
}
