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
        public string res1 = ""; //get result the comparations

        //------------------------------- PUBLIC FUNCTIONS ----------------------------------
        //direct method for AFD in tree
        public Nodo Direct_Method(Nodo x)
        {
            //1. Se inserta los valores de first y last en los nodos hoja
            Insert_Leaf_Node(x);

            //2. Se inserta los valores de first y last para los nodos que no son hoja
            Insert_First_Last(x);

            //3. Se inserta los valores de Follow en los nodos con recorrido post orden
            Insert_Follow(x);

            return x;
        }

        //direct method for AFD in tree --> transitions
        public List<string> Transitions_Insert(List<string> S, Nodo n)
        {
            //4. Se inserta la lista que guarda los valores de las transiciones
            Insert_Values_Columns(S, n); //save each name the data -->  first column

            return S;
        }

        //Last phase for completed table the AFD Transitions
        public List<string> Transitions_values(List<string> t, Nodo n, List<string> v, List<string> s)
        {
            Insert_FirstStatus(s, n);
            Insert_Transitions(t, n, v, s);
            return t;
        }

        //Method public the list
        public List<string> FixList(List<string> list)
        {
            List<string> newList = new List<string>();
            newList = FixListNumberStatus(list);
            return newList;
        }

        //Method public the list2
        public List<string> FixList2(List<string> list)
        {
            List<string> newList = new List<string>();
            newList = FixListNumber(list);
            return newList;
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
        }//tested

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
        }//tested

        //method for insert values in follow in node that not leaf node and add id for show in tabla
        private void Insert_Follow(Nodo n)
        {
            //scroll in PostOrder
            if (n.hi != null)
                Insert_Follow(n.hi);
            if (n.hd != null)
                Insert_Follow(n.hd);
            //1. Follow = L(c1) -> F(c2)
            if (n.valor == ".")
            {
                foreach (var item in n.hi.last)
                {
                    foreach (var item2 in n.hd.first)
                        InOrderFollow(item, item2, n);
                }
            }
            //2. Follow = L(c1) -> F(c1)
            else if (n.valor == "*" || n.valor == "+")
            {
                foreach (var item in n.hi.last)
                {
                    foreach (var item2 in n.hi.first)
                        InOrderFollow(item, item2, n);
                }
            }
            //4.No tienen follow  -->> "|" && "?"
            else if (n.valor != "|" && n.valor != "?")
            {
                //Se sabe que si llego aqui significa que es una hoja
                foreach (var item in n.first)
                    n.id = item;
            }

        }//tested

        //method for scroll in order for follows
        private void InOrderFollow(int id, int valor, Nodo n)
        {
            //scroll in Orden
            if (n.hi != null)
                InOrderFollow(id, valor, n.hi);
            if (n.id == id)
                n.follow.Add(valor);
            if (n.hd != null)
                InOrderFollow(id, valor, n.hd);
        }//tested

        //method for scroll nodo for each name unico
        private void Insert_Values_Columns(List<string> t, Nodo n)
        {
            //scroll in PostOrder
            if (n.hi != null)
                Insert_Values_Columns(t, n.hi);
            if (n.hd != null)
                Insert_Values_Columns(t, n.hd);
            //Only leaf node
            if (n.hi == null && n.hd == null)
            {
                bool flag = false;//check diferent name the column
                if (t.Count != 0)
                {
                    foreach (var item in t)
                    {
                        if (item == n.valor)
                            flag = true;
                    }
                    if (flag == false && n.valor != "#")
                        t.Add(n.valor);
                }
                else
                {
                    t.Add("Estado"); //first add
                    t.Add(n.valor);
                }
            }
        }//tested

        //method for insert the first root , because is first status
        private void Insert_FirstStatus(List<string> s, Nodo n)
        {
            string first = "";
            foreach (var item in n.first)
            {
                first = first + item + ",";
            }
            first = first.TrimEnd(',');
            s.Add(first);
        }//tested

        //method for AFD transitions
        private void Insert_Transitions(List<string> t, Nodo n, List<string> v, List<string> s)
        {
            for (int j = 0; j < s.Count; j++)
            {
                string[] status_value = s[j].Split(',');//status.Split(',');
                string res = "";
                foreach (var item in v)
                {
                    res1 = ""; //get result the comparations
                    for (int i = 0; i < status_value.Length; i++)
                    {
                        int value = Convert.ToInt32(status_value[i]);
                        GetResulComparations(value, item, n);
                    }  //scroll news status for each one
                    res1 = res1.TrimEnd(','); //delete last character
                    //add new status if exists
                    bool flag = false;
                    foreach (var item2 in s)
                    {
                        if (item2 == res1)
                            flag = true;
                    }
                    if (flag == false && res1 != "" && AgainVerification(s, res1) == false)
                        s.Add(res1);
                    if (res1 != "")
                        res = res + res1 + ";";
                    else
                        res = res + "0;"; //if the result not found , then is zero --> id zero no exists                  
                }//scroll name the columns
                t.Add(res);
            }//scroll the status 

        }//tested

        //method for comparations values
        private void GetResulComparations(int value, string name, Nodo n)
        {
            //scroll in PostOrder
            if (n.hi != null)
                GetResulComparations(value, name, n.hi);
            if (n.hd != null)
                GetResulComparations(value, name, n.hd);
            //search id
            if (n.id == value)
            {
                if (n.valor == name)
                {
                    foreach (var item in n.follow)
                    {
                        if (!res1.Contains(item.ToString()))
                        {
                            res1 = res1 + item.ToString() + ",";
                        }
                    }
                }
            }
        }//testeed

        //method for fix list
        private List<string> FixListNumber(List<string> x)
        {
            List<string> n = new List<string>();
            foreach (var item in x)
            {
                string res = "";
                res = item.TrimStart('0');
                res = res.TrimStart(';');
                string[] number = res.Split(';');
                string value = "";
                for (int i = 0; i < number.Length; i++)
                {
                    string[] data = number[i].Split(',');
                    int[] n_Data = new int[data.Length];
                    for (int j = 0; j < n_Data.Length; j++)
                    {
                        if (data[j] != "")
                            n_Data[j] = Convert.ToInt32(data[j]);

                    }
                    Array.Sort(n_Data); //Orden de menor a mayor
                    for (int j = 0; j < n_Data.Length; j++)
                        value = value + n_Data[j].ToString() + ",";
                    value = value.TrimEnd(',');
                    value = value + ";";
                }
                n.Add(value);
            }
            return n;
        }

        //method for fix list
        private List<string> FixListNumberStatus(List<string> x)
        {
            List<string> n = new List<string>();
            foreach (var item in x)
            {
                string[] number = item.Split(',');
                string value = "";
                int[] n_Data = new int[number.Length];
                for (int i = 0; i < n_Data.Length; i++)
                {
                    n_Data[i] = Convert.ToInt32(number[i]);
                }
                Array.Sort(n_Data); //Orden de menor a mayor
                for (int j = 0; j < n_Data.Length; j++)
                {
                    value = value + n_Data[j].ToString() + ",";
                }
                value = value.TrimEnd(',');
                n.Add(value);
            }
            return n;
        }//tested

        //method for verification again 
        private bool AgainVerification(List<string> s, string res)
        {
            bool flag = false;
            for (int i = 0; i < s.Count; i++)
            {
                string[] aux = s[i].Split(',');
                string[] aux2 = res.Split(',');
                int[] auxI = new int[aux.Length];
                int[] auxI2 = new int[aux2.Length];
                for (int j = 0; j < aux.Length; j++)
                {
                    auxI[j] = Convert.ToInt32(aux[j]);
                }
                for (int j = 0; j < aux2.Length; j++)
                {
                    auxI2[j] = Convert.ToInt32(aux2[j]);
                }
                Array.Sort(auxI);
                Array.Sort(auxI2);
                string res1 = "";
                string res2 = "";
                for (int j = 0; j < auxI.Length; j++)
                {
                    res1 = res1 + auxI[j];
                }
                for (int j = 0; j < auxI2.Length; j++)
                {
                    res2 = res2 + auxI2[j];
                }
                if (res1 == res2)
                {
                    flag = true;
                    i = s.Count;
                }

            }
            return flag;
        }
    }
}
