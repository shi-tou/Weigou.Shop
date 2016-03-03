using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Api.Base;
using Weigou.Common;
using Weigou.Service;

namespace Weigou.Api
{
    public partial class UploadPhoto : System.Web.UI.Page
    {
        #region 注入
        public static IMemberService memberService;
        public IMemberService MemberService
        {
            set { memberService = value; }
            get { return memberService; }
        }
        #endregion

        /// <summary>
        /// 会员ID
        /// </summary>
        public int MemberID
        {
            get { return RequestHelper.GetRequest("MemberID", 0); }
        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            int memberID = RequestHelper.GetRequest("MemberID", 0);
            HttpFileCollection MyFilecollection = Request.Files;
            string filePath = "/upload/{0}/" + DateTime.Now.ToString("yyyyMM") + "/";
            filePath = string.Format(filePath, "Member");

            string absfilePath = Server.MapPath(filePath);//绝对路径
            string guid = Guid.NewGuid().ToString();//文件名
            string fileName = absfilePath + guid + ".jpg";//要保存的文件全路径   
            if (!Directory.Exists(absfilePath))
            {
                Directory.CreateDirectory(absfilePath);
            }
            Result result = new Result();
            try
            {
                MyFilecollection[0].SaveAs(fileName);
                string strPhoto = filePath + guid + ".jpg";
                int res = UpdateMemberPhoto(memberID, strPhoto);
                if (res > 0)
                {
                    result.status = RT.SUCCESS;
                    result.data = Utils.GetConfig("ServerPicPath") + strPhoto;
                    result.msg = "上传成功";
                }
                else
                {
                    result.status = RT.FAILED;
                    result.data = "";
                    result.msg = "上传失败";
                }
            }
            catch (Exception ex)
            {
                Utils.SaveLog("上传头像", ex.Message);
                result.status = RT.FAILED;
                result.data = "";
                result.msg = "上传失败：" + ex.Message;
            }
            Response.Write(JsonConvert.SerializeObject(result));
            Response.End();
        }
        /// <summary>
        /// 更改会员头像
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="strPhoto"></param>
        protected int UpdateMemberPhoto(int memberID, string strPhoto)
        {
            int res = 0;
            DataTable dt = memberService.GetDataByKey("T_Member", "ID", memberID);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dr = dt.Rows[0];
                dr["Photo"] = strPhoto;
                res = memberService.UpdateDataTable(dt);
            }
            return res;
        }
    }
}