using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Web;
using System.IO;


namespace Weigou.Common
{
    public class ImageHelper
    {
        #region 生成缩略图
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            mode = mode.ToUpper();
            try
            {
                using (Image originalImage = Image.FromFile(originalImagePath))
                {
                    if (mode != "CUT")
                    {
                        double d1 = double.Parse(width.ToString()) / double.Parse(height.ToString());
                        //宽高比
                        double wd = double.Parse(originalImage.Width.ToString()) / double.Parse(originalImage.Height.ToString());
                        //高宽比
                        double hd = double.Parse(originalImage.Height.ToString()) / double.Parse(originalImage.Width.ToString());
                        double itsw = 0d;
                        double itsh = 0d;

                        //与宽高比对比
                        if (d1 > wd)
                        {
                            itsw = d1 - wd;
                        }
                        else
                        {
                            itsw = wd - d1;
                        }
                        //与高宽比对比
                        if (d1 > hd)
                        {
                            itsh = d1 - hd;
                        }
                        else
                        {
                            itsh = hd - d1;
                        }
                        //如果高宽比更接近比例
                        if (itsw > itsh)
                        {
                            mode = "W";
                        }
                        else
                        {
                            mode = "H";
                        }
                    }
                    int towidth = width;
                    int toheight = height;

                    int x = 0;
                    int y = 0;
                    int ow = originalImage.Width;
                    int oh = originalImage.Height;

                    switch (mode)
                    {
                        case "HW"://指定高宽缩放（可能变形）               
                            break;
                        case "W"://指定宽，高按比例                   
                            toheight = originalImage.Height * width / originalImage.Width;
                            break;
                        case "H"://指定高，宽按比例
                            towidth = originalImage.Width * height / originalImage.Height;
                            break;
                        case "CUT"://指定高宽裁减（不变形）               
                            if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                            {
                                oh = originalImage.Height;
                                ow = originalImage.Height * towidth / toheight;
                                y = 0;
                                x = (originalImage.Width - ow) / 2;
                            }
                            else
                            {
                                ow = originalImage.Width;
                                oh = originalImage.Width * height / towidth;
                                x = 0;
                                y = (originalImage.Height - oh) / 2;
                            }
                            break;
                        default:
                            break;
                    }
                    //新建一个bmp图片
                    System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
                    //新建一个画板
                    System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
                    //设置高质量插值法
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    //设置高质量,低速度呈现平滑程度
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    //清空画布并以透明背景色填充
                    g.Clear(System.Drawing.Color.Transparent);
                    //在指定位置并且按指定大小绘制原图片的指定部分
                    g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
                        new System.Drawing.Rectangle(x, y, ow, oh),
                        System.Drawing.GraphicsUnit.Pixel);

                    try
                    {
                        //以jpg格式保存缩略图
                        bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch (System.Exception e)
                    {
                        Utils.SaveLog("上传图片出错",e.ToString() + thumbnailPath);
                    }
                    finally
                    {
                        originalImage.Dispose();
                        bitmap.Dispose();
                        g.Dispose();
                    }
                }
            }
            catch
            {

            }
        }
        #endregion

        /// <summary>
        /// 获取指定大小的缩略图
        /// </summary>
        /// <param name="imgUrl"></param>
        /// <returns></returns>
        public static string GetThumbnail(string imgUrl, int w, int h)
        {
            if (w == 0 || h == 0 || string.IsNullOrEmpty(imgUrl))
                return imgUrl;
            string ext = Utils.GetFileExtension(imgUrl);
            string tempPath = imgUrl.Replace(ext, string.Format("_{0}x{1}", w, h) + ext);
            string thumbnailPath = HttpContext.Current.Server.MapPath(tempPath);
            //图片不存在，则生成一个图片
            if (!File.Exists(thumbnailPath))
            {
                MakeThumbnail(HttpContext.Current.Server.MapPath(imgUrl), thumbnailPath, w, h, "CUT");
            }
            return tempPath;
        }
    }
}
