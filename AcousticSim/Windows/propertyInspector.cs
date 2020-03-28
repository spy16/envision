using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.Windows
{
   public class propertyInspector  :WeifenLuo.WinFormsUI.Docking.DockContent
    {

       public propertyInspector(object obj = null)
       {
           InitializeComponent();
           if (obj != null)
           {
               propDisplay.SelectedObject = obj;
           }
       }

       public void Inspect(object obj)
       {
           if (obj != null)
           {
               propDisplay.SelectedObject = obj;
           }
       }

        private System.Windows.Forms.PropertyGrid propDisplay;

        private void InitializeComponent()
        {
            this.propDisplay = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // propDisplay
            // 
            this.propDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propDisplay.Location = new System.Drawing.Point(0, 0);
            this.propDisplay.Name = "propDisplay";
            this.propDisplay.Size = new System.Drawing.Size(284, 261);
            this.propDisplay.TabIndex = 0;
            this.propDisplay.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propDisplay_PropertyValueChanged);
            this.propDisplay.Click += new System.EventHandler(this.propDisplay_Click);
            // 
            // propertyInspector
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.propDisplay);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "propertyInspector";
            this.Text = "Property Inspector";
            this.ResumeLayout(false);

        }

        private void propDisplay_Click(object sender, EventArgs e)
        {

        }

        private void propDisplay_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
        {
            DiagramNet.Document d = AppGlobals.CurrentDesigner.modelDocEditor.Document;
            AppGlobals.CurrentDesigner.modelDocEditor.SetDocument(d);
        }
   }
}
