using Weigou.Common;
using Weigou.Service;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Menu;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weigou.Weixin
{
    public partial class _Default :BasePage
    {
        //注入
        IApplicationContext ctx;
        private static ISysService sysService;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ctx = ContextRegistry.GetContext();
                sysService = (ISysService)ctx["sysService"];
                this.repMenu.DataSource = sysService.GetData("T_WeixinMenu"); 
                this.repMenu.DataBind();
            }
        }
        /// <summary>
        /// 同步菜单至微信
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreateMenu_Click(object sender, EventArgs e)
        {
            string appID = "wx94d65eaa711a0bc0";
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
            AccessTokenResult result = CommonApi.GetToken(Utils.GetConfig("AppId"), Utils.GetConfig("AppSecret"));
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