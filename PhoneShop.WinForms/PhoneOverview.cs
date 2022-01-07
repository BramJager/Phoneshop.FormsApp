using Phoneshop.Domain.Entities;
using Phoneshop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Phoneshop.WinForms
{
    public partial class PhoneOverview : Form
    {
        private List<Phone> phones = new List<Phone>();
        private readonly IPhoneService phoneService;

        public PhoneOverview(IPhoneService phoneService)
        {
            this.phoneService = phoneService;

            InitializeComponent();

            GetPhones();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreatePhoneList();
        }

        private void lstPhones_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = (Phone)lstPhones.SelectedItem;
            lblBrand.Text = selected.Brand.Name;
            lblDescription.Text = selected.Description;
            lblPrice.Text = selected.Price.ToString();
            lblType.Text = selected.Type;
            lblStock.Text = selected.Stock.ToString();

            button2.Enabled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            GetPhones(textBox1.Text);
            CreatePhoneList();
        }

        private void CreatePhoneList()
        {
            lstPhones.DataSource = phones;
            lstPhones.DisplayMember = nameof(Phone.FullName);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var addPhoneForm = new AddPhone(phoneService);
            addPhoneForm.ShowDialog();

            if (addPhoneForm.DialogResult == DialogResult.OK)
            {
                GetPhones(string.Empty);
                CreatePhoneList();
            }
        }

        private void ButtonDelete(object sender, EventArgs e)
        {
            var selected = (Phone)lstPhones.SelectedItem;

            phoneService.Delete(selected.Id);
            GetPhones(string.Empty);
            CreatePhoneList();
        }

        private void GetPhones(string search = "")
        {
            if (string.IsNullOrEmpty(search))
                phones = phoneService.Get().ToList();
            else if (search.Length <= 3)
                return;
            else
                phones = phoneService.Search(search).ToList();
        }
    }
}
