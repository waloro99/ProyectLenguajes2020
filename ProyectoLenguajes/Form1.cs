using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProyectoLenguajes.Class;




namespace ProyectoLenguajes
{
    public partial class Form1 : Form
    {

        public string PathFile = string.Empty; // var path the file

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
            Semantic_Analysis();    //Semantic analysis      
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
            }

            textBox1.Text = PathFile;
        }

        //method to do semantic analysis
        public void Semantic_Analysis()
        {
            //instance class
            ReadFileC rf = new ReadFileC();

            //return config start
            button2.Enabled = false;
            textBox1.Enabled = false;

            //get data from the all file
            string[] res = rf.ReadFile(PathFile);

            //lists
            List<string> L_Sets = new List<string>(); //save data section sets
            List<string> L_Tokens = new List<string>(); //save data section sets
            List<string> L_Actions = new List<string>(); //save data section sets
            List<string> L_Error = new List<string>(); //save data section sets

            //add items at lists
            L_Sets = rf.SplitSets(res);  //tested
            L_Tokens = rf.SplitTokens(res); //tested
            L_Actions = rf.SplitActions(res);  //tested
            L_Error = rf.SplitError(res);  //tested

            //firs filter (key words)
            if (F_KeyWords(L_Tokens,L_Actions,L_Error) != "GG")
            {
                MessageBox.Show(F_KeyWords(L_Tokens, L_Actions, L_Error)); //end program
            }
            else
            {
                MessageBox.Show("Primer Filtro Correcto"); //CONTINUED
                                                           //------------------- SECOND FILTER

                //Create string ER
                string ER_sets1 = "(a+ f+ g (f+ h (a|b|c|i) h (j|k)?)+) #"; //LETRA   = 'A'..'Z'+'a'..'z'+'_'
                string ER_sets2 = "(a+ f+ g (f+ d+ l c+ m j?)+) #"; //CHARSET = CHR(32)..CHR(254)
                string ER_tokens = "(e+ f+ c+ f* g f* ((h (a|i) h)*|(a|f|n)*|(a|b|f|i)*)+) #"; //'"' CHARSET '"'|''' CHARSET ''' // LETRA ( LETRA | DIGITO )*   { RESERVADAS() } 
                string ER_actions = "(c+ f g f h a+ h) #"; //18 = 'PROGRAM'
                string ER_error = "(ñ+ f g f c+) #"; //ERROR = 54

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

                //recorrido
                T_Sets.InOrder(Tree_Sets.Pop());
                T_Sets2.InOrder(Tree_Sets2.Pop());
                T_Tokens.InOrder(Tree_Tokens.Pop());
                T_Actions.InOrder(Tree_Actions.Pop());
                T_Error.InOrder(Tree_Error.Pop());

                //mostrar recorrido
                MessageBox.Show(T_Sets.cadena);
                MessageBox.Show(T_Sets2.cadena);
                MessageBox.Show(T_Tokens.cadena);
                MessageBox.Show(T_Actions.cadena);
                MessageBox.Show(T_Error.cadena);






                //----------------------------- READ ERROR -----------------------------------------------

                //send all tokens 
                Token t = new Token();
                List<Token> L_t = new List<Token>();
                L_t = t.Insert_Tokens();


                //filter error
                if (rf.ReadError(L_Error, L_t, ER_error) != "GG")
                {
                    string er = rf.ReadError(L_Error, L_t, ER_error);
                    char[] x = er.ToArray();
                    int line = Error_Line(x,res);
                    MessageBox.Show("Error en la linea: " + line + " Columna: " + x[0]);
                }

        
    

                //Probar arbol
                //ETree nuevo = new ETree();

                //string ER
                //string pruebaER = "(a+ b (c (a|b|c)+ c (a|b)?)+) #";//"((a|b)+ a) #";//si funciona con los dos
                //Stack<Nodo> arbol = new Stack<Nodo>();
                //arbol = nuevo.Insert(pruebaER); //create tree
                //nuevo.InOrder(arbol.Pop());
                //MessageBox.Show(nuevo.cadena);


                //only tested 
                //string prueba = "Hola mundo)";
                //char[] prueba2 = prueba.ToArray();

                //foreach (var item in prueba2)
                //{
                //    if (item == ')')//item.CompareTo('o') == 0)
                //        MessageBox.Show(item.ToString());
                //}
                //Stack<string> prueba = new Stack<string>();
                //prueba.Push("hola");
                //prueba.Push("adios");
                //if(prueba.Peek() != null)//prueba.Count() > 0)
                //    MessageBox.Show(prueba.Peek());
                //MessageBox.Show(prueba2.Length.ToString());
            }


            //test filling sections in lists
            //foreach (var item in L_Sets)
            //{
            //    MessageBox.Show(item);
            //}
            //foreach (var item in L_Tokens)
            //{
            //    MessageBox.Show(item);
            //}
            //foreach (var item in L_Actions)
            //{
            //    MessageBox.Show(item);
            //}
            //foreach (var item in L_Error)
            //{
            //    MessageBox.Show(item);
            //}
            //test filling of hoc the file is separated by lines
            //foreach (var item in res)
            //{
            //    MessageBox.Show(item);
            //}
            //MessageBox.Show(res.Count().ToString());//number of lines read
        }

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

        private int Error_Line(char[] err , string[] res)
        {
            string cadena = "";

            for (int i = 0; i < err.Length; i++)
            {
                if (i!=0)
                {
                    cadena += err[i];
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
    }
}
