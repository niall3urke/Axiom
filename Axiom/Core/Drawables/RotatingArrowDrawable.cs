using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Timers;
using Axiom.Core.Utils;

namespace Axiom.Core.Drawables
{
    public class RotatingArrowDrawable : DrawableBase
    {

        // Events

        public Action RotationComplete { get; set; }

        public Action RotationStarted { get; set; }

        // Properties

        public bool IsRotating { get; private set; }

        // Fields

        private readonly Action _timerElapsed;

        private readonly Timer _timer;

        private double _targetAngle;

        private double _angle;

        private bool _cw;

        // Constructors

        public RotatingArrowDrawable(Action timerElapsed)
        {
            _timerElapsed = timerElapsed;

            _timer = new Timer(10);
            _timer.Elapsed += TimerElapsed;
            _timer.AutoReset = true;

            Height = 6;
            Width = 10;
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
            double increment = 20;
            bool stop;

            if (_cw)
            {
                _angle += increment;
                stop = _angle >= _targetAngle;
            }
            else
            {
                _angle -= increment;
                stop = _angle <= _targetAngle;
            }

            if (stop)
            {
                _timer.Stop();
                _angle = _targetAngle; // Incase we overshoot

                IsRotating = false;
                RotationComplete?.Invoke();
            }

            _timerElapsed?.Invoke();
        }

        // Methods: public

        public override void Draw(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (var path = GetPath())
            using (var pen = new Pen(ForegroundColor, 2f))
            {
                g.DrawPath(pen, path);
            }
        }

        public void RotateBy(double fromAngle, double toAngle)
        {
            if (IsRotating)
                return;

            if (_angle == toAngle)
                return;

            IsRotating = true;

            _cw = fromAngle < toAngle;

            _targetAngle = toAngle;

            _angle = fromAngle;

            RotationStarted?.Invoke();

            _timer.Start();
        }

        // Methods: private

        private GraphicsPath GetPath()
        {
            // Get center coords
            float cx = Location.X + Width * 0.5f;
            float cy = Location.Y + Height * 0.5f;

            // Get triangle coords
            float x0 = Location.X;
            float y0 = Location.Y;
            float x1 = x0 + Width * 0.5f;
            float y1 = y0 + Height;
            float x2 = x0 + Width;

            // Get triangle points
            var p0 = new PointF(x0, y0);
            var p1 = new PointF(x1, y1);
            var p2 = new PointF(x2, y0);

            // Get center point
            var cp = new PointF(cx, cy);

            // Get the points list
            var points = new List<PointF> { p0, p1, p2 };

            if (_angle != 0)
            {
                // Rotate the points wrt the center 
                points = Rotate.Points(points, cp, _angle);
            }

            // Create the path
            var p = new GraphicsPath();

            p.AddLine(points[0], points[1]);

            p.AddLine(points[1], points[2]);

            return p;
        }


    }
}
