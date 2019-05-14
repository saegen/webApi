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

        private void GetUsers(object sender, EventArgs e)
        {
            listBoxUsers.Items.Clear();
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
                client.CloseOrAbortService();
                //ClientExtensions.DisposeService(client);
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
                    listBoxUsers.Items.Add(user.Id + ":Id, " + user.FirstName + ", " + user.LastName);
                }
            }
            client.Close();
        }

        private void EF_Click(object sender, EventArgs e)
        {
            listBoxUsers.Items.Clear();
            using (Rebtel db = new Rebtel())
            {
                foreach (var user in db.Users.Include("Subscriptions"))
                {
                    listBoxUsers.Items.Add(user.Id + ":Id, " + user.FirstName + ", " + user.LastName);
                }
            }
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
                var userId = listBoxUsers.Items[listBoxUsers.SelectedIndex].ToString().Split(':')[0];
                selectedUser.Text = userId.ToString();
        }

        private void ListBoxSubs_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string subId = listBoxSubs.Items[listBoxSubs.SelectedIndex].ToString().Split(':')[0];
            selectedSub.Text = subId.ToString();
        }

        private void GetSubs_Click(object sender, EventArgs e)
        {
            listBoxSubs.Items.Clear();
            using (Rebtel db = new Rebtel())
            {
                foreach (var sub in db.Subscriptions)
                {
                    listBoxSubs.Items.Add(sub.Id + ":Id, " + sub.Name);
                }
            }
        }

        private void Subscribe_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedUser.Text) || string.IsNullOrEmpty(selectedSub.Text))
            {
                MessageBox.Show("Välj både en user och sub!");
                return;
            }
            using (Rebtel db = new Rebtel())
            {
                var sub = db.Subscriptions.FirstOrDefault(s => s.Id.ToString() == selectedSub.Text);
                var user = db.Users.FirstOrDefault(s => s.Id.ToString() == selectedUser.Text);
                if (user == null)
                {
                    throw new NullReferenceException("User med id = " + selectedUser.Text + " kunde hittas");
                }
                if (user == null)
                {
                    throw new NullReferenceException("Subscription med id = " + selectedSub.Text + " kunde hittas");
                }
                user.Subscriptions.Add(sub);
                db.SaveChanges();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetUsers(this as object,null);
            GetSubs_Click(this as object, null);

        }
    }
}
