﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Common;

namespace Weigou.Manage.Product
{
    public partial class ManagePic : ManagePage
    {
        /// <summary>
        /// 商品/商户ID
        /// </summary>
        public int TargetID
        {
            get { return GetRequest("TargetID", 0); }
        }
        /// <summary>
        /// 商品/商户名称
        /// </summary>
        public string CodeName
        {
            get { return GetRequest("Name", ""); }
        }
        /// <summary>
        /// 图片类别(1商品图片 2-其它图片 )
        /// </summary>
        public int Type
        {
            get { return GetRequest("Type", 0); }
        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.hfTargetID.Value = TargetID.ToString();
                this.labCodeName.Text = CodeName;
                this.hfType.Value = Type.ToString();
            }
        }
    }
}
