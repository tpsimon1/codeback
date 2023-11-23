/*
 * 由SharpDevelop创建。
 * 用户： tianpeng
 * 日期: 2015/8/14
 * 时间: 11:14
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Collections;
using System.Threading.Tasks;
using System.Windows.Forms; 

namespace codeback
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	public class ScriptRun
	{
		public ScriptRun()
		{
		}
		public static async void Run(Hashtable key_ht, String exec_content, RichTextBox tb,Button runc)
		{
			 
			String cron = "";
			 

				if (key_ht["lang"].Equals(null))
				{
					throw new Exception("缺少关键字lang=");
				}

				if (key_ht["cron"] !=null)
				{
					cron = key_ht["cron"].ToString();
				}

				var doSomething = new Action(() =>
				{
					String out_content = null;

					switch (key_ht["lang"].ToString())
					{
						case "Print":
							out_content = CSScript.Print(exec_content);
							break;
						case "Cmd":
							out_content = Cmd.Run(exec_content);
							break;
                        case "PowerShell":
                            out_content = PowerShell.Run(exec_content);
                            break;
                        case "C#":
							out_content = Csharp.Run(exec_content);
							break;
						case "CSScript":
							out_content = CSScript.Run( exec_content);
							break;
						case "Python":
							out_content = Python.Run(key_ht, exec_content);
							break;
					}
					System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
					tb.Text = out_content;
					tb.Select();
					

                });

			while (runc.Text=="停止")
			{
                if (cron != "")
                    await Task.Delay(int.Parse(cron) * 1000);
	 

                await Task.Run(doSomething);

                if (cron == "")
                    runc.Text = "运行(&r)";
            }
				
			}
		}
	 
}
