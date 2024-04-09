using Axiom.Controls.ColorHelpers;
using Axiom.Core;
using Axiom.Core.Drawables;
using Axiom.Core.Logic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Axiom.Controls.Box
{
    class BoxLogic : LogicBase, IAppearanceProperties
    {

        // =================
        // ===== Properties
        // =================

        public bool IsOutlined
        {
            get => _isOutlined;
            set => SetField(ref _isOutlined, value);
        }

        public bool IsInverted
        {
            get => _isInverted;
            set => SetField(ref _isInverted, value);
        }

        public bool IsLight
        {
            get => _isLight;
            set => SetField(ref _isLight, value);
        }

        public bool IsRounded
        {
            get => _isRounded;
            set => SetField(ref _isRounded, value);
        }

        public bool CurveTopLhs
        {
            get => _background.CurveTopLhs;
            set => _background.CurveTopLhs = value;
        }

        public bool CurveTopRhs
        {
            get => _background.CurveTopRhs;
            set => _background.CurveTopRhs = value;
        }

        public bool CurveBtmLhs
        {
            get => _background.CurveBtmLhs;
            set => _background.CurveBtmLhs = value;
        }

        public bool CurveBtmRhs
        {
            get => _background.CurveBtmRhs;
            set => _background.CurveBtmRhs = value;
        }

        // ==================================
        // ===== Properties - backing fields
        // ==================================

        private bool _isOutlined;

        private bool _isInverted;

        private bool _isRounded;

        private bool _isLight;

        // =============
        // ===== Fields
        // =============

        private readonly BackgroundDrawable _background;

        // ===================
        // ===== Constructors
        // ===================

        public BoxLogic() : base()
        {
            _background = new BackgroundDrawable();
        }

        // ======================
        // ===== Methods: public
        // ======================

        public void Draw(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            SetProperties();

            SetBackgroundProperties();

            _background.Draw(g);
        }

        // =======================
        // ===== Methods: private  
        // =======================

        private void SetProperties()
        {
            SetColors();
        }

        private void SetColors()
        {
            (BackgroundColor, ForegroundColor, BorderColor, FocusColor) =
                ButtonColorHelper.Get(Color, State, this, System.Drawing.Color.Transparent);
        }

        private void SetBackgroundProperties()
        {
            _background.Location = Point.Empty;

            _background.Height = Height - 1;

            _background.Width = Width - 1;

            _background.BackgroundColor = BackgroundColor;

            _background.ForegroundColor = ForegroundColor;

            _background.BorderColor = BorderColor;
        }

    }
}
