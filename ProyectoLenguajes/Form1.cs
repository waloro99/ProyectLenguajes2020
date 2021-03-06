﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProyectoLenguajes.Class;
using System.IO;
using System.Diagnostics;

namespace ProyectoLenguajes
{
    public partial class Form1 : Form
    {
        // ----------------------------------------VAR GLOBAL-------------------------------------------

        public string PathFile = string.Empty; // var path the file
        public string PathFileGeneric = string.Empty; // var path the new program 
        //lists
        public List<string> L_Sets = new List<string>(); //save data section sets
        public List<string> L_Tokens = new List<string>(); //save data section sets
        public List<string> L_Actions = new List<string>(); //save data section sets
        public List<string> L_Error = new List<string>(); //save data section sets

        //list for save names the sets
        public List<string> N_Sets = new List<string>(); //save data section sets
        public List<string> Columns_Transitions = new List<string>(); // Name the columns
        public List<string> Values_Transitions = new List<string>(); // Values the columns for each status
        public List<string> Status = new List<string>(); // Each status  
        public string LastFollow = string.Empty;

        public Form1()
        {

            InitializeComponent();

            //put button
            button1.MouseClick += this.Press_button;

            //mouse move
            button1.MouseLeave += this.Quit_button;

            //instance
            Bitmap imagen = new Bitmap(Application.StartupPath + @"\img\ima_archivo1.png");
            button1.Image = imagen;

        }

        //method for back imagen original 
        private void Quit_button(object obj, EventArgs evt)
        {
            Bitmap imagen = new Bitmap(Application.StartupPath + @"\img\ima_archivo1.png");
            button1.Image = imagen;
        }

        //method for change imagen dowload
        private void Press_button(object obj, EventArgs evt)
        {
            Bitmap imagen = new Bitmap(Application.StartupPath + @"\img\ima_archivo2.png");
            button1.Image = imagen;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //var for each phase the proyect
            int p1 = 0;

            //clear all list because if exist other file add in button "aceptar"
            L_Sets.Clear();
            L_Tokens.Clear();
            L_Actions.Clear();
            L_Error.Clear();
            N_Sets.Clear();


            p1 = Semantic_Analysis();    //Semantic analysis  ---->   phase 1

            if (p1 == 1)
            {
                Syntactic_Analysis();   //Syntactic analysis  ---->   phase 2
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {


            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text documents (.txt)|*.txt"; //only file .txt
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                PathFile = openFileDialog.FileName;
                button2.Enabled = true;
                textBox1.Enabled = true;
                button3.Enabled = true;
            }

            textBox1.Text = PathFile;
        }

        //---------------------------------------------FUNCTIONS PUBLIC----------------------------------------------

        //o --> error      1 --> correcto

        //method to do semantic analysis 
        public int Semantic_Analysis()
        {
            //instance class
            ReadFileC rf = new ReadFileC();

            //return config start
            button2.Enabled = false;
            textBox1.Enabled = false;

            //get data from the all file
            string[] res = rf.ReadFile(PathFile);

            if (res.Length == 0)
            {
                MessageBox.Show("El archivo se encontro vacio."); //end program
                return 0;
            }
            else
            {

                //add items at lists
                L_Sets = rf.SplitSets(res);  //tested
                L_Tokens = rf.SplitTokens(res); //tested
                L_Actions = rf.SplitActions(res);  //tested
                L_Error = rf.SplitError(res);  //tested
                L_Actions = rf.FixActions(L_Actions); //testeed
                //firs filter (key words)
                if (F_KeyWords(L_Tokens, L_Actions, L_Error) != "GG")
                {
                    MessageBox.Show(F_KeyWords(L_Tokens, L_Actions, L_Error)); //end program
                    return 0;
                }
                else
                {
                    MessageBox.Show("Primer Filtro Correcto"); //CONTINUED
                                                               //------------------- SECOND FILTER

                    //Create string ER
                    string ER_sets1 = "(a+.f+.g.(f+.h.(a|b|c|i).h.(j|k)?)+).#"; //LETRA   = 'A'..'Z'+'a'..'z'+'_'
                    string ER_sets2 = "(a+.f+.g.(f+.d+.l.c+.m.j?)+).#"; //CHARSET = CHR(32)..CHR(254)
                    string ER_tokens = "(e+.f+.c+.f*.g.f*.((h.(a|i).h)*|(a|f|n)*|(a|b|f|i)*)+).#"; //'"' CHARSET '"'|''' CHARSET ''' // LETRA ( LETRA | DIGITO )*   { RESERVADAS() } 
                    string ER_actions = "(c+.f.g.f.h.a+.h).#"; //18 = 'PROGRAM'
                    string ER_error = "(ñ+.f.g.f.c+).#"; //ERROR = 54

                    //Create tree for each ER...
                    ETree T_Sets = new ETree();
                    ETree T_Sets2 = new ETree();
                    ETree T_Tokens = new ETree();
                    ETree T_Actions = new ETree();
                    ETree T_Error = new ETree();

                    //Create stack for each ER
                    Stack<Nodo> Tree_Sets = new Stack<Nodo>();
                    Stack<Nodo> Tree_Sets2 = new Stack<Nodo>();
                    Stack<Nodo> Tree_Tokens = new Stack<Nodo>();
                    Stack<Nodo> Tree_Actions = new Stack<Nodo>();
                    Stack<Nodo> Tree_Error = new Stack<Nodo>();

                    //Insert value in differents trees
                    Tree_Sets = T_Sets.Insert(ER_sets1);
                    Tree_Sets2 = T_Sets2.Insert(ER_sets2);
                    Tree_Tokens = T_Tokens.Insert(ER_tokens);
                    Tree_Actions = T_Actions.Insert(ER_actions);
                    Tree_Error = T_Error.Insert(ER_error);

                    // recorrido
                    T_Sets.InOrder(Tree_Sets.Pop());
                    T_Sets2.InOrder(Tree_Sets2.Pop());
                    T_Tokens.InOrder(Tree_Tokens.Pop());
                    T_Actions.InOrder(Tree_Actions.Pop());
                    T_Error.InOrder(Tree_Error.Pop());

                    //mostrar recorrido de arboles
                    //MessageBox.Show(T_Sets.cadena);
                    //MessageBox.Show(T_Sets2.cadena);
                    //MessageBox.Show(T_Tokens.cadena);
                    //MessageBox.Show(T_Actions.cadena);
                    //MessageBox.Show(T_Error.cadena);

                    //----------------------------- READ SECTIONS -----------------------------------------------

                    //send all tokens 
                    Token t = new Token();
                    List<Token> L_t = new List<Token>();
                    L_t = t.Insert_Tokens();

                    //filter sets
                    if (rf.ReadSets(L_Sets, L_t, ER_sets1, ER_sets2) != "GG")
                    {
                        string er = rf.ReadSets(L_Sets, L_t, ER_sets1, ER_sets2);
                        char[] x = er.ToArray();
                        int columna = Error_Columna(x);
                        int line = Error_Line(x, res);
                        MessageBox.Show(er);
                        MessageBox.Show("Error en la linea: " + line + " Columna: " + columna);
                        return 0;
                    }
                    //filter tokens
                    else if (rf.ReadTokens(L_Tokens, L_t, ER_tokens) != "GG")
                    {
                        string er = rf.ReadTokens(L_Tokens, L_t, ER_tokens);
                        char[] x = er.ToArray();
                        int columna = Error_Columna(x);
                        int line = Error_Line(x, res);
                        MessageBox.Show(er);
                        MessageBox.Show("Error en la linea: " + line + " Columna: " + columna);
                        return 0;
                    }
                    //filter action
                    else if (rf.ReadAction(L_Actions, L_t, ER_actions) != "GG")
                    {
                        string er = rf.ReadAction(L_Actions, L_t, ER_actions);
                        char[] x = er.ToArray();
                        int columna = Error_Columna(x);
                        int line = Error_Line_A(x, res);
                        if (line == 0)
                            line = Error_Line(x, res);
                        MessageBox.Show(er);
                        MessageBox.Show("Error en la linea: " + line + " Columna: " + columna);
                        return 0;
                    }
                    //filter error
                    else if (rf.ReadError(L_Error, L_t, ER_error) != "GG")
                    {
                        string er = rf.ReadError(L_Error, L_t, ER_error);
                        char[] x = er.ToArray();
                        int columna = Error_Columna(x);
                        int line = Error_Line(x, res);
                        MessageBox.Show(er);
                        MessageBox.Show("Error en la linea: " + line + " Columna: " + columna);
                        return 0;
                    }
                    else
                    {
                        MessageBox.Show("Archivo leido correctamente :)");
                        return 1;
                    }
                }

            } //fiter the file empity
        }

        public void Syntactic_Analysis()
        {
            //Save the data the section SETS if exist
            if (L_Sets != null)
            {
                foreach (var item in L_Sets)
                    N_Sets.Add(Name_SETS(item));
            }

            //Create ER for syntactic analysis
            //instance class for functions the ER
            ER FER = new ER();
            string ER_analysis = ""; //Save here ER for syntactic analysis
            ER_analysis = FER.CreateER(L_Tokens); //SAVE ER version 1                                                      
            string flag_SETS = FER.Is_Correct_SETS(ER_analysis, N_Sets);
            if (flag_SETS == "GG")
            {
                ER_analysis = FER.String_Completed(ER_analysis); //completed string with symbol '.'
                textBox2.Text = ER_analysis; //show user

                //create tree
                ETree T_Tokens = new ETree();
                Stack<Nodo> Tree_Tokens = new Stack<Nodo>(); //stack the final tree
                Tree_Tokens = T_Tokens.Insert(ER_analysis); //get tree
                // SECOND PHASE AFD the ETree
                //insert values the first , last and follow for direct method AFD
                AFD afd = new AFD(); //instance class
                Nodo Node_Token = new Nodo();
                Node_Token = afd.Direct_Method(Tree_Tokens.Pop());

                //Third Phase Transitions the AFD the Etree
                 
                Columns_Transitions = afd.Transitions_Insert(Columns_Transitions, Node_Token); //save name the columns
                Values_Transitions = afd.Transitions_values(Values_Transitions, Node_Token, Columns_Transitions, Status);
                Status = afd.FixList(Status);//order number
                Values_Transitions = afd.FixList2(Values_Transitions);
                //Method for show in DataGridView
                Show_FirstLast(Node_Token); //show in data grid view data the first and last
                Show_Follow(Node_Token); // show in data grid view data the follow
                Show_Transitions(Columns_Transitions, Status, Values_Transitions); //shoq in data grid view data the transitions

                //Me servira para la fase 3
                GetLastFollow(Node_Token);

                //show tree in image
                //string ruta = T_Tokens.graphic(Node_Token);

                //System.Threading.Thread.Sleep(1000);

                //FileStream file = new FileStream(ruta, FileMode.Open);
                //Image img = Image.FromStream(file);
                //pictureBox1.Image = img;
                //file.Close();

            }
            else
            {
                MessageBox.Show(flag_SETS); //END PROGRAM
            }

        }


        //---------------------------------------------FUNCTIONS PRIVATE----------------------------------------------

        //method for verify correct opertation of sections
        private string F_KeyWords(List<string> t, List<string> a, List<string> e)
        {
            string res = "";//send answer
            //get first item at each list
            //no included sets becasuse it can come or not
            if (t.ElementAt(0) == "ERROR")
            {
                res = "ERROR FALTA PALABRA CLAVE = 'TOKENS' ";
            }
            else if (a.ElementAt(0) == "ERROR")
            {
                res = "ERROR FALTA PALABRA CLAVE = 'ACTIONS' O 'RESERVADAS(){}' ";
            }
            else if (e.ElementAt(0) == "ERROR")
            {
                res = "ERROR FALTA PALABRA CLAVE = 'ERROR =' ";
            }
            else
            {
                res = "GG";   //first filter correct
            }
            return res;
        }//tested

        //method for search row error
        private int Error_Line(char[] err, string[] res)
        {
            string cadena = "";
            char[] numeros = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            bool v = false;
            for (int i = 0; i < err.Length; i++)
            {
                if (i != 0)
                {
                    if (i < 3)
                    {
                        for (int j = 0; j < numeros.Length; j++)
                        {
                            if (err[i] == numeros[j])
                            {
                                v = true;
                            }
                        }
                        if (v == false)
                            cadena += err[i];
                    }
                    else
                    {
                        cadena += err[i];
                    }
                }
                v = false;
            }

            for (int i = 0; i < res.Length; i++)
            {
                if (res[i].Contains(cadena))
                {
                    return i;
                }
            }
            return 0;

        }

        //method for search row error
        private int Error_Line_A(char[] err, string[] res)
        {
            string cadena = "";
            char[] numeros = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            bool v = false;
            for (int i = 0; i < err.Length; i++)
            {
                if (i != 0)
                {
                    if (err[i] == '\t')
                    {
                        v = true;
                    }
                    if (v == true)
                    {
                        cadena += err[i];
                    }
                }
            }

            for (int i = 0; i < res.Length; i++)
            {
                if (res[i].Contains(cadena))
                {
                    return i;
                }
            }
            return 0;

        }

        //method for search column error
        private int Error_Columna(char[] x)
        {
            string num = "";
            char[] numeros = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            int cont = 0;

            for (int j = 0; j < numeros.Length; j++)
            {
                if (x[cont] == numeros[j])
                {
                    num += x[cont];
                    cont++;
                    j = -1;
                }
            }
            int res = Convert.ToInt32(num);

            return res;
        }

        //method for split the list sets
        private string Name_SETS(string cadena)
        {
            string res = "";
            char[] x = cadena.ToArray();

            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != ' ' && x[i] != '\t')
                {
                    if (x[i] == '=')
                        return res;
                    else
                        res = res + x[i];
                }
            }
            return res;
        }//tested

        //method for show in datagridview the first and last
        private void Show_FirstLast(Nodo n)
        {
            //scroll in PostOrder
            if (n.hi != null)
                Show_FirstLast(n.hi);
            if (n.hd != null)
                Show_FirstLast(n.hd);
            string first = "";
            string last = "";
            string nullable = "";
            foreach (var item in n.first)
                first = first + item.ToString() + ",";
            foreach (var item in n.last)
                last = last + item.ToString() + ",";
            if (n.nullable == false)
                nullable = "false";
            else
                nullable = "true";
            first = first.TrimEnd(',');
            last = last.TrimEnd(',');
            dataGridView1.Rows.Add(n.valor, first, last, nullable);
        }

        //method for show in datagridview the follow
        private void Show_Follow(Nodo n)
        {
            //scroll in PostOrder
            if (n.hi != null)
                Show_Follow(n.hi);
            if (n.hd != null)
                Show_Follow(n.hd);
            if (n.id != 0)
            {
                string follow = "";
                foreach (var item in n.follow)
                    follow = follow + item.ToString() + ",";
                follow = follow.TrimEnd(',');
                dataGridView3.Rows.Add(n.id, follow);
            }
        }

        //method for show in datagridview the transitions
        private void Show_Transitions(List<string> columns_v, List<string> status, List<string> values)
        {
            //Add name the columns
            foreach (var item in columns_v)
            {
                dataGridView2.Columns.Add(item, item);
            }
            //add status and values for columns the row
            for (int i = 0; i < status.Count; i++)
            {
                dataGridView2.Rows.Add(status[i]);
            }
            //scroll row
            for (int i = 0; i < values.Count; i++)
            {
                //name the columns first row
                string[] aux = values[i].Split(';');
                //scroll columns
                for (int j = 0; j < columns_v.Count; j++)
                {
                    //status
                    if (j != 0)
                    {
                        if (aux[j - 1] == "0")
                            dataGridView2.Rows[i].Cells[j].Value = "---";
                        else
                            dataGridView2.Rows[i].Cells[j].Value = aux[j - 1];
                    }
                }
            }



        }

        //method for get last follow
        private void GetLastFollow(Nodo n)
        {
            //scroll in PostOrder
            if (n.hi != null)
                GetLastFollow(n.hi);
            if (n.hd != null)
                GetLastFollow(n.hd);
            if (n.valor == "#")
                LastFollow = n.id.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)

        {
            //Button generar programa
            try
            {
                //------------------------------SAVE INFORMATION IN NEW PROGRAM-------------------

                WriteDataFile(); //pasa los datos relevantes para el scanner

                GenericScanner gs = new GenericScanner();
                string[] lines = File.ReadAllLines(@"Generic\GenericS\bin\Debug\data.txt");
                gs.NewProgramCS(lines); //modificamos el programa a ejecutar


                //------------------------------COPY DIRECTORY AND FINISH PROGRAM-------------------

                #region EXECUTE NEW PROGRAM
                //code for get path the where save new folder
                using (var folderDialog = new FolderBrowserDialog())
                {
                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        //pathfilegeneric contain the new path
                        PathFileGeneric = folderDialog.SelectedPath;
                    }
                }

                //The final part copy the new directory
                DirectoryInfo origen = new DirectoryInfo(@"Generic");
                DirectoryInfo destino = new DirectoryInfo(PathFileGeneric);
                CopyDirectory(origen, destino);            
                MessageBox.Show("PROGRAMA GENERICO EXITOSO!!!");

                /* NO SE ACTUALIZA EL .EXE CUANDO LO EJECUTO

                //Ejecutar el programa directamente del archivo creado
                ProcessStartInfo info = new ProcessStartInfo();

                info.UseShellExecute = true;
                info.FileName = "GenericS.exe";
                info.WorkingDirectory = PathFileGeneric + @"\GenericS\bin\Debug";
                Process.Start(info);

                */

                //Exit de program
                this.Dispose();
                #endregion

                //tested
            }
            catch (Exception)
            {
                //ocurrio algo no esperado :(
                MessageBox.Show("ERROR");
            }

        }

        #region Functions Button Generar Programa

        //method for copy the new folder with new program
        private void CopyDirectory(DirectoryInfo origen, DirectoryInfo destino)
        {
            if (!destino.Exists)
            {
                destino.Create();
            }

            FileInfo[] files = origen.GetFiles();
            foreach (FileInfo file in files)
            {
                file.CopyTo(Path.Combine(destino.FullName, file.Name));
            }

            // Subcarpetas
            DirectoryInfo[] dirs = origen.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                string destinoDir = Path.Combine(destino.FullName, dir.Name);
                // llamada recusriva
                CopyDirectory(dir, new DirectoryInfo(destinoDir));
            }
        }

        //Method for write in file but the new proyect
        private void WriteDataFile()
        {
            using (StreamWriter writer = new StreamWriter(@"Generic\GenericS\bin\Debug\data.txt"))
            {

                GenericScanner mGS = new GenericScanner();
                //Construir el archivo

                writer.WriteLine("wSets");
                if (L_Sets.Count > 0)
                {
                    foreach (var item2 in L_Sets)
                    {
                        string set_values = mGS.GetValuesLists(item2);
                        writer.WriteLine(set_values);
                    }
                 
                }
                writer.WriteLine("wTokens");
                foreach (var item in L_Tokens)
                {
                    writer.WriteLine(item);
                }
                writer.WriteLine("wActions");
                foreach (var item in L_Actions)
                {
                    writer.WriteLine(item);
                }
                writer.WriteLine("wError");
                foreach (var item in L_Error)
                {
                    writer.WriteLine(item);
                }
                writer.WriteLine("wNombreEstados");
                bool flag = false; //evito que se copie el primer estado que es "ESTADO"
                foreach (var item in Columns_Transitions)
                {
                    if (flag == true)
                        writer.WriteLine(item);
                    flag = true;
                }
                writer.WriteLine("wEstados");              
                foreach (var item in Status)
                {
                    writer.WriteLine(item);
                }
                writer.WriteLine("wValoresEstado");
                for (int i = 0; i < Values_Transitions.Count; i++)
                {
                    //elimino el estado de "ESTADO" no servia para nada
                    Values_Transitions[i] = Values_Transitions[i].TrimEnd(';');
                    Values_Transitions[i] = Values_Transitions[i].TrimEnd('0');
                    Values_Transitions[i] = Values_Transitions[i].TrimEnd(';');
                    writer.WriteLine(Values_Transitions[i]);
                }
                writer.WriteLine("wEstadoFinal");
                writer.WriteLine(LastFollow);
                writer.WriteLine("wFin");

            }
        }

        #endregion

    }
}
