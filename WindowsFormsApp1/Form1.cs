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
    public partial class Form1 : Form
    {
        private MySqlConnection con;
        private string server;
        private string database;
        private string uid;
        private string password;

        public Form1()
        {
            server = "localhost";
            database = "auth";

            uid = "test";
            password = "test";

            string conString;
            conString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";

            con = new MySqlConnection(conString);

            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            string user = txt_user.Text;
            string pass = txt_pass.Text;

            if (IsLogin(user, pass))
            {
                MessageBox.Show("good");
            }
            else
            {
                MessageBox.Show("fail");
            }
        }

        private bool IsLogin(string user, string pass)
        {
            string query = $"SELECT * FROM accounts WHERE username='{user}' AND password='{pass}';";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, con);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        reader.Close();
                        con.Close();
                        return true;
                    }
                    else
                    {
                        reader.Close();
                        con.Close();
                        return false;
                    }
                }
                else
                {
                    con.Close();
                    return false;
                }
            }

            catch (Exception ex)
            {
                con.Close();
                return false;
            }
        }

        private bool OpenConnection()
        {
            try
            {
                con.Open();
                return true;
            }

            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("hello world");
                        break;

                    case 1:
                        MessageBox.Show("hello world");
                        break;
                }
                return false;
            }
        }
    }
}