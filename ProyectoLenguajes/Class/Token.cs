using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoLenguajes.Class
{
    public class Token
    {
        //class to know the format the diferent tokens

        public string Name; //Name the token
        public char[] Values; //values the token 

        //method builder
        public Token()
        {
            Name = "";
            Values = null;
        }

        //method insert diferent tokens
        public List<Token> Insert_Tokens()
        {
            //create new list, then return values
            List<Token> tokens = new List<Token>();

            //t1
            //create token
            Token a = new Token();
            //name token
            a.Name = "a";
            //values token = Capital Letters
            string A = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
            char[] Mayusuculas = A.ToArray();
            a.Values = Mayusuculas;
            //insert token in list
            tokens.Add(a);

            //t2
            //create token
            Token b = new Token();
            //name token
            b.Name = "b";
            //values token = Lowercase Letters
            string B = "abcdefghijklmnñopqrstuvwxyz";
            char[] Minusculas = B.ToArray();
            b.Values = Minusculas;
            //insert token in list
            tokens.Add(b);

            //t3
            //create token
            Token c = new Token();
            //name token
            c.Name = "c";
            //values token = numbers
            string C = "0123456789";
            char[] Numeros = C.ToArray();
            c.Values = Numeros;
            //insert token in list
            tokens.Add(c);

            //t4
            //create token
            Token d = new Token();
            //name token
            d.Name = "d";
            //values token = CHR --> Character in ascii
            string D = "CHR";
            char[] Chr = D.ToArray();
            d.Values = Chr;
            //insert token in list
            tokens.Add(d);

            //t5
            //create token
            Token e = new Token();
            //name token
            e.Name = "e";
            //values token = TOKEN --> reserved word
            string E = "TOKEN";
            char[] TOKEN = E.ToArray();
            e.Values = TOKEN;
            //insert token in list
            tokens.Add(e);

            return tokens;//defect
        }
    }
   
}
