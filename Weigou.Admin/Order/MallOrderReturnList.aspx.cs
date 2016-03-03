using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weigou.Admin.Order
{
    public partial class MallOrderReturnList : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ToolBar1.PrivilegeCode = "WeixinReturnList";
                this.ToolBar2.PrivilegeCode = "AliPayReturnList";
                this.ToolBar3.PrivilegeCode = "UnionpayReturnList";
                this.hfWechat.Value = CheckAuth("ViewReturnWeixinDetail").ToString();//编辑权限
                this.hfAlipay.Value = CheckAuth("ViewReturnAliPayDetail").ToString();//编辑权限
                this.hfUnionPay.Value = CheckAuth("ViewReturnUnionpayDetail").ToString();//编辑权限
            }
        }
    }
}