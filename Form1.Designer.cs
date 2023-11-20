namespace codeback {
    partial class 窗体 {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(窗体));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.文件列表树 = new System.Windows.Forms.TreeView();
            this.txt_key = new System.Windows.Forms.TextBox();
            this.查询 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.标题 = new System.Windows.Forms.TextBox();
            this.内容 = new System.Windows.Forms.RichTextBox();
            this._htmleditor = new WinHtmlEditor.HtmlEditor();
            this.关键字 = new System.Windows.Forms.TextBox();
            this.时间 = new System.Windows.Forms.Label();
            this.文件操作 = new System.Windows.Forms.Button();
            this.保存 = new System.Windows.Forms.Button();
            this.新增 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.下层 = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.关闭ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.file_listView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comm_out = new System.Windows.Forms.RichTextBox();
            this.运行 = new System.Windows.Forms.Button();
            this.删除 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.file_listView)).BeginInit();
            this.SuspendLayout();
            // 
            // 文件列表树
            // 
            this.文件列表树.AllowDrop = true;
            this.文件列表树.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.文件列表树.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.文件列表树.HideSelection = false;
            this.文件列表树.Location = new System.Drawing.Point(3, 30);
            this.文件列表树.Name = "文件列表树";
            this.文件列表树.Size = new System.Drawing.Size(165, 379);
            this.文件列表树.TabIndex = 3;
            this.文件列表树.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView1_ItemDrag);
            this.文件列表树.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.文件列表树.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView1_DragDrop);
            this.文件列表树.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeView1_DragEnter);
            // 
            // txt_key
            // 
            this.txt_key.AllowDrop = true;
            this.txt_key.Location = new System.Drawing.Point(3, 3);
            this.txt_key.Name = "txt_key";
            this.txt_key.Size = new System.Drawing.Size(124, 21);
            this.txt_key.TabIndex = 1;
            this.txt_key.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_key_KeyDown);
            // 
            // 查询
            // 
            this.查询.Location = new System.Drawing.Point(127, 1);
            this.查询.Name = "查询";
            this.查询.Size = new System.Drawing.Size(41, 23);
            this.查询.TabIndex = 2;
            this.查询.Text = "查询(&f)";
            this.查询.UseVisualStyleBackColor = true;
            this.查询.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.文件列表树);
            this.panel1.Controls.Add(this.查询);
            this.panel1.Controls.Add(this.txt_key);
            this.panel1.Location = new System.Drawing.Point(1, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(172, 415);
            this.panel1.TabIndex = 9;
            // 
            // 标题
            // 
            this.标题.AllowDrop = true;
            this.标题.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.标题.Location = new System.Drawing.Point(232, 5);
            this.标题.Multiline = true;
            this.标题.Name = "标题";
            this.标题.Size = new System.Drawing.Size(363, 25);
            this.标题.TabIndex = 4;
            // 
            // 内容
            // 
            this.内容.AcceptsTab = true;
            this.内容.AllowDrop = true;
            this.内容.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.内容.AutoWordSelection = true;
            this.内容.Location = new System.Drawing.Point(191, 79);
            this.内容.Name = "内容";
            this.内容.Size = new System.Drawing.Size(484, 311);
            this.内容.TabIndex = 6;
            this.内容.Text = "";
            this.内容.DragEnter += new System.Windows.Forms.DragEventHandler(this.内容_DragEnter);
            this.内容.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_txt_KeyDown);
            // 
            // _htmleditor
            // 
            this._htmleditor.AllowDrop = true;
            this._htmleditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._htmleditor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this._htmleditor.BodyInnerHTML = null;
            this._htmleditor.BodyInnerText = null;
            this._htmleditor.EnterToBR = false;
            this._htmleditor.FontSize = WinHtmlEditor.FontSize.Three;
            this._htmleditor.Location = new System.Drawing.Point(191, 79);
            this._htmleditor.Name = "_htmleditor";
            this._htmleditor.ShowStatusBar = false;
            this._htmleditor.ShowToolBar = true;
            this._htmleditor.ShowWb = true;
            this._htmleditor.Size = new System.Drawing.Size(484, 311);
            this._htmleditor.TabIndex = 62;
            this._htmleditor.WebBrowserShortcutsEnabled = true;
            this._htmleditor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_txt_KeyDown);
            // 
            // 关键字
            // 
            this.关键字.AllowDrop = true;
            this.关键字.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.关键字.Location = new System.Drawing.Point(237, 39);
            this.关键字.Multiline = true;
            this.关键字.Name = "关键字";
            this.关键字.Size = new System.Drawing.Size(215, 21);
            this.关键字.TabIndex = 5;
            this.关键字.Enter += new System.EventHandler(this.关键字Enter);
            this.关键字.Leave += new System.EventHandler(this.关键字Leave);
            // 
            // 时间
            // 
            this.时间.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.时间.AutoSize = true;
            this.时间.Location = new System.Drawing.Point(458, 48);
            this.时间.Name = "时间";
            this.时间.Size = new System.Drawing.Size(11, 12);
            this.时间.TabIndex = 13;
            this.时间.Text = " ";
            // 
            // 文件操作
            // 
            this.文件操作.Location = new System.Drawing.Point(540, 43);
            this.文件操作.Name = "文件操作";
            this.文件操作.Size = new System.Drawing.Size(55, 23);
            this.文件操作.TabIndex = 7;
            this.文件操作.Text = "打开(&o)";
            this.文件操作.UseVisualStyleBackColor = true;
            this.文件操作.Visible = false;
            this.文件操作.Click += new System.EventHandler(this.btn_open_Click);
            // 
            // 保存
            // 
            this.保存.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.保存.Location = new System.Drawing.Point(621, 396);
            this.保存.Name = "保存";
            this.保存.Size = new System.Drawing.Size(55, 23);
            this.保存.TabIndex = 9;
            this.保存.Text = "保存(&s)";
            this.保存.UseVisualStyleBackColor = true;
            this.保存.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // 新增
            // 
            this.新增.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.新增.Location = new System.Drawing.Point(511, 396);
            this.新增.Name = "新增";
            this.新增.Size = new System.Drawing.Size(55, 23);
            this.新增.TabIndex = 8;
            this.新增.Text = "新增(&n)";
            this.新增.UseVisualStyleBackColor = true;
            this.新增.Click += new System.EventHandler(this.btn_new_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(191, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "标题:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(189, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "关键字:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(191, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "内容:";
            // 
            // 下层
            // 
            this.下层.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.下层.Location = new System.Drawing.Point(566, 396);
            this.下层.Name = "下层";
            this.下层.Size = new System.Drawing.Size(55, 23);
            this.下层.TabIndex = 21;
            this.下层.Text = "下层(&b)";
            this.下层.UseVisualStyleBackColor = true;
            this.下层.Click += new System.EventHandler(this.btn_below_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关闭ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // 关闭ToolStripMenuItem
            // 
            this.关闭ToolStripMenuItem.Name = "关闭ToolStripMenuItem";
            this.关闭ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.关闭ToolStripMenuItem.Text = "关闭";
            this.关闭ToolStripMenuItem.Click += new System.EventHandler(this.关闭ToolStripMenuItem_Click);
            // 
            // file_listView
            // 
            this.file_listView.AllowDrop = true;
            this.file_listView.AllowUserToAddRows = false;
            this.file_listView.AllowUserToDeleteRows = false;
            this.file_listView.AllowUserToResizeColumns = false;
            this.file_listView.AllowUserToResizeRows = false;
            this.file_listView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.file_listView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.file_listView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.file_listView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.file_listView.Location = new System.Drawing.Point(599, 5);
            this.file_listView.Name = "file_listView";
            this.file_listView.RowHeadersVisible = false;
            this.file_listView.RowHeadersWidth = 60;
            this.file_listView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.file_listView.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.file_listView.RowTemplate.Height = 23;
            this.file_listView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.file_listView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.file_listView.Size = new System.Drawing.Size(76, 71);
            this.file_listView.TabIndex = 22;
            this.file_listView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.File_listViewRowsAdded);
            this.file_listView.DragDrop += new System.Windows.Forms.DragEventHandler(this.File_listViewDragDrop);
            this.file_listView.DragEnter += new System.Windows.Forms.DragEventHandler(this.File_listViewDragEnter);
            this.file_listView.DoubleClick += new System.EventHandler(this.File_listViewDoubleClick);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            this.Column1.FillWeight = 60F;
            this.Column1.HeaderText = "文件名称";
            this.Column1.MinimumWidth = 60;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 60;
            // 
            // comm_out
            // 
            this.comm_out.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comm_out.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.comm_out.Location = new System.Drawing.Point(189, 396);
            this.comm_out.Name = "comm_out";
            this.comm_out.ReadOnly = true;
            this.comm_out.Size = new System.Drawing.Size(206, 23);
            this.comm_out.TabIndex = 23;
            this.comm_out.Text = "";
            this.comm_out.Enter += new System.EventHandler(this.Comm_outEnter);
            this.comm_out.Leave += new System.EventHandler(this.Comm_outLeave);
            // 
            // 运行
            // 
            this.运行.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.运行.Location = new System.Drawing.Point(401, 396);
            this.运行.Name = "运行";
            this.运行.Size = new System.Drawing.Size(55, 23);
            this.运行.TabIndex = 24;
            this.运行.Text = "运行(&r)";
            this.运行.UseVisualStyleBackColor = true;
            this.运行.Click += new System.EventHandler(this.运行Click);
            // 
            // 删除
            // 
            this.删除.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.删除.Location = new System.Drawing.Point(452, 396);
            this.删除.Name = "删除";
            this.删除.Size = new System.Drawing.Size(61, 23);
            this.删除.TabIndex = 63;
            this.删除.Text = "删除";
            this.删除.UseVisualStyleBackColor = true;
            this.删除.Click += new System.EventHandler(this.删除_Click);
            // 
            // 窗体
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 421);
            this.Controls.Add(this.删除);
            this.Controls.Add(this.运行);
            this.Controls.Add(this.comm_out);
            this.Controls.Add(this.file_listView);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.下层);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.新增);
            this.Controls.Add(this.保存);
            this.Controls.Add(this.文件操作);
            this.Controls.Add(this.关键字);
            this.Controls.Add(this.时间);
            this.Controls.Add(this.标题);
            this.Controls.Add(this.内容);
            this.Controls.Add(this._htmleditor);
            this.Controls.Add(this.panel1);
            this.Name = "窗体";
            this.Text = "资料收集";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.file_listView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.Button 运行;
        private System.Windows.Forms.RichTextBox comm_out;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
   

        #endregion
        private System.Windows.Forms.DataGridView file_listView;
        private System.Windows.Forms.TreeView 文件列表树;
        private System.Windows.Forms.TextBox txt_key;
        private System.Windows.Forms.Button 查询;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox 标题;
        private System.Windows.Forms.RichTextBox 内容;
        private WinHtmlEditor.HtmlEditor  _htmleditor;
        private System.Windows.Forms.TextBox 关键字;
        private System.Windows.Forms.Label 时间;
        private System.Windows.Forms.Button 文件操作;
        private System.Windows.Forms.Button 保存; 
        private System.Windows.Forms.Button 新增;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button 下层;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 关闭ToolStripMenuItem;
        private System.Windows.Forms.Button 删除;
    }
}

