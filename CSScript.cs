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
		
	 
		
		 public static String Print(string key,string content)
		{ 
		 	String[] _list  = key.Trim(';').Split(';');
		 	 String[] keylist  = new string[_list.Length];
		 	foreach (var element in _list) {
		 		var element1 = element.Split('=');
		 		if ( element1.Length ==2){
		 		string keyindex =  Regex.Replace(element1[0], @"[^\d]*","");
		 			Int64 _keyindex =  Convert.ToInt64(keyindex);
		 			keylist[_keyindex] =  element1[1];
		 		}
		 	} 
		 	StringBuilder sb = new StringBuilder();
		 	sb.Append(@"using System; 
		 	using System.Data;
			public class Temp {
				static void Main() { }
				public static string Test()
				{ return String.Format(@"""+content.Replace("\"","\"\"")+@""", new object[]{");
		 	
		 	foreach (var element in keylist) {
		 		sb.Append(element+",");
		 	}
		 	sb.Append("null});}}");  
		 	return Run(sb.ToString());
		}
		
	}
}
