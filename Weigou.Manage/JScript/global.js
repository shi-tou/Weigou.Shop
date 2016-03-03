//文件名：global.js
//描述：全局方法/变量
//时间：2013-09-29


//-----------描述：返回值-----------
var RT =
{
    // 成功
    SUCCESS: 0,
    // 失败
    FAILED: -1,
    // 已存在/重复
    RESULT_EXIST: 101,
    // 不存在
    RESULT_NOT_EXIST: 102,
    // 错误密码
    RESULT_ERROR_PASSWORD: 103,
    // 锁定
    RESULT_LOCK: 104,
    // 卡号已存在
    RESULT_CARDNO_EXIST: 105,
    // 名称已存在/重复
    RESULT_NAME_EXIST: 106,
    // 手机已存在/重复
    RESULT_MOBILENO_EXIST: 107,
    // 编码已存在/重复
    RESULT_CODE_EXIST: 108,
    // 金额不足
    RESULT_ACCOUNT_NOT_ENOUGH: 109,
    // 积分不足
    RESULT_SCORE_NOT_ENOUGH: 110
};

//----------描述：性别-----------
var EnumSex =
{
    // 0-未知/保密
    Unknown: 0,
    // 1-男
    M: 1,
    // 2-女
    F: 2
};
//---------会员状态-----------
var EnumMemStatus = {
    /// 0-不可用(待审核)
    Disabled: 0,
    /// 1-可用（审核通过）
    Normal: 1,
    /// 2-审核不通过
    DisAudit: 2,
    /// 7-挂失
    Lossed: 7,
    /// 8-锁定/冻结
    Locked: 8,
    /// 9-删除
    Delete: 9
};
//-----------描述:状态--------
var EnumStatus =
{
    /// 0-停用
    Disabled: 0,
    /// 1-(审核通过)启用
    Normal: 1,
    /// 2-审核不通过
    DisAudit: 2,
    /// 9-删除(逻辑删除)
    Delete : 9
};

/// 账户/积分操作类型
var EnumChangeType =
{
    // 0-登记赠送
    RegistAdd: 0,
    // 1-手动增加
    ManualAdd: 1,
    // 2-手动扣减
    ManualMinus: 2,
    // 3-消费赠送
    ConsumeAdd: 3,
    // 4-消费扣减
    ConsumeMinus: 4,
    // 5-会员充值
    RechargeAdd: 5,
    // 6-礼品兑换
    GiftExchange:6
}
/*------------系统模块-----------*/
var EnumModule =
{
    //1-商户管理
    MerchantManage: 1,
    // 2-会员管理
    MemberManage: 2,
    // 3-商品中心
    GoodsManage: 3,
    // 4-积分管理
    ScoreManage: 4,
    // 5-短信管理
    SmsManage: 5,
    // 6-报表管理
    ReportManage: 6,
    // 7-系统管理
    SystemManage: 7,
    // 8-订单管理
    OrderManage: 8,
    // 9-内容管理
    ContentManage: 9,
    // 10-活动管理
    ActivityManage: 10,
    // 0-其他
    Other: 99
}
var EnumOperation =
{
    // 0-登录
    Login: 0,
    // 1-添加
    Add: 1,
    // 2-修改
    Edit: 2,
    // 3-删除
    Delete: 3,
    // 4-审核通过
    Audit: 4,
    // 5-导入
    Import: 5,
    // 6-导出
    Export: 6,
    // 98登出
    LoginOut: 98,
    // 99-其他
    Other: 99
}