using System;
using System.Collections.Generic;
using System.Text;

namespace codeback
{
    public static class codebase
    {
        static int _level = 0;//当前等级
        static string _top_code = ""; //保存文档上层编号
        static string _code = ""; //保存文档内部编号 
        static string _content = ""; //TEXT内容
        static string _keyname = "";//关键字
        static string _rtxtext = "";//RTX格式内容
        static int _nowdbnumber =0; //当前db编号
        
	
        static byte[] _filecontent = null;
        static string _filename = "";
        public static int level
        {
            set { if (  value < 0) _level = 0; else _level = value; }
            get { return _level; }
        }

        public static string top_code
        {
        	set { if (string.IsNullOrEmpty( value))_top_code = "0"; else _top_code = value; }
            get { return _top_code; }
        }

        public static string code
        {
            set { if (string.IsNullOrEmpty( value))_code = "1"; else _code = value; }
            get { return _code; }
        }

        public static string content
        {
            set { _content = value; }
            get { return _content; }
        }

        public static string keyname
        {
            set { _keyname = value; }
            get { return _keyname; }
        }

        public static string rtxtext
        {
            set { _rtxtext = value; }
            get { return _rtxtext; }
        }

        public static string filename
        {
            set { _filename = value; }
            get { return _filename; }
        }

        public static byte[] filecontent
        {
            set { _filecontent = value; }
            get { return _filecontent; }
        }
       public static int Nowdbnumber {
			get { return _nowdbnumber; }
			set { _nowdbnumber = value; }
		}
    }
}
