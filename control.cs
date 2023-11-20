﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Data;

namespace codeback
{
    class control
    {
    	 /// <summary>  
         /// 对字符串进行压缩  
         /// </summary>  
         /// <param name="str">待压缩的字符串</param>  
         /// <returns>压缩后的字符串</returns>  
          public static string CompressString(string str)  
         {      
          	
             string compressString = "";  
             byte[] compressBeforeByte = Encoding.GetEncoding("UTF-8").GetBytes(str);  
             byte[] compressAfterByte=Compress(compressBeforeByte);  
             //compressString = Encoding.GetEncoding("UTF-8").GetString(compressAfterByte);  
             compressString = Convert.ToBase64String(compressAfterByte);  
             return compressString;  
         }  
         /// <summary>  
         /// 对字符串进行解压缩  
         /// </summary>  
         /// <param name="str">待解压缩的字符串</param>  
         /// <returns>解压缩后的字符串</returns>  
         public static string DecompressString(string str)  
         {  
         	
             string compressString = "";  
             //byte[] compressBeforeByte = Encoding.GetEncoding("UTF-8").GetBytes(str);  
            try { 
             	
              
             	
             	byte[] compressBeforeByte = Convert.FromBase64String(str);
             	 byte[] compressAfterByte = Decompress(compressBeforeByte);  
             compressString = Encoding.GetEncoding("UTF-8").GetString(compressAfterByte);  
             	}
             catch (System.ArgumentNullException) {
		      System.Console.WriteLine("Base 64 string is null.");
		      
		   }
		   catch (System.FormatException) {
		      System.Console.WriteLine("Base 64 string length is not " +
		         "4 or is not an even multiple of 4." );
             	   compressString =str;
		    
		   }
              
             return compressString;
         }  
         /// <summary>  
         /// 对文件进行压缩  
         /// </summary>  
         /// <param name="sourceFile">待压缩的文件名</param>  
         /// <param name="destinationFile">压缩后的文件名</param>  
         public static void CompressFile(string sourceFile, string destinationFile)  
         {  
             throw new Exception("The method or operation is not implemented.");  
         }  
         /// <summary>  
         /// 对文件进行解压缩  
         /// </summary>  
         /// <param name="sourceFile">待解压缩的文件名</param>  
         /// <param name="destinationFile">解压缩后的文件名</param>  
         /// <returns></returns>  
         public static void DecompressFile(string sourceFile, string destinationFile)  
         {  
             throw new Exception("The method or operation is not implemented.");  
         }  
         /// <summary>  
         /// 对byte数组进行压缩  
         /// </summary>  
         /// <param name="data">待压缩的byte数组</param>  
         /// <returns>压缩后的byte数组</returns>  
         public static byte[] Compress(byte[] data)  
         {  
             try  
             {  
                 MemoryStream ms = new MemoryStream();  
                 GZipStream zip = new GZipStream(ms, CompressionMode.Compress,true);  
                 zip.Write(data, 0, data.Length);  
                 zip.Close();  
                 byte[] buffer = new byte[ms.Length];  
                 ms.Position=0;  
                 ms.Read(buffer, 0, buffer.Length);  
                 ms.Close();  
                 return buffer;  
   
             }  
             catch (Exception e)  
             {  
                 throw new Exception(e.Message);  
             }  
         }  
         public static byte[] Decompress(byte[] data)  
         {     byte[] buffer = new byte[0x1000];  
             try  
             {  
                 MemoryStream ms = new MemoryStream(data);  
                 GZipStream zip = new GZipStream(ms, CompressionMode.Decompress, true);  
                 MemoryStream msreader = new MemoryStream();  
              
                 while (true)  
                 {  
                     int reader = zip.Read(buffer, 0, buffer.Length);  
                     if (reader <= 0)  
                     {  
                         break;  
                     }  
                     msreader.Write(buffer, 0, reader);  
                 }  
                 zip.Close();  
                 ms.Close();  
                 msreader.Position = 0;  
                 buffer = msreader.ToArray();  
                 msreader.Close();  
                
             }  
              catch (System.IO.InvalidDataException) {
		      System.Console.WriteLine("The magic number in GZip header is not correct.Make sure you are passing in a GZip stream." );
             	   buffer =data;
		    
		   }
           return buffer;  
         }  
    }
}
