namespace Axiom
{
    partial class Demo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Demo));
            this.PnlControls = new System.Windows.Forms.Panel();
            this.CmbShadow = new Axiom.Controls.Select.AxSelect();
            this.axBox4 = new Axiom.Controls.Box.AxBox();
            this.axImage3 = new Axiom.Controls.Image.AxImage();
            this.axBox3 = new Axiom.Controls.Box.AxBox();
            this.axRadioButton2 = new Axiom.Controls.AxRadioButton();
            this.axLabel4 = new Axiom.Controls.Label.AxLabel();
            this.axLabel3 = new Axiom.Controls.Label.AxLabel();
            this.axBox2 = new Axiom.Controls.Box.AxBox();
            this.axLabel2 = new Axiom.Controls.Label.AxLabel();
            this.axLabel1 = new Axiom.Controls.Label.AxLabel();
            this.axImage2 = new Axiom.Controls.Image.AxImage();
            this.axSelect1 = new Axiom.Controls.Select.AxSelect();
            this.axButton1 = new Axiom.Controls.AxButton();
            this.axRadioButton1 = new Axiom.Controls.AxRadioButton();
            this.axCheckbox1 = new Axiom.Controls.Checkbox.AxCheckbox();
            this.axSwitch1 = new Axiom.Controls.Switch.AxSwitch();
            this.axInput1 = new Axiom.Controls.Input.AxInput();
            this.CmbColor = new Axiom.Controls.Select.AxSelect();
            this.CmbShape = new Axiom.Controls.Select.AxSelect();
            this.TogLoading = new Axiom.Controls.Switch.AxSwitch();
            this.TogEnable = new Axiom.Controls.Switch.AxSwitch();
            this.TogInvert = new Axiom.Controls.Switch.AxSwitch();
            this.TogLight = new Axiom.Controls.Switch.AxSwitch();
            this.TogOutline = new Axiom.Controls.Switch.AxSwitch();
            this.TogRound = new Axiom.Controls.Switch.AxSwitch();
            this.axLabel5 = new Axiom.Controls.Label.AxLabel();
            this.PnlControls.SuspendLayout();
            this.axBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axImage3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axImage2)).BeginInit();
            this.SuspendLayout();
            // 
            // PnlControls
            // 
            this.PnlControls.BackColor = System.Drawing.Color.WhiteSmoke;
            this.PnlControls.Controls.Add(this.axLabel5);
            this.PnlControls.Controls.Add(this.axBox4);
            this.PnlControls.Controls.Add(this.axBox3);
            this.PnlControls.Controls.Add(this.axRadioButton2);
            this.PnlControls.Controls.Add(this.axLabel4);
            this.PnlControls.Controls.Add(this.axLabel3);
            this.PnlControls.Controls.Add(this.axBox2);
            this.PnlControls.Controls.Add(this.axLabel2);
            this.PnlControls.Controls.Add(this.axLabel1);
            this.PnlControls.Controls.Add(this.axImage2);
            this.PnlControls.Controls.Add(this.axSelect1);
            this.PnlControls.Controls.Add(this.axButton1);
            this.PnlControls.Controls.Add(this.axRadioButton1);
            this.PnlControls.Controls.Add(this.axCheckbox1);
            this.PnlControls.Controls.Add(this.axSwitch1);
            this.PnlControls.Controls.Add(this.axInput1);
            this.PnlControls.Location = new System.Drawing.Point(12, 147);
            this.PnlControls.Name = "PnlControls";
            this.PnlControls.Size = new System.Drawing.Size(1040, 529);
            this.PnlControls.TabIndex = 30;
            // 
            // CmbShadow
            // 
            this.CmbShadow.Color = Axiom.Core.AxColor.Default;
            this.CmbShadow.IsInverted = false;
            this.CmbShadow.IsLight = false;
            this.CmbShadow.IsOutlined = false;
            this.CmbShadow.IsRounded = false;
            this.CmbShadow.Items = ((System.Collections.Generic.List<string>)(resources.GetObject("CmbShadow.Items")));
            this.CmbShadow.Location = new System.Drawing.Point(594, 12);
            this.CmbShadow.Name = "CmbShadow";
            this.CmbShadow.SelectedIndex = 0;
            this.CmbShadow.SelectedItem = null;
            this.CmbShadow.Shape = Axiom.Core.AxShape.Normal;
            this.CmbShadow.Size = new System.Drawing.Size(180, 47);
            this.CmbShadow.State = Axiom.Core.AxState.Normal;
            this.CmbShadow.TabIndex = 33;
            this.CmbShadow.Text = "axSelect1";
            this.CmbShadow.SelectedIndexChanged += new System.EventHandler(this.CmbShadow_SelectedIndexChanged);
            // 
            // axBox4
            // 
            this.axBox4.Color = Axiom.Core.AxColor.White;
            this.axBox4.Controls.Add(this.axImage3);
            this.axBox4.CurveBtmLhs = true;
            this.axBox4.CurveBtmRhs = true;
            this.axBox4.CurveTopLhs = true;
            this.axBox4.CurveTopRhs = true;
            this.axBox4.HasShadow = true;
            this.axBox4.IsInverted = false;
            this.axBox4.IsLight = false;
            this.axBox4.IsOutlined = false;
            this.axBox4.IsRounded = true;
            this.axBox4.Location = new System.Drawing.Point(653, 172);
            this.axBox4.Name = "axBox4";
            this.axBox4.Padding = new System.Windows.Forms.Padding(0, 0, 12, 12);
            this.axBox4.ShadowBlur = 0.5F;
            this.axBox4.ShadowDepth = 6;
            this.axBox4.ShadowDirection = Axiom.Core.AxShadowDirection.BottomRight;
            this.axBox4.Shape = Axiom.Core.AxShape.Large;
            this.axBox4.Size = new System.Drawing.Size(267, 334);
            this.axBox4.State = Axiom.Core.AxState.Normal;
            this.axBox4.TabIndex = 165;
            // 
            // axImage3
            // 
            this.axImage3.Aspect = Axiom.Controls.Image.AxImage.AspectRatio.Free;
            this.axImage3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.axImage3.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.axImage3.Color = Axiom.Core.AxColor.Default;
            this.axImage3.CurveBtmLhs = false;
            this.axImage3.CurveBtmRhs = false;
            this.axImage3.CurveTopLhs = true;
            this.axImage3.CurveTopRhs = true;
            this.axImage3.Dock = System.Windows.Forms.DockStyle.Top;
            this.axImage3.Image = ((System.Drawing.Image)(resources.GetObject("axImage3.Image")));
            this.axImage3.IsCircle = false;
            this.axImage3.IsClickable = true;
            this.axImage3.IsInverted = false;
            this.axImage3.IsLight = false;
            this.axImage3.IsOutlined = false;
            this.axImage3.IsRounded = true;
            this.axImage3.Location = new System.Drawing.Point(0, 0);
            this.axImage3.Name = "axImage3";
            this.axImage3.Shape = Axiom.Core.AxShape.Large;
            this.axImage3.Size = new System.Drawing.Size(255, 165);
            this.axImage3.State = Axiom.Core.AxState.Normal;
            this.axImage3.TabIndex = 81;
            this.axImage3.TabStop = false;
            // 
            // axBox3
            // 
            this.axBox3.BackColor = System.Drawing.Color.Transparent;
            this.axBox3.Color = Axiom.Core.AxColor.White;
            this.axBox3.CurveBtmLhs = true;
            this.axBox3.CurveBtmRhs = true;
            this.axBox3.CurveTopLhs = true;
            this.axBox3.CurveTopRhs = true;
            this.axBox3.HasShadow = true;
            this.axBox3.IsInverted = false;
            this.axBox3.IsLight = false;
            this.axBox3.IsOutlined = false;
            this.axBox3.IsRounded = true;
            this.axBox3.Location = new System.Drawing.Point(485, 172);
            this.axBox3.Name = "axBox3";
            this.axBox3.Padding = new System.Windows.Forms.Padding(0, 0, 12, 12);
            this.axBox3.ShadowBlur = 0.5F;
            this.axBox3.ShadowDepth = 6;
            this.axBox3.ShadowDirection = Axiom.Core.AxShadowDirection.BottomRight;
            this.axBox3.Shape = Axiom.Core.AxShape.Large;
            this.axBox3.Size = new System.Drawing.Size(162, 162);
            this.axBox3.State = Axiom.Core.AxState.Normal;
            this.axBox3.TabIndex = 145;
            // 
            // axRadioButton2
            // 
            this.axRadioButton2.Checked = false;
            this.axRadioButton2.Color = Axiom.Core.AxColor.Default;
            this.axRadioButton2.IsInverted = false;
            this.axRadioButton2.IsLight = false;
            this.axRadioButton2.IsOutlined = true;
            this.axRadioButton2.IsRounded = false;
            this.axRadioButton2.Location = new System.Drawing.Point(144, 69);
            this.axRadioButton2.Name = "axRadioButton2";
            this.axRadioButton2.Shape = Axiom.Core.AxShape.Normal;
            this.axRadioButton2.Size = new System.Drawing.Size(149, 47);
            this.axRadioButton2.State = Axiom.Core.AxState.Normal;
            this.axRadioButton2.TabIndex = 129;
            this.axRadioButton2.Text = "axRadioButton2";
            // 
            // axLabel4
            // 
            this.axLabel4.AutoSize = true;
            this.axLabel4.Color = Axiom.Core.AxColor.Primary;
            this.axLabel4.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.axLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(209)))), ((int)(((byte)(178)))));
            this.axLabel4.IsClickable = false;
            this.axLabel4.IsInverted = false;
            this.axLabel4.IsLight = false;
            this.axLabel4.IsOutlined = false;
            this.axLabel4.IsRounded = false;
            this.axLabel4.LabelSize = Axiom.Controls.Label.AxLabelSize.IsSize6;
            this.axLabel4.Location = new System.Drawing.Point(649, 148);
            this.axLabel4.Name = "axLabel4";
            this.axLabel4.Shape = Axiom.Core.AxShape.Normal;
            this.axLabel4.Size = new System.Drawing.Size(319, 21);
            this.axLabel4.State = Axiom.Core.AxState.Normal;
            this.axLabel4.TabIndex = 93;
            this.axLabel4.Text = "Using box and image to create a card control";
            this.axLabel4.Weight = Axiom.Controls.Label.AxFontWeight.Normal;
            // 
            // axLabel3
            // 
            this.axLabel3.AutoSize = true;
            this.axLabel3.Color = Axiom.Core.AxColor.Primary;
            this.axLabel3.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.axLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(209)))), ((int)(((byte)(178)))));
            this.axLabel3.IsClickable = false;
            this.axLabel3.IsInverted = false;
            this.axLabel3.IsLight = false;
            this.axLabel3.IsOutlined = false;
            this.axLabel3.IsRounded = false;
            this.axLabel3.LabelSize = Axiom.Controls.Label.AxLabelSize.IsSize6;
            this.axLabel3.Location = new System.Drawing.Point(322, 148);
            this.axLabel3.Name = "axLabel3";
            this.axLabel3.Shape = Axiom.Core.AxShape.Normal;
            this.axLabel3.Size = new System.Drawing.Size(108, 21);
            this.axLabel3.State = Axiom.Core.AxState.Normal;
            this.axLabel3.TabIndex = 92;
            this.axLabel3.Text = "AxBox Control";
            this.axLabel3.Weight = Axiom.Controls.Label.AxFontWeight.Normal;
            // 
            // axBox2
            // 
            this.axBox2.Color = Axiom.Core.AxColor.White;
            this.axBox2.CurveBtmLhs = true;
            this.axBox2.CurveBtmRhs = true;
            this.axBox2.CurveTopLhs = true;
            this.axBox2.CurveTopRhs = true;
            this.axBox2.HasShadow = false;
            this.axBox2.IsInverted = false;
            this.axBox2.IsLight = false;
            this.axBox2.IsOutlined = false;
            this.axBox2.IsRounded = true;
            this.axBox2.Location = new System.Drawing.Point(326, 172);
            this.axBox2.Name = "axBox2";
            this.axBox2.ShadowBlur = 0.45F;
            this.axBox2.ShadowDepth = 5;
            this.axBox2.ShadowDirection = Axiom.Core.AxShadowDirection.Centered;
            this.axBox2.Shape = Axiom.Core.AxShape.Large;
            this.axBox2.Size = new System.Drawing.Size(150, 150);
            this.axBox2.State = Axiom.Core.AxState.Normal;
            this.axBox2.TabIndex = 91;
            // 
            // axLabel2
            // 
            this.axLabel2.AutoSize = true;
            this.axLabel2.Color = Axiom.Core.AxColor.Primary;
            this.axLabel2.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.axLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(209)))), ((int)(((byte)(178)))));
            this.axLabel2.IsClickable = false;
            this.axLabel2.IsInverted = false;
            this.axLabel2.IsLight = false;
            this.axLabel2.IsOutlined = false;
            this.axLabel2.IsRounded = false;
            this.axLabel2.LabelSize = Axiom.Controls.Label.AxLabelSize.IsSize6;
            this.axLabel2.Location = new System.Drawing.Point(140, 148);
            this.axLabel2.Name = "axLabel2";
            this.axLabel2.Shape = Axiom.Core.AxShape.Normal;
            this.axLabel2.Size = new System.Drawing.Size(126, 21);
            this.axLabel2.State = Axiom.Core.AxState.Normal;
            this.axLabel2.TabIndex = 90;
            this.axLabel2.Text = "AxImage Control";
            this.axLabel2.Weight = Axiom.Controls.Label.AxFontWeight.Normal;
            // 
            // axLabel1
            // 
            this.axLabel1.AutoSize = true;
            this.axLabel1.Color = Axiom.Core.AxColor.Primary;
            this.axLabel1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.axLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(209)))), ((int)(((byte)(178)))));
            this.axLabel1.IsClickable = false;
            this.axLabel1.IsInverted = false;
            this.axLabel1.IsLight = false;
            this.axLabel1.IsOutlined = false;
            this.axLabel1.IsRounded = false;
            this.axLabel1.LabelSize = Axiom.Controls.Label.AxLabelSize.IsSize6;
            this.axLabel1.Location = new System.Drawing.Point(27, 148);
            this.axLabel1.Name = "axLabel1";
            this.axLabel1.Shape = Axiom.Core.AxShape.Normal;
            this.axLabel1.Size = new System.Drawing.Size(71, 21);
            this.axLabel1.State = Axiom.Core.AxState.Normal;
            this.axLabel1.TabIndex = 89;
            this.axLabel1.Text = "axLabel1";
            this.axLabel1.Weight = Axiom.Controls.Label.AxFontWeight.Normal;
            // 
            // axImage2
            // 
            this.axImage2.Aspect = Axiom.Controls.Image.AxImage.AspectRatio.Free;
            this.axImage2.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.axImage2.Color = Axiom.Core.AxColor.Default;
            this.axImage2.CurveBtmLhs = true;
            this.axImage2.CurveBtmRhs = true;
            this.axImage2.CurveTopLhs = true;
            this.axImage2.CurveTopRhs = true;
            this.axImage2.Image = ((System.Drawing.Image)(resources.GetObject("axImage2.Image")));
            this.axImage2.IsCircle = false;
            this.axImage2.IsClickable = false;
            this.axImage2.IsInverted = false;
            this.axImage2.IsLight = false;
            this.axImage2.IsOutlined = false;
            this.axImage2.IsRounded = true;
            this.axImage2.Location = new System.Drawing.Point(144, 172);
            this.axImage2.Name = "axImage2";
            this.axImage2.Shape = Axiom.Core.AxShape.Large;
            this.axImage2.Size = new System.Drawing.Size(150, 150);
            this.axImage2.State = Axiom.Core.AxState.Normal;
            this.axImage2.TabIndex = 87;
            this.axImage2.TabStop = false;
            // 
            // axSelect1
            // 
            this.axSelect1.Color = Axiom.Core.AxColor.Default;
            this.axSelect1.IsInverted = false;
            this.axSelect1.IsLight = false;
            this.axSelect1.IsOutlined = false;
            this.axSelect1.IsRounded = false;
            this.axSelect1.Items = ((System.Collections.Generic.List<string>)(resources.GetObject("axSelect1.Items")));
            this.axSelect1.Location = new System.Drawing.Point(817, 16);
            this.axSelect1.Name = "axSelect1";
            this.axSelect1.SelectedIndex = 0;
            this.axSelect1.SelectedItem = null;
            this.axSelect1.Shape = Axiom.Core.AxShape.Normal;
            this.axSelect1.Size = new System.Drawing.Size(180, 47);
            this.axSelect1.State = Axiom.Core.AxState.Normal;
            this.axSelect1.TabIndex = 28;
            this.axSelect1.Text = "axSelect1";
            // 
            // axButton1
            // 
            this.axButton1.Color = Axiom.Core.AxColor.Primary;
            this.axButton1.CurveBtmLhs = true;
            this.axButton1.CurveBtmRhs = true;
            this.axButton1.CurveTopLhs = true;
            this.axButton1.CurveTopRhs = true;
            this.axButton1.IsInverted = false;
            this.axButton1.IsLight = false;
            this.axButton1.IsOutlined = false;
            this.axButton1.IsRounded = false;
            this.axButton1.IsStatic = false;
            this.axButton1.Location = new System.Drawing.Point(13, 16);
            this.axButton1.Name = "axButton1";
            this.axButton1.Shape = Axiom.Core.AxShape.Normal;
            this.axButton1.Size = new System.Drawing.Size(102, 47);
            this.axButton1.State = Axiom.Core.AxState.Normal;
            this.axButton1.TabIndex = 13;
            this.axButton1.Text = "axButton1";
            // 
            // axRadioButton1
            // 
            this.axRadioButton1.Checked = true;
            this.axRadioButton1.Color = Axiom.Core.AxColor.Default;
            this.axRadioButton1.IsInverted = false;
            this.axRadioButton1.IsLight = false;
            this.axRadioButton1.IsOutlined = true;
            this.axRadioButton1.IsRounded = false;
            this.axRadioButton1.Location = new System.Drawing.Point(144, 16);
            this.axRadioButton1.Name = "axRadioButton1";
            this.axRadioButton1.Shape = Axiom.Core.AxShape.Normal;
            this.axRadioButton1.Size = new System.Drawing.Size(149, 47);
            this.axRadioButton1.State = Axiom.Core.AxState.Active;
            this.axRadioButton1.TabIndex = 14;
            this.axRadioButton1.Text = "axRadioButton1";
            // 
            // axCheckbox1
            // 
            this.axCheckbox1.Checked = false;
            this.axCheckbox1.Color = Axiom.Core.AxColor.Default;
            this.axCheckbox1.IsInverted = false;
            this.axCheckbox1.IsLight = false;
            this.axCheckbox1.IsOutlined = true;
            this.axCheckbox1.IsRounded = false;
            this.axCheckbox1.Location = new System.Drawing.Point(326, 16);
            this.axCheckbox1.Name = "axCheckbox1";
            this.axCheckbox1.Shape = Axiom.Core.AxShape.Normal;
            this.axCheckbox1.Size = new System.Drawing.Size(130, 47);
            this.axCheckbox1.State = Axiom.Core.AxState.Normal;
            this.axCheckbox1.TabIndex = 17;
            this.axCheckbox1.Text = "axCheckbox1";
            // 
            // axSwitch1
            // 
            this.axSwitch1.Checked = false;
            this.axSwitch1.Color = Axiom.Core.AxColor.Primary;
            this.axSwitch1.IsInverted = false;
            this.axSwitch1.IsLight = false;
            this.axSwitch1.IsOutlined = false;
            this.axSwitch1.IsRounded = true;
            this.axSwitch1.IsThin = true;
            this.axSwitch1.Location = new System.Drawing.Point(485, 16);
            this.axSwitch1.Name = "axSwitch1";
            this.axSwitch1.Shape = Axiom.Core.AxShape.Normal;
            this.axSwitch1.Size = new System.Drawing.Size(138, 47);
            this.axSwitch1.State = Axiom.Core.AxState.Normal;
            this.axSwitch1.TabIndex = 21;
            this.axSwitch1.Text = "axSwitch1";
            // 
            // axInput1
            // 
            this.axInput1.BackColor = System.Drawing.Color.Transparent;
            this.axInput1.Color = Axiom.Core.AxColor.Default;
            this.axInput1.CurveBtmLhs = true;
            this.axInput1.CurveBtmRhs = true;
            this.axInput1.CurveTopLhs = true;
            this.axInput1.CurveTopRhs = true;
            this.axInput1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.axInput1.IsInverted = false;
            this.axInput1.IsLight = false;
            this.axInput1.IsOutlined = false;
            this.axInput1.IsRounded = false;
            this.axInput1.Location = new System.Drawing.Point(661, 16);
            this.axInput1.Name = "axInput1";
            this.axInput1.Padding = new System.Windows.Forms.Padding(17, 7, 17, 7);
            this.axInput1.Placeholder = "Your text";
            this.axInput1.Shape = Axiom.Core.AxShape.Normal;
            this.axInput1.Size = new System.Drawing.Size(150, 47);
            this.axInput1.State = Axiom.Core.AxState.Normal;
            this.axInput1.TabIndex = 20;
            // 
            // CmbColor
            // 
            this.CmbColor.Color = Axiom.Core.AxColor.Default;
            this.CmbColor.IsInverted = false;
            this.CmbColor.IsLight = false;
            this.CmbColor.IsOutlined = false;
            this.CmbColor.IsRounded = false;
            this.CmbColor.Items = ((System.Collections.Generic.List<string>)(resources.GetObject("CmbColor.Items")));
            this.CmbColor.Location = new System.Drawing.Point(408, 65);
            this.CmbColor.Name = "CmbColor";
            this.CmbColor.SelectedIndex = 0;
            this.CmbColor.SelectedItem = null;
            this.CmbColor.Shape = Axiom.Core.AxShape.Normal;
            this.CmbColor.Size = new System.Drawing.Size(180, 47);
            this.CmbColor.State = Axiom.Core.AxState.Normal;
            this.CmbColor.TabIndex = 11;
            this.CmbColor.Text = "axSelect2";
            this.CmbColor.SelectedIndexChanged += new System.EventHandler(this.CmbColor_SelectedIndexChanged);
            // 
            // CmbShape
            // 
            this.CmbShape.Color = Axiom.Core.AxColor.Default;
            this.CmbShape.IsInverted = false;
            this.CmbShape.IsLight = false;
            this.CmbShape.IsOutlined = false;
            this.CmbShape.IsRounded = false;
            this.CmbShape.Items = ((System.Collections.Generic.List<string>)(resources.GetObject("CmbShape.Items")));
            this.CmbShape.Location = new System.Drawing.Point(408, 12);
            this.CmbShape.Name = "CmbShape";
            this.CmbShape.SelectedIndex = 0;
            this.CmbShape.SelectedItem = null;
            this.CmbShape.Shape = Axiom.Core.AxShape.Normal;
            this.CmbShape.Size = new System.Drawing.Size(180, 47);
            this.CmbShape.State = Axiom.Core.AxState.Normal;
            this.CmbShape.TabIndex = 9;
            this.CmbShape.Text = "axSelect1";
            this.CmbShape.SelectedIndexChanged += new System.EventHandler(this.CmbShape_SelectedIndexChanged);
            // 
            // TogLoading
            // 
            this.TogLoading.Checked = false;
            this.TogLoading.Color = Axiom.Core.AxColor.Primary;
            this.TogLoading.IsInverted = false;
            this.TogLoading.IsLight = false;
            this.TogLoading.IsOutlined = false;
            this.TogLoading.IsRounded = true;
            this.TogLoading.IsThin = true;
            this.TogLoading.Location = new System.Drawing.Point(262, 65);
            this.TogLoading.Name = "TogLoading";
            this.TogLoading.Shape = Axiom.Core.AxShape.Normal;
            this.TogLoading.Size = new System.Drawing.Size(125, 47);
            this.TogLoading.State = Axiom.Core.AxState.Normal;
            this.TogLoading.TabIndex = 8;
            this.TogLoading.Text = "Loading";
            this.TogLoading.CheckedChanged += new System.EventHandler(this.TogLoading_CheckedChanged);
            // 
            // TogEnable
            // 
            this.TogEnable.Checked = false;
            this.TogEnable.Color = Axiom.Core.AxColor.Primary;
            this.TogEnable.IsInverted = false;
            this.TogEnable.IsLight = false;
            this.TogEnable.IsOutlined = false;
            this.TogEnable.IsRounded = true;
            this.TogEnable.IsThin = true;
            this.TogEnable.Location = new System.Drawing.Point(262, 12);
            this.TogEnable.Name = "TogEnable";
            this.TogEnable.Shape = Axiom.Core.AxShape.Normal;
            this.TogEnable.Size = new System.Drawing.Size(115, 47);
            this.TogEnable.State = Axiom.Core.AxState.Normal;
            this.TogEnable.TabIndex = 7;
            this.TogEnable.Text = "Enable";
            this.TogEnable.CheckedChanged += new System.EventHandler(this.TogEnable_CheckedChanged);
            // 
            // TogInvert
            // 
            this.TogInvert.Checked = false;
            this.TogInvert.Color = Axiom.Core.AxColor.Primary;
            this.TogInvert.IsInverted = false;
            this.TogInvert.IsLight = false;
            this.TogInvert.IsOutlined = false;
            this.TogInvert.IsRounded = true;
            this.TogInvert.IsThin = true;
            this.TogInvert.Location = new System.Drawing.Point(143, 65);
            this.TogInvert.Name = "TogInvert";
            this.TogInvert.Shape = Axiom.Core.AxShape.Normal;
            this.TogInvert.Size = new System.Drawing.Size(108, 47);
            this.TogInvert.State = Axiom.Core.AxState.Normal;
            this.TogInvert.TabIndex = 6;
            this.TogInvert.Text = "Invert";
            this.TogInvert.CheckedChanged += new System.EventHandler(this.TogInvert_CheckedChanged);
            // 
            // TogLight
            // 
            this.TogLight.Checked = false;
            this.TogLight.Color = Axiom.Core.AxColor.Primary;
            this.TogLight.IsInverted = false;
            this.TogLight.IsLight = false;
            this.TogLight.IsOutlined = false;
            this.TogLight.IsRounded = true;
            this.TogLight.IsThin = true;
            this.TogLight.Location = new System.Drawing.Point(143, 12);
            this.TogLight.Name = "TogLight";
            this.TogLight.Shape = Axiom.Core.AxShape.Normal;
            this.TogLight.Size = new System.Drawing.Size(103, 47);
            this.TogLight.State = Axiom.Core.AxState.Normal;
            this.TogLight.TabIndex = 5;
            this.TogLight.Text = "Light";
            this.TogLight.CheckedChanged += new System.EventHandler(this.TogLight_CheckedChanged);
            // 
            // TogOutline
            // 
            this.TogOutline.Checked = false;
            this.TogOutline.Color = Axiom.Core.AxColor.Primary;
            this.TogOutline.IsInverted = false;
            this.TogOutline.IsLight = false;
            this.TogOutline.IsOutlined = false;
            this.TogOutline.IsRounded = true;
            this.TogOutline.IsThin = true;
            this.TogOutline.Location = new System.Drawing.Point(12, 65);
            this.TogOutline.Name = "TogOutline";
            this.TogOutline.Shape = Axiom.Core.AxShape.Normal;
            this.TogOutline.Size = new System.Drawing.Size(120, 47);
            this.TogOutline.State = Axiom.Core.AxState.Normal;
            this.TogOutline.TabIndex = 4;
            this.TogOutline.Text = "Outline";
            this.TogOutline.CheckedChanged += new System.EventHandler(this.TogOutline_CheckedChanged);
            // 
            // TogRound
            // 
            this.TogRound.Checked = false;
            this.TogRound.Color = Axiom.Core.AxColor.Primary;
            this.TogRound.IsInverted = false;
            this.TogRound.IsLight = false;
            this.TogRound.IsOutlined = false;
            this.TogRound.IsRounded = true;
            this.TogRound.IsThin = true;
            this.TogRound.Location = new System.Drawing.Point(12, 12);
            this.TogRound.Name = "TogRound";
            this.TogRound.Shape = Axiom.Core.AxShape.Normal;
            this.TogRound.Size = new System.Drawing.Size(114, 47);
            this.TogRound.State = Axiom.Core.AxState.Normal;
            this.TogRound.TabIndex = 3;
            this.TogRound.Text = "Round";
            this.TogRound.CheckedChanged += new System.EventHandler(this.TogRound_CheckedChanged);
            // 
            // axLabel5
            // 
            this.axLabel5.AutoSize = true;
            this.axLabel5.Color = Axiom.Core.AxColor.Primary;
            this.axLabel5.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.axLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(209)))), ((int)(((byte)(178)))));
            this.axLabel5.IsClickable = false;
            this.axLabel5.IsInverted = false;
            this.axLabel5.IsLight = false;
            this.axLabel5.IsOutlined = false;
            this.axLabel5.IsRounded = false;
            this.axLabel5.LabelSize = Axiom.Controls.Label.AxLabelSize.IsSize6;
            this.axLabel5.Location = new System.Drawing.Point(481, 148);
            this.axLabel5.Name = "axLabel5";
            this.axLabel5.Shape = Axiom.Core.AxShape.Normal;
            this.axLabel5.Size = new System.Drawing.Size(144, 21);
            this.axLabel5.State = Axiom.Core.AxState.Normal;
            this.axLabel5.TabIndex = 168;
            this.axLabel5.Text = "AxBox with shadow";
            this.axLabel5.Weight = Axiom.Controls.Label.AxFontWeight.Normal;
            // 
            // Demo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1074, 702);
            this.Controls.Add(this.CmbShadow);
            this.Controls.Add(this.PnlControls);
            this.Controls.Add(this.CmbColor);
            this.Controls.Add(this.CmbShape);
            this.Controls.Add(this.TogLoading);
            this.Controls.Add(this.TogEnable);
            this.Controls.Add(this.TogInvert);
            this.Controls.Add(this.TogLight);
            this.Controls.Add(this.TogOutline);
            this.Controls.Add(this.TogRound);
            this.Name = "Demo";
            this.Text = "Demo";
            this.Load += new System.EventHandler(this.Demo_Load);
            this.PnlControls.ResumeLayout(false);
            this.PnlControls.PerformLayout();
            this.axBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axImage3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axImage2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.Switch.AxSwitch TogRound;
        private Controls.Switch.AxSwitch TogOutline;
        private Controls.Switch.AxSwitch TogLight;
        private Controls.Switch.AxSwitch TogInvert;
        private Controls.Switch.AxSwitch TogEnable;
        private Controls.Switch.AxSwitch TogLoading;
        private Controls.Select.AxSelect CmbShape;
        private Controls.Select.AxSelect CmbColor;
        private Controls.AxButton axButton1;
        private Controls.AxRadioButton axRadioButton1;
        private Controls.Checkbox.AxCheckbox axCheckbox1;
        private Controls.Input.AxInput axInput1;
        private Controls.Switch.AxSwitch axSwitch1;
        private System.Windows.Forms.Panel PnlControls;
        private Controls.Select.AxSelect axSelect1;
        private Controls.Image.AxImage axImage2;
        private Controls.Label.AxLabel axLabel1;
        private Controls.Label.AxLabel axLabel4;
        private Controls.Label.AxLabel axLabel3;
        private Controls.Box.AxBox axBox2;
        private Controls.Label.AxLabel axLabel2;
        private Controls.AxRadioButton axRadioButton2;
        private Controls.Box.AxBox axBox3;
        private Controls.Select.AxSelect CmbShadow;
        private Controls.Box.AxBox axBox4;
        private Controls.Image.AxImage axImage3;
        private Controls.Label.AxLabel axLabel5;
    }
}