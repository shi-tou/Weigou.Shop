using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Model.Enum;

namespace Weigou.Admin.Goods
{
    public partial class GoodsCommentReply : AdminPage
    {
        /// <summary>
        /// 评论ID
        /// </summary>
        private int _ID
        {
            get { return GetRequest("ID", 0); }
        }
        /// <summary>
        /// 是否编辑
        /// </summary>
        private int _IsEdit
        {
            get { return GetRequest("IsEdit", -1); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (_IsEdit == 0)
            {
                this.BtnSubmit.Enabled = false;
                this.BtnSubmit.Visible = false;
                this.btnCancel.Text = "返回";
            }
            if (!IsPostBack)
            {
                if (_ID != 0)
                {
                    BindInfo();
                }
            }
        }

        /// <summary>
        /// 绑定评论信息
        /// </summary>
        public void BindInfo()
        {
            DataTable dt = goodsService.GetDataByKey("T_Comment", "ID", _ID);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                this.labFirstCommentTime.Text = Convert.ToString(dr["CreateTime"]);
                this.labFirstCommentContent.Text = Convert.ToString(dr["Content"]);
                DataTable dt2 = goodsService.GetDataByKey("T_Comment", "ParentID", Convert.ToString(dr["ID"]));
                if (dt2.Rows.Count > 0)
                {
                    DataRow dr2 = dt2.Rows[0];
                    this.labContent.Text = Convert.ToString(dr2["Content"]);
                    this.labContentTime.Text = Convert.ToString(dr2["CreateTime"]);
                    this.txtContent.Visible = false;
                    this.BtnSubmit.Visible = false;
                    //DataTable dt3 = goodsService.GetDataByKey("T_Comment", "ParentID", Convert.ToString(dr2["ID"]));
                    //if (dt3.Rows.Count > 0)
                    //{
                    //    DataRow dr3 = dt3.Rows[0];
                    //    this.labAppendCommentTime.Text = Convert.ToString(dr3["CreateTime"]);
                    //    this.labAppendCommentContent.Text = Convert.ToString(dr3["Content"]);
                    //}
                }
                else
                {
                    this.labContent.Visible = false;
                }
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            //获取评论数据
            DataTable commentDt = goodsService.GetDataByKey("T_Comment", "ID", _ID);
            DataRow commentDr = commentDt.Rows[0];

            //新增回复数据
            DataTable consultDt = sysService.GetDataByKey("T_Comment", "ID", 0);
            DataRow consultDr = consultDt.NewRow();
            consultDr["ParentID"] = _ID;
            consultDr["OrderID"] = Convert.ToString(commentDr["OrderID"]);
            consultDr["Content"] = this.txtContent.Text;
            consultDr["CreateTime"] = DateTime.Now;
            consultDr["ReplyBy"] = UserInfo.ID;
            consultDt.Rows.Add(consultDr);

            int res = goodsService.UpdateDataTable(consultDt);
            if (res > 0)
            {
                goodsService.SaveSysLog(_ID.ToString(), EnumModule.GoodsManage, EnumOperation.Add, UserInfo.ID, "回复商品评论");
                RegistScript("CloseWin('回复成功！',parent.GetList);");
            }
            else
            {
                RegistScript("ShowMsg('回复失败');");
            }
        }

    }
}