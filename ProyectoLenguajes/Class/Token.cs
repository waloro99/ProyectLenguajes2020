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

            //t6
            //create token
            Token f = new Token();
            //name token
            f.Name = "f";
            //values token = TOKEN --> reserved word
            string F = " \t";
            char[] Espacio = F.ToArray();
            f.Values = Espacio;
            //insert token in list
            tokens.Add(f);

            //t7
            //create token
            Token g = new Token();
            //name token
            g.Name = "g";
            //values token = TOKEN --> reserved word
            string G = "=";
            char[] Igual = G.ToArray();
            g.Values = Igual;
            //insert token in list
            tokens.Add(g);

            //t8
            //create token
            Token h = new Token();
            //name token
            h.Name = "h";
            //values token = TOKEN --> reserved word
            string H = "'";
            char[] Comilla_simple = H.ToArray();
            h.Values = Comilla_simple;
            //insert token in list
            tokens.Add(h);

            //t9
            //create token
            Token i = new Token();
            //name token
            i.Name = "i";
            //values token = TOKEN --> reserved word
            string I = "_-.,:;=<>+*(){}[]";
            char[] Simbolos = I.ToArray();
            i.Values = Simbolos;
            //insert token in list
            tokens.Add(i);

            //t10
            //create token
            Token j = new Token();
            //name token
            j.Name = "j";
            //values token = TOKEN --> reserved word
            string J = "..";
            char[] Dos_puntos = J.ToArray();
            j.Values = Dos_puntos;
            //insert token in list
            tokens.Add(j);

            //t11
            //create token
            Token k = new Token();
            //name token
            k.Name = "k";
            //values token = TOKEN --> reserved word
            string K = "+";
            char[] Concatenar = K.ToArray();
            k.Values = Concatenar;
            //insert token in list
            tokens.Add(k);

            //t12
            //create token
            Token l = new Token();
            //name token
            l.Name = "l";
            //values token = TOKEN --> reserved word
            string L = "(";
            char[] Parentesis_Abierto = L.ToArray();
            l.Values = Parentesis_Abierto;
            //insert token in list
            tokens.Add(l);

            //t13
            //create token
            Token m = new Token();
            //name token
            m.Name = "m";
            //values token = TOKEN --> reserved word
            string M = ")";
            char[] Parentesis_Cerrado = M.ToArray();
            m.Values = Parentesis_Cerrado;
            //insert token in list
            tokens.Add(m);

            //t14
            //create token
            Token n = new Token();
            //name token
            n.Name = "n";
            //values token = TOKEN --> reserved word
            string N = "+*?()|";
            char[] Signos_Operacion = N.ToArray();
            n.Values = Signos_Operacion;
            //insert token in list
            tokens.Add(n);

            //t15
            //create token
            Token ñ = new Token();
            //name token
            ñ.Name = "ñ";
            //values token = TOKEN --> reserved word
            string Ñ = "ERO";
            char[] ERROR = Ñ.ToArray();
            ñ.Values = ERROR;
            //insert token in list
            tokens.Add(ñ);

            //t15
            //create token
            Token p = new Token();
            //name token
            p.Name = "p";
            //values token = TOKEN --> reserved word
            //string P = "ERO";
            char[] Comilla = new char[]{'"'};
            p.Values = Comilla;
            //insert token in list
            tokens.Add(p);

            //t# case special , end the ER
            //create token
            Token t_special = new Token();
            //name token
            t_special.Name = "#";
            //values token = TOKEN --> reserved word
            string special = "#";
            char[] specialA = special.ToArray();
            t_special.Values = specialA;
            //insert token in list
            tokens.Add(t_special);

            return tokens;//defect
        }
    }
   
}
