using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace TP_AIHM_Elevator
{
    public class Elevator : Control
    {
        private LinkedList<int> listFloor = new LinkedList<int>();

        Pen grayPen = new Pen(Color.Gray, 2);
        Pen blackPen = new Pen(Color.Black, 1);
        SolidBrush gray = new SolidBrush(Color.Gray);

        public Elevator()
        {

        }

        public void AddDraw(Graphics g)
        {
            g.DrawLine(this.blackPen, 50, 80, 50, 530);
            g.DrawLine(this.blackPen, 150, 80, 150, 530);

            g.FillRectangle(this.gray, 52, 80, 48, 150);
            g.FillRectangle(this.gray, 101, 80, 48, 150);

            g.Dispose();
        }

        public void AddFloorToList(int floor)
        {
            if (floor >= 0 && floor <= 2)
            {
                if (!this.listFloor.Contains(floor))
                {
                    this.listFloor.AddLast(floor);
                    //HMButtons.get(floor).setSelected(true);
                }
            }
        }
    }
}
