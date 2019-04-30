using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IISDataServiceCLient.UserService;


namespace IISDataServiceCLient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            label2.Text = "Errors :";
            var client = new UserService.UserServiceClient();
            var users = client.GetUsers();
            
            if (users == null)
            {
                label2.Text += " Hittade inga, users = null";
            }
            else
            {
                label2.Text += " Inga fel";
                foreach (var user in users)
                {
                    listBox1.Items.Add(user.FirstName + " " + user.FirstName);
                }
            }
        }
    }
}
