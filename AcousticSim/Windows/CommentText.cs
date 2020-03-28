using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Envision.Windows
{
    public partial class CommentText : Form
    {

        public CommentText(string text = "")
        {
            InitializeComponent();
            txtComment.Text = text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
