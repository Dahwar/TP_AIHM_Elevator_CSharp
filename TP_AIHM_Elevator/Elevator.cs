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
        Dictionary<int, CheckBox> listButton;
        CheckBox recupButton;

        private Timer timerElevator;
        private Timer timerDoor;
        private Timer timerPause;

        private enum CabinMoves { UP, DOWN, STAY };
        private CabinMoves currentMove = CabinMoves.STAY;
        private int currentPosition = 0;

        private int coef = 1000;

        private enum CabinDoorState { OPENING, CLOSING, OPEN, CLOSE };
        private CabinDoorState currentDoorState = CabinDoorState.CLOSE;
        private int doorSizeX = 48;
        private int positionXRightDoor = 101;

        Pen grayPen = new Pen(Color.Gray, 2);
        Pen blackPen = new Pen(Color.Black, 1);
        SolidBrush gray = new SolidBrush(Color.Orange);

        public Elevator(Dictionary<int, CheckBox> listButton)
        {
            this.listButton = listButton;
            this.Size = new Size(160, 540);
            this.Paint += new PaintEventHandler(AddDraw);
            this.DoubleBuffered = true;

            this.timerElevator = new Timer();
            this.timerElevator.Interval = 10;
            this.timerElevator.Tick += new EventHandler(this.TimerEventElevator);
            this.timerElevator.Start();

            this.timerPause = new Timer();
            this.timerPause.Interval = 500;
            this.timerPause.Tick += new EventHandler(this.TimerEventPause);

            this.timerDoor = new Timer();
            this.timerDoor.Interval = 20;
            this.timerDoor.Tick += new EventHandler(this.TimerEventDoor);
        }

        private void AddDraw(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            double positionY = 0.3 * coef;

            g.DrawLine(this.blackPen, 50, 80, 50, 529);
            g.DrawLine(this.blackPen, 150, 80, 150, 529);

            g.FillRectangle(this.gray, 52, (int)positionY+80, this.doorSizeX, 150);
            g.FillRectangle(this.gray, this.positionXRightDoor, (int)positionY + 80, this.doorSizeX, 150);
        }

        public void AddFloorToList(int floor)
        {
            if (floor >= 0 && floor <= 2)
            {
                if (!this.listFloor.Contains(floor))
                {
                    this.listFloor.AddLast(floor);
                    this.listButton.TryGetValue(floor, out this.recupButton);
                    this.recupButton.Checked = true;
                    this.recupButton = null;
                }
            }
        }

        private void TimerEventElevator(Object sender, EventArgs e)
        {
            if (this.currentDoorState == CabinDoorState.CLOSE)
            {
                switch (this.currentMove)
                {
                    case CabinMoves.STAY:
                        if (this.listFloor.Count != 0)
                        {
                            if (this.listFloor.First.Value != this.currentPosition)
                            {
                                if (this.listFloor.First.Value > currentPosition)
                                    this.currentMove = CabinMoves.UP;
                                else if (this.listFloor.First.Value < currentPosition)
                                    this.currentMove = CabinMoves.DOWN;
                                else
                                    this.listFloor.RemoveFirst();
                            }
                            else
                                this.listFloor.RemoveFirst();
                            this.listButton.TryGetValue(this.currentPosition, out this.recupButton);
                            this.recupButton.Checked = false;
                            this.recupButton = null;
                        }
                        break;

                    case CabinMoves.UP:
                        coef -= 5;
                        if (coef == 0)
                        {
                            currentPosition = 2;
                        }
                        else if (coef == 500)
                        {
                            currentPosition = 1;
                        }
                        else if (coef == 1000)
                        {
                            currentPosition = 0;
                        }
                        else ;

                        if (this.listFloor.First.Value == currentPosition)
                        {
                            currentMove = CabinMoves.STAY;
                            this.listButton.TryGetValue(this.currentPosition, out this.recupButton);
                            this.recupButton.Checked = false;
                            this.recupButton = null;
                            this.timerElevator.Stop();
                            this.currentDoorState = CabinDoorState.OPENING;
                            this.timerPause.Start();
                            this.listFloor.RemoveFirst();
                        }
                        break;

                    case CabinMoves.DOWN:
                        coef += 5;
                        if (coef == 0)
                        {
                            currentPosition = 2;
                        }
                        else if (coef == 500)
                        {
                            currentPosition = 1;
                        }
                        else if (coef == 1000)
                        {
                            currentPosition = 0;
                        }
                        else ;

                        if (this.listFloor.First.Value == currentPosition)
                        {
                            currentMove = CabinMoves.STAY;
                            this.listButton.TryGetValue(this.currentPosition, out this.recupButton);
                            this.recupButton.Checked = false;
                            this.recupButton = null;
                            this.timerElevator.Stop();
                            this.currentDoorState = CabinDoorState.OPENING;
                            this.timerPause.Start();
                            this.listFloor.RemoveFirst();
                        }
                        break;
                }
                this.Invalidate();
            }
        }

        private void TimerEventDoor(Object sender, EventArgs e)
        {
            switch (this.currentDoorState)
            {
                case CabinDoorState.CLOSE:
                    this.timerDoor.Stop();
                    this.timerElevator.Start();
                    break;
                case CabinDoorState.CLOSING:
                    this.doorSizeX++;
                    this.positionXRightDoor--;
                    if (this.doorSizeX >= 48)
                    {
                        this.currentDoorState = CabinDoorState.CLOSE;
                        this.timerDoor.Stop();
                        this.timerPause.Start();
                    }
                    break;
                case CabinDoorState.OPEN:
                    this.currentDoorState = CabinDoorState.CLOSING;
                    this.timerDoor.Stop();
                    this.timerPause.Start();
                    break;
                case CabinDoorState.OPENING:
                    this.doorSizeX--;
                    this.positionXRightDoor++;
                    if (this.doorSizeX <= 0)
                    {
                        this.currentDoorState = CabinDoorState.OPEN;
                        this.timerDoor.Stop();
                        this.timerPause.Start();
                    }
                    break;
            }
            this.Invalidate();
        }

        private void TimerEventPause(Object sender, EventArgs e)
        {
            this.timerPause.Stop();
            this.timerDoor.Start();
        }

        public int getCurrentFloor()
        {
            return this.currentPosition;
        }
    }
}
