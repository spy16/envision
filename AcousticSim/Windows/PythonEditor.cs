using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Envision.Windows
{
    public class PythonEditor : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        internal Editors.PythonEditor pyEditor = new Editors.PythonEditor();
        private Documents.ScriptDocumentModel DocumentModel;

        #region [ Desinger ]
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tstripUndoButton;
        private System.Windows.Forms.ToolStripButton tstripRedoButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Panel editorHost;
        private ToolStripStatusLabel tstripStatus;
        private ToolStripStatusLabel tstripFilePath;
        private System.Windows.Forms.StatusStrip statusStrip1;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PythonEditor));
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tstripUndoButton = new System.Windows.Forms.ToolStripButton();
            this.tstripRedoButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.editorHost = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tstripStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tstripFilePath = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator3,
            this.tstripUndoButton,
            this.tstripRedoButton,
            this.toolStripSeparator2,
            this.toolStripButton1});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(485, 25);
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "&Open";
            this.openToolStripButton.Click += new System.EventHandler(this.openToolStripButton_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "&Save";
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tstripUndoButton
            // 
            this.tstripUndoButton.Image = global::Envision.Properties.Resources.application_osx_left;
            this.tstripUndoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tstripUndoButton.Name = "tstripUndoButton";
            this.tstripUndoButton.Size = new System.Drawing.Size(56, 22);
            this.tstripUndoButton.Text = "Undo";
            // 
            // tstripRedoButton
            // 
            this.tstripRedoButton.Image = global::Envision.Properties.Resources.application_osx_right;
            this.tstripRedoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tstripRedoButton.Name = "tstripRedoButton";
            this.tstripRedoButton.Size = new System.Drawing.Size(54, 22);
            this.tstripRedoButton.Text = "Redo";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::Envision.Properties.Resources.greenarrowicon;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Run";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // editorHost
            // 
            this.editorHost.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editorHost.Location = new System.Drawing.Point(0, 28);
            this.editorHost.Name = "editorHost";
            this.editorHost.Size = new System.Drawing.Size(485, 210);
            this.editorHost.TabIndex = 4;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tstripStatus,
            this.tstripFilePath});
            this.statusStrip1.Location = new System.Drawing.Point(0, 239);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.ShowItemToolTips = true;
            this.statusStrip1.Size = new System.Drawing.Size(485, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tstripStatus
            // 
            this.tstripStatus.Name = "tstripStatus";
            this.tstripStatus.Size = new System.Drawing.Size(394, 17);
            this.tstripStatus.Spring = true;
            this.tstripStatus.Text = "Ready";
            this.tstripStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tstripFilePath
            // 
            this.tstripFilePath.Name = "tstripFilePath";
            this.tstripFilePath.Size = new System.Drawing.Size(76, 17);
            this.tstripFilePath.Text = "File : Untitled";
            // 
            // PythonEditor
            // 
            this.ClientSize = new System.Drawing.Size(485, 261);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.editorHost);
            this.Controls.Add(this.toolStrip2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "PythonEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Python Editor";
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        public PythonEditor()
        {
            InitializeComponent();
            pyEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            pyEditor.TextChanged += pyEditor_TextChanged;
            editorHost.Controls.Add(pyEditor);
            DocumentModel = new Documents.ScriptDocumentModel();
            DocumentModel.SaveStateChanged += DocumentModel_SaveStateChanged;
            this.SetTitle("Untitled*");
        }

        public void SetTitle(string title) {
            this.Text = title + " - Python Editor";
        }

        void DocumentModel_SaveStateChanged(object sender, EventArgs e)
        {
            string title = "";
            if(DocumentModel.FileName == ""){
                title = "Untitled";
            }
            else
            {
                title = DocumentModel.FileName.Split('\\').Last();
            }
            if (!DocumentModel.IsSaved) title += "*";
            SetTitle(title);
            tstripFilePath.Text = "File: " + DocumentModel.FileName;
        }

        void pyEditor_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            DocumentModel.Touch();
        }


        public void Open(string filename = "")
        {
            if (filename == "")
            {
                using (OpenFileDialog opf = new OpenFileDialog())
                {
                    opf.Title = "Open Script";
                    opf.Filter = "Python Scripts (*.py)|*.py";
                    if (opf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        DocumentModel = new Documents.ScriptDocumentModel(opf.FileName);
                    }
                }
            }
            else
            {
                DocumentModel = new Documents.ScriptDocumentModel(filename);
            }
            pyEditor.OpenFile(DocumentModel.FileName);
            DocumentModel.SaveStateChanged += DocumentModel_SaveStateChanged;
            DocumentModel.IsSaved = true;
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            if (!DocumentModel.IsSaved)
            {
                DocumentModel.IsSaved = Save();
            }
        }

        private bool Save()
        {
            if (DocumentModel.FileName == "")
            {
                using (SaveFileDialog sf = new SaveFileDialog())
                {
                    sf.Filter = "Python Scripts (*.py) | *.py";
                    if (sf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        DocumentModel.FileName = sf.FileName;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            pyEditor.SaveToFile(DocumentModel.FileName, Encoding.UTF8);
            return true;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
        }

    }
}
