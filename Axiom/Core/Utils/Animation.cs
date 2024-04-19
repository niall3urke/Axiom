using System;
using System.Collections.Generic;

namespace Axiom.Core.Utils
{
    public class Animation
    {

        // ==============
        // ===== Actions
        // ==============

        public Action<float> OnChangeIncrement;

        public Action<float> OnChange;

        public Action OnComplete;

        // =============================
        // ===== Fields: backing fields
        // =============================

        private int _originalValue;

        private int _targetValue;

        private int _frameRate;

        private float _duration;

        // =============
        // ===== Fields
        // =============

        private readonly System.Windows.Forms.Timer _timer;

        private float _currentValue;

        private float _increment;

        private float _difference;

        // ===================
        // ===== Constructors
        // ===================

        public Animation(int originalValue, int targetValue, int duration = 500, int frameRate = 60)
        {
            // Init fields
            _originalValue = originalValue;
            _targetValue = targetValue;
            _frameRate = frameRate;
            _duration = duration;

            // Create the timer and its handler
            _timer = new System.Windows.Forms.Timer();
            _timer.Tick += Timer_Tick;

            // Calculate the increment etc
            SetVariables();
        }

        public Animation()
        {
            // Create the timer and its handler
            _timer = new System.Windows.Forms.Timer();
            _timer.Tick += Timer_Tick;

            // Set some defaults 
            _duration = 1000;
            _frameRate = 60;

            // Calculate the increment etc
            SetVariables();
        }

        // Events
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Get the new height
            _currentValue += _increment;

            // Update the controls height
            OnChange?.Invoke(_currentValue);
            OnChangeIncrement?.Invoke(_increment);

            // Check if we've reached the target height
            if ((_increment < 0 && _currentValue <= _targetValue) ||
                (_increment >= 0 && _currentValue >= _targetValue))
            {
                // Ensure the target height is always hit
                OnChange?.Invoke(_targetValue);
                OnChangeIncrement?.Invoke((int)_increment);

                // Finish up the animation
                _timer.Stop();
                OnComplete?.Invoke();
            }
        }

        // ======================
        // ===== Methods: public
        // ======================

        public void Start()
        {
            _timer.Start();
        }

        // =======================
        // ===== Methods: private
        // =======================

        private void SetVariables()
        {
            _difference = _targetValue - _originalValue;
            _increment = _difference / (_duration / _frameRate);
            _currentValue = _originalValue;

            _timer.Interval = 1000 / _frameRate;
        }

        // =================
        // ===== Properties
        // =================

        public int OriginalValue
        {
            get { return _originalValue; }
            set
            {
                if (SetField(ref _originalValue, value))
                {
                    SetVariables();
                }
            }
        }

        public int TargetValue
        {
            get { return _targetValue; }
            set
            {
                if (SetField(ref _targetValue, value))
                {
                    SetVariables();
                }
            }
        }

        public int FrameRate
        {
            get { return _frameRate; }
            set
            {
                if (SetField(ref _frameRate, value))
                {
                    SetVariables();
                }
            }
        }

        public float Duration
        {
            get { return _duration; }
            set
            {
                if (SetField(ref _duration, value))
                {
                    SetVariables();
                }
            }
        }

        private bool SetField<T>(ref T field, T value)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            field = value;
            return true;
        }

    }
}
