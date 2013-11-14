using System;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

//=====================
using System.Data;
using System.Data.SqlClient;//
//=====================
public class MySQLExtend : MySQLBase
{
    //扩展功能
    /*
     * 函数功能：获取表strTableName总记录数
     * 参数：表格名称strTableName
     * 返回值：总记录数
     */
    public int GetTableCount(string strTableName)
    {
        int count = 0;
        try
        {
            string strSQL = "select count(*) from " + strTableName;
            base.Connect();
            object objCount = base.GetValue(strSQL, null);
            /*或者也可以构造1个参数
             string strSQL = "select count(*) from @tableName";
             SqlParameter[] sqlParams = new SqlParameter[1];
            */
            if (objCount != null)
                count = (int)objCount;
            base.DisConnect();
            return count;
        }
        catch (System.Exception ex)
        {
            return count;
        }
    }
    /*
    * 函数功能：判断数据库中的表是否存在主键为strIDValue的记录
    * 参数：表格名称strTableName,主键字段名称strIDName,主键值strIDValue
    * 返回值：不存在记录false,存在true
    */
    public bool IsExistID(string strTableName, string strIDName, string strIDValue)
    {//判断数据库中的表是否存在主键为strID的记录
        MySQLBase sqlBase = new MySQLBase();
        try
        {
            //string strSQL = "select* from @varTableName where @varIDName=@varIDValue";
            string strSQL = "select* from " + strTableName + " where " + strIDName + "='" + strIDValue+"'";
            sqlBase.Connect();
            //执行SQL语句
            SqlDataReader reader = sqlBase.GetDataReader(strSQL, null);//
            if (reader != null)
            {
                if (reader.HasRows)
                    return true;//存在主键为strIDValue的记录
                else
                    return false;
            }
            else
            {
                return false;
            }
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


