/*
 * 由SharpDevelop创建。
 * 用户： tianpeng
 * 日期: 2015/8/14
 * 时间: 11:14
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
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
		public static async void Run(String key_word, String exec_content,RichTextBox tb)
		{
			
		  		if (key_word.IndexOf("lang=") == -1) { 
					throw new Exception("缺少关键字lang=");
				}
		  
			

            var doSomething = new Action(() => {
                String out_content = null;
                String key = key_word.Substring(key_word.IndexOf("lang=") + 5);
                key = key.Substring(0, key.IndexOf(';'));
                switch (key) {
					case "Print":
						out_content = CSScript.Print(key_word.Replace("lang=Print;", ""), exec_content);
						break;
					case "Cmd":
						out_content = Cmd.Run(exec_content);
						break;
					case "C#":
						out_content = Csharp.Run(exec_content);
						break;
					case "CSScript":
						out_content = CSScript.Run(key_word.Replace("lang=CSScript;", "")+"\r\n"+exec_content);
						break;
					case "Python":
                        out_content = Python.Run(key_word.Replace("lang=Python;", ""), exec_content);
                        break;
                }
				System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
				tb.Text= out_content;
				tb.Select();
            });

            await Task.Run(doSomething);
        }
	}
}
