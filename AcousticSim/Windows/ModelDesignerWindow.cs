using Envision.Designer.Utils;
using Envision.Blocks;
using Envision.Blocks.CustomAttributes;
using DiagramNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Envision.Documents;
using System.Threading;
using System.ComponentModel;
using System.IO;
using DiagramNet.Elements;
using System.Drawing;

namespace Envision.Windows
{
    internal class ModelDesignerWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        internal DiagramNet.Designer modelDocEditor;
        internal List<string> IDs = new List<string>();
        internal BlockBase SelectedBlock = null;


        #region [   Designer   ]

        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripStatusLabel elementCountDisplayLabel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TrackBar doczoomLevel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox cmbLinkType;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton tstripUndoButton;
        private ToolStripButton tstripRedoButton;
        private ToolStripButton openToolStripButton;
        private ToolStripButton saveToolStripButton;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton tstripProfile;
        private ToolStripLabel tstripTime;
        private System.Windows.Forms.Timer simulationTimer;
        private ToolStripButton toolStripButton2;
        private ToolStripButton toolStripButton4;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem flipToolStripMenuItem;
        private ToolStripButton toolStripButton5;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripMenuItem commentBoxToolStripMenuItem;
        private ToolStripMenuItem labelToolStripMenuItem;
        private ToolStripSplitButton tstripRunControl;
        private ToolStripMenuItem continuosToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private System.ComponentModel.IContainer components;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelDesignerWindow));
            this.modelDocEditor = new DiagramNet.Designer(this.components);
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.elementCountDisplayLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.doczoomLevel = new System.Windows.Forms.TrackBar();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tstripUndoButton = new System.Windows.Forms.ToolStripButton();
            this.tstripRedoButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmbLinkType = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tstripRunControl = new System.Windows.Forms.ToolStripSplitButton();
            this.continuosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.tstripTime = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tstripProfile = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.commentBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simulationTimer = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.flipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainStatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.doczoomLevel)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // modelDocEditor
            // 
            this.modelDocEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modelDocEditor.AutoScroll = true;
            this.modelDocEditor.BackColor = System.Drawing.SystemColors.Window;
            this.modelDocEditor.Changed = false;
            this.modelDocEditor.Location = new System.Drawing.Point(0, 28);
            this.modelDocEditor.Name = "modelDocEditor";
            this.modelDocEditor.Size = new System.Drawing.Size(514, 261);
            this.modelDocEditor.TabIndex = 0;
            this.modelDocEditor.ElementClick += new DiagramNet.Designer.ElementEventHandler(this.modelDocEditor_ElementClick);
            this.modelDocEditor.ElementDoubleClick += new DiagramNet.Designer.ElementEventHandler(this.modelDocEditor_ElementDoubleClick);
            this.modelDocEditor.ElementMoving += new DiagramNet.Designer.ElementEventHandler(this.modelDocEditor_ElementMoved);
            this.modelDocEditor.ElementMoved += new DiagramNet.Designer.ElementEventHandler(this.modelDocEditor_ElementMoved);
            this.modelDocEditor.ElementResizing += new DiagramNet.Designer.ElementEventHandler(this.modelDocEditor_ElementMoved);
            this.modelDocEditor.ElementResized += new DiagramNet.Designer.ElementEventHandler(this.modelDocEditor_ElementMoved);
            this.modelDocEditor.ElementConnecting += new DiagramNet.Designer.ElementConnectEventHandler(this.modelDocEditor_ElementConnecting);
            this.modelDocEditor.ElementConnected += new DiagramNet.Designer.ElementConnectEventHandler(this.modelDocEditor_ElementConnected);
            this.modelDocEditor.ElementSelection += new DiagramNet.Designer.ElementSelectionEventHandler(this.modelDocEditor_ElementSelection);
            this.modelDocEditor.LinkRemoved += new DiagramNet.Designer.ElementEventHandler(this.modelDocEditor_LinkRemoved);
            this.modelDocEditor.Load += new System.EventHandler(this.modelDocEditor_Load);
            this.modelDocEditor.Scroll += new System.Windows.Forms.ScrollEventHandler(this.modelDocEditor_Scroll);
            this.modelDocEditor.Click += new System.EventHandler(this.modelDocEditor_Click);
            this.modelDocEditor.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.modelDocEditor_ControlAdded);
            this.modelDocEditor.Paint += new System.Windows.Forms.PaintEventHandler(this.modelDocEditor_Paint);
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.elementCountDisplayLabel});
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 297);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(542, 22);
            this.mainStatusStrip.SizingGrip = false;
            this.mainStatusStrip.TabIndex = 2;
            this.mainStatusStrip.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(470, 17);
            this.statusLabel.Spring = true;
            this.statusLabel.Text = "Ready";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // elementCountDisplayLabel
            // 
            this.elementCountDisplayLabel.Name = "elementCountDisplayLabel";
            this.elementCountDisplayLabel.Size = new System.Drawing.Size(57, 17);
            this.elementCountDisplayLabel.Text = "Blocks : #";
            // 
            // doczoomLevel
            // 
            this.doczoomLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.doczoomLevel.Location = new System.Drawing.Point(520, 28);
            this.doczoomLevel.Maximum = 500;
            this.doczoomLevel.Minimum = 50;
            this.doczoomLevel.Name = "doczoomLevel";
            this.doczoomLevel.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.doczoomLevel.Size = new System.Drawing.Size(45, 264);
            this.doczoomLevel.TabIndex = 3;
            this.doczoomLevel.TickStyle = System.Windows.Forms.TickStyle.None;
            this.doczoomLevel.Value = 50;
            this.doczoomLevel.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator3,
            this.tstripUndoButton,
            this.tstripRedoButton,
            this.toolStripSeparator2,
            this.cmbLinkType,
            this.toolStripButton5,
            this.toolStripSeparator1,
            this.tstripRunControl,
            this.toolStripButton2,
            this.toolStripButton4,
            this.tstripTime,
            this.toolStripSeparator4,
            this.tstripProfile,
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(542, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
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
            this.tstripUndoButton.Click += new System.EventHandler(this.tstripUndoButton_Click);
            // 
            // tstripRedoButton
            // 
            this.tstripRedoButton.Image = global::Envision.Properties.Resources.application_osx_right;
            this.tstripRedoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tstripRedoButton.Name = "tstripRedoButton";
            this.tstripRedoButton.Size = new System.Drawing.Size(54, 22);
            this.tstripRedoButton.Text = "Redo";
            this.tstripRedoButton.Click += new System.EventHandler(this.tstripRedoButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // cmbLinkType
            // 
            this.cmbLinkType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLinkType.Items.AddRange(new object[] {
            "Rectangular Link",
            "Straight Link"});
            this.cmbLinkType.Name = "cmbLinkType";
            this.cmbLinkType.Size = new System.Drawing.Size(121, 25);
            this.cmbLinkType.SelectedIndexChanged += new System.EventHandler(this.cmbLinkType_SelectedIndexChanged);
            this.cmbLinkType.Click += new System.EventHandler(this.cmbLinkType_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = global::Envision.Properties.Resources.document_letter_back;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton5.Text = "Flip";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tstripRunControl
            // 
            this.tstripRunControl.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.continuosToolStripMenuItem,
            this.toolStripSeparator5,
            this.settingsToolStripMenuItem});
            this.tstripRunControl.Image = global::Envision.Properties.Resources.greenarrowicon;
            this.tstripRunControl.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tstripRunControl.Name = "tstripRunControl";
            this.tstripRunControl.Size = new System.Drawing.Size(62, 22);
            this.tstripRunControl.Text = "&Step";
            this.tstripRunControl.ButtonClick += new System.EventHandler(this.tstripRunControl_ButtonClick);
            // 
            // continuosToolStripMenuItem
            // 
            this.continuosToolStripMenuItem.Name = "continuosToolStripMenuItem";
            this.continuosToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.continuosToolStripMenuItem.Text = "Continuos";
            this.continuosToolStripMenuItem.Click += new System.EventHandler(this.continuosToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(126, 6);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Image = global::Envision.Properties.Resources.sprocket_light_dropdown;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::Envision.Properties.Resources.media_controls_dark_pause;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "Pause";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::Envision.Properties.Resources.media_controls_dark_stop;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "Stop";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // tstripTime
            // 
            this.tstripTime.Name = "tstripTime";
            this.tstripTime.Size = new System.Drawing.Size(25, 22);
            this.tstripTime.Text = "t=0";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tstripProfile
            // 
            this.tstripProfile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tstripProfile.Image = global::Envision.Properties.Resources.plot_validation;
            this.tstripProfile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tstripProfile.Name = "tstripProfile";
            this.tstripProfile.Size = new System.Drawing.Size(23, 22);
            this.tstripProfile.Text = "Execution Plan";
            this.tstripProfile.Click += new System.EventHandler(this.tstripProfile_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.commentBoxToolStripMenuItem,
            this.labelToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Visible = false;
            // 
            // commentBoxToolStripMenuItem
            // 
            this.commentBoxToolStripMenuItem.Name = "commentBoxToolStripMenuItem";
            this.commentBoxToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.commentBoxToolStripMenuItem.Text = "Comment Box";
            this.commentBoxToolStripMenuItem.Click += new System.EventHandler(this.commentBoxToolStripMenuItem_Click);
            // 
            // labelToolStripMenuItem
            // 
            this.labelToolStripMenuItem.Name = "labelToolStripMenuItem";
            this.labelToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.labelToolStripMenuItem.Text = "Label";
            this.labelToolStripMenuItem.Click += new System.EventHandler(this.labelToolStripMenuItem_Click);
            // 
            // simulationTimer
            // 
            this.simulationTimer.Interval = 500;
            this.simulationTimer.Tick += new System.EventHandler(this.simulationTimer_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.flipToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(94, 26);
            // 
            // flipToolStripMenuItem
            // 
            this.flipToolStripMenuItem.Name = "flipToolStripMenuItem";
            this.flipToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.flipToolStripMenuItem.Text = "Flip";
            this.flipToolStripMenuItem.Click += new System.EventHandler(this.flipToolStripMenuItem_Click);
            // 
            // ModelDesignerWindow
            // 
            this.ClientSize = new System.Drawing.Size(542, 319);
            this.Controls.Add(this.doczoomLevel);
            this.Controls.Add(this.modelDocEditor);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.mainStatusStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ModelDesignerWindow";
            this.Text = "Untitled Model* - Model Designer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModelDesignerWindow_FormClosing);
            this.Load += new System.EventHandler(this.ModelDesignerWindow_Load);
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.doczoomLevel)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private string _currentFile;
        public string CurrentFile
        {
            get { return _currentFile; }
            set
            {
                _currentFile = value;
                UpdateFormTitle();
            }
        }

        public string CurrentDirectory
        {
            get { return string.IsNullOrEmpty(_currentFile) ? Envision.Utils.AssemblyDirectory : Path.GetDirectoryName(_currentFile); }
        }

        private bool _save(string file)
        {
            try
            {
                new DocumentSerializer().Save(DocumentModel, file);
                return true;
            } catch (Exception exception)
            {
                MessageBox.Show(string.Format("{0}: \r\n{1}", "File could not be saved", exception.Message), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool Save()
        {
            if (string.IsNullOrEmpty(CurrentFile))
            {
                using (SaveFileDialog sf = new SaveFileDialog())
                {
                    sf.Title = "Save Model";
                    sf.Filter = "Envision Models (*.emdl) | *.emdl";
                    if (sf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        _currentFile = sf.FileName;
                        return _save(_currentFile);
                    }
                    return false;
                }
            }
            else
            {
                return _save(CurrentFile);
            }

        }

        public bool Open(bool forceOpen = false, bool OpenAsTemplate = false)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Envision Models (*.emdl) | *.emdl";
            if (opf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                OpenFileByPath(opf.FileName, OpenAsTemplate, forceOpen);
            }
            return true;
        }

        private Bitmap Screenshot()
        {
            Bitmap bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            Graphics g = Graphics.FromImage(bmpScreenshot);
            g.CopyFromScreen(modelDocEditor.PointToScreen(modelDocEditor.Location), new Point(0, 0), modelDocEditor.Size);
            Form f = new Form();
            PictureBox p = new PictureBox();
            p.Dock = DockStyle.Fill;
            p.Image = bmpScreenshot;
            f.Controls.Add(p);
            p.SizeMode = PictureBoxSizeMode.Normal;
            f.ShowDialog();
            return bmpScreenshot;
        }

        public void OpenFileByPath(string filename, bool isTemplate = false, bool forceOpen = false)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (!Path.IsPathRooted(filename))
                {
                    filename = Path.Combine(Envision.Utils.AssemblyDirectory, filename);
                }

                var documentModel = new DocumentSerializer().Load(filename);
                documentModel.OnSaveChanged = OnSavedChanged;
                ModelDesignerWindow diagramForm;
                if (forceOpen)
                {
                    diagramForm = this;
                }
                else
                {
                    diagramForm = modelDocEditor.Document.Elements.Count > 0 ? new ModelDesignerWindow() : this;
                }
                diagramForm.Cursor = Cursors.WaitCursor;
                diagramForm.DocumentModel = documentModel;
                diagramForm.modelDocEditor.SetDocument(documentModel.Document);
                this.SimEvent = documentModel.SimulationState;
                if (SimEvent == null) SimEvent = new EventDescription() { SimulationStop = -1 };
                tstripTime.Text = "t=" + SimEvent.Time.ToString();
                if (!isTemplate) diagramForm.CurrentFile = filename;
                else
                {
                    diagramForm.CurrentFile = "";
                    documentModel.Touch();
                }
                diagramForm.Focus();
                diagramForm.Cursor = Cursors.Default;
                OnSavedChanged();
            } catch (Exception exception)
            {
                MessageBox.Show(string.Format("{0}:{1}{2}", "File could not be opened", Environment.NewLine, exception.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void OnSavedChanged()
        {
            UpdateFormTitle();
            UpdateUndoAndRedo();
            UpdateIDs();
        }

        private void UpdateIDs()
        {
            IDs.Clear();
            foreach (var item in modelDocEditor.Document.Elements)
            {
                if (ApplicationUtils.IsBlock(item))
                {
                    IDs.Add(ApplicationUtils.AsBlock(item).ID);
                }
            }
        }

        private void UpdateUndoAndRedo()
        {
            tstripUndoButton.Enabled = modelDocEditor.CanUndo;
            tstripRedoButton.Enabled = modelDocEditor.CanRedo;
        }

        public void UpdateFormTitle()
        {
            var isSaved = DocumentModel == null || DocumentModel.Saved;
            string title = ((string.IsNullOrEmpty(CurrentFile) ? "Untitled Model" : Path.GetFileName(CurrentFile)));
            ShowTitle((isSaved) ? title : title + "*");
        }

        private DocumentModel DocumentModel { get; set; }

        public ModelDesignerWindow()
        {
            InitializeComponent();
            DocumentModel = new DocumentModel {
                Document = modelDocEditor.Document,
                CreatedAt = DateTime.Now,
                OnSaveChanged = OnSavedChanged
            };

        }



        public void ShowTitle(string title)
        {
            this.Text = title + " - Model Designer";
        }

        public void SetStatus(string msg)
        {
            statusLabel.Text = msg;
            this.Refresh();
        }

        private void UpdateBlockCountDisplay()
        {
            elementCountDisplayLabel.Text = "Blocks : " + modelDocEditor.Document.ElementCount().ToString();
        }



        private void ModelDesignerWindow_Load(object sender, EventArgs e)
        {
            ShowTitle("Untitled Model*");
            SetStatus("Ready");
            doczoomLevel.Value = (int)(modelDocEditor.Document.Zoom * 100);
            cmbLinkType.SelectedIndex = 0;
            SimEvent = new EventDescription();
        }

        private void trvBlockSet_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                string k = e.Node.Text;
                if (AppGlobals.blocksetLib.ContainsKey(k))
                {
                    var ext = AppGlobals.blocksetLib[k];
                    var block = (BlockBase)AppGlobals.ExtensionServices.CreateInstance(ext);
                    block.CurrentDirectory = Application.StartupPath;
                    modelDocEditor.Document.Action = DesignerAction.Connect;
                    var diagramBlock = ApplicationUtils.CreateDiagramBlock(block, true); ;
                    modelDocEditor.Document.AddElement(diagramBlock);
                    modelDocEditor.Document.ClearSelection();
                    modelDocEditor.Document.SelectElement(diagramBlock);
                    DocumentModel.Touch();
                }
            }
        }

        public void InsertElement(AppGlobals.ExtensionServices.AvailablePlugin ext)
        {
            var block = (BlockBase)AppGlobals.ExtensionServices.CreateInstance(ext);
            block.ID = getUniqueID(block);
            block.CurrentDirectory = Application.StartupPath;
            try
            {
                block.Init();
            } catch (InitFailedException)
            {
                MessageBox.Show("Init failed for block `" + block.Name + "`. Insertion cancelled.");
                return;
            }
            modelDocEditor.Document.Action = DesignerAction.Connect;
            var diagramBlock = ApplicationUtils.CreateDiagramBlock(block, true); ;
            modelDocEditor.Document.AddElement(diagramBlock);
            modelDocEditor.Document.ClearSelection();
            modelDocEditor.Document.SelectElement(diagramBlock);
            SetVisualElements();
            DocumentModel.Touch();
        }


        public BlockBase GetElementById(string id)
        {
            foreach (var item in modelDocEditor.Document.Elements)
            {
                if (ApplicationUtils.IsBlock(item))
                {
                    if (ApplicationUtils.AsBlock(item).ID == id)
                    {
                        return ApplicationUtils.AsBlock(item);
                    }
                }
            }
            return null;
        }

        private string getUniqueID(BlockBase block)
        {
            string name = block.Name.ToLower();
            string id = Guid.NewGuid().ToString().Replace("-", "_");
            for (int i = 0 ; i < modelDocEditor.Document.ElementCount() + 1 ; i++)
            {
                if (name.Length > 5) id = name.Substring(0, 5) + i.ToString();
                else id = name + i.ToString();
                if (!IDs.Contains(id))
                {
                    break;
                }
            }
            IDs.Add(id);
            return id;
        }

        private void modelDocEditor_ElementConnected(object sender, DiagramNet.Events.ElementConnectEventArgs e)
        {
            DocumentModel.Touch();
            var src = (BlockNodeBase)e.Link.Connector1.State;
            var dst = (BlockNodeBase)e.Link.Connector2.State;
            foreach (var item in src.Root.OutputNodes)
            {
                item.Object = new InitialCondition();
            }
            src.ConnectTo(ref dst);
            e.Link.Connector1.FillColor1 = System.Drawing.Color.YellowGreen;
            e.Link.Connector2.FillColor2 = System.Drawing.Color.Yellow;
            modelDocEditor.Document.SelectElement(e.Link.Connector2);
            Logger.D(src.Root.Name + "." + src.Name + "-->" + dst.Root.Name + "." + dst.Name);
        }

        private void modelDocEditor_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            UpdateBlockCountDisplay();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            modelDocEditor.Document.Zoom = (float)(doczoomLevel.Value / 100.0);
        }

        private void modelDocEditor_ElementDoubleClick(object sender, DiagramNet.Events.ElementEventArgs e)
        {
            if (ApplicationUtils.IsBlock(e.Element))
            {
                BlockBase block = ApplicationUtils.AsBlock(e.Element);
                block.OpenAdditionalSettingsWindow();
            }
            if (e.Element.GetType() == typeof(DiagramNet.Elements.CommentBoxElement))
            {
                var commBox = (DiagramNet.Elements.CommentBoxElement)e.Element;
                string comment = InputBox(commBox.Label.Text);
                commBox.Label.Text = comment;
            }
        }

        private string InputBox(string _default = "")
        {
            Windows.CommentText com = new CommentText(_default);
            DialogResult d = com.ShowDialog();
            return com.txtComment.Text;
        }

        private void cmbLinkType_Click(object sender, EventArgs e)
        {

        }







        private void modelDocEditor_Load(object sender, EventArgs e)
        {

        }

        private void modelDocEditor_ElementClick(object sender, DiagramNet.Events.ElementEventArgs e)
        {
            var item = e.Element;
            if (item != null)
            {
                if (!ApplicationUtils.IsBlock(item))
                {
                    //ignore
                    return;
                }
                var el = (DiagramNet.Elements.DiagramBlock)item;
                var ty = (BlockBase)el.State;
                if (ty.Configuration != null)
                {
                    AppGlobals.ShowProperties(ty.Configuration);
                }
                else
                {
                    AppGlobals.ShowProperties(ty);
                }
                SetStatus(ty.Name + " - " + ty.Description);
                tstripProfile.Enabled = true;
                SelectedBlock = ty;
            }
            else
            {
                SetStatus("Ready");
                SelectedBlock = null;
            }
        }

        private void modelDocEditor_ElementSelection(object sender, DiagramNet.Events.ElementSelectionEventArgs e)
        {

        }

        private void modelDocEditor_LinkRemoved(object sender, DiagramNet.Events.ElementEventArgs e)
        {
            DocumentModel.Touch();
            var node1 = (BlockNodeBase)((BaseLinkElement)e.Element).Connector1.State;
            var node2 = (BlockNodeBase)((BaseLinkElement)e.Element).Connector2.State;

            ((BaseLinkElement)e.Element).Connector1.FillColor1 = System.Drawing.Color.White;
            ((BaseLinkElement)e.Element).Connector2.FillColor2 = System.Drawing.Color.White;
            node1.ConnectingNode = null;
            if (node2 != null)
            {
                node2.ConnectingNode = null;
                foreach (var node in node2.Root.OutputNodes)
                {
                    node.Object = null;
                }
            }

        }

        private void ModelDesignerWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!DocumentModel.Saved)
            {
                DialogResult d;
                if (e.CloseReason == CloseReason.ApplicationExitCall || e.CloseReason == CloseReason.FormOwnerClosing
                    || e.CloseReason == CloseReason.MdiFormClosing)
                {
                    d = MessageBox.Show("Model design is not saved, save before exit?", "Sure?",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                }
                else
                {
                    d = MessageBox.Show("Model design is not saved, save before exit?", "Sure?",
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                }
                if (d == System.Windows.Forms.DialogResult.Yes)
                {
                    Save();
                }
                else if (d == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        public void Undo()
        {
            if (modelDocEditor.CanUndo)
                modelDocEditor.Undo();
            DocumentModel.Touch();
        }

        public void Redo()
        {
            if (modelDocEditor.CanRedo)
                modelDocEditor.Redo();
            DocumentModel.Touch();
        }

       

        private void tstripUndoButton_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void tstripRedoButton_Click(object sender, EventArgs e)
        {
            Redo();
        }

        private void cmbLinkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            modelDocEditor.Document.LinkType = (cmbLinkType.SelectedIndex == 0) ? LinkType.RightAngle : LinkType.Straight;
        }

        private void modelDocEditor_Scroll(object sender, ScrollEventArgs e)
        {
        }

        private void modelDocEditor_ElementMoved(object sender, DiagramNet.Events.ElementEventArgs e)
        {
            DocumentModel.Touch();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            Open(true);
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void modelDocEditor_Click(object sender, EventArgs e)
        {
            SetStatus("Ready");
            SelectedBlock = null;
            SetVisualElements();
        }


        private void modelDocEditor_ControlAdded(object sender, ControlEventArgs e)
        {
            Logger.D("element added");
        }

        private void profilingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tstripProfile_Click(object sender, EventArgs e)
        {
            WeifenLuo.WinFormsUI.Docking.DockContent f = new WeifenLuo.WinFormsUI.Docking.DockContent();
            RichTextBox t = new RichTextBox();
            t.BorderStyle = BorderStyle.FixedSingle;
            f.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            t.Dock = DockStyle.Fill;
            t.Multiline = true;
            f.Text = "Execution Plan";
            ResolveAllDependencies();
            t.Text = "States :\n====================================\n"
                    + "Execution Time, t = " + ((SimEvent == null) ? "0" : SimEvent.Time.ToString())
                    + "\n\n\nExecution Order :\n===================================="
                    + CreateExecutionPlan();
            t.ReadOnly = true;
            t.BackColor = Color.White;
            f.Controls.Add(t);
            AppGlobals.ShowWin(f, WeifenLuo.WinFormsUI.Docking.DockState.Document);
        }


        #region [ Execution ]
        private EventDescription SimEvent;
        private Queue<BlockBase> ResolvedOrder;
        private List<string> ProcessedIDs = new List<string>();

        private void SetVisualElements()
        {
            foreach (var item in modelDocEditor.Document.Elements)
            {
                if (ApplicationUtils.IsBlock(item))
                {
                    var elem = (DiagramNet.Elements.DiagramBlock)item;
                    BlockBase block = (BlockBase)elem.State;
                    block.VisualElement = elem;
                }
            }
        }


        private void ResolveDeps(BlockBase block)
        {
            for (int i = 0 ; i < block.OutputNodes.Count ; i++)
            {
                if (block.OutputNodes[i].ConnectingNode != null)
                {
                    var tmp = block.OutputNodes[i].ConnectingNode.Root;
                    if (!ResolvedOrder.Contains(tmp))
                    {
                        ResolvedOrder.Enqueue(tmp);
                        Logger.D("=" + tmp.Name);
                        ResolveDeps(tmp);
                    }
                }
            }
        }

        private void ResolveAllDependencies()
        {
            ResolvedOrder = new Queue<BlockBase>();
            foreach (var elem in modelDocEditor.Document.Elements)
            {
                if (ApplicationUtils.IsBlock(elem))
                {
                    var block = ApplicationUtils.AsBlock(elem);
                    if (block.InputNodes.Count == 0)
                    {
                        ResolvedOrder.Enqueue(block);
                    }
                }
            }
            foreach (var elem in modelDocEditor.Document.Elements)
            {
                if (ApplicationUtils.IsBlock(elem))
                {
                    var block = ApplicationUtils.AsBlock(elem);
                    if (block.InputNodes.Count == 0)
                    {
                        ResolveDeps(block);
                    }
                }
            }
        }

        private string CreateExecutionPlan()
        {
            Logger.D("Execution Plan:");
            string plan = "";
            for (int i = 0 ; i < ResolvedOrder.Count ; i++)
            {
                var item = ResolvedOrder.ElementAt(i);
                plan += "\n" + ("+ " + item.ID + "::" + item.Name);
                for (int j = 0 ; j < item.OutputNodes.Count ; j++)
                {
                    var node = item.OutputNodes[j];
                    if (node.ConnectingNode == null)
                    {
                        plan += "\n|   " + node.ShortName + " (not connected, might create errors) ";
                    }
                    else
                    {
                        if (node.Object != null)
                        {
                            plan += "\n|   " + node.ShortName + " (Current State: " + node.Object.ToString() + ")";
                        }
                        else
                        {
                            plan += "\n|   " + node.ShortName + " (Current State: null)";
                        }
                    }
                }
                plan += "\n";
            }
            Logger.D(plan);
            return plan;
        }

        private object ExecuteOneIteration()
        {
            if (SimEvent == null)
            {
                SimEvent = new EventDescription() { Time = 0, SimulationStart = 0, SimulationStop = -1 };
            }
            else
            {
                SimEvent.Time += 1.0;
                tstripTime.Text = "t=" + SimEvent.Time.ToString();
            }
            DocumentModel.SimulationState = SimEvent;
            for (int i = 0 ; i < ResolvedOrder.Count ; i++)
            {
                var block = ResolvedOrder.ElementAt(i);
                SetStatus("Executing - " + block.ID + "<" + block.Name + ">  @ " + SimEvent.Time.ToString());
                try
                {
                    block.Execute(SimEvent);
                } catch (Exception ex)
                {
                    MessageBox.Show("Error while evaluating `" + block.ID + "::" + block.Name + "`, simulation will stop now.\n"
                        + "(" + ex.Message + ")", "Error"
                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return block;
                }
            }
            for (int i = 0 ; i < ResolvedOrder.Count ; i++)
            {
                var block = ResolvedOrder.ElementAt(i);
                block.LastEvent = new EventDescription() { Time = SimEvent.Time, SimulationStart = SimEvent.SimulationStart, SimulationStop = SimEvent.SimulationStop };
            }
            return null;
        }

        private void Cleanup()
        {
            SetStatus("Cleaning up...");
            ResolvedOrder.Clear();
            ProcessedIDs.Clear();
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            SetStatus("Tracking visual elements...");
            SetVisualElements();
            SetStatus("Resolving dependencies..");
            ResolveAllDependencies();
            SetStatus("Creating execution plan..");
            CreateExecutionPlan();
            SetStatus("Executing...");
            object result = ExecuteOneIteration();
            if (result == null)
            {
                Cleanup();
                SetStatus("Finished Executing");
            }
            else
            {
                var block = ((BlockBase)result);
                SetStatus("Finished with errors in block `" + block.ID + "::" + block.Name + "`");
            }

        }

        private void Reset()
        {
            SimEvent = new EventDescription();
            simulationTimer.Enabled = false;
            UpdateSimulationDisplay();
            tstripTime.Text = "t=0";
            foreach (var elem in modelDocEditor.Document.Elements)
            {
                if (ApplicationUtils.IsBlock(elem))
                {
                    var block = ApplicationUtils.AsBlock(elem);
                    for (int i = 0 ; i < block.OutputNodes.Count ; i++)
                    {
                        block.OutputNodes[i].Object = new InitialCondition();
                    }
                }
            }
        }

        private void UpdateSimulationDisplay()
        {
            tstripTime.Text = "t=" + SimEvent.Time.ToString();
        }


        #endregion




        private void flipToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (SelectedBlock != null)
            {
                if (SelectedBlock.VisualElement != null)
                {
                    SelectedBlock.VisualElement.Flip = !SelectedBlock.VisualElement.Flip;
                }
            }
        }

        private void commentBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //CommentBoxElement cmbox = new CommentBoxElement();
            //cmbox.Label.Text = InputBox("");
            //cmbox.Size = new System.Drawing.Size(100, 100);
            //modelDocEditor.Document.AddElement(cmbox);
            //modelDocEditor.Document.Action = DesignerAction.Select;
        }

        private void labelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //LabelElement lbl = new LabelElement();
            //lbl.Text = InputBox("");
            //modelDocEditor.Document.AddElement(lbl);
            //modelDocEditor.Document.Action = DesignerAction.Select;
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {
            Screenshot();
        }

        private void modelDocEditor_ElementConnecting(object sender, DiagramNet.Events.ElementConnectEventArgs e)
        {
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Reset();
            SimulationLock(false);
        }

        private void tstripRunControl_ButtonClick(object sender, EventArgs e)
        {
            SetStatus("Tracking visual elements...");
            SetVisualElements();
            SetStatus("Resolving dependencies..");
            ResolveAllDependencies();
            SetStatus("Creating execution plan..");
            CreateExecutionPlan();
            SetStatus("Executing...");
            object result = ExecuteOneIteration();
            if (result == null)
            {
                Cleanup();
                SetStatus("Finished Executing");
            }
            else
            {
                var block = ((BlockBase)result);
                SetStatus("Finished with errors in block `" + block.ID + "::" + block.Name + "`");
            }
        }

        private void SimulationLock(bool _lock)
        {
            cmbLinkType.Enabled = !_lock;
            modelDocEditor.Enabled = !_lock;
            tstripUndoButton.Enabled = !_lock;
            tstripRedoButton.Enabled = !_lock;
        }

        private void continuosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SimulationLock(true);
            SetStatus("Tracking visual elements...");
            SetVisualElements();
            SetStatus("Resolving dependencies..");
            ResolveAllDependencies();
            SetStatus("Creating execution plan..");
            CreateExecutionPlan();
            SetStatus("Executing...");
            simulationTimer.Enabled = true;
        }

        private void simulationTimer_Tick(object sender, EventArgs e)
        {
            if (SimEvent == null)
            {
                SimEvent = new EventDescription();
            }
            if (SimEvent.SimulationStop == -1 || SimEvent.Time < SimEvent.SimulationStop)
            {
                object result = ExecuteOneIteration();
                if (result !=  null) // error occured
                {
                    simulationTimer.Enabled = false;
                    MessageBox.Show("Error occured during simulation at t=" + SimEvent.Time.ToString()
                        + "s. Simulation will stop now. Please retry after rectifying the error", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    SimulationLock(false);
                }
            }
            else
            {
                simulationTimer.Enabled = false;
                SimulationLock(false);
            }

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SimConfig confWin = new SimConfig();
            if (confWin.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (SimEvent == null) SimEvent = new EventDescription();
                try
                {
                    SimEvent.SimulationStart = double.Parse(confWin.txtStart.Text);
                    SimEvent.SimulationStop = double.Parse(confWin.txtStop.Text);
                    SimEvent.SimulationStep = double.Parse(confWin.txtStep.Text);
                    simulationTimer.Interval = int.Parse(confWin.comboBox1.SelectedItem.ToString());
                } catch (Exception)
                {
                    MessageBox.Show("Error in timing values. Reverting to default values", "Error",MessageBoxButtons.OK
                        , MessageBoxIcon.Error);
                }
                SimEvent.Time = SimEvent.SimulationStart;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            simulationTimer.Enabled = false;
            SimulationLock(false);

        }
    }
}
