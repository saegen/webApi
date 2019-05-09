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
            List<ApiUser> users = new List<ApiUser>();
            UserServiceClient client = null;
            try
            {
                client = new UserService.UserServiceClient();
                if (userId.Value > 0)
                {
                    var foundUser = client.GetUser(Decimal.ToInt32(userId.Value));
                    if (foundUser != null)
                    {
                        users.Add(foundUser);
                    }
                }
                else
                {
                    users = client.GetUsers().ToList();
                }
                client.Close();
            }
            catch(Exception err)
            {
                label2.Text += " " + err.Message; //i.e log error
                throw err;
            }
            finally
            {
                ClientExtensions.DisposeService(client);
            }
                        
            if (users == null)
            {
                label2.Text += " Hittade inga, users = null";
            }            
            else
            {
                label2.Text += " Inga fel";
                foreach (var user in users)
                {
                    listBox1.Items.Add("Id : " + user.Id + " " + user.FirstName + " " + user.FirstName);
                }
            }
            client.Close();
        }

        private void EF_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            using (Rebtel db = new Rebtel())
            {
                foreach (var user in db.Users.Include("Subscriptions"))
                {
                    listBox1.Items.Add("Id : " + user.Id + " " + user.FirstName + " " + user.FirstName);
                }
            }
        }
    }
}
