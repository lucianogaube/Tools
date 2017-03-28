using System;
using System.Windows.Forms;

namespace RestAPIDiff
{
    public partial class WaitForm : Form
    {
        public bool ShouldClose = false;
        public WaitForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShouldClose = true;
            this.Dispose();
        }
    }
}
