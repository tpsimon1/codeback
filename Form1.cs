using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Collections;
using System.Text;
using System.IO;
using System.Configuration;
using System.Diagnostics;
using System.Text.RegularExpressions;
using WinHtmlEditor;
using mshtml;
using mshtmlTextRange = mshtml.IHTMLTxtRange; 

namespace codeback
{
	public partial class ���� : Form
	{
		public int level = 0;
		//��ǰ�ȼ�
		public string top_code = "";
		//�����ĵ��ϲ���
		public string code = "";
		//�����ĵ��ڲ����
		public string[] fileurl;
		//�����ļ�·��,����
		public int nowdbnumber = 0;
		//��ǰdb���
		public int newdbnumber = 0;
		//��db���
		public int newrowtype = 0;
		//��������ģʽ 0���� 1 �²�

 
		public ����()
		{
			InitializeComponent();
			init_load();
            ((HTMLDocumentEvents2_Event)this._htmleditor.document).onmouseup += new HTMLDocumentEvents2_onmouseupEventHandler(_htmleditor_Click);

        }
		void init_load()
		{
			find_db();
			load_tree();
		}
		void find_db()//����db���ݿ⣬���θ���Ϊ�ֿ�
		{
			string newdb = Application.StartupPath + "\\" + DateTime.Now.ToString("yyyyMM") + ".db";
			FileInfo[] dbname = new System.IO.DirectoryInfo(Application.StartupPath).GetFiles(DateTime.Now.ToString("yyyyMM") + ".db", SearchOption.TopDirectoryOnly);
			if (dbname.Length == 0) {
				SQLiteHelper.CreateDB(newdb);
				SQLiteHelper.SetSQLiteHelper(newdb);
				SQLiteHelper.ExecuteNonQuery("create table t_filelist(fl_code varchar(200),fl_file blob,fl_name varchar(300));");
				SQLiteHelper.ExecuteNonQuery("create table t_txtlist(tl_code varchar(200)," +
				"tl_time date," +
				"tl_name varchar(500)," +
				"tl_topcode varchar(200)," +
				"tl_content varchar(5000)," +
				"tl_keyname varchar(500)," +
				"tl_level varchar(100)," +
				"tl_rtxtext WORD);");
			}
			dbname = new System.IO.DirectoryInfo(Application.StartupPath).GetFiles("*.db", SearchOption.TopDirectoryOnly);
			for (int i = 0; i < dbname.Length; i++) {
				if (dbname[i].FullName == newdb)
					newdbnumber = i;
			}
			SQLiteHelper.SetSQLiteHelper(dbname);
		}
		void load_tree()
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			DataTable dt = SQLiteHelper.ExecuteDataTable("select tl_code,tl_name,tl_topcode,tl_time from t_txtlist  order by tl_name");
			loop_tree(dt, 1);
			sw.Stop();
			Console.WriteLine(sw.Elapsed.Minutes + ":" + sw.Elapsed.Milliseconds);
		}
		void load_tree(string key)
		{
			
			StringBuilder buff = new StringBuilder();
			
			if (key.Length == 0) {
				load_tree();
				return;
			}
			string[] keytmp = key.Replace("\\ ", "##").Split(' ');  // ���ת��ո�
			List<SQLiteParameter> tmp1 = new List<SQLiteParameter>();
			
			for (int i = 0; i < keytmp.Length; i++) {
				buff.Append(" and ( tl_keyname like @temp" + i.ToString() + " or tl_name like @temp" + i.ToString() + " or tl_content like @temp" + i.ToString() + " )");
				tmp1.Add(new SQLiteParameter("temp" + i.ToString(), "%" + keytmp[i].Replace("##", " ") + "%"));
			}
			SQLiteHelper.SetSQLiteHelper(nowdbnumber);
			
			loop_tree(SQLiteHelper.ExecuteDataSet(
				"select tl_code,tl_name,tl_topcode,tl_time from t_txtlist where 1=1 " + buff.ToString() + "   order by tl_name", tmp1.ToArray()
			).Tables[0], 0);
			
		}
		
		void loop_tree(DataTable dt, double top)
		{
			�ļ��б���.BeginUpdate();
			�ļ��б���.Nodes.Clear(); 
			
			foreach (DataRow dr in  dt.Select((top > 0 ?"tl_topcode='0'":""))) {
				TreeNode rootNode = new TreeNode();
				rootNode.Name = dr["tl_code"].ToString();
				rootNode.Text = dr["tl_name"].ToString();
				rootNode.Tag = dr["db_number"].ToString();
				rootNode.ToolTipText = dr["tl_time"].ToString();
				
				var tempnode = �ļ��б���.Nodes.Find(rootNode.Name, false);
				
				if (tempnode.Length > 0) {
					if (Convert.ToDateTime(tempnode[0].ToolTipText) < Convert.ToDateTime(rootNode.ToolTipText))
						//�ļ��б���.Nodes[rootNode.Name].Tag = rootNode.Tag;
						tempnode[0].Tag= rootNode.Tag;


                } else
					�ļ��б���.Nodes.Add(rootNode);
				
				
				if (top != 0)
					loop_tree(rootNode, dt);
			}
			dt.Dispose();
			dt = null;
			�ļ��б���.EndUpdate();
			GC.Collect();
		}
		void loop_tree(TreeNode tn, DataTable dt)
		{
			foreach (DataRow dr in dt.Select("tl_topcode='" + tn.Name + "'","tl_name")) {
				TreeNode rootNode = new TreeNode();
				rootNode.Name = dr["tl_code"].ToString();
				rootNode.Text = dr["tl_name"].ToString();
				rootNode.Tag = dr["db_number"].ToString();
				rootNode.ToolTipText = dr["tl_time"].ToString();
				
				var tempnode = tn.Nodes.Find(rootNode.Name, true);
				
				if (tempnode.Length > 0) {
					if (Convert.ToDateTime(tempnode[0].ToolTipText) < Convert.ToDateTime(rootNode.ToolTipText))
						tempnode[0].Tag = rootNode.Tag;
					
				} else
					tn.Nodes.Add(rootNode);
				
				 
				loop_tree(rootNode, dt);
			}
		}
		string MaxNew()
		{
			
			SQLiteHelper.SetSQLiteHelper(nowdbnumber);
			return SQLiteHelper.ExecuteQueryScalar("select case when code is null then 1 else code end  from (select  max(strftime('%Y%m%d%H%M%S','now','localtime')+'" + nowdbnumber + "')+1 code from t_txtlist) table1 ").ToString();
		}
		private void btn_open_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog file = new OpenFileDialog()) {
				file.DereferenceLinks = true;
				file.Multiselect = true;
				//ȡ�������ļ���
				if (file.ShowDialog() == DialogResult.Cancel)
					return;
				fileurl = file.FileNames;
				file.Dispose();
			}
			
			if (code.Length == 0) {
				����.Text = fileurl[0].Substring(fileurl[0].LastIndexOf('\\') + 1);
				btn_save_Click(null, null);
			}
			save_file();
		}
		bool save_file()
		{
			try {
				if (code.Length == 0)
					MessageBox.Show("��ѡ����ڵ㣡");
				SQLiteHelper.SetSQLiteHelper(nowdbnumber);
				
				string _filename = "";
				bool rowind = false;
				foreach (string _fileurl in  fileurl) {
					_filename = _fileurl.Substring(_fileurl.LastIndexOf('\\') + 1);
					foreach (DataGridViewRow row in file_listView.Rows) {
						if (row.Cells[0].Value.ToString().Equals(_filename)) {
							rowind = true;
						}
					}
					if (!rowind) {
						file_listView.Rows.Add();
						file_listView.Rows[file_listView.Rows.Count - 1].Cells[0].Value = _filename;
					}
					
					SQLiteHelper.ExecuteNonQuery("delete from t_filelist where fl_code =" + code + " and fl_name='" + _filename + "';");
					FileStream fs = new FileStream(_fileurl, FileMode.Open, FileAccess.Read, FileShare.Read);
					byte[] data = new byte[fs.Length];
					fs.Read(data, 0, data.Length);
					fs.Close();
					if (!file.WriteData(code, data, _filename)) {
						MessageBox.Show("����ʧ��");
					}
				}
				return true;
			} catch {
				return false;
			} finally {
				GC.Collect();
			}

		}

		private void btn_new_Click(object sender, EventArgs e)
		{
			����.Text = "";
			ʱ��.Text = "";
			�ؼ���.Text = "";
			����.Text = "";
			_htmleditor.BodyInnerHTML = "";
			file_listView.Rows.Clear();
			code = "";
			newrowtype = 0;
			fileurl = null;
			����.Visible = true;
			_htmleditor.Visible = false;
			_htmleditor.ResetText();
		}

		private void btn_save_Click(object sender, EventArgs e)
		{
			try {
				bool newtype = false;
				if (MessageBox.Show("�Ƿ񱣴棿", "����", MessageBoxButtons.YesNo) == DialogResult.No)
					return;
				string sql = "";
				
				ʱ��.Text = System.DateTime.Now.ToString();
				if (code.Length == 0) {
					code = MaxNew();
					newtype = true;
					nowdbnumber = newdbnumber;
					sql = "insert into t_txtlist(tl_code ,tl_time ,tl_name ,tl_topcode ,tl_content ,tl_keyname,tl_level,tl_rtxtext) values( @code,datetime('now','localtime'), @tl_name, @top_code, @tl_content, @tl_keyname , @level,@rtxtext)";
				} else {
					if (nowdbnumber == newdbnumber)
						sql = "update t_txtlist set tl_time=datetime('now','localtime'), tl_name= @tl_name,tl_content = @tl_content,tl_keyname= @tl_keyname,tl_level= @level,tl_rtxtext = @rtxtext where tl_code= @code";
					else {
						sql = "insert into t_txtlist(tl_code ,tl_time ,tl_name ,tl_topcode ,tl_content ,tl_keyname,tl_level,tl_rtxtext) values( @code,datetime('now','localtime'), @tl_name, @top_code, @tl_content, @tl_keyname , @level,@rtxtext)";
						nowdbnumber = newdbnumber;
					}
				}
				
				SQLiteHelper.SetSQLiteHelper(nowdbnumber);
				
				var htmltotext = new HtmlToText();
				string tmphtml ="",_relimg = "";
				if (_htmleditor.Visible)
				{
					tmphtml = _htmleditor.BodyInnerHTML; 
					_relimg = ImageRelpace.ImageReplace(tmphtml);
				}
                if (SQLiteHelper.ExecuteNonQuery(sql, new SQLiteParameter[] {
					new SQLiteParameter("code", code),
					new SQLiteParameter("tl_name", ����.Text),
					new SQLiteParameter("top_code", top_code),
					new SQLiteParameter("tl_content", ����.Visible ? ����.Text : htmltotext.Convert(tmphtml)),
					new SQLiteParameter("tl_keyname", �ؼ���.Text),
					new SQLiteParameter("level", level),
					new SQLiteParameter("rtxtext",
						
						����.Visible ? control.CompressString(����.Rtf) : control.CompressString(_relimg)
					)
				}) < 0) {
					MessageBox.Show("����ʧ��");
					return;
				}


				if (newtype && top_code != "0" && newrowtype == 0)
					�ļ��б���.SelectedNode.Parent.Nodes.Add(code, ����.Text);
				else if (newtype && top_code != "0" && newrowtype == 1)
					�ļ��б���.SelectedNode.Nodes.Add(code, ����.Text);
				else if (newtype && top_code == "0")
					�ļ��б���.Nodes.Add(code, ����.Text);
				else
					�ļ��б���.Nodes.Find(code, true)[0].Text = ����.Text;
				
				�ļ��б���.Nodes.Find(code, true)[0].Tag = nowdbnumber;
				
				�ļ��б���.SelectedNode = �ļ��б���.Nodes.Find(code, true)[0];
				
			} catch (Exception ex) {
				MessageBox.Show("����ʧ��" + ex.Message);
			} finally {
				GC.Collect(1);
			}
		}

		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			code = �ļ��б���.SelectedNode.Name;
			if (�ļ��б���.SelectedNode.Level == 0)
				top_code = "0";
			else
				top_code = �ļ��б���.SelectedNode.Parent.Name;
			level = �ļ��б���.SelectedNode.Level;
			nowdbnumber = Convert.ToInt32(�ļ��б���.SelectedNode.Tag);
			find_node();
		}
		void find_node()
		{
			SQLiteHelper.SetSQLiteHelper(nowdbnumber);
			using (SQLiteDataReader dr = SQLiteHelper.ExecuteDataReader("select * from t_txtlist where tl_code ='" + code + "';")) {
				if (dr.Read()) {
					����.Text = dr["tl_name"].ToString();
					�ؼ���.Text = dr["tl_keyname"].ToString();
					����.Text = null;
					
					String _tmp = control.DecompressString(dr["tl_rtxtext"].ToString());
					
					if (_tmp.Trim().StartsWith(@"{\rtf")) {
						����.Rtf = _tmp;
						_htmleditor.Visible = false;
						����.Visible = true;
					} else if (_tmp.Trim().StartsWith(@"<")) {
						_htmleditor.BodyInnerHTML =	_tmp;
						_htmleditor.Visible = true;
						����.Visible = false;
					} else {
						����.AppendText(
							dr["tl_content"].ToString()
						);
						_htmleditor.Visible = false;
						����.Visible = true;
					}
					 
					
					foreach (string tmp in txt_key.Text.Replace("\\ ","#|#").Split(' ')) {
						if (!tmp.IsNullOrEmpty()) {
							if (����.Visible) {
								MatchCollection mc = Regex.Matches(����.Text, tmp.Replace("#|#", " "), RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
								foreach (Match var in mc) {
									����.Select(var.Index, tmp.Length);
									����.SelectionBackColor = Color.Yellow;
								}
							} else { 
								_htmleditor.FindFirst(tmp);
							}
						}
					}
					
					ʱ��.Text = dr["tl_time"].ToString();
					if (����.Visible)
						����.Width = ����.ActiveForm.Width - panel1.Width - 31;
					else
						_htmleditor.Width = ����.ActiveForm.Width - panel1.Width - 31;
					file_listView.Height = 71;
					file_listView.Rows.Clear();
					using (SQLiteDataReader drt = SQLiteHelper.ExecuteDataReader("select fl_name from t_filelist where fl_code='" + code + "' ;")) {
						while (drt.Read()) {
							file_listView.Rows.Add();
							file_listView.Rows[file_listView.Rows.Count - 1].Cells[0].Value = drt["fl_name"].ToString();
						}
						drt.Close();
					}
				}
				dr.Close();
			}
		}
		private void btn_search_Click(object sender, EventArgs e)
		{
			load_tree(txt_key.Text);
			btn_new_Click(null, null);
		}

		private void treeView1_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetData(typeof(TreeNode)) != null) {
				this.�ļ��б���.SelectedNode = ((TreeNode)e.Data.GetData(typeof(TreeNode)));
				
				e.Effect = DragDropEffects.Move;
			} else
				e.Effect = DragDropEffects.None;
			
		}

		private void treeView1_DragDrop(object sender, DragEventArgs e)
		{
			SQLiteHelper.SetSQLiteHelper(nowdbnumber);
			TreeNode tn = (TreeNode)e.Data.GetData(typeof(TreeNode));
			TreeNode ty = �ļ��б���.GetNodeAt(�ļ��б���.PointToClient(new Point(e.X, e.Y)));
			if (ty != null && tn != ty && !find_childnode(ty, tn) && SQLiteHelper.ExecuteNonQuery("update t_txtlist set tl_topcode='" + ty.Name + "' where tl_code='" + tn.Name + "';") > 0) {
				tn.Remove();
				ty.Nodes.Add(tn);
				this.�ļ��б���.SelectedNode = ty;
			}
		}

		bool find_childnode(TreeNode tn, TreeNode ty)
		{
			if (ty.Parent == null)
				return false;

			if (ty.Parent != tn)
				find_childnode(tn, ty.Parent);
			else
				return true;
			return false;
		}

		private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
		{
			DoDragDrop(e.Item, DragDropEffects.Move);
		}

		private void btn_below_Click(object sender, EventArgs e)
		{
			����.Text = "";
			ʱ��.Text = "";
			�ؼ���.Text = "";
			����.Text = "";
			file_listView.Rows.Clear();
			top_code = code;
			level = level + 1;
			newrowtype = 1;
			code = "";
			fileurl = null;
		}

		private void txt_key_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
				btn_search_Click(null, null);
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing) {
				this.Hide();
				e.Cancel = true;
				return;
			} else
				e.Cancel = false;

			this.Dispose(true);
			GC.Collect();
		}

		private void txt_txt_KeyDown(object sender, KeyEventArgs e)
		{ 
			//ctrl+A
			if (e.KeyValue == 65 && e.Control) {
				����.SelectAll();
				e.Handled = true;
				return;
			}

			//ctrl+V
			if (e.KeyValue == 86 && e.Control) {
				
								
				if (Clipboard.ContainsText(TextDataFormat.Html) ){
					
				
					if (MessageBox.Show("�Ƿ�ҪHTMLת��?", "ת��", MessageBoxButtons.YesNo) == DialogResult.No) {
						����.Paste(DataFormats.GetFormat(DataFormats.Text));
						return;
					} 
					 
					����.Visible = false;
					_htmleditor.Visible = true;
					_htmleditor.Paste();
                    _htmleditor.Focus();

                } else {
					����.Paste();
					����.Visible = true;
					����.Focus();

                    _htmleditor.Visible = false;
				}
				e.Handled = true;
				return;
			}
		}

        private void _htmleditor_Click(IHTMLEventObj obj)
        {
           
            _htmleditor.Focus();
            Comm_outLeave(new object(), null);
            �ؼ���Leave(new object(), null);
        }

        private void html_KeyDown(object sender, KeyEventArgs e)
        {
			
            //ctrl+A
            if (e.KeyValue == 65 && e.Control)
            {
                ����.SelectAll();
                e.Handled = true;
                return;
            }

            //ctrl+V
            if (e.KeyValue == 86 && e.Control)
            {


                if (Clipboard.ContainsText(TextDataFormat.Html))
                {


                    if (MessageBox.Show("�Ƿ�ҪHTMLת��?", "ת��", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        ����.Paste(DataFormats.GetFormat(DataFormats.Text));
                        return;
                    }

                    ����.Visible = false;
                    _htmleditor.Visible = true;
                    _htmleditor.Paste();

                }
                else
                {
                    ����.Paste();
                    ����.Visible = true;
                    _htmleditor.Visible = false;
                }
                e.Handled = true;
                return;
            }
        }

        private void ����_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetData(typeof(String)) != null) {
				����.Text += (string)e.Data.GetData(typeof(String));
			}
		}

		private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.Show();
			if (this.WindowState == FormWindowState.Minimized)
				this.WindowState = FormWindowState.Normal;
			else if (this.WindowState == FormWindowState.Normal)
				this.WindowState = FormWindowState.Minimized;
			
			this.Activate(); //����岢Ϊ�丳�轹��

		}

		private void �ر�ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Dispose();
			GC.Collect();
		}


		private void Form1_Resize(object sender, System.EventArgs e)
		{
			//	if (this.WindowState == FormWindowState.Minimized)
			//	{
			//		this.Hide();
			//	}
		}

		
		void File_listViewDoubleClick(object sender, EventArgs e)
		{
			SQLiteHelper.SetSQLiteHelper(nowdbnumber);
			SaveFileDialog sfd = new SaveFileDialog();
			if (file_listView.SelectedCells == null)
				return;

			string filename = (string)file_listView.SelectedCells[0].Value;
			sfd.FileName = filename;
			if (sfd.ShowDialog() != DialogResult.OK)
				return;
			sfd.Dispose();
			using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create)) {
				byte[] result = file.ReadData(code, filename);
				fs.Write(result, 0, result.Length);
				fs.Close();
			}
		}
		
		void File_listViewDragDrop(object sender, DragEventArgs e)
		{
			if (code.Length == 0) {
				MessageBox.Show("��ѡ���㣡");
				return;
			}
			//e.Effect = DragDropEffects.None;
			SQLiteHelper.SetSQLiteHelper(nowdbnumber);
			
			fileurl = ((string[])e.Data.GetData(DataFormats.FileDrop));
			save_file();
			
		}
		
		void File_listViewDragEnter(object sender, DragEventArgs e)
		{
			e.Effect = e.AllowedEffect;

		}
		
		void File_listViewRowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			file_listView.Height = (file_listView.Rows.Count + 1) * 24;
			if (file_listView.Height + file_listView.Top > ����.Top) {
				����.Width = ����.ActiveForm.Width - panel1.Width - file_listView.Width - 44;
				_htmleditor.Width = ����.ActiveForm.Width - panel1.Width - file_listView.Width - 44;

            }
		}



		void Comm_outLeave(object sender, EventArgs e)
		{
			if (comm_out.Height != 23)
			{
				comm_out.Height = 23;
				comm_out.Top = comm_out.Top + 200;
			}
		}
		
		public void Comm_outEnter(object sender, EventArgs e)
		{
			comm_out.Height = comm_out.Height + 200;
			comm_out.Top = comm_out.Top - 200;
		}
		
		void ����Click(object sender, EventArgs e)
		{
			try {
				
				����.Text  = (����.Text== "����(&r)" ? "ֹͣ": "����(&r)");
				string exec_content = "";
				
				if (����.Visible && ����.SelectedText.Length > 0) {
				exec_content = ����.SelectedText;
				} else if (����.Visible && ����.SelectedText.Length == 0)
				exec_content = ����.Text;
				else if (!����.Visible) {
				mshtmlTextRange range = (mshtmlTextRange)_htmleditor.document.selection.createRange();
				HtmlToText convert = new HtmlToText(); 
				if (range.IsNull())
				exec_content = convert.Convert(_htmleditor.BodyInnerHTML);
				else
				exec_content = convert.Convert(range.htmlText); 
				}

                Hashtable key_ht = new Hashtable();

				foreach (String key1 in �ؼ���.Text.Split(';'))
				{
					string[] tmp = key1.Split('=');
					if (tmp.Length > 1)
					{
						key_ht.Add(tmp[0], tmp[1]);
					}
				}


                if (key_ht["lang"] != null)
                {
					ScriptRun.Run(key_ht, exec_content, comm_out,����);
                }





            } catch (Exception ex) {
				
				comm_out.Text = ex.Message;
			} finally {
				GC.Collect(1);
			}
		}
		
		void �ؼ���Leave(object sender, EventArgs e)
		{
			 if (�ؼ���.Height != 21)
			�ؼ���.Height = 21;
		}
		
		void �ؼ���Enter(object sender, EventArgs e)
		{
			�ؼ���.Height = comm_out.Height + 200;
		}

        private void ɾ��_Click(object sender, EventArgs e)
        {
            bool newtype = false;

			if(this.�ļ��б���.SelectedNode is null )
			{
                MessageBox.Show("����ѡ��һ���ڵ�");
                return;

            }

            if (MessageBox.Show("�Ƿ�ɾ����", "ɾ��", MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            string sql  = "update t_txtlist set tl_topcode='-1'  where tl_code= @code";


            SQLiteHelper.SetSQLiteHelper(nowdbnumber); 

            if (SQLiteHelper.ExecuteNonQuery(sql, new SQLiteParameter[] {
                    new SQLiteParameter("code", code)
                    
                }) < 0)
            {
                MessageBox.Show("����ʧ��");
                return;
            }
			this.�ļ��б���.SelectedNode.Remove();
        }
    }
}
