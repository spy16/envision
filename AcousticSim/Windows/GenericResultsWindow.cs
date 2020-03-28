using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.Windows
{
    internal class GenericResultsWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        #region [ Designer ]
        internal     System.Windows.Forms.RichTextBox rtfResultsDisplay;

        private void InitializeComponent()
        {
            this.rtfResultsDisplay = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtfResultsDisplay
            // 
            this.rtfResultsDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtfResultsDisplay.Location = new System.Drawing.Point(0, 0);
            this.rtfResultsDisplay.Name = "rtfResultsDisplay";
            this.rtfResultsDisplay.ReadOnly = true;
            this.rtfResultsDisplay.Size = new System.Drawing.Size(284, 261);
            this.rtfResultsDisplay.TabIndex = 0;
            this.rtfResultsDisplay.Text = "";
            // 
            // GenericResultsWindow
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.ControlBox = false;
            this.Controls.Add(this.rtfResultsDisplay);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GenericResultsWindow";
            this.Text = "Results";
            this.ResumeLayout(false);

        }
        #endregion
        public GenericResultsWindow()
        {
            InitializeComponent();
        }
    }
}
