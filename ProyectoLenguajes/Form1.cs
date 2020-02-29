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
            if (F_KeyWords(L_Tokens,L_Actions,L_Error,res) != "GG")
            {
                MessageBox.Show(F_KeyWords(L_Tokens, L_Actions, L_Error, res)); //end program
            }
            else
            {
                MessageBox.Show("Primer Filtro Correcto"); //CONTINUED

                //Probar arbol
                ETree nuevo = new ETree();

                //string ER
                string pruebaER = "(a+ b (c (a|b|c)+ c (a|b)?)+) #";//"((a|b)+ a) #";//si funciona con los dos
                Stack<Nodo> arbol = new Stack<Nodo>();
                arbol = nuevo.Insert(pruebaER); //create tree
                nuevo.InOrder(arbol.Pop());
                MessageBox.Show(nuevo.cadena);


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
            //foreach (var item in L_Error)
            //{
            //    MessageBox.Show(item);
            //}
            //test filling of hoc the file is separated by lines
            //foreach (var item in res)
            //{
            //    MessageBox.Show(item + "-1");
            //}
            //MessageBox.Show(res.Count().ToString());//number of lines read
        }

        //method for verify correct opertation of sections
        private string F_KeyWords(List<string> t, List<string> a, List<string> e, string[] f)
        {
            string res = "";//send answer
            //get first item at each list
            //no included sets becasuse it can come or not
            if (t.ElementAt(0) == "ERROR")
            {
                res = "ERROR PALABRA CLAVE = 'TOKENS' ";
            }
            else if (a.ElementAt(0) == "ERROR")
            {
                res = "ERROR PALABRA CLAVE = 'ACTIONS' ";
            }
            else if (e.ElementAt(0) == "ERROR")
            {
                res = "ERROR PALABRA CLAVE = 'ERROR =' ";
            }
            else
            {
                res = "GG";   //first filter correct
            }
            return res;
        }//tested


    }
}
