/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2014-4-2
 * Time: 11:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Diagnostics;
namespace codeback
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	public class Cmd
	{
		public Cmd()
		{
		}
			/// <summary>
			/// 运行CMD命令
			/// </summary>
			/// <param name="cmd">命令</param>
			/// <returns></returns>
			public static string Run(string cmd)
			{
			Process p = new Process();
			p.StartInfo.FileName = "cmd.exe";
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.RedirectStandardInput = true;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.RedirectStandardError = true;
			p.StartInfo.CreateNoWindow = true;
			p.Start();
			p.StandardInput.AutoFlush = true; 
			p.StandardInput.WriteLine(cmd); 
			p.StandardInput.WriteLine("exit");
			string strRst = p.StandardOutput.ReadToEnd();
			p.WaitForExit();
			p.Close();
			return strRst;
			}
		
	}
}
