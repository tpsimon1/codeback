using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text.RegularExpressions;

public  class ImageRelpace{



/// 替换img
public static string ImageReplace(string html)
{
    var relust = string.Empty;
    string pattern = @"<[img|IMG]{3}\b[^<>]*?\b[src|SRC]{3}[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>";
    relust = Regex.Replace(html, pattern, (c =>
    {
        string newValue = string.Empty;
        string oldImgUrl = c.Groups["imgUrl"].Value;
        if (!String.IsNullOrEmpty(oldImgUrl))
        {
            if (!oldImgUrl.Contains("data:image"))
            {

                newValue = c.Value.Replace(oldImgUrl, ImageToBase64(oldImgUrl)); ;
            }
            else
            {
                //string bb = c.GetPath(@"Resource") + oldImgUrl;//需要替换的图片地址
                newValue = c.Value;

            }
        }
        return newValue;
    }));
    return relust;
}

    // Image 对象转换为 base64 字符串
    static string ImageToBase64(string url)
{

        WebClient client = new WebClient(); 

        var html = client.DownloadData(url);
        
        // client.DownloadFile(url, "1.jpg");
        Image img ;

    try
    {
            using (MemoryStream ms = new MemoryStream(html))
            {   
                if (IsGZip(html)) {
                    GZipStream gz = new GZipStream(ms, CompressionMode.Decompress);
                      img = Image.FromStream(gz);
                } else {
                      img = Image.FromStream(ms);
                }
               
                string imgtype = ImageFormatGuidToString(img.RawFormat);
                MemoryStream out1 = new MemoryStream();
                img.Save(out1, img.RawFormat);
                img.Dispose();
                return "data:image/" + imgtype + ";base64," + Convert.ToBase64String(out1.GetBuffer());
            }
      
    }
    catch (Exception ex)
    {
         throw ex;
        return "";
    }
    finally
    {
            client.Dispose();
            client = null;
            
            img = null;
    }
}

    // base64 字符串转换为 Image 对象
    static Image Base64ToImage(string _base64)
{
    byte[] arr = Convert.FromBase64String(_base64);
    MemoryStream ms = new MemoryStream(arr);
    Bitmap bmp = new Bitmap(ms);
    try
    {
        Image result = new Bitmap(bmp.Width, bmp.Height);
        Graphics g = Graphics.FromImage(result);
        g.DrawImage(bmp, 0, 0, bmp.Width, bmp.Height);
        g.Dispose();
        return result;
    }
    catch
    {
        return null;
    }
    finally
    {
        bmp.Dispose();
        ms.Close();
    }
}

    // 用于检查图像格式
    static string ImageFormatGuidToString(ImageFormat _format)
{
    if (_format.Guid == ImageFormat.Bmp.Guid)
    {
        return "bmp";
    }
    else if (_format.Guid == ImageFormat.Gif.Guid)
    {
        return "gif";
    }
    else if (_format.Guid == ImageFormat.Jpeg.Guid)
    {
        return "jpg";
    }
    else if (_format.Guid == ImageFormat.Png.Guid)
    {
        return "png";
    }
    else if (_format.Guid == ImageFormat.Icon.Guid)
    {
        return "ico";
    }
    else if (_format.Guid == ImageFormat.Emf.Guid)
    {
        return "emf";
    }
    else if (_format.Guid == ImageFormat.Exif.Guid)
    {
        return "exif";
    }
    else if (_format.Guid == ImageFormat.Tiff.Guid)
    {
        return "tiff";
    }
    else if (_format.Guid == ImageFormat.Wmf.Guid)
    {
        return "wmf";
    }
    else
    {
        return "png";
    }
}

    public static bool IsGZip(byte[] arr)
    {
        return arr.Length >= 2 && arr[0] == 31 && arr[1] == 139;
    }
}