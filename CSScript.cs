/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2014-5-15
 * Time: 9:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Text;
using System.Text.RegularExpressions;
using CSScriptLibrary;

	

namespace codeback
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	public class CSScript
	{
		public CSScript()
		{
		}
		public static String Run(string tmp)
		{
			 
			System.Reflection.Assembly assembly = CSScriptLibrary.CSScript.LoadCode(@tmp);
			AsmHelper script = new AsmHelper(assembly);
			return script.Invoke("Temp.Test", new object[]{}).ToString(); 
		}
		
	 
		
		 public static String Print(string content)
		{ 
		 	
		 	StringBuilder sb = new StringBuilder();
		 	sb.Append(@"using System; 
		 	using System.Data;
			public class Temp {
				static void Main() { }
				public static string Test()
				{ return String.Format(@"""+content.Replace("\"","\"\"")+@""", new object[]{");
		 	
		 
		 	sb.Append("null});}}");  
		 	return Run(sb.ToString());
		}
		
	}
}
