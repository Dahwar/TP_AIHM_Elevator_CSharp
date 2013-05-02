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

        private Timer timerElevator;
        private Timer timerPause;

        private enum CabinMoves { UP, DOWN, STAY };
        private CabinMoves currentMove = CabinMoves.STAY;
        private int currentPosition = 0;

        private int coef = 1000;

        Pen grayPen = new Pen(Color.Gray, 2);
        Pen blackPen = new Pen(Color.Black, 1);
        SolidBrush gray = new SolidBrush(Color.Gray);

    

        public Elevator()
        {
            this.Size = new Size(160, 540);
            this.Paint += new PaintEventHandler(AddDraw);
            this.DoubleBuffered = true;

            this.timerElevator = new Timer();
            this.timerElevator.Interval = 10;
            this.timerElevator.Tick += new EventHandler(this.TimerEvent);
            this.timerElevator.Start();
        }

        private void AddDraw(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            double positionY = 0.3 * coef;

            g.DrawLine(this.blackPen, 50, 80, 50, 529);
            g.DrawLine(this.blackPen, 150, 80, 150, 529);

            g.FillRectangle(this.gray, 52, (int)positionY+80, 48, 150);
            g.FillRectangle(this.gray, 101, (int)positionY+80, 48, 150);
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

        private void TimerEvent(Object sender, EventArgs e)
        {
            switch(this.currentMove){
				case CabinMoves.STAY:
					if(this.listFloor.Count!=0){
						if(this.listFloor.First.Value != this.currentPosition){
                            if (this.listFloor.First.Value > currentPosition)
                                this.currentMove = CabinMoves.UP;
                            else if (this.listFloor.First.Value < currentPosition)
                                this.currentMove = CabinMoves.DOWN;
                            else
                                this.listFloor.RemoveFirst();
						}
						else
                            this.listFloor.RemoveFirst();
							//HMButtons.get(currentPosition).setSelected(false);
					}
					break;
						
				case CabinMoves.UP:
					coef-=5;
					if(coef==0){
						currentPosition=2;
						//myWindow.setCabinLevel(currentPosition);
					}
					else if(coef==500){
						currentPosition=1;
						//myWindow.setCabinLevel(currentPosition);
					}
					else if(coef==1000){
						currentPosition=0;
						//myWindow.setCabinLevel(currentPosition);
					}
					else;
						
					if(this.listFloor.First.Value==currentPosition){
						currentMove = CabinMoves.STAY;
						//HMButtons.get(currentPosition).setSelected(false);
						//this.timerElevator.Stop();
						//this.timerPause.Start();
                        this.listFloor.RemoveFirst();
					}
					break;
						
				case CabinMoves.DOWN:
					coef+=5;
					if(coef==0){
						currentPosition=2;
						//myWindow.setCabinLevel(currentPosition);
					}
					else if(coef==500){
						currentPosition=1;
						//myWindow.setCabinLevel(currentPosition);
					}
					else if(coef==1000){
						currentPosition=0;
						//myWindow.setCabinLevel(currentPosition);
					}
					else;
						
					if(this.listFloor.First.Value==currentPosition){
						currentMove = CabinMoves.STAY;
						//HMButtons.get(currentPosition).setSelected(false);
						//timer.stop();
						//timerPause.start();
                        this.listFloor.RemoveFirst();
					}
					break;
			}
            //Console.Write(coef);
            this.Invalidate();
        }
    }
}
