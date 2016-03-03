using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Weigou.Service;
using Weigou.Common;
using System.Text;
using System.IO;
using System.Web.SessionState;
using Weigou.Model.Enum; 

namespace Weigou.Manage.Ajax
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Goods : BaseAjax, IHttpHandler, IRequiresSessionState
    {
        #region 商品类别

        /// <summary>
        /// 获取商品二级类别
        /// </summary>
        /// <param name="hc"></param>
        public void GetGoodsType(HttpContext hc)
        {
            Hashtable hs = new Hashtable();
            hs["ParentID"] = GetRequest("ParentID", 0);
            DataTable dt = goodsService.GetDataByKey("T_GoodsType", "ParentID", hs["ParentID"]);            
            ResponseWrite(hc,dt);
        }

        /// <summary>
        /// 删除商品类别
        /// </summary>
        /// <param name="hc"></param>
        public void DeleteGoodsType(HttpContext hc)
        {
            int res = 0;
            int curID = GetRequest("ID", 0);
            //首先判断当前节点是否为最后一级
            DataTable dt = goodsService.GetDataByKey("T_GoodsType", "ParentID", curID);
            if (dt.Rows.Count > 0)
            {
                res = -1; //含有子节点，不能删除
            }
            else
            {
                dt = goodsService.GetDataByKey("T_GoodsType", "ID", curID);
                if (dt.Rows.Count > 0)
                {
                    dt.Rows[0]["Status"] = (int)EnumStatus.Delete;
                    res = goodsService.UpdateDataTable(dt);
                    if (res > 0)
                        goodsService.SaveSysLog(curID.ToString(), EnumModule.GoodsManage, EnumOperation.Delete, UserInfo.ID, "删除商品类别");
                }
            }
            ResponseWrite(hc, res > 0 ? "0" : "1");
        }

        #endregion

        #region 图片管理(Type=1:商品图片，Type=2:其它图片)
        /// <summary>
        /// 获取图片
        /// </summary>
        public void GetPictureList(HttpContext hc)
        {
            int Code = GetRequest("TargetID", 0);
            int type = GetRequest("Type", 0);
            DataTable dt = goodsService.GetPictureList(Code.ToString(), type);
            hc.Response.ContentType = "text/plain";
            hc.Response.Write(ToJson(dt));
        }
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="hc"></param>
        public void UploadPicture(HttpContext hc)
        {
            int type = GetRequest("Type", 0);
            string picturePath = string.Empty;
            if (type == (int)EnumPicture.Goods)
                picturePath = "/upload/Goods/";
            else
                picturePath = "/upload/Other/";

            int smallPicWidth = Convert.ToInt16(Utils.GetConfig("SmallPicWidth"));
            int smallPicHeight = Convert.ToInt16(Utils.GetConfig("SmallPicHeight"));
            int largePicWidth = Convert.ToInt16(Utils.GetConfig("LargePicWidth"));
            int largePicHeight = Convert.ToInt16(Utils.GetConfig("LargePicHeight"));
            GeneratePic(hc, smallPicWidth, smallPicHeight, largePicWidth, largePicHeight, picturePath);
        }
        /// <summary>
        /// 处理上传图片
        /// </summary>
        /// <param name="hc"></param>
        /// <param name="smallPicWidth"></param>
        /// <param name="smallPicHeight"></param>
        /// <param name="largePicWidth"></param>
        /// <param name="largePicHeight"></param>
        /// <param name="picturePath"></param>
        private void GeneratePic(HttpContext hc, int smallPicWidth, int smallPicHeight, int largePicWidth, int largePicHeight, string picturePath)
        {
            picturePath = picturePath + DateTime.Now.ToString("yyyyMM") + "/";
            //绝对路径
            string absPicPaht = Server.MapPath(picturePath);
            if (!Directory.Exists(absPicPaht))
            {
                Directory.CreateDirectory(absPicPaht);
            }
            //获取上传文件
            HttpPostedFile postedFile = hc.Request.Files["Filedata"];

            //保存原图
            string ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf(".")).Trim();
            string originalFile = absPicPaht + postedFile.FileName;
            postedFile.SaveAs(originalFile);

            //保存小图
            string smallPicName = System.Guid.NewGuid().ToString() + "_s" + ext;
            ImageHelper.MakeThumbnail(originalFile, absPicPaht + smallPicName, smallPicWidth, smallPicHeight, "CUT");
            //保存大图
            string largePicName = System.Guid.NewGuid().ToString() + ext;
            ImageHelper.MakeThumbnail(originalFile, absPicPaht + largePicName, largePicWidth, largePicHeight, "CUT");
            ///删除原图
            if (File.Exists(originalFile))
            {
                File.Delete(originalFile);
            }
            hc.Response.ContentType = "text/plain";
            hc.Response.Write(picturePath + smallPicName + "|" + picturePath + largePicName);

        }
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="hc"></param>
        public void SavePicture(HttpContext hc)
        {
            int res = 0;
            DataTable dt = goodsService.GetDataByKey("T_Picture", "ID", "0");
            int targetID = GetRequest("TargetID", 0);
            int type = GetRequest("Type", 0);
            string strPic = GetRequest("StrPic", "");
            if (strPic != "")
            {
                DateTime createTime = DateTime.Now;
                string[] arrPic = strPic.Split(";".ToCharArray());
                foreach (string s in arrPic)
                {
                    if (s.Trim() == "")
                        continue;
                    string[] pic = s.Split("|".ToCharArray());
                    DataRow dr = dt.NewRow();
                    dr["Type"] = type;
                    dr["TargetID"] = targetID;
                    dr["SmallPicture"] = pic[0];
                    dr["LargePicture"] = pic[1];
                    dr["Sort"] = 0;
                    dr["IsTop"] = 0;
                    dt.Rows.Add(dr);
                }
                res = goodsService.UpdateDataTable(dt);
            }
            ResponseWrite(hc, res > 0 ? "0" : "1");
        }
        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="hc"></param>
        public void DeletePicture(HttpContext hc)
        {
            int res = 0;
            int id = GetRequest("ID", 0);
            DataTable dt = goodsService.GetDataByKey("T_Picture", "ID", id);
            if (dt.Rows.Count > 0)
            {
                string smallPic = dt.Rows[0]["SmallPicture"].ToString();
                string largePic = dt.Rows[0]["LargePicture"].ToString();
                if (File.Exists(smallPic))
                    File.Delete(smallPic);
                if (File.Exists(largePic))
                    File.Delete(largePic);
                res = goodsService.Delete("T_Picture", "ID", id);
            }
            ResponseWrite(hc, res > 0 ? "0" : "1");
        }
        /// <summary>
        /// 置顶图片
        /// </summary>
        /// <param name="hc"></param>
        public void SetPictureTop(HttpContext hc)
        {
            int ID = GetRequest("ID", 0);
            int targetID = GetRequest("TargetID", 0);
            int type = GetRequest("Type", 0);
            if (goodsService.SetPictureTop(ID, targetID.ToString(), type) > 0)
                ResponseWrite(hc, "0");
            else
                ResponseWrite(hc, "1");
        }
        #endregion

    }
}
