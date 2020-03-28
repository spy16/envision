using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.Components.Visualization
{
    public class PlotterWindowDockable : WeifenLuo.WinFormsUI.Docking.DockContent
    {

        public PlotterWindowDockable()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // PlotterWindowDockable
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "PlotterWindowDockable";
            this.Text = "Plot";
            this.Load += new System.EventHandler(this.PlotterWindowDockable_Load);
            this.ResumeLayout(false);

        }

        private void PlotterWindowDockable_Load(object sender, EventArgs e)
        {

        }
    }
}
