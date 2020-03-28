namespace Envision.Components.Basic
{
    partial class PyCodeBlockConfigure
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
            this.chkAllowInput = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbIPNos = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkAllowOutput = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbOutputCount = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkAllowInput
            // 
            this.chkAllowInput.AutoSize = true;
            this.chkAllowInput.Checked = true;
            this.chkAllowInput.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllowInput.Location = new System.Drawing.Point(9, 124);
            this.chkAllowInput.Name = "chkAllowInput";
            this.chkAllowInput.Size = new System.Drawing.Size(83, 17);
            this.chkAllowInput.TabIndex = 2;
            this.chkAllowInput.Text = "Allow Inputs";
            this.chkAllowInput.UseVisualStyleBackColor = true;
            this.chkAllowInput.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbIPNos);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(8, 139);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(237, 89);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // cmbIPNos
            // 
            this.cmbIPNos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIPNos.FormattingEnabled = true;
            this.cmbIPNos.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cmbIPNos.Location = new System.Drawing.Point(101, 13);
            this.cmbIPNos.Name = "cmbIPNos";
            this.cmbIPNos.Size = new System.Drawing.Size(75, 21);
            this.cmbIPNos.TabIndex = 2;
            this.cmbIPNos.SelectedIndexChanged += new System.EventHandler(this.cmbIPNos_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(225, 49);
            this.label3.TabIndex = 1;
            this.label3.Text = "Inputs can be accessed by using variable names in1, in2, in3, in4, in5 depending " +
    "on how many inputs are enabled";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Number Of Inputs";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // chkAllowOutput
            // 
            this.chkAllowOutput.AutoSize = true;
            this.chkAllowOutput.Checked = true;
            this.chkAllowOutput.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllowOutput.Location = new System.Drawing.Point(8, 243);
            this.chkAllowOutput.Name = "chkAllowOutput";
            this.chkAllowOutput.Size = new System.Drawing.Size(86, 17);
            this.chkAllowOutput.TabIndex = 2;
            this.chkAllowOutput.Text = "Allow Output";
            this.chkAllowOutput.UseVisualStyleBackColor = true;
            this.chkAllowOutput.CheckedChanged += new System.EventHandler(this.chkAllowOutput_CheckedChanged);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(170, 354);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            this.button2.Location = new System.Drawing.Point(9, 354);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Defaults";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmbOutputCount);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(8, 259);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(237, 89);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            // 
            // cmbOutputCount
            // 
            this.cmbOutputCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOutputCount.FormattingEnabled = true;
            this.cmbOutputCount.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cmbOutputCount.Location = new System.Drawing.Point(101, 13);
            this.cmbOutputCount.Name = "cmbOutputCount";
            this.cmbOutputCount.Size = new System.Drawing.Size(75, 21);
            this.cmbOutputCount.TabIndex = 2;
            this.cmbOutputCount.SelectedIndexChanged += new System.EventHandler(this.cmbIPNos_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(4, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(225, 49);
            this.label4.TabIndex = 1;
            this.label4.Text = "Outputs can be sent by assigning results to variable names out1, out2, out3, out4" +
    ", out5 depending on how many outputs are enabled";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Number Of Outputs";
            this.label5.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 64);
            this.label1.TabIndex = 6;
            this.label1.Text = "This block allows you to execute some code inside a model and have the results us" +
    "ed by other blocks. The code can be edited by double clicking on the block in th" +
    "e model.";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(8, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(237, 100);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Python Code Block";
            // 
            // PyCodeBlockConfigure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 385);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chkAllowInput);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkAllowOutput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PyCodeBlockConfigure";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configure";
            this.Load += new System.EventHandler(this.PyCodeBlockConfigure_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        internal System.Windows.Forms.CheckBox chkAllowInput;
        internal System.Windows.Forms.CheckBox chkAllowOutput;
        internal System.Windows.Forms.ComboBox cmbIPNos;
        private System.Windows.Forms.GroupBox groupBox3;
        internal System.Windows.Forms.ComboBox cmbOutputCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;

    }
}