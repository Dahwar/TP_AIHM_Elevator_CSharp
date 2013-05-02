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
        Timer timerCheckBox1;
        Timer timerCheckBox2;
        Timer timerCheckBox3;
        Timer timerTextBox;

        Dictionary<int, CheckBox> listButton = new Dictionary<int, CheckBox>();

        public Form1()
        {
            InitializeComponent();

            this.listButton.Add(0, this.checkBox4);
            this.listButton.Add(1, this.checkBox5);
            this.listButton.Add(2, this.checkBox6);

            this.elevator = new Elevator(this.listButton);
            this.Controls.Add(this.elevator);

            this.timerCheckBox1 = new Timer();
            this.timerCheckBox1.Interval = 750;
            this.timerCheckBox1.Tick += new EventHandler(this.TimerCheckBox1);

            this.timerCheckBox2 = new Timer();
            this.timerCheckBox2.Interval = 750;
            this.timerCheckBox2.Tick += new EventHandler(this.TimerCheckBox2);

            this.timerCheckBox3 = new Timer();
            this.timerCheckBox3.Interval = 750;
            this.timerCheckBox3.Tick += new EventHandler(this.TimerCheckBox3);

            this.timerTextBox = new Timer();
            this.timerTextBox.Interval = 50;
            this.timerTextBox.Tick += new EventHandler(this.TimerTextBox);
            this.timerTextBox.Start();

        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.elevator.AddFloorToList(2);
            this.timerCheckBox1.Start();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.elevator.AddFloorToList(1);
            this.timerCheckBox2.Start();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            this.elevator.AddFloorToList(0);
            this.timerCheckBox3.Start();
        }

        private void TimerCheckBox1(Object sender, EventArgs e)
        {
            this.timerCheckBox1.Stop();
            checkBox1.Checked = false;
        }

        private void TimerCheckBox2(Object sender, EventArgs e)
        {
            this.timerCheckBox2.Stop();
            checkBox2.Checked = false;
        }

        private void TimerCheckBox3(Object sender, EventArgs e)
        {
            this.timerCheckBox3.Stop();
            checkBox3.Checked = false;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            this.elevator.AddFloorToList(2);
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            this.elevator.AddFloorToList(1);
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            this.elevator.AddFloorToList(0);
        }

        private void TimerTextBox(object sender, EventArgs e)
        {
            this.textBox1.Text = this.elevator.getCurrentFloor().ToString();
        }
    }
}
