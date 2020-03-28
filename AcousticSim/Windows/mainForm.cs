using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Envision.Windows
{
    public partial class mainForm : Form
    {


        public mainForm()
        {
            InitializeComponent();
            AppGlobals.mainFormInstance = this;
            SplashScreen.SplashScreen.CloseForm();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModelDesignerWindow win = new ModelDesignerWindow();
            win.Show(masterDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
           if(this.ActiveMdiChild.GetType() == typeof(ModelDesignerWindow))
           {
               ModelDesignerWindow designerWin = (ModelDesignerWindow)this.ActiveMdiChild;
               designerWin.Save();
           }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists("./core/samples"))
            {
                string[] files = System.IO.Directory.GetFiles("./core/samples", "*.emdl", System.IO.SearchOption.AllDirectories);
                foreach (var file in files)
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(file);
                    var tentry = samplesToolStripMenuItem.DropDownItems.Add(fi.Name);
                    tentry.Click += tentry_Click;
                }
            }
            if (AppGlobals.args.Count() > 0)
            {
                for (int i = 0 ; i < AppGlobals.args.Count() ; i++)
                {
                    string arg = AppGlobals.args[i];
                    if (System.IO.File.Exists(arg))
                    {
                        ModelDesignerWindow designerWin = new ModelDesignerWindow();
                        designerWin.Show(masterDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                        designerWin.OpenFileByPath(arg);
                    }
                }
            }
            AppGlobals.ShowGenericResults();
        }

        void tentry_Click(object sender, EventArgs e)
        {
            ModelDesignerWindow mdlWin = new ModelDesignerWindow();
            mdlWin.OpenFileByPath(System.IO.Path.Combine("./core/samples/", ((ToolStripItem)sender).Text), true);
            AppGlobals.ShowWin(mdlWin, WeifenLuo.WinFormsUI.Docking.DockState.Document);
        }

        private void mainForm_MdiChildActivate(object sender, EventArgs e)
        {
            var child = this.ActiveMdiChild;
            if (child != null)
            {
                if (child.GetType() == typeof(ModelDesignerWindow))
                {
                    AppGlobals.CurrentDesigner = (ModelDesignerWindow)child;
                    AppGlobals.ShowBlocksetBrowser();
                    undoToolStripMenuItem.Enabled = true;
                    redoToolStripMenuItem.Enabled = true;
                }
                else
                {
                    undoToolStripMenuItem.Enabled = false;
                    redoToolStripMenuItem.Enabled = false;
                }
            }
        }

        private void EditorActivationChanged(bool activated)
        {
            saveToolStripMenuItem.Enabled = activated;
        }

        private void blocksetWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppGlobals.ShowBlocksetBrowser();
        }

        private void propertyInspectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppGlobals.ShowProperties(null);
        }

        private void modelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void scriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PythonEditor win = new PythonEditor();
            win.Show(masterDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
        }

        private void modelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
        }

        private void scriptToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PythonEditor designerWin;
            if (this.ActiveMdiChild != null && this.ActiveMdiChild.GetType() == typeof(PythonEditor))
            {
                designerWin = (PythonEditor)this.ActiveMdiChild;
                designerWin.Open();
            }
            else
            {
                designerWin = new PythonEditor();
                designerWin.Show(masterDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                designerWin.Open();
            }
        }

        private void resultsWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppGlobals.ShowGenericResults();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AppGlobals.CurrentDesigner != null) AppGlobals.CurrentDesigner.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(AppGlobals.CurrentDesigner != null ) AppGlobals.CurrentDesigner.Redo();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ModelDesignerWindow designerWin;
            if (this.ActiveMdiChild != null && this.ActiveMdiChild.GetType() == typeof(ModelDesignerWindow))
            {
                designerWin = (ModelDesignerWindow)this.ActiveMdiChild;
                if (designerWin.modelDocEditor.Document.ElementCount() == 0)
                {
                    designerWin.Open(false, true);
                    return;
                }
            }
            designerWin = new ModelDesignerWindow();
            designerWin.Show(masterDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            designerWin.Open(false, true);
            designerWin.Invalidate();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ModelDesignerWindow designerWin;
            if (this.ActiveMdiChild != null && this.ActiveMdiChild.GetType() == typeof(ModelDesignerWindow))
            {
                designerWin = (ModelDesignerWindow)this.ActiveMdiChild;
                if (designerWin.modelDocEditor.Document.ElementCount() == 0)
                {
                    designerWin.Open();
                    return;
                }
            }
            designerWin = new ModelDesignerWindow();
            designerWin.Show(masterDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            designerWin.Open();
            designerWin.Invalidate();
        }


    }
}
