using System;
using System.Data.SQLite;
using System.Data;
namespace codeback {
    class file {
        public file() {

        }
        public static bool WriteData(string code, byte[] br, string filename) {
            SQLiteParameter pValue = new SQLiteParameter("@openfile", DbType.Binary);
            pValue.Value =control.Compress(br);
            if (SQLiteHelper.ExecuteNonQuery("insert into t_filelist (fl_code,fl_file,fl_name)  values( " + code + ",@openfile,'" + filename + "')", new SQLiteParameter[] { pValue }) > 0)
                return true;
            else
                return false;
        }
        public static byte[] ReadData(string code, string filename) {
            byte[] by = null;
            using (SQLiteDataReader dr = SQLiteHelper.ExecuteDataReader("select fl_file from t_filelist where fl_code=" + code + " and fl_name='" + filename + "'")) {
                while (dr.Read()) {
                    by=(byte[])dr.GetValue(0);
                }
                dr.Close();
                return  control.Decompress(by);
            }
        }
    }
}
