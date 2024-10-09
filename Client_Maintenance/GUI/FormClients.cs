using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client_Maintenance.VALIDATION;
using Client_Maintenance.BLL;
using Client_Maintenance.DAL;

namespace Client_Maintenance.GUI
{
    public partial class FormClients : Form
    {
        public FormClients()
        {
            InitializeComponent();
        }

        private void listViewUsers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string input = textBoxClientNumber.Text.Trim();
            if (!Validator.IsValidClientNumber(input, 4))
            {
                MessageBox.Show("ClientNumber must be 4-digit number.", "Invalid ClientNumber", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxClientNumber.Clear();
                textBoxClientNumber.Focus();
                return;

            }

            
            Clients cli = new Clients();
            if (!cli.IsUniqueClientNumber(Convert.ToInt32(input)))
            {
                MessageBox.Show("ClientNumber must be unique.\n" + "Please enter another ClientNumber.", "Duplicate ClientNumber", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxClientNumber.Clear();
                textBoxClientNumber.Focus();
                return;

            }
            string input1 = textBoxPhoneNumber.Text.Trim();
            
            if (!Validator.IsValidPhoneNumber(input1))
            {
                MessageBox.Show("Phone Number is in Incorrect format.", "Invalid PhoneNumber", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ;
            }
                cli.ClientNumber = Convert.ToInt32(textBoxClientNumber.Text.Trim());
            cli.FirstName = textBoxFName.Text.Trim();
            cli.LastName = textBoxLName.Text.Trim();
            cli.PhoneNumber = textBoxPhoneNumber.Text.Trim();
            cli.Email = textBoxEmail.Text.Trim();
            cli.SaveClient(cli);
            MessageBox.Show("Client data has been saved successfully.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            textBoxClientNumber.Clear();
            textBoxFName.Clear();
            textBoxEmail.Clear();
            textBoxPhoneNumber.Clear();
            textBoxLName.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {

            var answer = MessageBox.Show("Do you really want to exit the application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            Clients clients = new Clients();
            List<Clients> listC = clients.GetClientList();
            listViewClient.Items.Clear();
            foreach (Clients cli in listC)
            {
                ListViewItem item = new ListViewItem(cli.ClientNumber.ToString());
                item.SubItems.Add(cli.LastName);
                item.SubItems.Add(cli.FirstName);
                item.SubItems.Add(cli.PhoneNumber);
                item.SubItems.Add(cli.Email);
                listViewClient.Items.Add(item);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the Search option first.", "Search Option", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string input = "";
            Clients cli = new Clients();
            switch (comboBox1.SelectedIndex)
            {
                case 0: 
                    input = txtInput.Text.Trim();
                    if (!Validator.IsValidClientNumber(input, 4))
                    {
                        MessageBox.Show("ClientNumber must be 4-digit number.", "Invalid ClientNumber", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtInput.Clear();
                        txtInput.Focus();
                        return;
                    }
                    cli = cli.SearchClient(Convert.ToInt32(input));
                    if (cli != null)
                    {
                        textBoxClientNumber.Text = cli.ClientNumber.ToString();
                        textBoxFName.Text = cli.FirstName.ToString();
                        textBoxLName.Text = cli.LastName.ToString();
                        textBoxPhoneNumber.Text = cli.PhoneNumber.ToString();
                        textBoxEmail.Text = cli.Email.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Client not found!", "Invalid ClientNumber", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtInput.Clear();
                        txtInput.Focus();
                        return;
                    }
                    break;
                case 1: 
                    input = txtInput.Text.Trim();
                    List<Clients> listC = new List<Clients>();
                    listC = cli.SearchClient(input);
                    listViewClient.Items.Clear();

                    if (listC.Count == 0)
                    {
                        MessageBox.Show("Client not found!", "Invalid First Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtInput.Clear();
                        txtInput.Focus();
                        return;

                    }
                    else
                    {
                        foreach (Clients cliItem in listC)
                        {
                           
                            ListViewItem item = new ListViewItem(cliItem.ClientNumber.ToString());
                            item.SubItems.Add(cliItem.FirstName);
                            item.SubItems.Add(cliItem.LastName);
                            item.SubItems.Add(cliItem.PhoneNumber);
                            item.SubItems.Add(cliItem.Email);
                            listViewClient.Items.Add(item);
                        }

                    }
                    break;
                default:
                    break;
            }
        }
      

     
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indexSelected = comboBox1.SelectedIndex;
            switch (indexSelected)
            {
                case 0:

                    txtInput.Clear();
                    txtInput.Focus();
                    break;

                default:
                    break;
            }
        }

     


















    }
}
