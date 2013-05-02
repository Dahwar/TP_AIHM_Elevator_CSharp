using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TP_AIHM_Elevator
{
    public partial class Form1 : Form
    {
        private Elevator elevator;

        public Form1()
        {
            InitializeComponent();
            this.elevator = new Elevator();
            this.Controls.Add(this.elevator);
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.elevator.AddFloorToList(2);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.elevator.AddFloorToList(1);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            this.elevator.AddFloorToList(0);
        }
    }
}
