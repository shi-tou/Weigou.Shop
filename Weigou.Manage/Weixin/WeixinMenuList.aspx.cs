using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Menu;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Common;
using Weigou.Config;

namespace Weigou.Manage.Weixin
{
    public partial class WeixinMenuList : ManagePage
    {
        public int ParentID
        {
            get { return GetRequest("ParentID", 0); }
        }
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindList();
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void BindList()
        {
            Hashtable hs = new Hashtable();
            hs.Add("ParentID", ParentID);
            DataTable dt = weixinService.GetWeixinMenuList(hs);
            this.repRole.DataSource = dt;
            this.repRole.DataBind();
            //添加按键、返回上一级
            DataTable dtP = sysService.GetDataByKey("T_WeixinMenu", "ID", ParentID);
            if (dtP.Rows.Count > 0)
            {
                this.backUrl.HRef = "WeixinMenuList.aspx?ParentID=" + dtP.Rows[0]["ParentID"];
                this.addUrl.HRef = "WeixinMenuAdd.aspx?ParentID=" + ParentID;
            }
            else
            {
                this.backUrl.Visible = false;
            }
        }
        /// <summary>
        /// 同步微信菜单 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSetMenu_Click(object sender, EventArgs e)
        {
            ButtonGroup buttonGrpup = new ButtonGroup();
            Hashtable hs = new Hashtable();
            DataTable dt = weixinService.GetWeixinMenuList(hs);

            DataTable dtParent = Utils.SelectDataTable(dt, "ParentID=0");
            foreach (DataRow dr in dtParent.Rows)
            {
                int count = Convert.ToInt32(dr["SubMenuCount"]);
                if (count == 0)
                {
                    buttonGrpup.button.Add(CreateButton(dr));
                }
                else
                {
                    SubButton subButton = new SubButton();
                    subButton.name = Convert.ToString(dr["Name"]);
                    DataTable dtSubMenu = Utils.SelectDataTable(dt, "ParentID=" + Convert.ToInt32(dr["ID"]));
                    foreach (DataRow drSubMenu in dtSubMenu.Rows)
                    {
                        subButton.sub_button.Add((SingleButton)CreateButton(drSubMenu));
                    }
                    buttonGrpup.button.Add(subButton);
                }
            }
            WeixinConfigInfo weixinConfigInfo = WeixinConfigs.GetConfig();
            AccessTokenResult result = CommonApi.GetToken(weixinConfigInfo.AppID, weixinConfigInfo.AppSecret);
            CommonApi.CreateMenu(result.access_token, buttonGrpup);
        }
        /// <summary>
        /// 创建菜单按钮实体
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public BaseButton CreateButton(DataRow dr)
        {
            string type = Convert.ToString(dr["Type"]);//click/view
            string name = Convert.ToString(dr["Name"]);
            string key = Convert.ToString(dr["Key"]);
            string url = Convert.ToString(dr["Url"]);
            if (type == "click")
            {
                SingleClickButton btn = new SingleClickButton();
                btn.type = type;
                btn.name = name;
                btn.key = key;
                return btn;
            }
            else
            {
                SingleViewButton btn = new SingleViewButton();
                btn.type = type;
                btn.name = name;
                btn.url = url;
                return btn;
            }
        }
    }
}