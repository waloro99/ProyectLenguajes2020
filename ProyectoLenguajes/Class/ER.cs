using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoLenguajes.Class
{
    public class ER
    {

        public string CreateER(List<string> list)
        {
            string res = "(";
            string or = "|"; //symbol 
            string comilla = "'";
            char c = Convert.ToChar(comilla);
            foreach (var item in list)
            {
                string aux = "";
                char[] y = item.ToArray();
                bool copy = false;
                for (int i = 0; i < y.Length; i++)
                {
                    if (copy == true)
                    { 
                        // last item , contain name the methods the actions
                        if (y[i] == '{')
                        {                           
                            if(y[i-1] != c)
                                i = y.Length-1; //yes, exit bucle
                            else
                                res = res + y[i]; //case false
                        }
                        else
                        {
                            res = res + y[i]; //normal
                        }              
                    }
                    if (y[i] == '=')
                    {
                        copy = true;
                    }
                }
                res = res + or;
            }

            //delete last or
            res = res.TrimEnd('|');
            res = res + ").#"; //end string
            return res;
        }


    }
}
