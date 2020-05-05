using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProyectoLenguajes.Class
{
    public class GenericScanner
    {
        public int valoresE = 0;//que string debe de agarrar
        string EstadoAceptado = "";//El estado de aceptacion 

        #region PUBLIC FUNCTIONS

        //devuelve los valores que colocaron en sets
        public string GetValuesLists(string set)
        {
            string res = "";
            string caracteres = "";
            char[] x = set.ToArray();
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != ' ' && x[i] != '\t')
                {
                    if (x[i] == '=')
                        i = x.Length; //exit bucle
                    else
                        res = res + x[i];
                }
            }

            res = res + ","; //separador de nombre y caracter
            string numeros = "0123456789";
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] == Convert.ToChar("'")) //normal
                {
                    i++;//valor del caracter
                    caracteres = caracteres + x[i];
                    i++; //siguiente comilla
                    i++; //siguiente caracter
                    if (i < x.Length)
                    {
                        if (x[i] == '.')
                        {
                            caracteres = caracteres + x[i];
                            i++;//valor del otro punto
                            caracteres = caracteres + x[i];
                            i++;//valor de la comilla
                            i++;//valor del caracter
                            caracteres = caracteres + x[i];
                            i++; //siguiente comilla
                        }
                    }
                }
                else if (x[i] == 'C') //charset
                {
                    i = i + 3;//HR(
                    if (x[i] == '(')
                    {
                        i++;//valores
                        caracteres = caracteres + "°";
                        while (numeros.Contains(x[i]))
                        {
                            caracteres = caracteres + x[i];
                            i++;
                        }
                        if (x[i] == ')')
                        {
                            i++;
                            if (i < x.Length)
                            {
                                if (x[i] == '.')
                                {
                                    caracteres = caracteres + x[i];
                                    i++;
                                    caracteres = caracteres + x[i];
                                }
                            }
                        }
                    }


                }
            }
            caracteres = ValuesCaracteres(caracteres);
            res = res + caracteres;
            return res;
        }

        //modificar el archivo program.cs
        public void NewProgramCS(string[]lines)
        {
            using (StreamWriter writer = new StreamWriter(@"Generic\GenericS\Program.cs"))
            {
                //Insertar librerias para el programa
                List<string> librery = InsertLibreri();
                foreach (var item in librery)
                {
                    writer.WriteLine(item);
                }

                //flujo del programa
                string c = '"'.ToString(); //comilla para strings

                List<string> Estados = new List<string>();
                List<string> NombresE = new List<string>();
                List<string> Valores = new List<string>();
                              
                //Obtenemos todos los datos necesarios para hacer los switches
                GetAllData(Estados, NombresE, Valores,lines);
 
                //obtener los estados aceptables
                List<string> VEAceptables = new List<string>();//valores de estado aceptables
                StatusCheck(VEAceptables,EstadoAceptado,Estados);

                //Continuamos con la creacion del archivo
                writer.WriteLine("\t\t\tstring estado = " + c + Estados[0] + c + ";");
                string EstadoInicial = Estados[0];

                //Escribo el segundo while que es el que mira la cadena
                writer.WriteLine("\t\t\t while (i < y.Length)");
                writer.WriteLine("\t\t\t {");//abro el segundo while
                writer.WriteLine("\t\t\t\t switch (estado)");//El switch
                writer.WriteLine("\t\t\t\t {");//abro el switch

                //generar los case dependiendo de la gramatica
                foreach (var item in Estados)
                {
                    List<string> CaseUnic = new List<string>();//tendra las lineas del case
                    CreateCase(CaseUnic,item,NombresE,Valores,EstadoInicial);
                    foreach (var item2 in CaseUnic)
                        writer.WriteLine("\t\t\t\t\t" + item2);//default:              
                }

                writer.WriteLine("\t\t\t\t\t default:");//default:
                writer.WriteLine("\t\t\t\t\t\t break;");//break;
                writer.WriteLine("\t\t\t\t }");//cierro el switch
                writer.WriteLine("\t\t\t }"); //cierre del segundo while

                //Asignar el ultimo IF para que corra completo
                writer.WriteLine("\t\t\t" + GetLastIF(VEAceptables)); //cierre del segundo while

                //El archivo con codigo generico finalizado con esto...
                string[] FCorchetes = FinalProgram();
                foreach (var item in FCorchetes)
                {
                    writer.WriteLine(item);
                }
            }
        }

        #endregion


        #region PRIVATE FUNCTIONS

        //devuleve los valores de los caracteres
        private string ValuesCaracteres(string c)
        {
            string res = "";
            string numeros = "0123456789";
            string Mayusculas = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
            string Minusculas = "abcdefghijklmnñopqrstuvwxyz";
            char[] datos = c.ToCharArray();

            for (int i = 0; i < datos.Length; i++)
            {
                if (datos[i] == '°') //son charsets
                {
                    int num = 0;
                    int num2 = 0;
                    string numA = "";
                    string numB = "";
                    i++;
                    while (numeros.Contains(datos[i]))
                    {
                        numA = numA + datos[i];
                        i++;
                    }
                    num = Convert.ToInt32(numA);
                    i = i + 3;
                    bool flag = false;
                    while (numeros.Contains(datos[i]) && flag == false)
                    {
                        numB = numB + datos[i];
                        if (i < datos.Length - 1)
                            i++;
                        else
                            flag = true;
                    }
                    num2 = Convert.ToInt32(numB);

                    for (int j = num; j < num2 + 1; j++)
                    {
                        res = res + Convert.ToChar(j);
                    }

                }
                else if (numeros.Contains(datos[i])) //son numeros
                {
                    char nextdata = datos[i + 3];
                    bool flag = false;
                    for (int j = 0; j < numeros.Length; j++)
                    {
                        if (numeros[j] == datos[i] && numeros[j] != nextdata)
                        {
                            res = res + datos[i];
                            flag = true;
                        }
                        else if (flag == true && numeros[j] != nextdata)
                        {
                            res = res + numeros[j];
                        }
                        if (nextdata == numeros[j])
                        {
                            res = res + numeros[j];
                            j = numeros.Length;
                        }
                    }
                    i = i + 3;
                }
                else if (Mayusculas.Contains(datos[i])) //son numeros
                {
                    char nextdata = datos[i + 3];
                    bool flag = false;
                    for (int j = 0; j < Mayusculas.Length; j++)
                    {
                        if (Mayusculas[j] == datos[i] && Mayusculas[j] != nextdata)
                        {
                            res = res + datos[i];
                            flag = true;
                        }
                        else if (flag == true && Mayusculas[j] != nextdata)
                        {
                            res = res + Mayusculas[j];
                        }
                        if (nextdata == Mayusculas[j])
                        {
                            res = res + Mayusculas[j];
                            j = Mayusculas.Length;
                        }
                    }
                    i = i + 3;
                }
                else if (Minusculas.Contains(datos[i])) //son numeros
                {
                    char nextdata = datos[i + 3];
                    bool flag = false;
                    for (int j = 0; j < Minusculas.Length; j++)
                    {
                        if (Minusculas[j] == datos[i] && Minusculas[j] != nextdata)
                        {
                            res = res + datos[i];
                            flag = true;
                        }
                        else if (flag == true && Minusculas[j] != nextdata)
                        {
                            res = res + Minusculas[j];
                        }
                        if (nextdata == Minusculas[j])
                        {
                            res = res + Minusculas[j];
                            j = Minusculas.Length;
                        }
                    }
                    i = i + 3;
                }
                else
                {
                    res = res + datos[i];
                }
            }


            return res;
        }//tested

        //insertar librerias
        private List<string> InsertLibreri()
        {
            List<string> res = new List<string>();
            string c = '"'.ToString(); //comilla para strings
            //agregar si se requiere otra libreria
            res.Add("using System;");
            res.Add("using System.Collections.Generic;");
            res.Add("using System.Linq;");
            res.Add("using System.Text;");
            res.Add("using System.Threading.Tasks;");
            res.Add("using System.IO;");
            res.Add("  ");
            res.Add("namespace Generics");
            res.Add("{");
            res.Add("   class Program");
            res.Add("   {");
            res.Add("       static void Main(string[] args)");
            res.Add("       {");
            res.Add("       while (true)");
            res.Add("       {");
            //aprovecho para colocar valores que igual sirven en todos los programas
            res.Add("           string x = " + c + c + ";");
            res.Add("           Console.WriteLine(" + c + "INSERTE CADENA: " + c + ");");
            res.Add("           x = Console.ReadLine();");
            res.Add("           ");
            res.Add("           List<string> sets = new List<string>();");
            res.Add("           sets = SET_File();");
            res.Add("           string y = EliminarEspacios(x);");
            res.Add("           int i = 0; //longitud del string");

            return res;
        }

        //Fin del programa con corchetes estructura general
        private string[] FinalProgram()
        {
            string[] lines = File.ReadAllLines(@"CopyFile.txt");
            return lines;
        }

        //get all data 
        private void GetAllData(List<string> E, List<string> Ne, List<string> V,string[] lines)
        {
            
            bool flagne = false;
            bool flage = false;
            bool flagv = false;
            bool flagef = false;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == "wNombreEstados")
                {
                    flagne = true;
                }
                else if (flagne == true && lines[i] != "wEstados")
                {
                    Ne.Add(lines[i]);
                }
                else if (lines[i] == "wEstados")
                {
                    flagne = false;
                    flage = true;
                }
                else if (flage == true && lines[i] != "wValoresEstado")
                {
                    E.Add(lines[i]);
                }
                else if (lines[i] == "wValoresEstado")
                {
                    flagv = true;
                    flage = false;
                }
                else if (flagv == true && lines[i] != "wEstadoFinal")
                {
                    V.Add(lines[i]);
                }
                else if (lines[i] == "wEstadoFinal")
                {
                    flagv = false;
                    flagef = true;
                }
                else if (flagef == true && lines[i] != "wFin")
                {
                    EstadoAceptado = lines[i];
                }
            }
        }

        //get status aceptable //statuscheck,estadoaceptado,estados
        private void StatusCheck(List<string> SC,string EA, List<string> E)
        {
            foreach (var item in E)
            {
                if (item.Contains(EA))
                {
                    SC.Add(item);
                }
            }
        }

        //get the last if whith status check
        private string GetLastIF(List<string> e)
        {
            string r = "if ( ";
            string estado = "estado";
            string c = '"'.ToString();
            string or = "||";
            int cont = 0;
            foreach (var item in e)
            {
                if (cont == 0)
                {
                    r = r + estado + " == " + c + item  + c + " "; //if (estado == "2,9"
                    cont++;
                }
                else
                    r = r + or + estado + " == " + c + item + c + " "; //|| estado == "9"
            }
            r = r + ")";
            return r;
        }

        //get unic case for each status
        private void CreateCase(List<string> line, string status, List<string> NameStatus, List<string> Values,string EstadoIncial)
        {
            string c = '"'.ToString();
            line.Add("case "+c+status+c+":"); //case "1,3,4,6":
            int cont = 0; //el primero es if los demas son else if
            //hacemos un if por cada estado
            string[] Trasladar = Values[valoresE].Split(';');
            foreach (var item in NameStatus)
            {
                //existen dos tipos de if los que tienen una palabra y los que tienen caracter
                if (cont == 0)
                {
                    line.Add("if (" + TypeIf(item) + ")"); //if (Comparar(sets, y[i], "DIGITO"))
                    line.Add("{"); //{
                    if (TypeStatus(Trasladar[cont]))
                    {
                        //verdadero
                        line.Add("estado = " + c + Trasladar[cont] + c + ";");  //estado = "1,3,4,6";
                        line.Add("i++;");  //i++;
                    }
                    else
                    {
                        //falso 
                        line.Add("estado = "+c+EstadoIncial+c+";");  //estado = "1,3,4,6";
                    }
                    line.Add("}"); //}
                    cont++;
                }
                else
                {
                    line.Add("else if (" + TypeIf(item) + ")"); //else if (y[i] == '=')
                    line.Add("{"); //{
                    if (TypeStatus(Trasladar[cont]))
                    {
                        //verdadero
                        line.Add("estado = " + c + Trasladar[cont] + c + ";");  //estado = "1,3,4,6";
                        line.Add("i++;");  //i++;
                    }
                    else
                    {
                        //falso 
                        line.Add("estado = " + c + EstadoIncial + c + ";");  //estado = "1,3,4,6";
                    }
                    line.Add("}"); //}
                    cont++;
                }
            }
            valoresE++;//para el siguiente caso
            //todos los casos tiene este else
            line.Add("else"); //else
            line.Add("{"); //{
            line.Add("Console.WriteLine("+c+ "Caracter incorrecto: " + c+ " + y[i]);"); //Console.WriteLine("Caracter incorrecto: " + y[i]);
            line.Add("i = x.Length; estado = "+c+ EstadoIncial +c+";"); //i = x.Length; estado = "1,3,4,6";
            line.Add("}"); //}
            line.Add("break;"); //break;
           
        }

        //existen dos tipos de if los que tienen una palabra y los que tienen caracter
        private string TypeIf(string status)
        {
            string c = '"'.ToString();
            if (status[0] == Convert.ToChar("'"))
            {
                if (status == "'''")
                {
                    return "y[i] == Convert.ToChar("+c+"'"+c+")"; //y[i] == ''' --> caso particular ''' :(
                }
                else
                    return "y[i] == " + status; //y[i] == '=' --> caso particular ''' :(
            }                
            else
                return "Comparar(sets, y[i], "+c+status+c+")"; //Comparar(sets, y[i], "DIGITO")         
        }

        //devuelve a que estado se trasalada --> true o si es cero --> falso
        private bool TypeStatus(string x)
        {
            if (x == "0")
                return false;
            else
                return true;
        }


        #endregion



    }
}
