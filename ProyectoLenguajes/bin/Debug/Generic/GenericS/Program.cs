using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
  
namespace Generics
{
   class Program
   {
       static void Main(string[] args)
       {
       while (true)
       {
           string x = "";
           Console.WriteLine("INSERTE CADENA: ");
           x = Console.ReadLine();
           
           List<string> sets = new List<string>();
           sets = SET_File();
           string y = EliminarEspacios(x);
           int i = 0; //longitud del string
			string estado = "1,4,5,7,10,11";
			 while (i < y.Length)
			 {
				 switch (estado)
				 {
					case "1,4,5,7,10,11":
					if (Comparar(sets, y[i], "DIGITOS"))
					{
					estado = "2,4,8,11";
					i++;
					}
					else if (y[i] == '+')
					{
					estado = "1,4,5,7,10,11";
					}
					else if (Comparar(sets, y[i], "LETRA"))
					{
					estado = "6,10,11";
					i++;
					}
					else if (y[i] == '-')
					{
					estado = "1,4,5,7,10,11";
					}
					else
					{
					Console.WriteLine("Caracter incorrecto: " + y[i]);
					i = x.Length; estado = "1,4,5,7,10,11";
					}
					break;
					case "2,4,8,11":
					if (Comparar(sets, y[i], "DIGITOS"))
					{
					estado = "4,11";
					i++;
					}
					else if (y[i] == '+')
					{
					estado = "3";
					i++;
					}
					else if (Comparar(sets, y[i], "LETRA"))
					{
					estado = "1,4,5,7,10,11";
					}
					else if (y[i] == '-')
					{
					estado = "9";
					i++;
					}
					else
					{
					Console.WriteLine("Caracter incorrecto: " + y[i]);
					i = x.Length; estado = "1,4,5,7,10,11";
					}
					break;
					case "6,10,11":
					if (Comparar(sets, y[i], "DIGITOS"))
					{
					estado = "5,11";
					i++;
					}
					else if (y[i] == '+')
					{
					estado = "1,4,5,7,10,11";
					}
					else if (Comparar(sets, y[i], "LETRA"))
					{
					estado = "10,11";
					i++;
					}
					else if (y[i] == '-')
					{
					estado = "1,4,5,7,10,11";
					}
					else
					{
					Console.WriteLine("Caracter incorrecto: " + y[i]);
					i = x.Length; estado = "1,4,5,7,10,11";
					}
					break;
					case "4,11":
					if (Comparar(sets, y[i], "DIGITOS"))
					{
					estado = "4,11";
					i++;
					}
					else if (y[i] == '+')
					{
					estado = "1,4,5,7,10,11";
					}
					else if (Comparar(sets, y[i], "LETRA"))
					{
					estado = "1,4,5,7,10,11";
					}
					else if (y[i] == '-')
					{
					estado = "1,4,5,7,10,11";
					}
					else
					{
					Console.WriteLine("Caracter incorrecto: " + y[i]);
					i = x.Length; estado = "1,4,5,7,10,11";
					}
					break;
					case "3":
					if (Comparar(sets, y[i], "DIGITOS"))
					{
					estado = "11";
					i++;
					}
					else if (y[i] == '+')
					{
					estado = "1,4,5,7,10,11";
					}
					else if (Comparar(sets, y[i], "LETRA"))
					{
					estado = "1,4,5,7,10,11";
					}
					else if (y[i] == '-')
					{
					estado = "1,4,5,7,10,11";
					}
					else
					{
					Console.WriteLine("Caracter incorrecto: " + y[i]);
					i = x.Length; estado = "1,4,5,7,10,11";
					}
					break;
					case "9":
					if (Comparar(sets, y[i], "DIGITOS"))
					{
					estado = "11";
					i++;
					}
					else if (y[i] == '+')
					{
					estado = "1,4,5,7,10,11";
					}
					else if (Comparar(sets, y[i], "LETRA"))
					{
					estado = "1,4,5,7,10,11";
					}
					else if (y[i] == '-')
					{
					estado = "1,4,5,7,10,11";
					}
					else
					{
					Console.WriteLine("Caracter incorrecto: " + y[i]);
					i = x.Length; estado = "1,4,5,7,10,11";
					}
					break;
					case "5,11":
					if (Comparar(sets, y[i], "DIGITOS"))
					{
					estado = "1,4,5,7,10,11";
					}
					else if (y[i] == '+')
					{
					estado = "1,4,5,7,10,11";
					}
					else if (Comparar(sets, y[i], "LETRA"))
					{
					estado = "6";
					i++;
					}
					else if (y[i] == '-')
					{
					estado = "1,4,5,7,10,11";
					}
					else
					{
					Console.WriteLine("Caracter incorrecto: " + y[i]);
					i = x.Length; estado = "1,4,5,7,10,11";
					}
					break;
					case "10,11":
					if (Comparar(sets, y[i], "DIGITOS"))
					{
					estado = "1,4,5,7,10,11";
					}
					else if (y[i] == '+')
					{
					estado = "1,4,5,7,10,11";
					}
					else if (Comparar(sets, y[i], "LETRA"))
					{
					estado = "10,11";
					i++;
					}
					else if (y[i] == '-')
					{
					estado = "1,4,5,7,10,11";
					}
					else
					{
					Console.WriteLine("Caracter incorrecto: " + y[i]);
					i = x.Length; estado = "1,4,5,7,10,11";
					}
					break;
					case "11":
					if (Comparar(sets, y[i], "DIGITOS"))
					{
					estado = "1,4,5,7,10,11";
					}
					else if (y[i] == '+')
					{
					estado = "1,4,5,7,10,11";
					}
					else if (Comparar(sets, y[i], "LETRA"))
					{
					estado = "1,4,5,7,10,11";
					}
					else if (y[i] == '-')
					{
					estado = "1,4,5,7,10,11";
					}
					else
					{
					Console.WriteLine("Caracter incorrecto: " + y[i]);
					i = x.Length; estado = "1,4,5,7,10,11";
					}
					break;
					case "6":
					if (Comparar(sets, y[i], "DIGITOS"))
					{
					estado = "5,11";
					i++;
					}
					else if (y[i] == '+')
					{
					estado = "1,4,5,7,10,11";
					}
					else if (Comparar(sets, y[i], "LETRA"))
					{
					estado = "1,4,5,7,10,11";
					}
					else if (y[i] == '-')
					{
					estado = "1,4,5,7,10,11";
					}
					else
					{
					Console.WriteLine("Caracter incorrecto: " + y[i]);
					i = x.Length; estado = "1,4,5,7,10,11";
					}
					break;
					 default:
						 break;
				 }
			 }
			if ( estado == "1,4,5,7,10,11" ||estado == "2,4,8,11" ||estado == "6,10,11" ||estado == "4,11" ||estado == "5,11" ||estado == "10,11" ||estado == "11" )
               {
                    Console.WriteLine("Cadena exitosa!");
                    //luegp si la bandera no cambio enviar a que token es cada char del string
                    //llegando a este punto el programa va ser capaz de indicar que token es
                    List<string> word = new List<string>();//contendra las palabras
                    word = CreateWords(x);
                    foreach (var item in word)
                    {
                        int type = TypeWord(item);
                        switch (type)
                        {
                            case 0: //es una palabra
                                string res = GetToken(item);
                                Console.WriteLine(res);
                                break;
                            case 1://descomponer la palabra

                                break;
                            case 2://solo caracteres
                                string res2 = GetCaracter(item);
                                Console.WriteLine(res2);
                                break;
                            default:
                                break;
                        }

                    }
                }
                else
                    Console.WriteLine("Error termino en un estado no aceptado");

                Console.ReadLine();//para que se pueda ver el resultado
                Console.Clear();//limpia consola
            }

        }




        private static List<string> SET_File()
        {
            List<string> x = new List<string>();
            string[] lines = File.ReadAllLines(@"data.txt");
            bool flag = false;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == "wSets")
                    flag = true;
                else if (flag == true && lines[i] != "wTokens")
                    x.Add(lines[i]);
                else if (lines[i] == "wTokens")
                    i = i + lines.Length;
            }
            return x;
        }

        private static bool Comparar(List<string> v, char x, string cadena)
        {
            foreach (var item in v)
            {
                string[] f = item.Split(',');
                if (f[0] == cadena)
                {
                    if (f[1].Contains(x))
                        return true;
                }
            }
            return false;
        }

        private static string EliminarEspacios(string x)
        {
            string res = "";

            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != ' ')
                    res = res + x[i];
            }
            return res;
        }

        private static List<string> CreateWords(string x)
        {
            List<string> res = new List<string>();
            string word = "";
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != ' ')
                    word = word + x[i];
                else if (word != "")
                {
                    res.Add(word);
                    word = "";
                }
            }

            if (word != "")
                res.Add(word);

            return res;
        }

        private static int TypeWord(string x)
        {
            string letras = "ABCDEFGHIJKLMN�OPQRSTUVWXYZabcdefghijqlmn�opqrstuvwxyz";
            string numeros = "0123456789";
            int type = 0;
            for (int i = 0; i < x.Length; i++)
            {
                if (letras.Contains(x[i]))
                    type = 0;
                else if (numeros.Contains(x[i]))
                    type = 0;
                else
                {
                    if (i == 0)
                    {
                        i = x.Length;
                        type = 2;
                    }
                    else
                    {
                        type = 1;
                        i = x.Length;
                    }
                }
            }

            return type;
        }

        private static string GetToken(string x)
        {
            string res = "";
            string[] lines = File.ReadAllLines(@"data.txt");
            List<string> tokens = new List<string>();
            List<string> actions = new List<string>();
            List<string> sets = new List<string>();
            bool flags = false;
            bool flagt = false;
            bool flaga = false;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == "wSets")
                    flags = true;
                else if (flags == true && lines[i] != "wTokens")
                    sets.Add(lines[i]);
                else if (lines[i] == "wTokens")
                {
                    flagt = true;
                    flags = false;
                }
                else if (flagt == true && lines[i] != "wActions")
                    tokens.Add(lines[i]);
                else if (lines[i] == "wActions")
                {
                    flaga = true;
                    flagt = false;
                }

                else if (flaga == true && lines[i] != "wError")
                    actions.Add(lines[i]);
                else if (lines[i] == "wError")
                    flaga = false;
            }
            flags = false;
            flagt = false;
            flaga = false;
            //comparar actions
            foreach (var item in actions)
            {
                //error debemos verificar que no solo lo contenga sino que sea igual
                string act = ObtenerAction(item);
                if (act == x.ToUpper())
                {
                    string num = ObtenerNumeroToken(item);
                    res = x + " = " + num;
                    flaga = true;
                }
            }
            List<string> names = new List<string>();
            if (flaga != true)
            {
                for (int i = 0; i < sets.Count; i++)
                {
                    string[] s = sets[i].Split(',');
                    for (int j = 0; j < x.Length; j++)
                    {
                        if (s[1].Contains(x[j]))
                        {
                            foreach (var item in names)
                            {
                                if (s[0] == item)
                                {
                                    flags = true;
                                }
                            }
                            if (flags == false)
                            {
                                names.Add(s[0]);
                            }
                            flags = false;//reinicio bandera
                            j = x.Length;//alir
                        }
                    }
                }
            }

            if (names.Count > 0)
            {
                res = ObtenerCadena(names, x, tokens);
            }

            return res;
        }

        private static string ObtenerNumeroToken(string x)
        {
            string r = "";
            string n = "0123456789";
            for (int i = 0; i < x.Length; i++)
            {
                if (n.Contains(x[i]))
                    r = r + x[i];
                else if (x[i] == '=')
                    i = x.Length;
            }

            return r;
        }

        private static string ObtenerCadena(List<string> s, string x, List<string> t)
        {
            string r = "";
            for (int j = 0; j < t.Count; j++)
            {
                bool flag = false;
                for (int i = 0; i < s.Count; i++)
                {
                    if (t[j].Contains(s[i]))
                        flag = true;
                    if (flag == false)
                        i = s.Count;
                }
                if (flag == true)
                {
                    string num = ObtenerNumeroToken(t[j]);
                    r = x + " = " + num;
                }
                if (s.Count == 1 && r != "")
                {
                    j = t.Count;
                }
            }

            return r;

        }

        private static string ObtenerAction(string x)
        {
            string res = "";

            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] == Convert.ToChar("'"))
                {
                    i++;
                    while (x[i] != Convert.ToChar("'"))
                    {
                        res = res + x[i];
                        i++;
                    }
                }
            }

            return res;
        }

       private static string GetCaracter(string x)
        {
            string res = "";
            string[] lines = File.ReadAllLines(@"data.txt");
            List<string> tokens = new List<string>();
            bool flagt = false;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == "wTokens")
                    flagt = true;
                else if (flagt == true && lines[i] != "wActions")
                    tokens.Add(lines[i]);
                else if (lines[i] == "wActions")
                {
                    flagt = true;
                    i = lines.Length;
                }
            }

            for (int i = 0; i < tokens.Count; i++)
            {
                if (CompareCaracter(tokens[i], x))
                {
                    string num = ObtenerNumeroToken(tokens[i]);
                    res = x + " = " + num;
                }
            }

            return res;
        }

        private static bool CompareCaracter(string x, string y)
        {
            string data = "";

            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] == Convert.ToChar("'"))
                {
                    i++;
                    data = data + x[i];
                    i++;
                }
            }

            if (data == y)
                return true;
            else
                return false;
        }

    }

}
