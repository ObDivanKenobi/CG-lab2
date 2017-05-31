namespace lab2
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.groupBoxLight = new System.Windows.Forms.GroupBox();
            this.buttonAccept = new System.Windows.Forms.Button();
            this.textBoxLightZ = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxLightY = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxLightX = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.trackBarRadius = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.trackBarHeight = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.trackBarPhi = new System.Windows.Forms.TrackBar();
            this.label7 = new System.Windows.Forms.Label();
            this.trackBarPsi = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.groupBoxLight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPhi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPsi)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Location = new System.Drawing.Point(12, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(388, 348);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // groupBoxLight
            // 
            this.groupBoxLight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxLight.Controls.Add(this.buttonAccept);
            this.groupBoxLight.Controls.Add(this.textBoxLightZ);
            this.groupBoxLight.Controls.Add(this.label3);
            this.groupBoxLight.Controls.Add(this.textBoxLightY);
            this.groupBoxLight.Controls.Add(this.label2);
            this.groupBoxLight.Controls.Add(this.textBoxLightX);
            this.groupBoxLight.Controls.Add(this.label1);
            this.groupBoxLight.Location = new System.Drawing.Point(406, 12);
            this.groupBoxLight.Name = "groupBoxLight";
            this.groupBoxLight.Size = new System.Drawing.Size(202, 134);
            this.groupBoxLight.TabIndex = 1;
            this.groupBoxLight.TabStop = false;
            this.groupBoxLight.Text = "Освещение";
            // 
            // buttonAccept
            // 
            this.buttonAccept.Location = new System.Drawing.Point(63, 100);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(75, 23);
            this.buttonAccept.TabIndex = 2;
            this.buttonAccept.Text = "Ок";
            this.buttonAccept.UseVisualStyleBackColor = true;
            this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click);
            // 
            // textBoxLightZ
            // 
            this.textBoxLightZ.Location = new System.Drawing.Point(27, 74);
            this.textBoxLightZ.Name = "textBoxLightZ";
            this.textBoxLightZ.Size = new System.Drawing.Size(169, 20);
            this.textBoxLightZ.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "z:";
            // 
            // textBoxLightY
            // 
            this.textBoxLightY.Location = new System.Drawing.Point(27, 48);
            this.textBoxLightY.Name = "textBoxLightY";
            this.textBoxLightY.Size = new System.Drawing.Size(169, 20);
            this.textBoxLightY.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "y:";
            // 
            // textBoxLightX
            // 
            this.textBoxLightX.Location = new System.Drawing.Point(27, 22);
            this.textBoxLightX.Name = "textBoxLightX";
            this.textBoxLightX.Size = new System.Drawing.Size(169, 20);
            this.textBoxLightX.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "x:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(414, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Радиус";
            // 
            // trackBarRadius
            // 
            this.trackBarRadius.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarRadius.Location = new System.Drawing.Point(415, 179);
            this.trackBarRadius.Maximum = 9;
            this.trackBarRadius.Minimum = 1;
            this.trackBarRadius.Name = "trackBarRadius";
            this.trackBarRadius.Size = new System.Drawing.Size(193, 45);
            this.trackBarRadius.TabIndex = 3;
            this.trackBarRadius.Value = 9;
            this.trackBarRadius.ValueChanged += new System.EventHandler(this.trackBarRadius_ValueChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(414, 213);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Высота";
            // 
            // trackBarHeight
            // 
            this.trackBarHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarHeight.Location = new System.Drawing.Point(415, 230);
            this.trackBarHeight.Maximum = 20;
            this.trackBarHeight.Minimum = 2;
            this.trackBarHeight.Name = "trackBarHeight";
            this.trackBarHeight.Size = new System.Drawing.Size(193, 45);
            this.trackBarHeight.TabIndex = 3;
            this.trackBarHeight.Value = 10;
            this.trackBarHeight.ValueChanged += new System.EventHandler(this.trackBarHeight_ValueChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(414, 264);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Горизонтальный угол";
            // 
            // trackBarPhi
            // 
            this.trackBarPhi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarPhi.Location = new System.Drawing.Point(415, 281);
            this.trackBarPhi.Maximum = 18;
            this.trackBarPhi.Minimum = -18;
            this.trackBarPhi.Name = "trackBarPhi";
            this.trackBarPhi.Size = new System.Drawing.Size(193, 45);
            this.trackBarPhi.TabIndex = 3;
            this.trackBarPhi.ValueChanged += new System.EventHandler(this.OnAngleChange);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(414, 315);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Вертикальный угол";
            // 
            // trackBarPsi
            // 
            this.trackBarPsi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarPsi.Location = new System.Drawing.Point(415, 332);
            this.trackBarPsi.Maximum = 18;
            this.trackBarPsi.Minimum = -18;
            this.trackBarPsi.Name = "trackBarPsi";
            this.trackBarPsi.Size = new System.Drawing.Size(193, 45);
            this.trackBarPsi.TabIndex = 3;
            this.trackBarPsi.ValueChanged += new System.EventHandler(this.OnAngleChange);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 372);
            this.Controls.Add(this.trackBarPsi);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.trackBarPhi);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.trackBarHeight);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.trackBarRadius);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBoxLight);
            this.Controls.Add(this.pictureBox);
            this.MinimumSize = new System.Drawing.Size(636, 411);
            this.Name = "Form1";
            this.Text = "Вертикальный угол";
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.groupBoxLight.ResumeLayout(false);
            this.groupBoxLight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPhi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPsi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.GroupBox groupBoxLight;
        private System.Windows.Forms.Button buttonAccept;
        private System.Windows.Forms.TextBox textBoxLightZ;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxLightY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxLightX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar trackBarRadius;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar trackBarHeight;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar trackBarPhi;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TrackBar trackBarPsi;
    }
}

