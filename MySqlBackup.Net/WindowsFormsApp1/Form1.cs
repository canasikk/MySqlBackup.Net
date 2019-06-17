using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    //http://instagram.com/canasikk
    
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        string constring = "server=localhost;user=root;database=canlink;";

        private void Button1_Click(object sender, EventArgs e)
        {
 
            string file = Application.StartupPath + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString()+ "_" + DateTime.Now.Hour.ToString()+ DateTime.Now.Minute.ToString()+ DateTime.Now.Second.ToString() + ".sql";
            using (MySqlConnection conn = new MySqlConnection(constring))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Close();
                        conn.Open();
                        mb.ExportToFile(file);
                        conn.Close();
                        MessageBox.Show("Done!!");
                    }
                }
            }
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "SQL Dosyası |*.sql";  
            file.FilterIndex = 1;
            file.RestoreDirectory = true;
            file.CheckFileExists = true;
            file.Title = "SQL Dosyası seçiniz!!";
            file.Multiselect = false;
             

            if (file.ShowDialog() == DialogResult.OK)
            {
                
                string DosyaYolu = file.FileName;
                string DosyaAdi = file.SafeFileName;
                
                using (MySqlConnection conn = new MySqlConnection(constring))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            cmd.Connection = conn;
                            conn.Close();
                            conn.Open();
                            mb.ImportFromFile(DosyaYolu);
                            conn.Close();
                            MessageBox.Show("Done!!");

                        }
                    }
                } 
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "Path @@ " + Application.StartupPath.ToString();
        }
    }
}
