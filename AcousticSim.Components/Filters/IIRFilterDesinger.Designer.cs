namespace Envision.Components.Filters
{
    partial class IIRFilterDesinger
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IIRFilterDesinger));
            this.label1 = new System.Windows.Forms.Label();
            this.txtFc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFs = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtF0 = new System.Windows.Forms.TextBox();
            this.cmbBandType = new System.Windows.Forms.ComboBox();
            this.cmbFilterType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtOrder = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtAp = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtAs = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 202);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cut-off Frequency";
            // 
            // txtFc
            // 
            this.txtFc.Location = new System.Drawing.Point(100, 199);
            this.txtFc.Name = "txtFc";
            this.txtFc.Size = new System.Drawing.Size(67, 20);
            this.txtFc.TabIndex = 2;
            this.txtFc.Text = "10";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 176);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Sampling Rate";
            // 
            // txtFs
            // 
            this.txtFs.Location = new System.Drawing.Point(100, 173);
            this.txtFs.Name = "txtFs";
            this.txtFs.Size = new System.Drawing.Size(67, 20);
            this.txtFs.TabIndex = 2;
            this.txtFs.Text = "22100";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 228);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Center Frequency";
            // 
            // txtF0
            // 
            this.txtF0.Location = new System.Drawing.Point(100, 225);
            this.txtF0.Name = "txtF0";
            this.txtF0.Size = new System.Drawing.Size(67, 20);
            this.txtF0.TabIndex = 2;
            this.txtF0.Text = "12";
            // 
            // cmbBandType
            // 
            this.cmbBandType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBandType.FormattingEnabled = true;
            this.cmbBandType.Location = new System.Drawing.Point(100, 119);
            this.cmbBandType.Name = "cmbBandType";
            this.cmbBandType.Size = new System.Drawing.Size(121, 21);
            this.cmbBandType.TabIndex = 3;
            // 
            // cmbFilterType
            // 
            this.cmbFilterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterType.FormattingEnabled = true;
            this.cmbFilterType.Location = new System.Drawing.Point(100, 146);
            this.cmbFilterType.Name = "cmbFilterType";
            this.cmbFilterType.Size = new System.Drawing.Size(121, 21);
            this.cmbFilterType.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Band Type";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(38, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Filter Type";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(61, 254);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Order";
            // 
            // txtOrder
            // 
            this.txtOrder.Location = new System.Drawing.Point(100, 251);
            this.txtOrder.Name = "txtOrder";
            this.txtOrder.Size = new System.Drawing.Size(67, 20);
            this.txtOrder.TabIndex = 2;
            this.txtOrder.Text = "12";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(173, 228);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Hz";
            this.label8.Click += new System.EventHandler(this.label7_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(173, 202);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(20, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Hz";
            this.label9.Click += new System.EventHandler(this.label7_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(173, 176);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "samples/sec";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(165, 343);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "&OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 280);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Pass band gain";
            // 
            // txtAp
            // 
            this.txtAp.Location = new System.Drawing.Point(100, 277);
            this.txtAp.Name = "txtAp";
            this.txtAp.Size = new System.Drawing.Size(67, 20);
            this.txtAp.TabIndex = 2;
            this.txtAp.Text = "1.0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(15, 306);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Stop band gain";
            // 
            // txtAs
            // 
            this.txtAs.Location = new System.Drawing.Point(100, 303);
            this.txtAs.Name = "txtAs";
            this.txtAs.Size = new System.Drawing.Size(67, 20);
            this.txtAs.TabIndex = 2;
            this.txtAs.Text = "60.0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(173, 280);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(20, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "dB";
            this.label12.Click += new System.EventHandler(this.label7_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(173, 306);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(20, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "dB";
            this.label13.Click += new System.EventHandler(this.label7_Click);
            // 
            // label14
            // 
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label14.Location = new System.Drawing.Point(0, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(240, 107);
            this.label14.TabIndex = 5;
            this.label14.Text = resources.GetString("label14.Text");
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IIRFilterDesinger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 368);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmbFilterType);
            this.Controls.Add(this.cmbBandType);
            this.Controls.Add(this.txtFs);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAs);
            this.Controls.Add(this.txtAp);
            this.Controls.Add(this.txtOrder);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtF0);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFc);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "IIRFilterDesinger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "IIR Filter Desinger";
            this.Load += new System.EventHandler(this.IIRFilterDesinger_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        internal System.Windows.Forms.TextBox txtFc;
        internal System.Windows.Forms.TextBox txtFs;
        internal System.Windows.Forms.TextBox txtF0;
        internal System.Windows.Forms.ComboBox cmbBandType;
        internal System.Windows.Forms.ComboBox cmbFilterType;
        internal System.Windows.Forms.TextBox txtOrder;
        private System.Windows.Forms.Label label10;
        internal System.Windows.Forms.TextBox txtAp;
        private System.Windows.Forms.Label label11;
        internal System.Windows.Forms.TextBox txtAs;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
    }
}