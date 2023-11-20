//==============================================
//
//        Copyright(C) 2009-2010 ����SoftWare������
//        All Rights Reserved
//
//        FileName: SQLiteHelper
//        Description:
//
//          Author: Wang Lian Lin(������)
//          CLR�汾: 2.0.50727.42
//        MachineName: WLL
//          ע����֯��: WLL
//        Created By Wang Lian Lin(������) at 2009-3-12 0:57:49
//        Email: LianLin.Wang@163.com
//        http://chnboy.cnblogs.com
//
//==============================================
using System;
using System.Data;
using System.Configuration;
using System.Data.SQLite;
using System.IO;
/// <summary>
/// SQLiteHelper ��ժҪ˵��
/// </summary>
public  static class SQLiteHelper {
	//   System.Configuration.ConfigurationSettings.AppSettings["SQLiteConnectString"] = "Data Source=" + Application.ExecutablePath + @"/codeback.db";
	static string SQLiteConnectionString;
	static FileInfo[] DBList;
	//���������ݿ�SQLite�������ַ���д����"Data Source=D:\database\test.s3db"
	//���������ݿ�SQLite�ļ��ܺ�������ַ���д����"Data Source=D:"database\test.s3db;Version=3;Password=���SQLite���ݿ�����;"
	public static void SetSQLiteHelper(FileInfo[] DBName) {
		DBList =	DBName;
	}
	public static void SetSQLiteHelper(int DBNumber) {
		if (DBList==null && DBList == null  )
			throw new Exception("δ���������б��Ҫ����б��������ļ�������") ;
		SetSQLiteHelper(DBList[DBNumber].FullName);
	}
	
	public static void SetSQLiteHelper(string DBName) {
		SQLiteConnectionString =  "Data Source=" +DBName;
	}
	
	
	public static void CreateDB(string DBName)
	{
		SQLiteConnection.CreateFile(DBName);
	}
	#region ExecuteNonQuery

	/// <summary>
	/// ������ִ��Transact-SQL��䲢������Ӱ�������
	/// </summary>
	/// <param name="commandText">SQL����洢������</param>
	/// <param name="isProcedure">��һ�������Ƿ�Ϊ�洢������,trueΪ��,falseΪ��</param>
	/// <param name="paras">SQLiteParameter�����б�0����������</param>
	/// <returns></returns>
	public static int ExecuteNonQuery(string commandText, bool isProcedure, params SQLiteParameter[] paras) {
		
		SQLiteConnection con = new SQLiteConnection(SQLiteHelper.SQLiteConnectionString);
		SQLiteCommand cmd = new SQLiteCommand(commandText, con);

		if (isProcedure) {
			cmd.CommandType = CommandType.StoredProcedure;
		}
		else {
			cmd.CommandType = CommandType.Text;
		}

		cmd.Parameters.Clear();
		foreach (SQLiteParameter para in paras) {
			cmd.Parameters.Add(para);
		}

		try {
			con.Open();
			int i =cmd.ExecuteNonQuery();
			return i;
		}
		finally {
			con.Close();
		}
	}

	/// <summary>
	/// ������ִ��Transact-SQL��䲢������Ӱ�������
	/// </summary>
	/// <param name="commandText">SQL���</param>
	/// <param name="paras">SQLiteParameter�����б�0����������</param>
	/// <returns></returns>
	public static int ExecuteNonQuery(string commandText, params SQLiteParameter[] paras) {
		return ExecuteNonQuery(commandText, false, paras);
	}
	/// <summary>
	/// ������ִ��Transact-SQL��䲢������Ӱ�������
	/// </summary>
	/// <param name="commandText">SQL���</param>
	/// <returns></returns>
	public static int ExecuteNonQuery(string commandText) {
		return ExecuteNonQuery(commandText, false, new SQLiteParameter[] { });
	}
	/// <summary>
	/// ������ִ��Transact-SQL��䲢������Ӱ�������
	/// </summary>
	/// <param name="trans">�����������</param>
	/// <param name="commandText">SQL����洢������</param>
	/// <param name="isProcedure">�ڶ��������Ƿ�Ϊ�洢������,trueΪ��,falseΪ��</param>
	/// <param name="paras">SQLiteParameter�����б�0����������</param>
	/// <returns></returns>
	public static int ExecuteNonQuery(SQLiteTransaction trans, string commandText, bool isProcedure, params SQLiteParameter[] paras) {

		SQLiteConnection con = trans.Connection;
		SQLiteCommand cmd = new SQLiteCommand(commandText, con);

		if (isProcedure) {
			cmd.CommandType = CommandType.StoredProcedure;
		}
		else {
			cmd.CommandType = CommandType.Text;
		}

		cmd.Parameters.Clear();
		foreach (SQLiteParameter para in paras) {
			cmd.Parameters.Add(para);
		}

		if (trans != null) {
			cmd.Transaction = trans;
		}

		try {
			if (con.State != ConnectionState.Open) {
				con.Open();
			}
			return cmd.ExecuteNonQuery();
		}

		finally {
			if (trans == null) {
				con.Close();
			}
		}
	}

	/// <summary>
	/// ������ִ��Transact-SQL��䲢������Ӱ�������
	/// </summary>
	/// <param name="trans">�����������</param>
	/// <param name="commandText">SQL���</param>
	/// <param name="paras">SQLiteParameter�����б�0����������</param>
	/// <returns></returns>
	public static int ExecuteNonQuery(SQLiteTransaction trans, string commandText, params SQLiteParameter[] paras) {
		return ExecuteNonQuery(trans, commandText, false, paras);
	}

	#endregion

	#region ExecuteQueryScalar

	/// <summary>
	/// ִ�в�ѯ�������ز�ѯ������еĵ�һ�е�һ�У����������л���
	/// </summary>
	/// <param name="commandText">SQL����洢������</param>
	/// <param name="isProcedure">��һ�������Ƿ�Ϊ�洢������,trueΪ��,falseΪ��</param>
	/// <param name="paras">SQLiteParameter�����б�0����������</param>
	/// <returns></returns>
	public static object ExecuteQueryScalar(string commandText, bool isProcedure, params SQLiteParameter[] paras) {
		
		if(SQLiteConnectionString==null)
			throw new Exception("����ȷ��ĳ�����ݿ�") ;
		SQLiteConnection con = new SQLiteConnection(SQLiteHelper.SQLiteConnectionString);
		SQLiteCommand cmd = new SQLiteCommand(commandText, con);

		if (isProcedure) {
			cmd.CommandType = CommandType.StoredProcedure;
		}
		else {
			cmd.CommandType = CommandType.Text;
		}

		foreach (SQLiteParameter para in paras) {
			cmd.Parameters.Add(para);
		}

		try {
			con.Open();
			return cmd.ExecuteScalar();
		}
		finally {
			con.Close();
		}
	}

	/// <summary>
	/// ִ�в�ѯ�������ز�ѯ������еĵ�һ�е�һ�У����������л���
	/// </summary>
	/// <param name="commandText">SQL���</param>
	/// <param name="paras">SQLiteParameter�����б�0����������</param>
	/// <returns></returns>
	public static object ExecuteQueryScalar(string commandText, params SQLiteParameter[] paras) {
		return ExecuteQueryScalar(commandText, false, paras);
	}
	/// <summary>
	/// ִ�в�ѯ�������ز�ѯ������еĵ�һ�е�һ�У����������л���
	/// </summary>
	/// <param name="commandText">SQL���</param>
	/// <returns></returns>
	public static object ExecuteQueryScalar(string commandText) {
		return ExecuteQueryScalar(commandText, false, new SQLiteParameter[] { });
	}
	/// <summary>
	/// ִ�в�ѯ�������ز�ѯ������еĵ�һ�е�һ�У����������л���
	/// </summary>
	/// <param name="trans">�����������</param>
	/// <param name="commandText">SQL����洢������</param>
	/// <param name="isProcedure">�ڶ��������Ƿ�Ϊ�洢������,trueΪ��,falseΪ��</param>
	/// <param name="paras">SQLiteParameter�����б�0����������</param>
	/// <returns></returns>
	public static object ExecuteQueryScalar(SQLiteTransaction trans, string commandText, bool isProcedure, params SQLiteParameter[] paras) {
		SQLiteConnection con = trans.Connection;
		SQLiteCommand cmd = new SQLiteCommand(commandText, con);

		if (isProcedure) {
			cmd.CommandType = CommandType.StoredProcedure;
		}
		else {
			cmd.CommandType = CommandType.Text;
		}
		cmd.Parameters.Clear();
		foreach (SQLiteParameter para in paras) {
			cmd.Parameters.Add(para);
		}

		if (trans != null) {
			cmd.Transaction = trans;
		}

		try {
			if (con.State != ConnectionState.Open) {
				con.Open();
			}
			return cmd.ExecuteScalar();
		}
		finally {
			if (trans == null) {
				con.Close();
			}
		}
	}

	/// <summary>
	/// ִ�в�ѯ�������ز�ѯ������еĵ�һ�е�һ�У����������л���
	/// </summary>
	/// <param name="trans">�����������</param>
	/// <param name="commandText">SQL���</param>
	/// <param name="paras">SQLiteParameter�����б�0����������</param>
	/// <returns></returns>
	public static object ExecuteQueryScalar(SQLiteTransaction trans, string commandText, params SQLiteParameter[] paras) {
		return ExecuteQueryScalar(trans, commandText, false, paras);
	}

	#endregion

	#region ExecuteDataReader

	/// <summary>
	/// ִ��SQL�������ؽ������ֻǰ�����ݶ�ȡ��
	/// </summary>
	/// <param name="commandText">SQL����洢������</param>
	/// <param name="isProcedure">��һ�������Ƿ�Ϊ�洢������,trueΪ��,falseΪ��</param>
	/// <param name="paras">SQLiteParameter�����б�0����������</param>
	/// <returns></returns>
	public static SQLiteDataReader ExecuteDataReader(string commandText, bool isProcedure, params SQLiteParameter[] paras) {
		
		if(SQLiteConnectionString==null)
			throw new Exception("����ȷ��ĳ�����ݿ�") ;
		
		SQLiteConnection con = new SQLiteConnection(SQLiteHelper.SQLiteConnectionString);
		SQLiteCommand cmd = new SQLiteCommand(commandText, con);

		if (isProcedure) {
			cmd.CommandType = CommandType.StoredProcedure;
		}
		else {
			cmd.CommandType = CommandType.Text;
		}

		foreach (SQLiteParameter para in paras) {
			cmd.Parameters.Add(para);
		}

		try {
			if (con.State != ConnectionState.Open) {
				con.Open();
			}
			return cmd.ExecuteReader(CommandBehavior.CloseConnection);
		}
		catch {
			con.Close();
			throw;
		}

	}

	/// <summary>
	/// ִ��SQL�������ؽ������ֻǰ�����ݶ�ȡ��
	/// </summary>
	/// <param name="commandText">SQL���</param>
	/// <param name="paras">SQLiteParameter�����б�0����������</param>
	/// <returns></returns>
	public static SQLiteDataReader ExecuteDataReader(string commandText, params SQLiteParameter[] paras) {
		return ExecuteDataReader(commandText, false, paras);
	}

	/// <summary>
	/// ִ��SQL�������ؽ������ֻǰ�����ݶ�ȡ��
	/// </summary>
	/// <param name="commandText">SQL���</param>
	/// <param name="paras">SQLiteParameter�����б�0����������</param>
	/// <returns></returns>
	public static SQLiteDataReader ExecuteDataReader(string commandText) {
		return ExecuteDataReader(commandText, false, new SQLiteParameter[] { });
	}

	/// <summary>
	/// ִ��SQL�������ؽ������ֻǰ�����ݶ�ȡ��
	/// </summary>
	/// <param name="trans">�����������</param>
	/// <param name="commandText">SQL����洢������</param>
	/// <param name="isProcedure">�ڶ��������Ƿ�Ϊ�洢������,trueΪ��,falseΪ��</param>
	/// <param name="paras">SQLiteParameter�����б�0����������</param>
	/// <returns></returns>
	public static SQLiteDataReader ExecuteDataReader(SQLiteTransaction trans, string commandText, bool isProcedure, params SQLiteParameter[] paras) {
		SQLiteConnection con = trans.Connection;
		SQLiteCommand cmd = new SQLiteCommand(commandText, con);

		if (isProcedure) {
			cmd.CommandType = CommandType.StoredProcedure;
		}
		else {
			cmd.CommandType = CommandType.Text;
		}

		cmd.Parameters.Clear();
		foreach (SQLiteParameter para in paras) {
			cmd.Parameters.Add(para);
		}

		if (trans != null) {
			cmd.Transaction = trans;
		}

		try {
			if (con.State != ConnectionState.Open) {
				con.Open();
			}
			return cmd.ExecuteReader(CommandBehavior.CloseConnection);
		}
		catch {
			if (trans == null) {
				con.Close();
			}
			throw;
		}

	}

	/// <summary>
	/// ִ��SQL�������ؽ������ֻǰ�����ݶ�ȡ��
	/// </summary>
	/// <param name="trans">�����������</param>
	/// <param name="commandText">SQL���</param>
	/// <param name="paras">SQLiteParameter�����б�0����������</param>
	/// <returns></returns>
	public static SQLiteDataReader ExecuteDataReader(SQLiteTransaction trans, string commandText, params SQLiteParameter[] paras) {
		return ExecuteDataReader(trans, commandText, false, paras);
	}

	#endregion

	#region ExecuteDataSet

	/// <summary>
	/// ִ��SQL��������DataSet�����
	/// </summary>
	/// <param name="commandText">SQL����洢������</param>
	/// <param name="isProcedure">��һ�������Ƿ�Ϊ�洢������,trueΪ��,falseΪ��</param>
	/// <param name="paras">SQLiteParameter�����б�0����������</param>
	/// <returns></returns>
	public static DataSet ExecuteDataSet(string commandText, bool isProcedure, params SQLiteParameter[] paras) {
		
		if(SQLiteConnectionString==null && DBList == null)
			throw new Exception("����ȷ��ĳ�����ݿ�") ;
		DataSet ds = new DataSet();
		for ( int i =0 ; i<DBList.Length;i++) {
			SetSQLiteHelper(i);
			SQLiteConnection con = new SQLiteConnection(SQLiteHelper.SQLiteConnectionString);
			SQLiteCommand cmd = new SQLiteCommand(commandText, con);

			if (isProcedure) {
				cmd.CommandType = CommandType.StoredProcedure;
			}
			else {
				cmd.CommandType = CommandType.Text;
			}

			foreach (SQLiteParameter para in paras) {
				cmd.Parameters.Add(para);
			}

			try {
				using (DataSet tmp = new DataSet())
				{
					con.Open();
					SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
					adapter.Fill(tmp);
					DataColumn dc=new DataColumn();
					dc.ColumnName="db_number";
					dc.DataType =System.Type.GetType("System.Int32");
					dc.DefaultValue = i;
					tmp.Tables[0].Columns.Add(dc);
					if (i==0)
					 ds = tmp;
					else
					ds.Merge(tmp, false, MissingSchemaAction.Ignore);
				}
			} 
			finally {
				con.Close();
			}
		} 
		return ds;
	}
	

	/// <summary>
	/// ִ��SQL��������DataSet�����
	/// </summary>
	/// <param name="commandText">SQL���</param>
	/// <param name="paras">SQLiteParameter�����б�0����������</param>
	/// <returns></returns>
	public static DataSet ExecuteDataSet(string commandText, params SQLiteParameter[] paras) {
		return ExecuteDataSet(commandText, false, paras);
	}

	/// <summary>
	/// ִ��SQL��������DataTable�����
	/// </summary>
	/// <param name="commandText">SQL���</param>
	/// <returns></returns>
	public static DataTable ExecuteDataTable(string commandText) {
		return ExecuteDataSet(commandText, false, new SQLiteParameter[] { }).Tables[0];
	}

	/// <summary>
	/// ִ��SQL��������DataSet�����
	/// </summary>
	/// <param name="trans">�����������</param>
	/// <param name="commandText">SQL����洢������</param>
	/// <param name="isProcedure">�ڶ��������Ƿ�Ϊ�洢������,trueΪ��,falseΪ��</param>
	/// <param name="paras">SQLiteParameter�����б�0����������</param>
	/// <returns></returns>
	public static DataSet ExecuteDataSet(SQLiteTransaction trans, string commandText, bool isProcedure, params SQLiteParameter[] paras) {
		SQLiteConnection con = trans.Connection;
		SQLiteCommand cmd = new SQLiteCommand(commandText, con);

		if (isProcedure) {
			cmd.CommandType = CommandType.StoredProcedure;
		}
		else {
			cmd.CommandType = CommandType.Text;
		}

		cmd.Parameters.Clear();
		foreach (SQLiteParameter para in paras) {
			cmd.Parameters.Add(para);
		}

		if (trans != null) {
			cmd.Transaction = trans;
		}

		try {
			if (con.State != ConnectionState.Open) {
				con.Open();
			}
			SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
			DataSet ds = new DataSet();
			adapter.Fill(ds);
			return ds;
		}
		finally {
			if (trans == null) {
				con.Close();
			}
		}
	}

	/// <summary>
	/// ִ��SQL��������DataSet�����
	/// </summary>
	/// <param name="trans">�����������</param>
	/// <param name="commandText">SQL���</param>
	/// <param name="paras">SQLiteParameter�����б�0����������</param>
	/// <returns></returns>
	public static DataSet ExecuteDataSet(SQLiteTransaction trans, string commandText, params SQLiteParameter[] paras) {
		return ExecuteDataSet(trans, commandText, false, paras);
	}

	#endregion
}
