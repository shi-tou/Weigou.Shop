using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weiche.Model
{
    /// <summary>
    /// 收货人信息管理
    /// </summary>
    public class DeliverAddress
    {
        public DeliverAddress() { }
        private int _id;
        private int _memberid;
        private int _provinceid;
        private int _cityid;
        private int _discrictid;
        private string _zipcode;
        private string _address;
        private string _consigneename;
        private string _consigneemobileno;
        private int _isdefault;
        private DateTime _createtime;

        public DateTime CreateTime
        {
            get { return _createtime; }
            set { _createtime = value; }
        }
        /// <summary>
        /// 是否默认地址
        /// </summary>
        public int IsDefault
        {
            get { return _isdefault; }
            set { _isdefault = value; }
        }
        /// <summary>
        /// 收货人手机
        /// </summary>
        public string ConsigneeMobileNo
        {
            get { return _consigneemobileno; }
            set { _consigneemobileno = value; }
        }
        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string ConsigneeName
        {
            get { return _consigneename; }
            set { _consigneename = value; }
        }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        /// <summary>
        /// 邮编
        /// </summary>
        public string ZipCode
        {
            get { return _zipcode; }
            set { _zipcode = value; }
        }
        /// <summary>
        /// 地区
        /// </summary>
        public int DiscrictID
        {
            get { return _discrictid; }
            set { _discrictid = value; }
        }
        /// <summary>
        /// 城市
        /// </summary>
        public int CityID
        {
            get { return _cityid; }
            set { _cityid = value; }
        }
        /// <summary>
        /// 省份
        /// </summary>
        public int ProvinceID
        {
            get { return _provinceid; }
            set { _provinceid = value; }
        }
        /// <summary>
        /// 会员ID
        /// </summary>
        public int MemberID
        {
            get { return _memberid; }
            set { _memberid = value; }
        }
        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
    }
}
