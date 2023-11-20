/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2014-4-2
 * Time: 11:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System; 
using System.Text.RegularExpressions;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection; 
using System.Collections.Generic;
using System.Windows.Forms;

namespace codeback
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	public class Csharp
	{
		public Csharp()
		{
		}
			/// <summary>
			/// 运行CMD命令
			/// </summary>
			/// <param name="cmd">命令</param>
			/// <returns></returns>
			public static string Run(string cmd)
			{ 
				foreach (string i in System.IO.Directory.GetFiles(Application.StartupPath,"TMP*.exe"))
				{
					System.IO.File.Delete  ( i);//c#动态执行生成文件删除
				}
			    ICodeCompiler vCodeCompiler = new CSharpCodeProvider().CreateCompiler();
			    CompilerParameters vCompilerParameters = new CompilerParameters();
			    vCompilerParameters.GenerateExecutable = true;
			    vCompilerParameters.GenerateInMemory = true;
			    vCompilerParameters.OutputAssembly =Application.StartupPath+"\\TMP"+DateTime.Now.ToString("yyyyMMdd") +".exe";
			    string vSource = cmd;
			
			    CompilerResults vCompilerResults =
			        vCodeCompiler.CompileAssemblyFromSource(vCompilerParameters, vSource);
			    if(vCompilerResults.Errors.HasErrors)
			    {
			    	throw new Exception(vCompilerResults.Errors[0].ErrorText) ;
			    	return "";
			    }
			    Assembly vAssembly = vCompilerResults.CompiledAssembly;
			    object vTemp = vAssembly.CreateInstance("Temp");
			    MethodInfo vTest = vTemp.GetType().GetMethod("Test");
			    
			        object vDouble = vTest.Invoke(vTemp, new object[]{});
			      return vDouble.ToString(); 
			}
			
		 public static String Print(string key,string content)
		{
		 	String[] keylist  = new string[50];
		 	foreach (var element in key.Split(';')) {
		 		var element1 = element.Split('=');
		 		if ( element1.Length ==2){
		 		string keyindex =  Regex.Replace(element1[0], @"[^\d]*","");
		 			Int64 _keyindex =  Convert.ToInt64(keyindex);
		 			keylist[_keyindex] =  element1[1];
		 		}
		 	}
		 	
		 	string temp  =  String.Format(content,keylist);
			return temp;
		}
		
	}
}
