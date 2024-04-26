using Axiom.Controls.ColorHelpers;
using Axiom.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace Axiom.Controls.Label
{
    class LabelLogic : INotifyPropertyChanged, IAppearanceProperties
    {

        // ==============
        // ===== Actions
        // ==============

        public event PropertyChangedEventHandler PropertyChanged;

        // =================
        // ===== Properties
        // =================

        public bool IsClickable
        {
            get => _isClickable;
            set => SetField(ref _isClickable, value);
        }

        public AxFontWeight Weight
        {
            get => _weight;
            set => SetField(ref _weight, value);
        }

        public AxLabelSize Size
        {
            get => _size;
            set => SetField(ref _size, value);
        }

        public AxState State
        {
            get => _state;
            set => SetField(ref _state, value);
        }

        public AxColor Color
        {
            get => _color;
            set => SetField(ref _color, value);
        }

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

        public Color ForeColor { get; private set; }

        public Font Font { get; private set; }

        public Color BackgroundColor { get; set; }

        public Color BorderColor { get; set; }

        public Color FocusColor { get; set; }

        // ==================================
        // ===== Properties - backing fields
        // ==================================

        private bool _isClickable;

        private bool _isOutlined;

        private bool _isInverted;

        private bool _isLight;

        private AxFontWeight _weight;

        private AxLabelSize _size;

        private AxState _state;

        private AxColor _color;

        // =============
        // ===== Fields
        // =============


        // ===================
        // ===== Constructors
        // ===================

        public LabelLogic()
        {
            Font = new Font("Segoe UI", 16, FontStyle.Regular, GraphicsUnit.Pixel);

            _weight = AxFontWeight.Normal;

            _size = AxLabelSize.IsSize6;
        }

        // =============
        // ===== Events
        // =============

        public virtual void EnabledChanged(bool enabled) =>
            State = enabled ? AxState.Normal : AxState.Disabled;

        public virtual bool MouseLeave(Point p) => false;

        public virtual bool MouseEnter(Point p) => false;

        public virtual bool MouseDown(Point p) => false;

        public virtual bool MouseMove(Point p) => false;

        public virtual bool MouseUp(Point p) => false;

        public virtual bool Click(Point p) => false;

        // ==============
        // ===== Methods
        // ==============

        protected virtual void OnStateChanged() { }

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

        // ======================
        // ===== Methods: public 
        // ======================

        public void Update()
        {
            SetColor();
            SetFont();
        }

        private void SetColor()
        {
            ForeColor  = LabelColorHelper.Get(Color, State, this, System.Drawing.Color.Transparent);
        }

        private void SetFont()
        {
            int size = GetFontSize();

            var (style, fontFamilyModifier) = GetFontStyle();

            if (TryGetStyleModifier(out FontStyle fontStyleModifier))
            {
                style |= fontStyleModifier;
            }

            Font = new Font($"Segoe UI{fontFamilyModifier}", size, style, GraphicsUnit.Pixel);
        }

        private (FontStyle, string) GetFontStyle()
        {
            if (Weight == AxFontWeight.Normal)
            {
                return (FontStyle.Regular, "");
            }
            else if (Weight == AxFontWeight.Bold)
            { 
                return (FontStyle.Bold, "");
            }
            else if (Weight == AxFontWeight.SemiLight)
            {
                return (FontStyle.Regular, " SemiLight");
            }

            // Semibold
            return (FontStyle.Regular, " Semibold");

        }

        private bool TryGetStyleModifier(out FontStyle styleModifier)
        {
            styleModifier = FontStyle.Strikeout;

            if (Color == AxColor.Text)
            {
                styleModifier = FontStyle.Underline;
                return true;
            }
            else if (Color == AxColor.Ghost && (State == AxState.Hover || State == AxState.Active))
            {
                styleModifier = FontStyle.Underline;
                return true;
            }
            else if (Color == AxColor.Ghost)
            {
                styleModifier = FontStyle.Regular;
                return true;
            }

            return false;
        }

        private int GetFontSize()
        {
            // Default size: IsSize6
            double size = 16;
            
            if (Size == AxLabelSize.IsSize7)
            {
                size *= 0.75;
            }
            else if (Size == AxLabelSize.IsSize5)
            {
                size *= 1.25;
            }
            else if (Size == AxLabelSize.IsSize4)
            {
                size *= 1.50;
            }
            else if (Size == AxLabelSize.IsSize3)
            {
                size *= 2.00;
            }
            else if (Size == AxLabelSize.IsSize2)
            {
                size *= 2.5;
            }
            else if (Size == AxLabelSize.IsSize1)
            {
                size *= 3;
            }

            return (int)size;

        }

    }
}
