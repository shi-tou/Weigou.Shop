using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SigeShop.Web.Fancy
{
    public partial class index : Page
    {
        protected void Button1_Click(object sender, EventArgs e)
        {
            string temp;
            StringBuilder builder = new StringBuilder();
            builder.Append("<hr/>");

            foreach (string key in "e d t r".Split(' '))
            {
                temp = Form.ID + "_" + key;
                builder.AppendFormat("<strong class=\"ttl\">{0}：</strong> <span class=\"blue\">{1}</span><br/>", temp, Request.Form[temp]);
            }

            builder.Append("<hr/>");
            IterControls(Form.Controls, builder);
            builder.Append("<hr/>");
            result.Text = builder.ToString();
        }

        void IterControls(ControlCollection controls, StringBuilder builder)
        {
            if (controls != null)
            {
                string temp;
                foreach (Control c in controls)
                {
                    temp = GetValue(c);
                    if (temp != null)
                    {
                        builder.AppendFormat("<strong class=\"ttl\">{0}：</strong> <span class=\"blue\">{1}</span><br/>", c.ID, temp);
                    }
                    else
                        IterControls(c.Controls, builder);
                }
            }
        }

        string GetValue(Control c)
        {
            if (c is TextBox)
            {
                return ((TextBox)c).Text;
            }
            else if (c is ListControl)
            {
                ListControl list = (ListControl)c;
                StringBuilder builder2 = new StringBuilder();
                foreach (ListItem item in list.Items)
                {
                    if (item.Selected)
                        builder2.AppendFormat("{1},", item.Text, item.Value);
                }
                if (builder2.Length > 0) builder2.Length = builder2.Length - 1;
                return builder2.ToString();
            }
            else if (c is HtmlInputCheckBox)
            {
                return ((HtmlInputCheckBox)c).Checked.ToString();
            }
            else if (c is ICheckBoxControl)
            {
                return ((ICheckBoxControl)c).Checked.ToString();
            }
            else if (c is FileUpload)
            {
                FileUpload file = (FileUpload)c;
                return string.Format("{0} ({1})", file.PostedFile.FileName, file.PostedFile.ContentLength);
            }
            return null;
        }
    }

}