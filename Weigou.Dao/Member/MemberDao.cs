using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Data.Common;
using Weigou.Common;
using System.Data;
using System.Collections;
using Weigou.Model.Enum;
using Weigou.Model;

namespace Weigou.Dao
{
    public class MemberDao : BaseDao, IMemberDao
    {
        #region 会员管理
        /// <summary>
        /// 会员列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetMemberList(Pager p, Hashtable hs)
        {
            string sql = @"select a.*,b.Name as EducationName, c.Name as OccupationName,d.Name as CreateName,l.Name as LevelName from T_Member as a
                            left join T_BaseData b on b.Value=a.Education and b.ParentID=@Education
                            left join T_BaseData c on c.Value=a.Occupation and c.ParentID=@Occupation
                            left join T_User d on d.ID=a.CreateBy
                            left join T_MemberLevel l on l.ID=a.LevelID
                            where 1=1";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            param.AddWithValue("Education", BaseDataConst.Education);
            param.AddWithValue("Occupation", BaseDataConst.Occupation);
            if (hs.Contains("Name"))
            {
                sql += " and a.Name like @Name";
                param.AddWithValue("Name", "%" + hs["Name"] + "%");
            }
            if (hs.Contains("MobileNo"))
            {
                sql += " and a.MobileNo like @MobileNo";
                param.AddWithValue("MobileNo", "%" + hs["MobileNo"] + "%");
            }
            if (hs.ContainsKey("LevelID"))
            {
                sql += "  and a.LevelID=@LevelID";
                param.AddWithValue("LevelID", hs["LevelID"].ToString());
            }
            if (hs.Contains("Sex"))
            {
                sql += " and a.Sex=@Sex";
                param.AddWithValue("Sex", hs["Sex"]);
            }
            if (hs.ContainsKey("Status"))
            {
                sql += "  and a.Status=@Status";
                param.AddWithValue("Status", hs["Status"].ToString());
            }
            if (hs.Contains("MinTime"))
            {
                sql += " and datediff(day, @MinTime, a.CreateTime) >= 0";
                param.AddWithValue("MinTime", hs["MinTime"]);
            }
            if (hs.Contains("MaxTime"))
            {
                sql += " and datediff(day, a.CreateTime, @MaxTime) >= 0";
                param.AddWithValue("MaxTime", hs["MaxTime"]);
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return RT.SUCCESS;
        }
        /// <summary>
        /// 根据id获取会员
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public DataTable GetMemberInfo(int memberID)
        {
            string sql = @"select a.*, b.Name as EducationName, c.Name as OccupationName  from T_Member a
                            left join T_BaseData b on b.Value=a.Education and b.ParentID=@Education
                            left join T_BaseData c on c.Value=a.Occupation and c.ParentID=@Occupation
                            where a.ID=@MemberID ";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            param.AddWithValue("Education", BaseDataConst.Education);
            param.AddWithValue("Occupation", BaseDataConst.Occupation);
            param.AddWithValue("MemberID", memberID);
            return AdoTemplate.DataTableCreateWithParams(CommandType.Text, sql, param);
        }
        /// <summary>
        /// 根据卡号或手机号获取会员
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public DataTable GetMemberInfo(string mobileOrEmail)
        {
            string sql = @"select * from T_Member where Email=@Key or MobileNo=@Key";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            param.AddWithValue("key", mobileOrEmail);
            return AdoTemplate.DataTableCreateWithParams(CommandType.Text, sql, param);
        }
        /// <summary>
        /// 会员申请解冻列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetMemberUnlockList(Pager p, Hashtable hs)
        {
            string sql = @"select a.*,b.Name as MemberName,b.MobileNo,b.Email,b.Status as MemberStatus,c.Name asApprovedName from T_MemberUnlock as a
                            left join T_Member b on b.ID=a.MemberID
                            left join T_User c on c.ID=a.ApprovedBy
                            where 1=1";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("MemberName"))
            {
                sql += " and b.Name like @Name";
                param.AddWithValue("Name", "%" + hs["MemberName"] + "%");
            }
            if (hs.Contains("MobileNo"))
            {
                sql += " and a.MobileNo like @MobileNo";
                param.AddWithValue("MobileNo", "%" + hs["MobileNo"] + "%");
            }
            if (hs.Contains("Email"))
            {
                sql += " and a.Email like @Email";
                param.AddWithValue("Email", "%" + hs["Email"] + "%");
            }
            if (hs.ContainsKey("Status"))
            {
                sql += "  and a.Status=@Status";
                param.AddWithValue("Status", hs["Status"].ToString());
            }
            if (hs.Contains("MinTime"))
            {
                sql += " and datediff(day, @MinTime, a.CreateTime) >= 0";
                param.AddWithValue("MinTime", hs["MinTime"]);
            }
            if (hs.Contains("MaxTime"))
            {
                sql += " and datediff(day, a.CreateTime, @MaxTime) >= 0";
                param.AddWithValue("MaxTime", hs["MaxTime"]);
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return RT.SUCCESS;
        }
        #endregion

        #region 会员账户管理
        /// <summary>
        /// 会员账户余额
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roletype"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public decimal GetAccountBalance(int memberID)
        {
            string sql = "select top 1 * from T_Account where MemberID=@MemberID order by ID desc";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            param.AddWithValue("MemberID", memberID);
            DataTable dt = AdoTemplate.DataTableCreateWithParams(CommandType.Text, sql, param);
            if (dt.Rows.Count == 0)
                return 0;
            return Convert.ToDecimal(dt.Rows[0]["Balance"]);
        }
        /// <summary>
        /// 获取会员账户明细
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetAccountList(Pager p, Hashtable hs)
        {
            string sql = @"select a.*,b.Name as MemberName,c.Name as CreateName from T_Account a
                            inner join T_Member b on b.ID=a.MemberID
                            left join T_User c on c.CreateBy=a.CreateBy
                            where 1=1 ";
            IDbParameters param = AdoTemplate.CreateDbParameters();

            if (hs.Contains("MemberID"))
            {
                sql += " and a.MemberID=@MemberID";
                param.AddWithValue("MemberID", hs["MemberID"]);
            }
            if (hs.Contains("Type"))
            {
                if (hs["Type"].ToString() == "1")
                {
                    sql += " and a.Account>0";
                }
                else
                {
                    sql += " and a.Account<0";
                }
            }
            if (hs.Contains("MinTime"))
            {
                sql += " and datediff(day,a.CreateTime,@MinTime)<=0 ";
                param.AddWithValue("MinTime", hs["MinTime"].ToString());
            }
            if (hs.Contains("MaxTime"))
            {
                sql += " and datediff(day,a.CreateTime,@MaxTime)>=0";
                param.AddWithValue("MaxTime", hs["MaxTime"].ToString());
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return RT.SUCCESS;
        }
        #endregion

        #region 会员收藏
        /// <summary>
        /// 添加收藏
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int GetFavoriteList(Pager p, Hashtable hs)
        {
            int type = Convert.ToInt16(hs["Type"]);
            IDbParameters param = AdoTemplate.CreateDbParameters();
            string sql = "";
           
            //商品收藏
            if (type == (int)EnumFavType.Goods)
            {
                sql = @"select a.ID,a.CreateTime,b.Name,b.Price,b.SmallPicture as Picture,c.FavCount from dbo.T_Favorite a
                            inner join 
                            (
                                select a.*,b.SmallPicture from T_Goods a 
                                left join T_Picture b on b.TargetID=a.ID and b.Type=@PicType and b.IsTop=1
                            ) b on b.ID=a.TargetID
                            left join 
                            (
                                select COUNT(ID) as FavCount,Type,TargetID from T_Favorite group by Type,TargetID
                            ) c on c.TargetID=a.TargetID and c.Type=@Type
                            where 1=1 ";
                param.AddWithValue("PicType", (int)EnumPicture.Goods);
            }
            if (hs.Contains("MemberID"))
            {
                sql += " and a.MemberID = @MemberID";
                param.AddWithValue("MemberID", hs["MemberID"]);
            }
            sql += " and a.Type = @Type";
            param.AddWithValue("Type", type);

            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return RT.SUCCESS;
        }
        #endregion

        #region 会员评论
        /// <summary>
        /// 会员评论
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetCommentsList(Pager p, Hashtable hs)
        {
            string @sql = @"select a.*,b.Name as GoodsName, c.Name as MerchantName,d.SmallPicture as GoodsPic,e.Content as AppendContent ,e.CreateTime as AppendTime,f.Content as ReplyContent,f.CreateTime as ReplyTime from dbo.T_Comment a
                            left join T_Goods b on b.ID=a.GoodsID
                            left join T_Merchant c on c.ID=b.MerchantID
                            left join T_Picture d on d.TargetID=a.GoodsID and d.Type=1 and d.IsTop=1
                            left join T_Comment e on e.ParentID=a.ID and e.MemberID=a.MemberID
                            left join T_Comment f on f.ParentID=a.ID and isnull(f.ReplyBy,0)<>0
                            where a.ParentID=0 ";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("MemberID"))
            {
                sql += " and a.MemberID=@MemberID";
                param.AddWithValue("MemberID", hs["MemberID"]);
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return RT.SUCCESS;
        }
        /// <summary>
        /// 会员评论
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetGoodsCommentInfoByID(Pager p, Hashtable hs)
        {
            string sql = @"select a.Star,b.MobileNo,a.CreateTime as CommentTime,a.SaleProp,a.Content as CommentContent,c.Content as ReplyContent,c.CreateTime as ReplyTime from T_Comment a
                           left join T_Member b on b.ID=a.MemberID
                           left join T_Comment c on c.ParentID=a.ID and isnull(c.ReplyBy,0)<>0
                           where 1=1 and a.[Type]=2 ";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("GoodsID"))
            {
                sql += " and a.TargetID=@TargetID";
                param.AddWithValue("TargetID", hs["GoodsID"]);
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return RT.SUCCESS;
        }
        #endregion

        #region 收货地址管理

        /// <summary>
        /// 收货地址列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetDeliverAddressList(Pager p, Hashtable hs)
        {
            string sql = @"select a.*, b.ProvinceName,c.CityName ,d.DistrictName from T_DeliverAddress a
                            left join T_Province b on b.ID=a.ProvinceID
                            left join T_City c on c.ID=a.CityID
                            left join T_District d on d.ID=a.DistrictID
                            where a.Status=" + (int)EnumStatus.Normal;
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("MemberID"))
            {
                sql += " and a.MemberID = @MemberID";
                param.AddWithValue("MemberID", hs["MemberID"]);
            }
            else if (hs.Contains("ID"))
            {
                sql += " and a.ID = @ID";
                param.AddWithValue("ID", hs["ID"]);
            }

            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return RT.SUCCESS;
        }

        /// <summary>
        /// 获取收货地址
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public DataTable GetDeliverAddress(Hashtable hs)
        {
            string sql = @"select a.*, b.ProvinceName,c.CityName ,d.DistrictName from T_DeliverAddress a
                            left join T_Province b on b.ID=a.ProvinceID
                            left join T_City c on c.ID=a.CityID
                            left join T_District d on d.ID=a.DistrictID 
                            where a.Status=" + (int)EnumStatus.Normal;
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("MemberID"))
            {
                sql += " and a.MemberID = @MemberID";
                param.AddWithValue("MemberID", hs["MemberID"]);
            }
            if (hs.Contains("IsDefault"))
            {
                sql += " and a.IsDefault = @IsDefault";
                param.AddWithValue("IsDefault", hs["IsDefault"]);
            }
            if (hs.Contains("ID"))
            {
                sql += " and a.ID = @DeliverAddressID";
                param.AddWithValue("DeliverAddressID", hs["ID"]);
            }
            return AdoTemplate.DataTableCreateWithParams(CommandType.Text, sql, param);
        }

        #endregion

        #region 购物车相关
        /// <summary>
        /// 获取购物车列表
        /// </summary>
        /// <param name="strMemberID">会员ID</param>
        /// <returns></returns>
        public DataTable GetShoppingCartList(int strMemberID)
        {
            string sql = @"select a.*,(case when a.SaleProp='0' then c.SalePrice else d.SalePrice end) as SalePrice,e.SmallPicture,c.Name from T_ShoppingCart a
                          inner join T_Member b on a.MemberID=b.ID
                          inner join T_Goods c on a.GoodsID=c.ID
                          left join T_GoodsSaleProp d on d.SaleProp=a.SaleProp
                          left join T_Picture e on c.ID=e.TargetID
                          where b.ID=@MemberID and c.Status=" + ((int)EnumStatus.Normal).ToString();
            IDbParameters param = AdoTemplate.CreateDbParameters();
            param.AddWithValue("MemberID", strMemberID);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            return ds.Tables[0];
        }
        #endregion

        #region 订单管理
        /// <summary>
        /// 订单列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetOrderList(Pager p, Hashtable hs)
        {
            string sql = @"select a.*,b.MemName from T_Order a
                            left join T_Member b on b.ID=a.MemberID
                            where 1=1";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("OrderNo"))
            {
                sql += " and a.OrderNo like @OrderNo";
                param.AddWithValue("OrderNo", "%" + hs["OrderNo"] + "%");
            }
            if (hs.Contains("Status"))
            {
                sql += " and a.Status = @Status";
                param.AddWithValue("Status", hs["Status"]);
            }
            if (hs.Contains("Paid"))
            {
                sql += " and a.Paid = @Paid";
                param.AddWithValue("Paid", hs["Paid"]);
            }
            if (hs.Contains("MinTime"))
            {
                sql += " and datediff(day, @MinTime, a.CreateTime) >= 0";
                param.AddWithValue("MinTime", hs["MinTime"]);
            }
            if (hs.Contains("MaxTime"))
            {
                sql += " and datediff(day, a.CreateTime, @MaxTime) >= 0";
                param.AddWithValue("MaxTime", hs["MaxTime"]);
            }
            if (hs.Contains("MinPayTime"))
            {
                sql += " and datediff(day, @MinPayTime, a.PayTime) >= 0";
                param.AddWithValue("MinPayTime", hs["MinPayTime"]);
            }
            if (hs.Contains("MaxPayTime"))
            {
                sql += " and datediff(day, a.PayTime, @MaxPayTime) >= 0";
                param.AddWithValue("MaxPayTime", hs["MaxPayTime"]);
            }
            if (hs.Contains("MemName"))
            {
                sql += " and b.MemName like @MemName";
                param.AddWithValue("MemName", "%" + hs["MemName"] + "%");
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return RT.SUCCESS;
        }

        #endregion

        #region 会员级别
        /// <summary>
        /// 会员级别列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        public int GetMemberLevelList(Pager p, Hashtable hs)
        {
            string sql = @"select a.*,d.Name as CreateName from T_MemberLevel a
                            left join T_User d on d.ID=a.CreateBy
                            where 1=1 and a.Status<>9 ";
            IDbParameters param = AdoTemplate.CreateDbParameters();            
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return RT.SUCCESS;
        }

        #endregion

        #region 环信IM管理
        /// <summary>
        /// 环信用户列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetEasemobUserList(Pager p, Hashtable hs)
        {
            string sql = @"select a.*,b.Name,b.NickName,b.MobileNo,b.Sex,b.Photo from T_EasemobUser a 
                            inner join T_Member b on b.ID=a.MemberID 
                            where 1=1";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("UserName"))
            {
                sql += " and a.UserName like @UserName";
                param.AddWithValue("UserName", "%" + hs["UserName"] + "%");
            }
            if (hs.Contains("Name"))
            {
                sql += " and b.Name like @Name";
                param.AddWithValue("Name", "%" + hs["Name"] + "%");
            }
            if (hs.Contains("MobileNo"))
            {
                sql += " and b.MobileNo like @MobileNo";
                param.AddWithValue("MobileNo", "%" + hs["MobileNo"] + "%");
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return RT.SUCCESS;

        }
        /// <summary>
        /// 获取环信群组
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetEasemobGroupList(Pager p, Hashtable hs)
        {
            string sql = @"select a.*,c.Name as MemberName,c.NickName from T_EasemobGroup a
                            inner join T_EasemobUser b on b.UserName=a.Owner
                            inner join T_Member c on c.ID=b.MemberID
                            where 1=1 ";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("GroupName"))
            {
                sql += " and a.GroupName like @GroupName";
                param.AddWithValue("GroupName", "%" + hs["GroupName"] + "%");
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return RT.SUCCESS;
        }
        #endregion
    }
}
