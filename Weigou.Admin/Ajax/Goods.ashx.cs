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

namespace Weigou.Admin.Ajax
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Goods : BaseAjax, IHttpHandler, IRequiresSessionState
    {
        #region 商品管理
        /// <summary>
        /// 商品列表
        /// </summary>
        public void GetGoodsList(HttpContext hc)
        {
            Hashtable hs = GetWhereForGoods();
            Pager p = new Pager(PageSize, PageIndex, "a.CreateTime desc");
            goodsService.GetGoodsList(p, hs);
            ResponseWrite(hc, p);
        }
       
        /// <summary>
        /// 删除商品
        /// </summary>
        public void DeleteGoods(HttpContext hc)
        {
            int goodsID = GetRequest("ID", 0);
            int res = 0;
            DataTable dt = goodsService.GetDataByKey("T_Goods", "ID", goodsID);
            if (dt.Rows.Count > 0)
            {
                dt.Rows[0]["Status"] = (int)EnumStatus.Delete;
                res = goodsService.UpdateDataTable(dt);
                if (res > 0)
                    goodsService.SaveSysLog(goodsID.ToString(), EnumModule.GoodsManage, EnumOperation.Delete, UserInfo.ID, "删除商品");
            }
            ResponseWrite(hc, res > 0 ? RT.SUCCESS.ToString() : RT.FAILED.ToString());
        }
        /// <summary>
        /// 审核商品
        /// </summary>
        /// <param name="hc"></param>
        public void AuditGoods(HttpContext hc)
        {
            int goodsID = GetRequest("ID", 0);
            int flag = GetRequest("Status", 0);
            int res = 0;
            DataTable dt = goodsService.GetDataByKey("T_Goods", "ID", goodsID);
            if (dt.Rows.Count > 0)
            {
                if (flag == 1)
                {
                    dt.Rows[0]["Status"] = (int)EnumStatus.Normal;
                }
                else
                {
                    dt.Rows[0]["Status"] = (int)EnumStatus.DisAudit;
                }
                dt.Rows[0]["ApprovedBy"] = UserInfo.ID;
                dt.Rows[0]["ApprovedTime"] = DateTime.Now;
                res = goodsService.UpdateDataTable(dt);
                if (res > 0)
                    goodsService.SaveSysLog(goodsID.ToString(), EnumModule.GoodsManage, EnumOperation.Audit, UserInfo.ID, "审核商品");
            }
            ResponseWrite(hc, res > 0 ? RT.SUCCESS.ToString() : RT.FAILED.ToString());
        }
        /// <summary>
        /// 获取商品查询条件
        /// </summary>
        /// <param name="hc"></param>
        /// <returns></returns>
        public Hashtable GetWhereForGoods()
        {
            Hashtable hs = new Hashtable();
            string GoodsName = GetRequest("GoodsName", "");
            string GoodsType = GetRequest("GoodsType", "");
            string GoodsStatus = GetRequest("GoodsStatus", "");
            string GoodsShelvesStatus = GetRequest("GoodsShelvesStatus", "");
            if (!string.IsNullOrEmpty(GoodsName))
            {
                hs["Name"] = GoodsName;
            }
            if (!string.IsNullOrEmpty(GoodsType))
            {
                hs["Type"] = GoodsType;
            }
            if (!string.IsNullOrEmpty(GoodsStatus))
            {
                hs["Status"] = GoodsStatus;
            }
            if (!string.IsNullOrEmpty(GoodsShelvesStatus))
            {
                hs["ShelvesStatus"] = GoodsShelvesStatus;
            }
            return hs;
        }
        #endregion

        #region 商品类别
        /// <summary>
        /// 商品类别列表
        /// </summary>
        /// <param name="hc"></param>
        public void GetGoodsType(HttpContext hc)
        {
            Hashtable hs = new Hashtable();
            hs["ParentID"] = GetRequest("ParentID", 0);
            Pager p = new Pager(PageSize, PageIndex, "a.CreateTime asc");
            goodsService.GetGoodsType(p, hs);
            ResponseWrite(hc, p);
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

        #region 图片管理(1-车辆图片 2-商品图片 3-用车图片 )
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
            string picturePath = string.Empty;if (type == (int)EnumPicture.Goods)
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

        #region 商品评论
        /// <summary>
        /// 评论列表
        /// </summary>
        public void GetGoodsCommentList(HttpContext hc)
        {
            Hashtable hs = GetWhereForGoodsComment();
            Pager p = new Pager(PageSize, PageIndex, "a.CreateTime desc");
            goodsService.GetGoodsCommentList(p, hs);
            ResponseWrite(hc, p);
        }
        /// <summary>
        /// 获取评论查询条件
        /// </summary>
        /// <param name="hc"></param>
        /// <returns></returns>
        public Hashtable GetWhereForGoodsComment()
        {
            Hashtable hs = new Hashtable();
            string GoodsName = GetRequest("GoodsName", "");
            string GoodsType = GetRequest("GoodsType", "");
            string reply = GetRequest("Reply", "");
            if (!string.IsNullOrEmpty(GoodsName))
            {
                hs["GoodsName"] = GoodsName;
            }
            if (!string.IsNullOrEmpty(GoodsType))
            {
                hs["GoodsType"] = GoodsType;
            }
            if (!string.IsNullOrEmpty(reply))
            {
                hs.Add("Reply", reply);
            }
            return hs;
        }
        #endregion

        #region 商品品牌
        /// <summary>
        /// 商品品牌
        /// </summary>
        public void GetGoodsBrandList(HttpContext hc)
        {
            Hashtable hs = new Hashtable();
            string BrandName = GetRequest("BrandName", "");
            if (!string.IsNullOrEmpty(BrandName))
            {
                hs["Name"] = BrandName;
            }
            Pager p = new Pager(PageSize, PageIndex, "a.CreateTime desc");
            goodsService.GetGoodsBrandList(p, hs);
            ResponseWrite(hc, p);
        }

        #endregion

        #region 商品属性
        /// <summary>
        /// 商品属性列表
        /// </summary>
        public void GetGoodsAttributeList(HttpContext hc)
        {
            Hashtable hs = new Hashtable();
            string name = GetRequest("Name", "");
            string type = GetRequest("Type", "");
            if (!string.IsNullOrEmpty(name))
            {
                hs["Name"] = name;
            }
            if (!string.IsNullOrEmpty(type))
            {
                hs["Type"] = type;
            }
            Pager p = new Pager(PageSize, PageIndex, "a.CreateTime desc");
            goodsService.GetGoodsAttributeList(p, hs);
            ResponseWrite(hc, p);
        }
        /// <summary>
        /// 属性的值列表
        /// </summary>
        /// <param name="hc"></param>
        public void GetAttributeValueList(HttpContext hc)
        {
            Hashtable hs = new Hashtable();
            string attributeid = GetRequest("ID", "");
            if (!string.IsNullOrEmpty(attributeid))
            {
                hs.Add("AttributeID", attributeid);
            }
            Pager p = new Pager(PageSize, PageIndex, "a.CreateTime desc");
            goodsService.GetAttributeValueList(p, hs);
            ResponseWrite(hc, p);
        }
        /// <summary>
        /// 删除属性值
        /// </summary>
        /// <param name="hc"></param>
        public void DeleteAttributeValue(HttpContext hc)
        {
            int strID = GetRequest("ID", 0);
            DataTable dt = goodsService.GetDataByKey("T_GoodsAttributeValue", "ID", strID);
            if (dt.Rows.Count > 0)
            {
                res = goodsService.Delete("T_GoodsAttributeValue", "ID", strID);
            }
            ResponseWrite(hc, res > 0 ? "0" : "1");
        }
        #endregion

    }
}
