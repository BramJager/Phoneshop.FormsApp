using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Entities;
using System;
using System.Text;
using System.Windows.Forms;

namespace Phoneshop.WinForms
{
    public partial class AddPhone : Form
    {
        private readonly IPhoneService phoneService;

        public AddPhone(IPhoneService phoneService)
        {
            InitializeComponent();
            this.phoneService = phoneService;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!CheckPhone())
                return;
;
            phoneService.Create(new Phone
            {
                Brand = new Brand
                {
                    Name = txtBrandName.Text
                },
                Type = txtType.Text,
                Description = txtDescription.Text,
                Price = Convert.ToDouble(txtPrice.Text),
                Stock = Convert.ToInt32(txtStock.Text)
            });

            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool CheckPhone()
        {
            var messages = new StringBuilder();

            if (string.IsNullOrEmpty(txtBrandName.Text))
                messages.AppendLine("Brand is required");
            if (string.IsNullOrEmpty(txtType.Text))
                messages.AppendLine("Type is required");
            if (string.IsNullOrEmpty(txtDescription.Text))
                messages.AppendLine("Description is required");
            if (string.IsNullOrEmpty(txtPrice.Text) || Convert.ToInt32(txtPrice.Text) <= 0)
                messages.AppendLine("Price is invalid. It can't be negative.");
            if (string.IsNullOrEmpty(txtStock.Text) || Convert.ToInt32(txtStock.Text) <= 0)
                messages.AppendLine("Stock is invalid. It can't be negative.");

            if (messages.Length > 0)
            {
                MessageBox.Show(messages.ToString(), "Errors", MessageBoxButtons.OK);
                return false;
            }

            return true;
        }
    }
}
