using Axiom.Controls.Input;
using Axiom.Core.Drawables;
using Axiom.Core.Logic;
using System.Drawing;

namespace Axiom.Controls.Select
{
    public class SelectBoxLogic : LogicBase
    {

        // =================
        // ===== Properties
        // =================

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

        // =============
        // ===== Fields
        // =============

        private readonly BackgroundDrawable _background;

        // ===================
        // ===== Constructors
        // ===================

        public SelectBoxLogic() : base()
        {
            _background = new BackgroundDrawable()
            {
                CurveBtmLhs = false,
                CurveBtmRhs = false,
                Radius = 6,
            };
        }

        // =============
        // ===== Events  
        // =============

        // ======================
        // ===== Methods: public  
        // ======================

        public void Draw(Graphics g)
        {
            if (DesignMode)
                return;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            SetProperties();

            SetBackgroundProperties();

            _background.Draw(g);

            using (var p = new Pen(BorderColor, 2f))
            {
                g.DrawLine(p, new Point(0, Height - 2), new Point(Width, Height - 2));
            }
        }

        // =======================
        // ===== Methods: private  
        // =======================

        private void SetProperties()
        {
            SetColors();
        }

        private void SetBackgroundProperties()
        {
            _background.BackgroundColor = BackgroundColor;

            _background.ForegroundColor = ForegroundColor;

            _background.BorderColor = BorderColor;

            _background.FocusColor = FocusColor;

            _background.Height = Height;

            _background.Width = Width;
        }

        private void SetColors()
        {
            (BackgroundColor, ForegroundColor, BorderColor, FocusColor) =
                InputColorHelper.Get(Color, State, System.Drawing.Color.Transparent);
        }


    }
}
