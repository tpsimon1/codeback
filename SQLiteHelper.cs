//==============================================
//
//        Copyright(C) 2009-2010 连林SoftWare工作室
//        All Rights Reserved
//
//        FileName: SQLiteHelper
//        Description:
//
//          Author: Wang Lian Lin(王连林)
//          CLR版本: 2.0.50727.42
//        MachineName: WLL
//          注册组织名: WLL
//        Created By Wang Lian Lin(王连林) at 2009-3-12 0:57:49
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
/// SQLiteHelper 的摘要说明
/// </summary>
public  static class SQLiteHelper {
	//   System.Configuration.ConfigurationSettings.AppSettings["SQLiteConnectString"] = "Data Source=" + Application.ExecutablePath + @"/codeback.db";
	static string SQLiteConnectionString;
	static FileInfo[] DBList;
	//轻量级数据库SQLite的连接字符串写法："Data Source=D:\database\test.s3db"
	//轻量级数据库SQLite的加密后的连接字符串写法："Data Source=D:"database\test.s3db;Version=3;Password=你的SQLite数据库密码;"
	public static void SetSQLiteHelper(FileInfo[] DBName) {
		DBList =	DBName;
	}
	public static void SetSQLiteHelper(int DBNumber) {
		if (DBList==null && DBList == null  )
			throw new Exception("未设置数据列表或要求的列表数超过文件数据量") ;
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
	/// 对连接执行Transact-SQL语句并返回受影响的行数
	/// </summary>
	/// <param name="commandText">SQL语句或存储过程名</param>
	/// <param name="isProcedure">第一个参数是否为存储过程名,true为是,false为否</param>
	/// <param name="paras">SQLiteParameter参数列表，0个或多个参数</param>
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
	/// 对连接执行Transact-SQL语句并返回受影响的行数
	/// </summary>
	/// <param name="commandText">SQL语句</param>
	/// <param name="paras">SQLiteParameter参数列表，0个或多个参数</param>
	/// <returns></returns>
	public static int ExecuteNonQuery(string commandText, params SQLiteParameter[] paras) {
		return ExecuteNonQuery(commandText, false, paras);
	}
	/// <summary>
	/// 对连接执行Transact-SQL语句并返回受影响的行数
	/// </summary>
	/// <param name="commandText">SQL语句</param>
	/// <returns></returns>
	public static int ExecuteNonQuery(string commandText) {
		return ExecuteNonQuery(commandText, false, new SQLiteParameter[] { });
	}
	/// <summary>
	/// 对连接执行Transact-SQL语句并返回受影响的行数
	/// </summary>
	/// <param name="trans">传递事务对象</param>
	/// <param name="commandText">SQL语句或存储过程名</param>
	/// <param name="isProcedure">第二个参数是否为存储过程名,true为是,false为否</param>
	/// <param name="paras">SQLiteParameter参数列表，0个或多个参数</param>
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
	/// 对连接执行Transact-SQL语句并返回受影响的行数
	/// </summary>
	/// <param name="trans">传递事务对象</param>
	/// <param name="commandText">SQL语句</param>
	/// <param name="paras">SQLiteParameter参数列表，0个或多个参数</param>
	/// <returns></returns>
	public static int ExecuteNonQuery(SQLiteTransaction trans, string commandText, params SQLiteParameter[] paras) {
		return ExecuteNonQuery(trans, commandText, false, paras);
	}

	#endregion

	#region ExecuteQueryScalar

	/// <summary>
	/// 执行查询，并返回查询结果集中的第一行第一列，忽略其它行或列
	/// </summary>
	/// <param name="commandText">SQL语句或存储过程名</param>
	/// <param name="isProcedure">第一个参数是否为存储过程名,true为是,false为否</param>
	/// <param name="paras">SQLiteParameter参数列表，0个或多个参数</param>
	/// <returns></returns>
	public static object ExecuteQueryScalar(string commandText, bool isProcedure, params SQLiteParameter[] paras) {
		
		if(SQLiteConnectionString==null)
			throw new Exception("请先确定某个数据库") ;
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
	/// 执行查询，并返回查询结果集中的第一行第一列，忽略其它行或列
	/// </summary>
	/// <param name="commandText">SQL语句</param>
	/// <param name="paras">SQLiteParameter参数列表，0个或多个参数</param>
	/// <returns></returns>
	public static object ExecuteQueryScalar(string commandText, params SQLiteParameter[] paras) {
		return ExecuteQueryScalar(commandText, false, paras);
	}
	/// <summary>
	/// 执行查询，并返回查询结果集中的第一行第一列，忽略其它行或列
	/// </summary>
	/// <param name="commandText">SQL语句</param>
	/// <returns></returns>
	public static object ExecuteQueryScalar(string commandText) {
		return ExecuteQueryScalar(commandText, false, new SQLiteParameter[] { });
	}
	/// <summary>
	/// 执行查询，并返回查询结果集中的第一行第一列，忽略其它行或列
	/// </summary>
	/// <param name="trans">传递事务对象</param>
	/// <param name="commandText">SQL语句或存储过程名</param>
	/// <param name="isProcedure">第二个参数是否为存储过程名,true为是,false为否</param>
	/// <param name="paras">SQLiteParameter参数列表，0个或多个参数</param>
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
	/// 执行查询，并返回查询结果集中的第一行第一列，忽略其它行或列
	/// </summary>
	/// <param name="trans">传递事务对象</param>
	/// <param name="commandText">SQL语句</param>
	/// <param name="paras">SQLiteParameter参数列表，0个或多个参数</param>
	/// <returns></returns>
	public static object ExecuteQueryScalar(SQLiteTransaction trans, string commandText, params SQLiteParameter[] paras) {
		return ExecuteQueryScalar(trans, commandText, false, paras);
	}

	#endregion

	#region ExecuteDataReader

	/// <summary>
	/// 执行SQL，并返回结果集的只前进数据读取器
	/// </summary>
	/// <param name="commandText">SQL语句或存储过程名</param>
	/// <param name="isProcedure">第一个参数是否为存储过程名,true为是,false为否</param>
	/// <param name="paras">SQLiteParameter参数列表，0个或多个参数</param>
	/// <returns></returns>
	public static SQLiteDataReader ExecuteDataReader(string commandText, bool isProcedure, params SQLiteParameter[] paras) {
		
		if(SQLiteConnectionString==null)
			throw new Exception("请先确定某个数据库") ;
		
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
	/// 执行SQL，并返回结果集的只前进数据读取器
	/// </summary>
	/// <param name="commandText">SQL语句</param>
	/// <param name="paras">SQLiteParameter参数列表，0个或多个参数</param>
	/// <returns></returns>
	public static SQLiteDataReader ExecuteDataReader(string commandText, params SQLiteParameter[] paras) {
		return ExecuteDataReader(commandText, false, paras);
	}

	/// <summary>
	/// 执行SQL，并返回结果集的只前进数据读取器
	/// </summary>
	/// <param name="commandText">SQL语句</param>
	/// <param name="paras">SQLiteParameter参数列表，0个或多个参数</param>
	/// <returns></returns>
	public static SQLiteDataReader ExecuteDataReader(string commandText) {
		return ExecuteDataReader(commandText, false, new SQLiteParameter[] { });
	}

	/// <summary>
	/// 执行SQL，并返回结果集的只前进数据读取器
	/// </summary>
	/// <param name="trans">传递事务对象</param>
	/// <param name="commandText">SQL语句或存储过程名</param>
	/// <param name="isProcedure">第二个参数是否为存储过程名,true为是,false为否</param>
	/// <param name="paras">SQLiteParameter参数列表，0个或多个参数</param>
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
	/// 执行SQL，并返回结果集的只前进数据读取器
	/// </summary>
	/// <param name="trans">传递事务对象</param>
	/// <param name="commandText">SQL语句</param>
	/// <param name="paras">SQLiteParameter参数列表，0个或多个参数</param>
	/// <returns></returns>
	public static SQLiteDataReader ExecuteDataReader(SQLiteTransaction trans, string commandText, params SQLiteParameter[] paras) {
		return ExecuteDataReader(trans, commandText, false, paras);
	}

	#endregion

	#region ExecuteDataSet

	/// <summary>
	/// 执行SQL，并返回DataSet结果集
	/// </summary>
	/// <param name="commandText">SQL语句或存储过程名</param>
	/// <param name="isProcedure">第一个参数是否为存储过程名,true为是,false为否</param>
	/// <param name="paras">SQLiteParameter参数列表，0个或多个参数</param>
	/// <returns></returns>
	public static DataSet ExecuteDataSet(string commandText, bool isProcedure, params SQLiteParameter[] paras) {
		
		if(SQLiteConnectionString==null && DBList == null)
			throw new Exception("请先确定某个数据库") ;
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
	/// 执行SQL，并返回DataSet结果集
	/// </summary>
	/// <param name="commandText">SQL语句</param>
	/// <param name="paras">SQLiteParameter参数列表，0个或多个参数</param>
	/// <returns></returns>
	public static DataSet ExecuteDataSet(string commandText, params SQLiteParameter[] paras) {
		return ExecuteDataSet(commandText, false, paras);
	}

	/// <summary>
	/// 执行SQL，并返回DataTable结果集
	/// </summary>
	/// <param name="commandText">SQL语句</param>
	/// <returns></returns>
	public static DataTable ExecuteDataTable(string commandText) {
		return ExecuteDataSet(commandText, false, new SQLiteParameter[] { }).Tables[0];
	}

	/// <summary>
	/// 执行SQL，并返回DataSet结果集
	/// </summary>
	/// <param name="trans">传递事务对象</param>
	/// <param name="commandText">SQL语句或存储过程名</param>
	/// <param name="isProcedure">第二个参数是否为存储过程名,true为是,false为否</param>
	/// <param name="paras">SQLiteParameter参数列表，0个或多个参数</param>
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
	/// 执行SQL，并返回DataSet结果集
	/// </summary>
	/// <param name="trans">传递事务对象</param>
	/// <param name="commandText">SQL语句</param>
	/// <param name="paras">SQLiteParameter参数列表，0个或多个参数</param>
	/// <returns></returns>
	public static DataSet ExecuteDataSet(SQLiteTransaction trans, string commandText, params SQLiteParameter[] paras) {
		return ExecuteDataSet(trans, commandText, false, paras);
	}

	#endregion
}
