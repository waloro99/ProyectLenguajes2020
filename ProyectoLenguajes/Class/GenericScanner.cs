using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoLenguajes.Class
{
    public class GenericScanner
    {

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
                        i=x.Length; //exit bucle
                    else
                        res = res + x[i];
                }
            }

            res = res+","; //separador de nombre y caracter
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
                    if(x[i] == '(')
                    {
                        i++;//valores
                        caracteres = caracteres + "°";
                        while (numeros.Contains(x[i]))
                        {
                            caracteres = caracteres + x[i];
                            i++;
                        }
                        if(x[i] == ')')
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
                        if (i < datos.Length-1)
                            i++;
                        else
                            flag = true;
                    }
                    num2 = Convert.ToInt32(numB);

                    for (int j = num; j < num2+1; j++)
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
                        else if(flag == true && numeros[j] != nextdata)
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


        #endregion



    }
}
