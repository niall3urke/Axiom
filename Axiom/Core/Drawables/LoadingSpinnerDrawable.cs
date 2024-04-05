using System;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Timers;

namespace Axiom.Core.Drawables
{
    public class LoadingSpinnerDrawable : DrawableBase
    {

        // Properties

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                ToggleTimer();
            }
        }

        // Fields - backing properties

        private bool _isLoading;

        // Fields 

        private readonly Action _timerElapsed;

        private readonly Timer _timer;

        private float _startAngle = 0;

        private Pen _pen;

        // Constructors

        public LoadingSpinnerDrawable(Action timerElapsed)
        {
            _timerElapsed = timerElapsed;

            _timer = new Timer(50);
            _timer.Elapsed += TimerElapsed;
            _timer.AutoReset = true;
        }

        // Events

        public override void EnabledChanged(bool enabled) { }

        public override bool MouseLeave(Point p) => false;

        public override bool MouseEnter(Point p) => false;

        public override bool MouseDown(Point p) => false;

        public override bool MouseUp(Point p) => false;

        public override bool Click(Point p) => false;

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            _startAngle += 40;

            if (_startAngle >= 360)
            {
                _startAngle = 0;
            }

            _timerElapsed?.Invoke();
        }

        // Methods

        public override void Draw(Graphics g)
        {
            using (var path = new GraphicsPath())
            using (_pen = new Pen(ForegroundColor, 2f))
            {
                path.StartFigure();

                path.AddArc(new RectangleF(Location.X, Location.Y, Width, Height), _startAngle, 135);

                g.DrawPath(_pen, path);
            }
        }

        private void ToggleTimer()
        {
            if (_isLoading)
            {
                _timer.Start();
            }
            else
            {
                _timer.Stop();
            }
        }


    }
}
