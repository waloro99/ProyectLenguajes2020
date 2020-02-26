using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


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
            //instance class
            ReadFileC rf = new ReadFileC();

            //return config start
            button2.Enabled = false;
            textBox1.Enabled = false;

            //get data the first line
            string[] res = rf.ReadFile(PathFile);
            List<string> n = new List<string>();
            List<string> n2 = new List<string>();
            List<string> n3 = new List<string>();
            List<string> n4 = new List<string>();
            //n = rf.SplitSets(res);
            //n2 = rf.SplitTokens(res);
            //n3 = rf.SplitActions(res);
            n4 = rf.SplitError(res);
            foreach (var item in n4)
            {
                MessageBox.Show(item);
            }
            //foreach (var item in res)
            //{
            //    MessageBox.Show(item + "-1");
            //}
            //MessageBox.Show(res.Count().ToString());//cantidad de lineas leidas
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
    }
}
