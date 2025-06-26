using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lozovoi_Lab4_Diagrammer
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public string filePath = "";
        public string fileName = "";

        private void folderDialogButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                schemaPathField.Text = folderBrowserDialog1.SelectedPath;
            }
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        private void createButton_Click(object sender, EventArgs e)
        {
            filePath = schemaPathField.Text;
            fileName = schemaNameField.Text;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
