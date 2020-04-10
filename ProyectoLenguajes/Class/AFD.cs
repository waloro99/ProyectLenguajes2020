using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoLenguajes.Class
{
    public class AFD
    {
        //Var global
        public int cont = 1;

        //------------------------------- PUBLIC FUNCTIONS ----------------------------------
        //direct method for AFD in tree
        public Nodo Direct_Method(Nodo x)
        {
            //1. Se inserta los valores de first y last en los nodos hoja
            Insert_Leaf_Node(x);

            //2. Se inserta los valores de first y last para los nodos que no son hoja
            Insert_First_Last(x);

            return x;
        }

        //------------------------------- PRIVATE FUNCTIONS ---------------------------------

        //method for insert values in first and last in leaf node
        private void Insert_Leaf_Node(Nodo n)
        {
            //scroll in order
            if (n.hi != null)
                Insert_Leaf_Node(n.hi);
            if (n.hi == null && n.hd == null)
            {
                n.first.Add(cont);
                n.last.Add(cont);
                cont++;
            }
            if (n.hd != null)
                Insert_Leaf_Node(n.hd);
        }

        //method for insert values in first and last in node that not leaf node and NUllable
        private void Insert_First_Last(Nodo n)
        {
            //scroll in PostOrder
            if (n.hi != null)
                Insert_First_Last(n.hi);
            if (n.hd != null)
                Insert_First_Last(n.hd);
            if (n.first.Count == 0)
            {
                //Rules first for Node --> depend type the valor node
                if (n.valor == "|")
                {
                    //first = F(c1) u F(c2)
                    foreach (var item in n.hi.first)
                        n.first.Add(item);
                    foreach (var item in n.hd.first)
                        n.first.Add(item);
                    //last = L(c1) u L(c2)
                    foreach (var item in n.hi.last)
                        n.last.Add(item);
                    foreach (var item in n.hd.last)
                        n.last.Add(item);
                    //nullable == N(c1) ó N(c2)
                    if (n.hi.nullable == true || n.hd.nullable == true)
                        n.nullable = true;
                }
                else if (n.valor == ".")
                {
                    //first = if N(c1) then F = F(c1) u F(c2) Else F = F(c1)
                    if (n.hi.nullable == true)
                    {
                        foreach (var item in n.hi.first)
                            n.first.Add(item);
                        foreach (var item in n.hd.first)
                            n.first.Add(item);
                    }
                    else
                    {
                        foreach (var item in n.hi.first)
                            n.first.Add(item);
                    }
                    //last = if N(c2) then L = L(c1) u L(c2) Else L = L(c2)
                    if (n.hd.nullable == true)
                    {
                        foreach (var item in n.hi.last)
                            n.last.Add(item);
                        foreach (var item in n.hd.last)
                            n.last.Add(item);
                    }
                    else
                    {
                        foreach (var item in n.hd.last)
                            n.last.Add(item);
                    }
                    //nullable == N(c1) y N(c2)
                    if (n.hi.nullable == true && n.hd.nullable == true)
                        n.nullable = true;
                }
                else if (n.valor == "*" || n.valor == "?")
                {
                    //first = F = F(c1)
                    foreach (var item in n.hi.first)
                        n.first.Add(item);
                    //last = L = L(c1)
                    foreach (var item in n.hi.last)
                        n.last.Add(item);
                    //nullable == N
                    n.nullable = true;
                }
                else if (n.valor == "+")
                {
                    //first = F = F(c1)
                    foreach (var item in n.hi.first)
                        n.first.Add(item);
                    //last = L = L(c1)
                    foreach (var item in n.hi.last)
                        n.last.Add(item);
                    //nullable == FALSE for defect
                }
            }
        }
    }
}
