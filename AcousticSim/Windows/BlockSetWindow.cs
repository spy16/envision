using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Envision.Windows
{
    public class BlockSetWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        #region [ Designer ]
        private Label label1;
        private PictureBox pictureBox1;
        private Panel panel1;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private Label lblDescription;
        private ToolStripTextBox txtSearchBox;
        private System.Windows.Forms.TreeView trvBlockSet;
        private void InitializeComponent()
        {
            this.trvBlockSet = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtSearchBox = new System.Windows.Forms.ToolStripTextBox();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // trvBlockSet
            // 
            this.trvBlockSet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trvBlockSet.Location = new System.Drawing.Point(5, 63);
            this.trvBlockSet.Name = "trvBlockSet";
            this.trvBlockSet.Size = new System.Drawing.Size(206, 228);
            this.trvBlockSet.TabIndex = 0;
            this.trvBlockSet.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvBlockSet_AfterSelect);
            this.trvBlockSet.DoubleClick += new System.EventHandler(this.trvBlockSet_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Blockset Library";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Location = new System.Drawing.Point(5, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(206, 27);
            this.panel1.TabIndex = 5;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtSearchBox,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(206, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::Envision.Properties.Resources.gem_remove;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Collapse All";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = global::Envision.Properties.Resources.unknownicon;
            this.pictureBox1.Image = global::Envision.Properties.Resources.unknownicon;
            this.pictureBox1.Location = new System.Drawing.Point(5, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // lblDescription
            // 
            this.lblDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDescription.Location = new System.Drawing.Point(2, 295);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(209, 52);
            this.lblDescription.TabIndex = 6;
            this.lblDescription.Text = "Description : N/A";
            // 
            // txtSearchBox
            // 
            this.txtSearchBox.Name = "txtSearchBox";
            this.txtSearchBox.Size = new System.Drawing.Size(150, 25);
            this.txtSearchBox.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // BlockSetWindow
            // 
            this.ClientSize = new System.Drawing.Size(220, 352);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.trvBlockSet);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "BlockSetWindow";
            this.Text = "Blockset Library";
            this.Load += new System.EventHandler(this.BlockSetWindow_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        List<TreeNode> mainNodes = new List<TreeNode>();

        private void LoadBlocksetTree()
        {
            trvBlockSet.Nodes.Clear();
            foreach (var item in AppGlobals.blocksetLib)
            {
                TreeNode p_node;
                string cat = item.Value.Category;
                if (trvBlockSet.Nodes.ContainsKey(cat))
                {
                    p_node = trvBlockSet.Nodes[cat];
                }
                else
                {
                    p_node = trvBlockSet.Nodes.Add(cat, cat);
                }
                p_node.Nodes.Add(item.Key, item.Key);
            }
        }

        public BlockSetWindow()
        {
            InitializeComponent();
            LoadBlocksetTree();
        }
        private void BlockSetWindow_Load(object sender, EventArgs e)
        {

        }

        private void trvBlockSet_DoubleClick(object sender, EventArgs e)
        {
            if (AppGlobals.CurrentDesigner != null)
            {
                TreeNode node = trvBlockSet.SelectedNode;
                if (node != null)
                {
                    string name = node.Text;
                    if (AppGlobals.blocksetLib.ContainsKey(name))
                    {
                        AppGlobals.ExtensionServices.AvailablePlugin p = AppGlobals.blocksetLib[name];
                        try
                        {
                            AppGlobals.CurrentDesigner.InsertElement(p);
                        } catch (NotImplementedException)
                        {
                            AppGlobals.blocksetLib.Remove(name);
                            LoadBlocksetTree();
                            MessageBox.Show("Block `" + name + "` is still under development.", "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error); 
                        }
                    }
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (toolStripButton1.Text == "Expand All")
            {
                trvBlockSet.ExpandAll();
                toolStripButton1.Text = "Collapse All";
                toolStripButton1.Image = Properties.Resources.gem_remove;
            }
            else
            {
                trvBlockSet.CollapseAll();
                toolStripButton1.Text = "Expand All";
                toolStripButton1.Image = Properties.Resources.add_small;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            trvBlockSet.CollapseAll();
        }

        private void trvBlockSet_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = trvBlockSet.SelectedNode;
            if (node != null)
            {
                string name = node.Text;
                if (AppGlobals.blocksetLib.ContainsKey(name))
                {
                    AppGlobals.ExtensionServices.AvailablePlugin p = AppGlobals.blocksetLib[name];
                    lblDescription.Text = p.Description;
                }
            }
        }

        
            
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadBlocksetTree();
            if (txtSearchBox.Text != "")
            {
                TreeNode node = new TreeNode("Search");
                foreach (TreeNode item in trvBlockSet.Nodes)
                {
                    var nodes = item.Nodes;
                    foreach (TreeNode b in nodes)
                    {
                        if (txtSearchBox.Text == "*" || b.Name.ToLower().Contains(txtSearchBox.Text.ToLower()))
                        {
                            if (!node.Nodes.Contains(b)) node.Nodes.Add(b.Name);
                        }                        
                    }
                }
                trvBlockSet.Nodes.Clear();
                trvBlockSet.Nodes.Add(node);
                node.Expand();
            }
        }
    }
}
