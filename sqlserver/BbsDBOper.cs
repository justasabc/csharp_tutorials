using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

//=====================
using System.Data.SqlClient;//
//=====================
public class BbsDBOper
{
    public BbsDBOper()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    /*
     * 函数功能：/插入Member信息
     * 参数：strUserName(用户名)strUserPwd(密码)strEmail(电子邮件)
     * 返回值：成功或失败
     */
    public bool InsertMemberInfo(string strUserName, string strUserPwd, string strEmail)
    {
        if (string.IsNullOrEmpty(strUserName) || string.IsNullOrEmpty(strUserPwd) || string.IsNullOrEmpty(strEmail))
            return false;

        MySQLBase sqlBase = new MySQLBase();
        try
        {
            string strSQL = "insert into Member values(@varUserName,@varUserPwd,@varEmail)";
            sqlBase.Connect();
            //设置相应的参数
            int paramCount = 3;
            SqlParameter[] sqlParams = new SqlParameter[paramCount];
            /*UserName UserPwd Email
             nvarchar(20) nvarchar(20) nvarchar(50)*/
            //参数1  
            int index = 0;//
            sqlParams[index] = new SqlParameter();
            sqlParams[index].ParameterName = "@varUserName";//参数名称
            sqlParams[index].SqlDbType = SqlDbType.NVarChar;//参数类型
            sqlParams[index].Size = 20;//参数的长度
            sqlParams[index].Value = strUserName;//参数值
            sqlParams[index].Direction = ParameterDirection.Input;//输入参数
            //参数2   
            index++;
            sqlParams[index] = new SqlParameter();
            sqlParams[index].ParameterName = "@varUserPwd";//参数名称
            sqlParams[index].SqlDbType = SqlDbType.NVarChar;//参数类型
            sqlParams[index].Size = 20;//参数的长度
            sqlParams[index].Value = strUserPwd;//参数值
            sqlParams[index].Direction = ParameterDirection.Input;//输入参数
            //参数3   
            index++;
            sqlParams[index] = new SqlParameter();
            sqlParams[index].ParameterName = "@varEmail";//参数名称
            sqlParams[index].SqlDbType = SqlDbType.NVarChar;//参数类型
            sqlParams[index].Size = 50;//参数的长度
            sqlParams[index].Value = strEmail;//参数值
            sqlParams[index].Direction = ParameterDirection.Input;//输入参数
            //执行SQL语句
            int ret = -1;
            ret = sqlBase.RunSQL(strSQL, sqlParams);//
            sqlBase.DisConnect();
            if (ret > 0) return true;
            else return false;
        }
        catch (System.Exception ex)
        {
            return false;
        }
        finally
        {
            if (sqlBase.HasConnected())
                sqlBase.DisConnect();
        }
    }
    /*
    * 函数功能：获取登录状态
    * 参数：strUserName(用户名)strUserPwd(密码)
    * 返回值：用户名不存在-1，用户名存在但密码不正确0，用户名存在且密码正确1
    */
    public enum LoginState:int
    {
        NoneUser = -1, //用户不存在
        ErrowPwd = 0,   //密码错误
        LoginSuccess = 1 //登录成功
    }

    public LoginState GetLoginState(string strUserName, string strUserPwd)
    {
        if (string.IsNullOrEmpty(strUserName) || string.IsNullOrEmpty(strUserPwd))
            return LoginState.NoneUser;

        //1判断用户名strUserName是否已经存在
        bool bExist = false;
        MySQLExtend sqlExtend = new MySQLExtend();
        bExist = sqlExtend.IsExistID("Member", "UserName", strUserName);
        if (!bExist)
        {
            return LoginState.NoneUser;
        }
        else
        {
            //2判断密码是否正确
            MySQLBase sqlBase = new MySQLBase();
            try
            {
                string strSQL = "select* from Member where UserName='" + strUserName + "' and UserPwd='" + strUserPwd + "'";
                sqlBase.Connect();
                SqlDataReader reader = sqlBase.GetDataReader(strSQL, null);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        return LoginState.LoginSuccess;
                    }
                    else
                    {
                        return LoginState.ErrowPwd;
                    }
                }
                else
                {
                    return LoginState.ErrowPwd;
                }
            }
            catch (System.Exception ex)
            {
                return LoginState.ErrowPwd;
            }
            finally
            {
                if (sqlBase.HasConnected())
                    sqlBase.DisConnect();
            }
        }
    }

    /*     
    * 函数功能：插入帖子信息
    * 参数：参数列表
    * 返回值：成功或失败
    */
    public bool InsertInfo(int ID,string strTitleName, string strContentInfo, DateTime dtPostTime, string strReplyID, string strUserName)
    {
        if (string.IsNullOrEmpty(strTitleName) || string.IsNullOrEmpty(strContentInfo) || string.IsNullOrEmpty(strReplyID) || string.IsNullOrEmpty(strUserName) || dtPostTime == null)
            return false;

        MySQLBase sqlBase = new MySQLBase();
        try
        {
            /* ID  TitleName ContentInfo PostTime ReplyID          UserName
             * int nvarchar(50) ntext          datetime  nvarchar(50)  nvarchar(20)
             */
            string strSQL = "insert into Info values(@varID,@varTitleName,@varContentInfo,@varPostTime,@varReplyID,@varUserName)";
            sqlBase.Connect();
            //设置相应的参数
            int paramCount = 6;
            SqlParameter[] sqlParams = new SqlParameter[paramCount];
            //参数1  
            int index = 0;
            sqlParams[index] = new SqlParameter();
            sqlParams[index].ParameterName = "@varID";//参数名称
            sqlParams[index].SqlDbType = SqlDbType.Int;//参数类型
            sqlParams[index].Value = ID;//参数值
            sqlParams[index].Direction = ParameterDirection.Input;//输入参数
            //参数2   
            index++;
            sqlParams[index] = new SqlParameter();
            sqlParams[index].ParameterName = "@varTitleName";//参数名称
            sqlParams[index].SqlDbType = SqlDbType.NVarChar;//参数类型
            sqlParams[index].Size = 50;//参数的长度
            sqlParams[index].Value = strTitleName;//参数值
            sqlParams[index].Direction = ParameterDirection.Input;//输入参数
            //参数3  
            index++;
            sqlParams[index] = new SqlParameter();
            sqlParams[index].ParameterName = "@varContentInfo";//参数名称
            sqlParams[index].SqlDbType = SqlDbType.NText;//参数类型
            sqlParams[index].Value = strContentInfo;//参数值
            sqlParams[index].Direction = ParameterDirection.Input;//输入参数
            //参数4 
            index++;
            sqlParams[index] = new SqlParameter();
            sqlParams[index].ParameterName = "@varPostTime";//参数名称
            sqlParams[index].SqlDbType = SqlDbType.DateTime;//参数类型
            sqlParams[index].Value = dtPostTime;//参数值
            sqlParams[index].Direction = ParameterDirection.Input;//输入参数
            //参数5  
            index++;
            sqlParams[index] = new SqlParameter();
            sqlParams[index].ParameterName = "@varReplyID";//参数名称
            sqlParams[index].SqlDbType = SqlDbType.NVarChar;//参数类型
            sqlParams[index].Size = 50;//参数的长度
            sqlParams[index].Value = strReplyID;//参数值
            sqlParams[index].Direction = ParameterDirection.Input;//输入参数
            //参数6 
            index++;
            sqlParams[index] = new SqlParameter();
            sqlParams[index].ParameterName = "@varUserName";//参数名称
            sqlParams[index].SqlDbType = SqlDbType.NVarChar;//参数类型
            sqlParams[index].Size =20;//参数的长度
            sqlParams[index].Value = strUserName;//参数值
            sqlParams[index].Direction = ParameterDirection.Input;//输入参数

            //执行SQL语句
            int ret = -1;
            ret = sqlBase.RunSQL(strSQL, sqlParams);//
            sqlBase.DisConnect();
            if (ret > 0) return true;
            else return false;
        }
        catch (System.Exception ex)
        {
            return false;
        }
        finally
        {
            if (sqlBase.HasConnected())
                sqlBase.DisConnect();
        }
    }
}
