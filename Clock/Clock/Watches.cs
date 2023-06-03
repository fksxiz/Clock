using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clock
{
    public class Watches : Control
    {
        protected int _x;
        protected int _y;
        protected int _ObjSize;
        protected int _Minutes;
        protected int _Seconds;
        protected int _ss;
        protected int _mm;
        protected int _cnt;
        protected Color _BackColor;
        protected Color _UnActiveColor;
        protected Color _ActiveColor;
        protected int NSize;
        protected int ObjWidth;
        protected Color _DarkColor;
        protected Color _LightColor;
        public enum ObjStates
        {
            osConvex,
            osConcavity
        }

        public ObjStates _ObjState;

        public virtual ObjStates ObjState
        {
            get
            {
                return _ObjState;
            }
            set
            {
                if (value != _ObjState)
                {
                    _ObjState = value;
                    Invalidate();
                }

            }
        }
        public Watches() : base()
        {
            _BackColor = Color.Black;
            _UnActiveColor = Color.DarkSlateBlue;
            _ActiveColor = Color.MediumPurple;
            _LightColor = Color.DimGray;
            _DarkColor = Color.DarkGray;
            _ObjState = ObjStates.osConcavity;
            _Timer = new System.Timers.Timer(1000);
            _Timer.Elapsed += OnTimer;
            _Timer.AutoReset = true;
            _Timer.Enabled = false;
            _cnt = 0;
            _ss = 40;
            _mm = 20;
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (width < 200)
            {
                width = 200;
            }
            height = width / 2;
            _x = x;
            _y = y;
            NSize = width / 8;
            ObjWidth = width / 40;
            base.SetBoundsCore(x, y, width, height, specified);
            Invalidate();
        }
        public virtual int Cnt
        {
            get
            {
                return _cnt;
            }
            set
            {
                if (_cnt != value)
                {
                    _cnt = value;
                }
            }
        }

        public virtual int Minutes
        {
            get
            {
                return _Minutes;
            }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                else
                {
                    if (value > 23)
                    {
                        value = 0;
                        if (_cnt != 0)
                        {
                            _Seconds = 0;
                            _cnt = 0;
                        }
                    }
                }
                if (_Minutes != value)
                {
                    _Minutes = value;
                    Invalidate();
                }
            }
        }

        public virtual int Seconds
        {
            get
            {
                return _Seconds;
            }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                else
                {
                    if (value > 59)
                    {
                        value = 0;
                        if (_cnt != 0)
                        {
                            _Minutes++;
                            _cnt = 0;
                        }
                    }
                }
                if (_Seconds != value)
                {
                    _Seconds = value;
                    Invalidate();
                }
            }
        }

        public virtual int mm
        {
            get
            {
                return _mm;
            }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                else
                {
                    if (value > 23)
                    {
                        value = 0;
                    }
                }
                if (_mm != value)
                {
                    _mm = value;
                }
            }
        }

        public virtual int ss
        {
            get
            {
                return _ss;
            }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                else
                {
                    if (value > 59)
                    {
                        value = 0;
                    }
                }

                if (_ss != value)
                {
                    _ss = value;
                }
            }
        }
        public Color DarkColor
        {
            get
            {
                return _DarkColor;
            }
            set
            {
                if (value != _DarkColor)
                {
                    _DarkColor = value;
                    Invalidate();
                }
            }
        }

        public Color LightColor
        {
            get
            {
                return _LightColor;
            }
            set
            {
                if (value != _LightColor)
                {
                    _LightColor = value;
                    Invalidate();
                }
            }
        }

        public override Color BackColor
        {
            get
            {
                return _BackColor;
            }
            set
            {
                if (value != _BackColor)
                {
                    _BackColor = value;
                    Invalidate();
                }
            }
        }

        public virtual Color UnActiveColor
        {
            get
            {
                return _UnActiveColor;
            }
            set
            {
                if (value != _UnActiveColor)
                {
                    _UnActiveColor = value;
                    Invalidate();
                }
            }
        }

        public virtual Color ActiveColor
        {
            get
            {
                return _ActiveColor;
            }
            set
            {
                if (value != _ActiveColor)
                {
                    _ActiveColor = value;
                    Invalidate();
                }
            }
        }
        protected System.Timers.Timer _Timer;

        protected virtual void OnTimer(Object source, System.Timers.ElapsedEventArgs e)
        {
            SuccessivelyDigitChange();
            _cnt++;
            Invalidate();
        }

        public virtual void StartClock()
        {
            _Timer.Start();
        }
        public virtual void StopClock()
        {
            _Timer.Stop();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int Offset = Width / 10;
            NSize = Width / 8;
            ObjWidth = Width / 40;
            int Offsety = Height / 2 - NSize;
            base.OnPaint(e);
            Brush B = new SolidBrush(BackColor);
            Pen LightPen = new Pen(_LightColor);
            Pen DarkPen = new Pen(_DarkColor);
            e.Graphics.FillRectangle(B, ClientRectangle);
            Brush A = new SolidBrush(ActiveColor);
            Brush UA = new SolidBrush(UnActiveColor);
            for (int i = 0; i < Height / 10; i++)
            {
                Point[] D =
                    {
                        new Point(Width-i,i),
                        new Point(i,i),
                        new Point(i,Height-i)
                        };
                Point[] L =
            {
                        new Point(Width-i,i),
                        new Point(Width-i,Height-i),
                        new Point(i,Height-i)
                        };
                if ((int)ObjState == 0)
                {
                    e.Graphics.DrawLines(DarkPen, D);
                    e.Graphics.DrawLines(LightPen, L);
                }
                else
                {
                    e.Graphics.DrawLines(DarkPen, L);
                    e.Graphics.DrawLines(LightPen, D);
                }
            }
            for (int i = 1; i <= 4; i++)
            {
                e.Graphics.FillRectangle(A, Offset * i + NSize * (i - 1) + ObjWidth, Offsety, NSize - ObjWidth, ObjWidth);
                e.Graphics.FillRectangle(A, Offset * i + NSize * (i - 1) + ObjWidth, Offsety + NSize, NSize - ObjWidth, ObjWidth);
                e.Graphics.FillRectangle(A, Offset * i + NSize * (i - 1) + ObjWidth, Offsety + NSize * 2, NSize - ObjWidth, ObjWidth);
                e.Graphics.FillRectangle(A, Offset * i + NSize * (i - 1), Offsety + ObjWidth, ObjWidth, NSize - ObjWidth);
                e.Graphics.FillRectangle(A, Offset * i + NSize * (i - 1), Offsety + ObjWidth + NSize, ObjWidth, NSize - ObjWidth);
                e.Graphics.FillRectangle(A, Offset * i + NSize * (i - 1) + NSize, Offsety + ObjWidth, ObjWidth, NSize - ObjWidth);
                e.Graphics.FillRectangle(A, Offset * i + NSize * (i - 1) + NSize, Offsety + ObjWidth + NSize, ObjWidth, NSize - ObjWidth);
            }
            if (_cnt % 2 == 0)
            {
                e.Graphics.FillRectangle(A, Width / 2 - 2, Height / 2 - Height / 10, Height / 20, Height / 20);
                e.Graphics.FillRectangle(A, Width / 2 - 2, Height / 2 + Height / 10, Height / 20, Height / 20);
            }
            else
            {
                e.Graphics.FillRectangle(UA, Width / 2 - 2, Height / 2 - Height / 10, Height / 20, Height / 20);
                e.Graphics.FillRectangle(UA, Width / 2 - 2, Height / 2 + Height / 10, Height / 20, Height / 20);
            }
            switch (_Minutes % 10)
            {
                case 0:
                    e.Graphics.FillRectangle(UA, Offset * 2 + NSize * (1) + ObjWidth, Offsety + NSize, NSize - ObjWidth, ObjWidth);
                    break;
                case 1:
                    e.Graphics.FillRectangle(UA, Offset * 2 + NSize * (1) + ObjWidth, Offsety, NSize - ObjWidth, ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 2 + NSize * (1) + ObjWidth, Offsety + NSize, NSize - ObjWidth, ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 2 + NSize * (1) + ObjWidth, Offsety + NSize * 2, NSize - ObjWidth, ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 2 + NSize * (1), Offsety + ObjWidth, ObjWidth, NSize - ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 2 + NSize * (1), Offsety + ObjWidth + NSize, ObjWidth, NSize - ObjWidth);
                    break;
                case 2:
                    e.Graphics.FillRectangle(UA, Offset * 2 + NSize * (1), Offsety + ObjWidth, ObjWidth, NSize - ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 2 + NSize * (1) + NSize, Offsety + ObjWidth + NSize, ObjWidth, NSize - ObjWidth);
                    break;
                case 3:
                    e.Graphics.FillRectangle(UA, Offset * 2 + NSize * (1), Offsety + ObjWidth, ObjWidth, NSize - ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 2 + NSize * (1), Offsety + ObjWidth + NSize, ObjWidth, NSize - ObjWidth);
                    break;
                case 4:
                    e.Graphics.FillRectangle(UA, Offset * 2 + NSize * (1) + ObjWidth, Offsety, NSize - ObjWidth, ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 2 + NSize * (1) + ObjWidth, Offsety + NSize * 2, NSize - ObjWidth, ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 2 + NSize * (1), Offsety + ObjWidth + NSize, ObjWidth, NSize - ObjWidth);
                    break;
                case 5:
                    e.Graphics.FillRectangle(UA, Offset * 2 + NSize * (1), Offsety + ObjWidth + NSize, ObjWidth, NSize - ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 2 + NSize * (1) + NSize, Offsety + ObjWidth, ObjWidth, NSize - ObjWidth);
                    break;
                case 6:
                    e.Graphics.FillRectangle(UA, Offset * 2 + NSize * (1) + NSize, Offsety + ObjWidth, ObjWidth, NSize - ObjWidth);
                    break;
                case 7:
                    e.Graphics.FillRectangle(UA, Offset * 2 + NSize * (1) + ObjWidth, Offsety + NSize, NSize - ObjWidth, ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 2 + NSize * (1) + ObjWidth, Offsety + NSize * 2, NSize - ObjWidth, ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 2 + NSize * (1), Offsety + ObjWidth, ObjWidth, NSize - ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 2 + NSize * (1), Offsety + ObjWidth + NSize, ObjWidth, NSize - ObjWidth);
                    break;
                case 9:
                    e.Graphics.FillRectangle(UA, Offset * 2 + NSize * (1), Offsety + ObjWidth + NSize, ObjWidth, NSize - ObjWidth);
                    break;
            }
            switch (_Minutes / 10)
            {
                case 0:
                    e.Graphics.FillRectangle(UA, Offset * 1 + NSize * (0) + ObjWidth, Offsety + NSize, NSize - ObjWidth, ObjWidth);
                    break;
                case 1:
                    e.Graphics.FillRectangle(UA, Offset * 1 + NSize * (0) + ObjWidth, Offsety, NSize - ObjWidth, ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 1 + NSize * (0) + ObjWidth, Offsety + NSize, NSize - ObjWidth, ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 1 + NSize * (0) + ObjWidth, Offsety + NSize * 2, NSize - ObjWidth, ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 1 + NSize * (0), Offsety + ObjWidth, ObjWidth, NSize - ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 1 + NSize * (0), Offsety + ObjWidth + NSize, ObjWidth, NSize - ObjWidth);
                    break;
                case 2:
                    e.Graphics.FillRectangle(UA, Offset * 1 + NSize * (0), Offsety + ObjWidth, ObjWidth, NSize - ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 1 + NSize * (0) + NSize, Offsety + ObjWidth + NSize, ObjWidth, NSize - ObjWidth);
                    break;
                case 3:
                    e.Graphics.FillRectangle(UA, Offset * 1 + NSize * (0), Offsety + ObjWidth, ObjWidth, NSize - ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 1 + NSize * (0), Offsety + ObjWidth + NSize, ObjWidth, NSize - ObjWidth);
                    break;
                case 4:
                    e.Graphics.FillRectangle(UA, Offset * 1 + NSize * (0) + ObjWidth, Offsety, NSize - ObjWidth, ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 1 + NSize * (0) + ObjWidth, Offsety + NSize * 2, NSize - ObjWidth, ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 1 + NSize * (0), Offsety + ObjWidth + NSize, ObjWidth, NSize - ObjWidth);
                    break;
                case 5:
                    e.Graphics.FillRectangle(UA, Offset * 1 + NSize * (0), Offsety + ObjWidth + NSize, ObjWidth, NSize - ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 1 + NSize * (0) + NSize, Offsety + ObjWidth, ObjWidth, NSize - ObjWidth);
                    break;
                case 6:
                    e.Graphics.FillRectangle(UA, Offset * 1 + NSize * (0) + NSize, Offsety + ObjWidth, ObjWidth, NSize - ObjWidth);
                    break;
                case 7:
                    e.Graphics.FillRectangle(UA, Offset * 1 + NSize * (0) + ObjWidth, Offsety + NSize, NSize - ObjWidth, ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 1 + NSize * (0) + ObjWidth, Offsety + NSize * 2, NSize - ObjWidth, ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 1 + NSize * (0), Offsety + ObjWidth, ObjWidth, NSize - ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 1 + NSize * (0), Offsety + ObjWidth + NSize, ObjWidth, NSize - ObjWidth);
                    break;
                case 9:
                    e.Graphics.FillRectangle(UA, Offset * 1 + NSize * (0), Offsety + ObjWidth + NSize, ObjWidth, NSize - ObjWidth);
                    break;
            }
            switch (_Seconds % 10)
            {
                case 0:
                    e.Graphics.FillRectangle(UA, Offset * 4 + NSize * (3) + ObjWidth, Offsety + NSize, NSize - ObjWidth, ObjWidth);
                    break;
                case 1:
                    e.Graphics.FillRectangle(UA, Offset * 4 + NSize * (3) + ObjWidth, Offsety, NSize - ObjWidth, ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 4 + NSize * (3) + ObjWidth, Offsety + NSize, NSize - ObjWidth, ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 4 + NSize * (3) + ObjWidth, Offsety + NSize * 2, NSize - ObjWidth, ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 4 + NSize * (3), Offsety + ObjWidth, ObjWidth, NSize - ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 4 + NSize * (3), Offsety + ObjWidth + NSize, ObjWidth, NSize - ObjWidth);
                    break;
                case 2:
                    e.Graphics.FillRectangle(UA, Offset * 4 + NSize * (3), Offsety + ObjWidth, ObjWidth, NSize - ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 4 + NSize * (3) + NSize, Offsety + ObjWidth + NSize, ObjWidth, NSize - ObjWidth);
                    break;
                case 3:
                    e.Graphics.FillRectangle(UA, Offset * 4 + NSize * (3), Offsety + ObjWidth, ObjWidth, NSize - ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 4 + NSize * (3), Offsety + ObjWidth + NSize, ObjWidth, NSize - ObjWidth);
                    break;
                case 4:
                    e.Graphics.FillRectangle(UA, Offset * 4 + NSize * (3) + ObjWidth, Offsety, NSize - ObjWidth, ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 4 + NSize * (3) + ObjWidth, Offsety + NSize * 2, NSize - ObjWidth, ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 4 + NSize * (3), Offsety + ObjWidth + NSize, ObjWidth, NSize - ObjWidth);
                    break;
                case 5:
                    e.Graphics.FillRectangle(UA, Offset * 4 + NSize * (3), Offsety + ObjWidth + NSize, ObjWidth, NSize - ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 4 + NSize * (3) + NSize, Offsety + ObjWidth, ObjWidth, NSize - ObjWidth);
                    break;
                case 6:
                    e.Graphics.FillRectangle(UA, Offset * 4 + NSize * (3) + NSize, Offsety + ObjWidth, ObjWidth, NSize - ObjWidth);
                    break;
                case 7:
                    e.Graphics.FillRectangle(UA, Offset * 4 + NSize * (3) + ObjWidth, Offsety + NSize, NSize - ObjWidth, ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 4 + NSize * (3) + ObjWidth, Offsety + NSize * 2, NSize - ObjWidth, ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 4 + NSize * (3), Offsety + ObjWidth, ObjWidth, NSize - ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 4 + NSize * (3), Offsety + ObjWidth + NSize, ObjWidth, NSize - ObjWidth);
                    break;
                case 9:
                    e.Graphics.FillRectangle(UA, Offset * 4 + NSize * (3), Offsety + ObjWidth + NSize, ObjWidth, NSize - ObjWidth);
                    break;
            }
            switch (_Seconds / 10)
            {
                case 0:
                    e.Graphics.FillRectangle(UA, Offset * 3 + NSize * (2) + ObjWidth, Offsety + NSize, NSize - ObjWidth, ObjWidth);
                    break;
                case 1:
                    e.Graphics.FillRectangle(UA, Offset * 3 + NSize * (2) + ObjWidth, Offsety, NSize - ObjWidth, ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 3 + NSize * (2) + ObjWidth, Offsety + NSize, NSize - ObjWidth, ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 3 + NSize * (2) + ObjWidth, Offsety + NSize * 2, NSize - ObjWidth, ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 3 + NSize * (2), Offsety + ObjWidth, ObjWidth, NSize - ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 3 + NSize * (2), Offsety + ObjWidth + NSize, ObjWidth, NSize - ObjWidth);
                    break;
                case 2:
                    e.Graphics.FillRectangle(UA, Offset * 3 + NSize * (2), Offsety + ObjWidth, ObjWidth, NSize - ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 3 + NSize * (2) + NSize, Offsety + ObjWidth + NSize, ObjWidth, NSize - ObjWidth);
                    break;
                case 3:
                    e.Graphics.FillRectangle(UA, Offset * 3 + NSize * (2), Offsety + ObjWidth, ObjWidth, NSize - ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 3 + NSize * (2), Offsety + ObjWidth + NSize, ObjWidth, NSize - ObjWidth);
                    break;
                case 4:
                    e.Graphics.FillRectangle(UA, Offset * 3 + NSize * (2) + ObjWidth, Offsety, NSize - ObjWidth, ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 3 + NSize * (2) + ObjWidth, Offsety + NSize * 2, NSize - ObjWidth, ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 3 + NSize * (2), Offsety + ObjWidth + NSize, ObjWidth, NSize - ObjWidth);
                    break;
                case 5:
                    e.Graphics.FillRectangle(UA, Offset * 3 + NSize * (2), Offsety + ObjWidth + NSize, ObjWidth, NSize - ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 3 + NSize * (2) + NSize, Offsety + ObjWidth, ObjWidth, NSize - ObjWidth);
                    break;
                case 6:
                    e.Graphics.FillRectangle(UA, Offset * 3 + NSize * (2) + NSize, Offsety + ObjWidth, ObjWidth, NSize - ObjWidth);
                    break;
                case 7:
                    e.Graphics.FillRectangle(UA, Offset * 3 + NSize * (2) + ObjWidth, Offsety + NSize, NSize - ObjWidth, ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 3 + NSize * (2) + ObjWidth, Offsety + NSize * 2, NSize - ObjWidth, ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 3 + NSize * (2), Offsety + ObjWidth, ObjWidth, NSize - ObjWidth);
                    e.Graphics.FillRectangle(UA, Offset * 3 + NSize * (2), Offsety + ObjWidth + NSize, ObjWidth, NSize - ObjWidth);
                    break;
                case 9:
                    e.Graphics.FillRectangle(UA, Offset * 3 + NSize * (2), Offsety + ObjWidth + NSize, ObjWidth, NSize - ObjWidth);
                    break;
            }
        }

        public virtual void SuccessivelyDigitChange()
        {
            Seconds++;
            OnDigitChange();
            if ((ss == _Seconds) && (mm == _Minutes))
            {
                OnAlarm();
            }
        }

        public virtual void State()
        {
            int I = (int)_ObjState + 1;
            I = I > 1 ? 0 : I;
            ObjState = (ObjStates)I;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams P = base.CreateParams;
                P.ExStyle = P.ExStyle | 0x02000000;
                return P;
            }
        }

        protected event EventHandler _OnDigitChange;
        protected event EventHandler _OnAlarm;

        public event EventHandler DigitChange
        {
            add
            {
                _OnDigitChange += value;
            }
            remove
            {
                _OnDigitChange -= value;
            }
        }

        public event EventHandler Alarm
        {
            add
            {
                _OnAlarm += value;
            }
            remove
            {
                _OnAlarm -= value;
            }
        }

        protected void OnDigitChange()
        {
            if (_OnDigitChange != null)
            {
                _OnDigitChange(this, new EventArgs());
            }
        }

        protected void OnAlarm()
        {
            if (_OnAlarm != null)
            {
                _OnAlarm(this, new EventArgs());
            }
        }
    }
}
