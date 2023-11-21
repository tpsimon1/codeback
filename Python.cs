/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2014-4-2
 * Time: 11:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
namespace codeback
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	public class Python
	{
		public Python()
		{
		}
			/// <summary>
			/// 运行CMD命令
			/// </summary>
			/// <param name="cmd">命令</param>
			/// <returns></returns>
			public static string Run(Hashtable key,string cmd)
			{
			string strRst="";
 
			if (key["pip"] != null ) {

                Process pip = new Process();
                pip.StartInfo.FileName = Application.StartupPath + "\\python\\Scripts\\pip.exe";
                pip.StartInfo.Arguments = " install " + key["pip"].ToString();
						
                pip.StartInfo.UseShellExecute = false;
                pip.StartInfo.RedirectStandardInput = true;
                pip.StartInfo.RedirectStandardOutput = true;
                pip.StartInfo.RedirectStandardError = true;
                pip.StartInfo.CreateNoWindow = true;
                pip.Start();
                strRst += pip.StandardOutput.ReadToEnd() + pip.StandardError.ReadToEnd();
                pip.WaitForExit();
                pip.Close();
            } 
                 

            File.WriteAllText("temp.py", cmd);
            Process p = new Process();
			p.StartInfo.FileName = Application.StartupPath + "\\python\\python.exe";
			p.StartInfo.Arguments = "temp.py";
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.RedirectStandardInput = true;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.RedirectStandardError = true;
			p.StartInfo.CreateNoWindow = true;
			p.Start(); 

            p.StandardInput.AutoFlush = true;
            //p.StandardInput.WriteLine("exit()");
            strRst += p.StandardOutput.ReadToEnd()+ p.StandardError.ReadToEnd();
			//string strRst = "";

            p.WaitForExit();
			p.Close();
			//File.Delete("temp.py");
			return strRst;
			}
		
	}
}
