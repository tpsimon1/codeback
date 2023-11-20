/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2013-11-16
 * Time: 16:24
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using Boo.Lang.Interpreter;
using Boo.Lang.Parser;
using System.Text.RegularExpressions;

namespace codeback
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	public class Boo
	{
		static readonly string [][] keyword = new string[][] {
			new string[]{"\"", "#1#"}, 
			new string[]{"\n", "#2#"} 
		}; 
		public Boo()
		{
			
		}
		
		public static string BooPrint (string key,string code)
		{  
			
			return restore(RunBoo(key.TrimEnd(';')+"; \""+filter(code)+"\""));
		}
		
		static string filter (string code)
		{
			 foreach (string[] b in keyword)
			 	code= Regex.Replace(code,b[0],b[1]);
			 return code;
		}
		static string restore (string code)
		{
			 foreach (string[] b in keyword)
			 	code= Regex.Replace(code,b[1],b[0]);
			 return code;
		}
		
		public static string RunBoo (string code)
		{     string output="" ;
			InteractiveInterpreter i  = new InteractiveInterpreter(); 
			i.BlockStarters= new string[]{";"};
			i.RememberLastValue =true;
			i.Eval("import System;");
			i.Eval(code);
			output= i.LastValue.ToString();
			return   (output != null ? output : "") ;
		}
	}
}
