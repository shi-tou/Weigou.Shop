using Weigou.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Model.Content;
using Weigou.Model.Enum;

namespace Weigou.Manage.Content
{
    public partial class NewsAdd : ManagePage
    {
        //ID
        private int _ID
        {
            get { return GetRequest("ID", 0); }
        }

        /// <summary>
        /// 加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = contentService.GetData("T_WeixinKeyword");
                this.cblKeyword.DataSource = dt;
                this.cblKeyword.DataTextField = "Name";
                this.cblKeyword.DataValueField = "ID";
                this.cblKeyword.DataBind();
                BindInfo();
            }
        }
        /// <summary>
        /// 绑定
        /// </summary>
        public void BindInfo()
        {
            int index = 0;
            BindType(this.ddlType, contentService.GetData("T_NewsType"), 0, ref index);
            if (_ID == 0)
                return;
            DataTable dt2 = contentService.GetDataByKey("T_News", "ID", _ID);
            DataRow dr = dt2.Rows[0];
            this.ddlType.SelectedValue = dr["Type"].ToString();
            this.txtTitle.Text = Convert.ToString(dr["Title"]);
            this.txtContent.Text = Convert.ToString(dr["Description"]);
            this.hfPicture.Value = dr["Picture"].ToString();
            this.PictureUrl.ImageUrl = dr["Picture"].ToString();
            //绑定文章关键词
            DataTable dtKeyword = contentService.GetDataByKey("T_NewsKeyword", "NewsID", _ID);
            foreach (DataRow drKey in dtKeyword.Rows)
            {
                foreach (ListItem li in this.cblKeyword.Items)
                {
                    if (li.Value == drKey["KeywordID"].ToString())
                    {
                        li.Selected = true;
                    }
                }
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            NewsInfo info = new NewsInfo();
            info.ID = _ID;
            info.Type = Convert.ToInt32(this.ddlType.SelectedValue);
            info.Title = this.txtTitle.Text;
            info.Description = this.txtContent.Text;
            info.Picture = Upload(this.txtPicture, this.hfPicture.Value, "News");
            info.Status = (int)EnumStatus.Normal;
            info.CreateBy = UserInfo.ID;
            info.CreateTime = DateTime.Now;
            int res = contentService.SaveNewsInfo(info, GetCheckBoxValue());
            if (res > 0)
                ShowMsg("/Content/NewsList.aspx", "恭喜您，操作成功！");
            else
                ShowMsg("/Content/NewsList.aspx", "很抱歉，操作失败,请重试！");
        }
        /// <summary>
        /// 获取关键词
        /// </summary>
        /// <returns></returns>
        public List<string> GetCheckBoxValue()
        {
            List<string> list = new List<string>();
            foreach (ListItem li in this.cblKeyword.Items)
            {
                if (li.Selected)
                {
                    list.Add(li.Value);
                }
            }
            return list;
        }
    }
}