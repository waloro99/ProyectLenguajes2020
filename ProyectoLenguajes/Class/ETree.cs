using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoLenguajes.Class
{
    public class ETree
    {
        public Nodo raiz; //root the ETree
        public Stack<string> T = new Stack<string>(); //Stack for st
        public Stack<Nodo> S = new Stack<Nodo>(); //stack for tree
        public char[] unario = { '*', '+', '?' }; //arrray symbols used in ER
        public char[] op = { '*', '+', '(', ')', '?', '|',' '}; //arrray symbols used in ER
        public string cadena = ""; //Save InOrder 
        //Space symbol equals concatenation

        //Method builder
        public ETree()
        {
            raiz = null;
        }

        //method for insert in the expression tree with Moises Algorithm
        public Stack<Nodo> Insert(string er)
        {
            char[] tokens = er.ToArray(); //read character for character 
            int cts = 0; //count tokens position
            //Step 1.	Mientras existan tokens en la expresión regular ---------------------------------------
            while (cts < tokens.Length)
            {
                char token = tokens[cts]; //Step 2.	Obtener token ---------------------------------------------

                //yes false then token = st
                if (!Is_op(token)) //Step 3. Si token es st ---------------------------------------------------
                {
                    //a. Convertir st en árbol
                    Nodo nuevo = new Nodo();
                    nuevo.valor = token.ToString();
                    nuevo.hd = null;
                    nuevo.hi = null;

                    //b.Hacer “push” a la pila S con el nuevo árbol generado de st
                    S.Push(nuevo);
                    cts++; //next token 
                }
                else if (token == '(') //Step 4. Sino Si token es “(“ -----------------------------------------
                {
                    //a. Hacer “push” a la pila T con token
                    T.Push(token.ToString());
                    cts++; //next token
                }
                else if (token == ')') //Step 5. Sino Si token es “)“ -----------------------------------------
                {
                    //a. Mientras la longitud de T sea mayor que 0 y
                    //el último dato insertado en T sea diferente de “(“
                    while ((T.Count() >= 0) && (T.Peek() != "("))  // se le puso mayor o igual porque si solo se le pone mayor no va entrar                 
                    {
                        //i. Si Longitud de T es igual a 0
                        if (T.Count() == 0)
                        {
                            //1. Existe error, faltan operandos
                            S.Clear();//Removes all objects from the Stack.
                            return S; //returns empty for the error
                        }

                        //ii. Si la longitud de S es menor a 2
                        if (S.Count() < 2)
                        {
                            //1. Existe error, faltan operandos
                            S.Clear();//Removes all objects from the Stack.
                            return S; //returns empty for the error
                        }

                        //iii.	Hacer “pop” a T y convertirlo en árbol llamado temp
                        Nodo temp = new Nodo();
                        temp.valor = T.Pop();

                        //iv. Hacer “pop” a S y asignarlo al hijo derecho de temp
                        temp.hd = S.Pop();

                        //v. Hacer “pop” a S y asignarlo al hijo izquierdo de temp
                        temp.hi = S.Pop();

                        //vi. Hacer “Push” de temp en la pila S 
                        S.Push(temp);

                    }//end while a. 

                    //b. Hacer “pop” a T con el último dato
                    T.Pop();
                    cts++; //next token

                }
                else if (Is_op(token)) //Step 6. Sino si token es op ------------------------------------------
                {
                    //a. Si op es unario
                    if (Is_Unario(token))
                    {
                        //i. Convertir op en árbol
                        Nodo nuevo = new Nodo();
                        nuevo.valor = token.ToString();
                        nuevo.hd = null;

                        //ii. Si la longitud de S es menor que 0
                        if (S.Count() <= 0)
                        {
                            //1. Existe error, faltan operandos
                            S.Clear();//Removes all objects from the Stack.
                            return S; //returns empty for the error
                        }

                        //iii. Hacer “pop” de S y asignarlo como hijo izquierdo
                        nuevo.hi = S.Pop();

                        //iv. Hacer “push” a la pila S con el nuevo árbol generado de op
                        S.Push(nuevo);
                    }
                    //b. Sino si T no está vacia y el “top” op en T es diferente
                    //a “(“ y precedencia de token es menor a último op en T
                    else if ( (T.Peek() != null) && (T.Peek() != "(") && (Is_Precedence(token,T.Peek())) ) //no tested precedence
                    {
                        //i. Extraer de T a op, convertirlo en árbol y llamarlo temp
                        Nodo temp = new Nodo();
                        temp.valor = T.Pop();

                        //ii. Si cantidad de elementos en S es menor a 2
                        if (S.Count() < 2)
                        {
                            //1. Existe error, faltan operandos
                            S.Clear();//Removes all objects from the Stack.
                            return S; //returns empty for the error
                        }

                        //iii. Extraer último árbol de S y asignarlo al hijo derecho de temp
                        temp.hd = S.Pop();

                        //iv. Extraer último árbol de S y asignarlo al hijo izquierdo de temp
                        temp.hi = S.Pop();

                        //v. Push de temp en la pila S 
                        S.Push(temp);
                    }

                    //c. Si op no es unario Hacer “push” en la pila T con token
                    if (!Is_Unario(token))
                    {
                        T.Push(token.ToString());
                    }
                    cts++; //next token 

                } // end step 6
                else //Step 7. De lo contrario ----------------------------------------------------------------
                {
                    //a. Error, no es token reconocido
                    S.Clear();//Removes all objects from the Stack.
                    return S; //returns empty for the error
                }

                //Step 8. Si aún existen tokens en la expresión regular, ir a paso 2 --------------------------

            }// end while step 1

            //Step 9. Mientras la longitud de T sea Mayor que 0 -----------------------------------------------
            while (T.Count() > 0)
            {
                //a. Hacer “pop” de T y crear un nuevo árbol llamado temp
                Nodo temp = new Nodo();
                temp.valor = T.Pop();

                //b. Si temp es “(“
                if (temp.valor == "(")
                {
                    //1. Existe error, faltan operandos
                    S.Clear();//Removes all objects from the Stack.
                    return S; //returns empty for the error
                }

                //c. Si longitud de S menor que 2
                if (S.Count() < 2)
                {
                    //1. Existe error, faltan operandos
                    S.Clear();//Removes all objects from the Stack.
                    return S; //returns empty for the error
                }

                //d. Hacer “pop” a la pila S y asignarlo como hijo derecho de temp
                temp.hd = S.Pop();

                //e. Hacer “pop” a la pila S y asignarlo como hijo izquierdo de temp
                temp.hi = S.Pop();

                //f. Hacer “push” a la pila S con el árbol temp
                S.Push(temp);
            }
            //Step 10. Si longitud de T es mayor que 0 ir a paso 9 --------------------------------------------

            //Step 11. Si longitud de S es diferente de 1 -----------------------------------------------------
            if (S.Count() != 1)
            {
                //1. Existe error, faltan operandos
                S.Clear();//Removes all objects from the Stack.
                return S; //returns empty for the error
            }
            else
            {
                //Step 12. Hacer “pop” a S y retornar el valor ----------------------------------------------------
                return S;
            }
            
        }//no tested

        //method to know if the character of the ER is an op
        private bool Is_op(char t)
        {
            for (int i = 0; i < op.Length; i++)
            {
                if (t.CompareTo(op[i]) == 0) //if the return value is zero its because its the same character
                    return true; //yes is a token op
            }
            return false; //is st
        }//no tested

        //method to know if the character of the ER is unario
        private bool Is_Unario(char t)
        {
            for (int i = 0; i < unario.Length; i++)
            {
                if (t.CompareTo(unario[i]) == 0) //if the return value is zero its because its the same character
                    return true; //yes is a token unario
            }
            return false;
        }//no tested

        //Method to know if the token is precedent or not
        private bool Is_Precedence(char t, string op)
        {
            //return  true if token is less that last op
            char top = Convert.ToChar(op);

            if (Is_Unario(top))
            {
                return true;
            }
            else if (top == t)
            {
                return true;
            }
            return false;
        }//no tested

        //method for walk the tree in order
        public void InOrder(Nodo n)
        {
            if (n.hi != null)
                InOrder(n.hi);
            cadena = cadena + " " + n.valor;
            if (n.hd != null)
                InOrder(n.hd);
        }

    }

    //create class Nodo for ETree
    public class Nodo
    {
        public Nodo hi; //song left
        public Nodo hd; //song right
        public string valor; //value the nodo

        //method builder
        public Nodo()
        {
            hd = null;
            hi = null;
            valor = "";
        }
    }
}
