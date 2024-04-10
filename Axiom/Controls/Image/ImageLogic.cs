using Axiom.Core;
using Axiom.Core.Drawables;
using Axiom.Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace Axiom.Controls.Image
{
    public class ImageLogic : INotifyPropertyChanged
    {

        // ==============
        // ===== Actions
        // ==============

        public event PropertyChangedEventHandler PropertyChanged;

        // =================
        // ===== Properties
        // =================

        public AxState State
        {
            get => _state;
            set => SetField(ref _state, value);
        }

        public AxShape Shape
        {
            get => _shape;
            set => SetField(ref _shape, value);
        }

        public bool IsClickable
        {
            get => _isClickable;
            set => SetField(ref _isClickable, value);
        }

        public bool IsRounded
        {
            get => _isRounded;
            set => SetField(ref _isRounded, value);
        }

        public bool IsCircle
        {
            get => _isCircle;
            set => SetField(ref _isCircle, value);
        }

        public bool CurveTopLhs
        {
            get => _curveTopLhs;
            set => SetField(ref _curveTopLhs, value);
        }

        public bool CurveTopRhs
        {
            get => _curveTopRhs;
            set => SetField(ref _curveTopRhs, value);
        }

        public bool CurveBtmLhs
        {
            get => _curveBtmLhs;
            set => SetField(ref _curveBtmLhs, value);
        }

        public bool CurveBtmRhs
        {
            get => _curveBtmRhs;
            set => SetField(ref _curveBtmRhs, value);
        }

        public Color BorderColor
        {
            get => _border.BorderColor;
            set => _border.BorderColor = value;
        }

        public Point Location { get; set; }

        public float Radius { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        // ==================================
        // ===== Properties - backing fields
        // ==================================

        private bool _curveTopLhs;

        private bool _curveTopRhs;

        private bool _curveBtmLhs;

        private bool _curveBtmRhs;

        private bool _isClickable;

        private bool _isRounded;

        private bool _isCircle;

        private AxState _state;

        private AxShape _shape;

        // =============
        // ===== Fields
        // =============

        private readonly BackgroundDrawable _background;

        private readonly BorderDrawable _border;

        // ===================
        // ===== Constructors
        // ===================

        public ImageLogic()
        {
            _background = new BackgroundDrawable();

            _border = new BorderDrawable();

            _curveTopLhs = true;

            _curveTopRhs = true;

            _curveBtmLhs = true;

            _curveBtmRhs = true;
        }

        // =============
        // ===== Events
        // =============

        public virtual void EnabledChanged(bool enabled) =>
            State = enabled ? AxState.Normal : AxState.Disabled;

        // ======================
        // ===== Methods: public  
        // ======================

        public void Draw(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            SetBorderProperties();

            if (IsRounded || IsCircle)
            {
                _border.Draw(g);
            }
        }

        public Region GetRegion()
        {
            if (IsRounded || IsCircle)
            {
                SetBackgroundProperties();

                using (var g = _background.GetPath())
                    return new Region(g);
            }

            return new Region(new Rectangle(Location.X, Location.Y, Width, Height));
        }

        // =======================
        // ===== Methods: private  
        // =======================

        private void SetBackgroundProperties()
        {
            if (IsCircle)
            {
                _background.Radius = Height * 0.5f;
            }
            else
            {
                _background.Radius = 6f;

                if (Shape == AxShape.Small)
                    _background.Radius *= 0.5f;

                else if (Shape == AxShape.Medium)
                    _background.Radius *= 2f;

                else if (Shape == AxShape.Large)
                    _background.Radius *= 3f;
            }

            _background.CurveBtmLhs = CurveBtmLhs;

            _background.CurveBtmRhs = CurveBtmRhs;

            _background.CurveTopLhs = CurveTopLhs;

            _background.CurveTopRhs = CurveTopRhs;

            _background.Height = Height;

            _background.Width = Width;  
        }

        private void SetBorderProperties()
        {
            if (IsCircle)
            {
                _border.Radius = Height * 0.5f;
            }
            else
            {
                _border.Radius = 6f;

                if (Shape == AxShape.Small)
                    _border.Radius *= 0.5f;

                else if (Shape == AxShape.Medium)
                    _border.Radius *= 2f;

                else if (Shape == AxShape.Large)
                    _border.Radius *= 3f;
            }

            _border.CurveBtmLhs = CurveBtmLhs;

            _border.CurveBtmRhs = CurveBtmRhs;

            _border.CurveTopLhs = CurveTopLhs;

            _border.CurveTopRhs = CurveTopRhs;

            _border.Height = Height;

            _border.Width = Width;
        }

        protected void SetField<T>(ref T field, T value, Action todo = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;

                todo?.Invoke();

                NotifyPropertyChanged();
            }
        }

        protected void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
