
namespace WindowsFormsApp
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.rightBox = new System.Windows.Forms.GroupBox();
            this.splitContainerChild = new System.Windows.Forms.SplitContainer();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.tabAllInfo = new System.Windows.Forms.TabControl();
            this.tabPageInfo = new System.Windows.Forms.TabPage();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.tabPageResult = new System.Windows.Forms.TabPage();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.menusList = new System.Windows.Forms.ToolStrip();
            this.txtBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.delBtn = new System.Windows.Forms.ToolStripButton();
            this.cleanBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.moreFilter = new System.Windows.Forms.ToolStripButton();
            this.downBtn = new System.Windows.Forms.ToolStripButton();
            this.leftBox = new System.Windows.Forms.GroupBox();
            this.treeView = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.fileBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.openFile = new System.Windows.Forms.ToolStripMenuItem();
            this.saveBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.quit = new System.Windows.Forms.ToolStripMenuItem();
            this.helpBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.testBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.rightBox.SuspendLayout();
            this.splitContainerChild.Panel1.SuspendLayout();
            this.splitContainerChild.Panel2.SuspendLayout();
            this.splitContainerChild.SuspendLayout();
            this.tabAllInfo.SuspendLayout();
            this.tabPageInfo.SuspendLayout();
            this.tabPageResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.menusList.SuspendLayout();
            this.leftBox.SuspendLayout();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.Filter = "txt文件|*.txt|csv文件|*.csv";
            this.openFileDialog.Multiselect = true;
            // 
            // rightBox
            // 
            this.rightBox.Controls.Add(this.splitContainerChild);
            this.rightBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightBox.Location = new System.Drawing.Point(0, 0);
            this.rightBox.Margin = new System.Windows.Forms.Padding(10);
            this.rightBox.Name = "rightBox";
            this.rightBox.Padding = new System.Windows.Forms.Padding(15);
            this.rightBox.Size = new System.Drawing.Size(794, 650);
            this.rightBox.TabIndex = 2;
            this.rightBox.TabStop = false;
            this.rightBox.Text = "自定义查询";
            // 
            // splitContainerChild
            // 
            this.splitContainerChild.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerChild.Location = new System.Drawing.Point(15, 33);
            this.splitContainerChild.Name = "splitContainerChild";
            this.splitContainerChild.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerChild.Panel1
            // 
            this.splitContainerChild.Panel1.Controls.Add(this.txtFilter);
            // 
            // splitContainerChild.Panel2
            // 
            this.splitContainerChild.Panel2.Controls.Add(this.tabAllInfo);
            this.splitContainerChild.Size = new System.Drawing.Size(764, 602);
            this.splitContainerChild.SplitterDistance = 181;
            this.splitContainerChild.TabIndex = 0;
            // 
            // txtFilter
            // 
            this.txtFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFilter.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFilter.Location = new System.Drawing.Point(0, 0);
            this.txtFilter.Multiline = true;
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtFilter.Size = new System.Drawing.Size(764, 181);
            this.txtFilter.TabIndex = 0;
            // 
            // tabAllInfo
            // 
            this.tabAllInfo.Controls.Add(this.tabPageInfo);
            this.tabAllInfo.Controls.Add(this.tabPageResult);
            this.tabAllInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabAllInfo.Location = new System.Drawing.Point(0, 0);
            this.tabAllInfo.Name = "tabAllInfo";
            this.tabAllInfo.SelectedIndex = 0;
            this.tabAllInfo.Size = new System.Drawing.Size(764, 417);
            this.tabAllInfo.TabIndex = 0;
            // 
            // tabPageInfo
            // 
            this.tabPageInfo.Controls.Add(this.txtInfo);
            this.tabPageInfo.Location = new System.Drawing.Point(4, 25);
            this.tabPageInfo.Name = "tabPageInfo";
            this.tabPageInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInfo.Size = new System.Drawing.Size(756, 388);
            this.tabPageInfo.TabIndex = 2;
            this.tabPageInfo.Text = "信息";
            this.tabPageInfo.UseVisualStyleBackColor = true;
            // 
            // txtInfo
            // 
            this.txtInfo.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInfo.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.txtInfo.Location = new System.Drawing.Point(3, 3);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.ReadOnly = true;
            this.txtInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtInfo.Size = new System.Drawing.Size(750, 382);
            this.txtInfo.TabIndex = 1;
            // 
            // tabPageResult
            // 
            this.tabPageResult.Controls.Add(this.dataGridView);
            this.tabPageResult.Location = new System.Drawing.Point(4, 25);
            this.tabPageResult.Name = "tabPageResult";
            this.tabPageResult.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageResult.Size = new System.Drawing.Size(756, 388);
            this.tabPageResult.TabIndex = 3;
            this.tabPageResult.Text = "结果";
            this.tabPageResult.UseVisualStyleBackColor = true;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(3, 3);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.RowTemplate.Height = 27;
            this.dataGridView.Size = new System.Drawing.Size(750, 382);
            this.dataGridView.TabIndex = 2;
            // 
            // menusList
            // 
            this.menusList.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menusList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtBtn,
            this.toolStripSeparator1,
            this.toolStripSeparator2,
            this.delBtn,
            this.cleanBtn,
            this.toolStripSeparator3,
            this.toolStripSeparator4,
            this.moreFilter,
            this.downBtn});
            this.menusList.Location = new System.Drawing.Point(0, 34);
            this.menusList.Name = "menusList";
            this.menusList.Padding = new System.Windows.Forms.Padding(5);
            this.menusList.Size = new System.Drawing.Size(1066, 37);
            this.menusList.TabIndex = 6;
            this.menusList.Text = "toolStrip1";
            // 
            // txtBtn
            // 
            this.txtBtn.Image = ((System.Drawing.Image)(resources.GetObject("txtBtn.Image")));
            this.txtBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.txtBtn.Name = "txtBtn";
            this.txtBtn.Size = new System.Drawing.Size(63, 24);
            this.txtBtn.Text = "打开";
            this.txtBtn.ToolTipText = "打开";
            this.txtBtn.Click += new System.EventHandler(this.txtBtn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // delBtn
            // 
            this.delBtn.Image = ((System.Drawing.Image)(resources.GetObject("delBtn.Image")));
            this.delBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.delBtn.Name = "delBtn";
            this.delBtn.Size = new System.Drawing.Size(63, 24);
            this.delBtn.Text = "删除";
            this.delBtn.Click += new System.EventHandler(this.delBtn_Click);
            // 
            // cleanBtn
            // 
            this.cleanBtn.Image = ((System.Drawing.Image)(resources.GetObject("cleanBtn.Image")));
            this.cleanBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cleanBtn.Name = "cleanBtn";
            this.cleanBtn.Size = new System.Drawing.Size(63, 24);
            this.cleanBtn.Text = "清空";
            this.cleanBtn.Click += new System.EventHandler(this.cleanBtn_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            // 
            // moreFilter
            // 
            this.moreFilter.Image = ((System.Drawing.Image)(resources.GetObject("moreFilter.Image")));
            this.moreFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moreFilter.Name = "moreFilter";
            this.moreFilter.Size = new System.Drawing.Size(108, 24);
            this.moreFilter.Text = "自定义查询";
            this.moreFilter.Click += new System.EventHandler(this.moreFilter_Click);
            // 
            // downBtn
            // 
            this.downBtn.Image = ((System.Drawing.Image)(resources.GetObject("downBtn.Image")));
            this.downBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.downBtn.Name = "downBtn";
            this.downBtn.Size = new System.Drawing.Size(86, 24);
            this.downBtn.Text = "导出csv";
            this.downBtn.Click += new System.EventHandler(this.downBtn_Click);
            // 
            // leftBox
            // 
            this.leftBox.Controls.Add(this.treeView);
            this.leftBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftBox.Location = new System.Drawing.Point(0, 0);
            this.leftBox.Margin = new System.Windows.Forms.Padding(10);
            this.leftBox.Name = "leftBox";
            this.leftBox.Padding = new System.Windows.Forms.Padding(15);
            this.leftBox.Size = new System.Drawing.Size(268, 650);
            this.leftBox.TabIndex = 5;
            this.leftBox.TabStop = false;
            this.leftBox.Text = "数据库";
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.ImageIndex = 0;
            this.treeView.ImageList = this.imageList;
            this.treeView.Location = new System.Drawing.Point(15, 33);
            this.treeView.Margin = new System.Windows.Forms.Padding(0);
            this.treeView.Name = "treeView";
            this.treeView.SelectedImageIndex = 0;
            this.treeView.Size = new System.Drawing.Size(238, 602);
            this.treeView.TabIndex = 0;
            this.treeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseDoubleClick);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "file.png");
            this.imageList.Images.SetKeyName(1, "txt.png");
            this.imageList.Images.SetKeyName(2, "docx1.png");
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 71);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.leftBox);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.rightBox);
            this.splitContainerMain.Size = new System.Drawing.Size(1066, 650);
            this.splitContainerMain.SplitterDistance = 268;
            this.splitContainerMain.TabIndex = 8;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "csv文件|*.csv";
            // 
            // fileBtn
            // 
            this.fileBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFile,
            this.saveBtn,
            this.quit});
            this.fileBtn.Name = "fileBtn";
            this.fileBtn.Size = new System.Drawing.Size(71, 24);
            this.fileBtn.Text = "文件(&F)";
            // 
            // openFile
            // 
            this.openFile.Image = ((System.Drawing.Image)(resources.GetObject("openFile.Image")));
            this.openFile.Name = "openFile";
            this.openFile.Size = new System.Drawing.Size(144, 26);
            this.openFile.Text = "打开(&O)";
            this.openFile.Click += new System.EventHandler(this.txtBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(144, 26);
            this.saveBtn.Text = "保存(&S)";
            this.saveBtn.Click += new System.EventHandler(this.downBtn_Click);
            // 
            // quit
            // 
            this.quit.Name = "quit";
            this.quit.Size = new System.Drawing.Size(144, 26);
            this.quit.Text = "退出(&X)";
            // 
            // helpBtn
            // 
            this.helpBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testBtn});
            this.helpBtn.Name = "helpBtn";
            this.helpBtn.Size = new System.Drawing.Size(75, 24);
            this.helpBtn.Text = "帮助(&H)";
            // 
            // testBtn
            // 
            this.testBtn.Name = "testBtn";
            this.testBtn.Size = new System.Drawing.Size(224, 26);
            this.testBtn.Text = "测试导入";
            this.testBtn.Click += new System.EventHandler(this.openFile_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileBtn,
            this.helpBtn});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.menuStrip.Size = new System.Drawing.Size(1066, 34);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1066, 721);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.menusList);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pandora Tool";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.rightBox.ResumeLayout(false);
            this.splitContainerChild.Panel1.ResumeLayout(false);
            this.splitContainerChild.Panel1.PerformLayout();
            this.splitContainerChild.Panel2.ResumeLayout(false);
            this.splitContainerChild.ResumeLayout(false);
            this.tabAllInfo.ResumeLayout(false);
            this.tabPageInfo.ResumeLayout(false);
            this.tabPageInfo.PerformLayout();
            this.tabPageResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.menusList.ResumeLayout(false);
            this.menusList.PerformLayout();
            this.leftBox.ResumeLayout(false);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            this.splitContainerMain.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.GroupBox rightBox;
        private System.Windows.Forms.ToolStrip menusList;
        private System.Windows.Forms.ToolStripButton txtBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton delBtn;
        private System.Windows.Forms.ToolStripButton cleanBtn;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.GroupBox leftBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton moreFilter;
        private System.Windows.Forms.SplitContainer splitContainerChild;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.TabControl tabAllInfo;
        private System.Windows.Forms.TabPage tabPageInfo;
        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.TabPage tabPageResult;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ToolStripButton downBtn;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem fileBtn;
        private System.Windows.Forms.ToolStripMenuItem openFile;
        private System.Windows.Forms.ToolStripMenuItem saveBtn;
        private System.Windows.Forms.ToolStripMenuItem quit;
        private System.Windows.Forms.ToolStripMenuItem helpBtn;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem testBtn;
    }
}

