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
public class MySQL
{
    private string m_strMySQLConnectionString = "Data Source=WWW-D7B960610C7\\SQLEXPRESS;Initial Catalog=MyASPDB;Integrated Security=sspi;";
    /* WWW-D7B960610C7\\SQLEXPRESS;   
     当设置Integrated Security为 True 的时候，连接语句前面的 UserID, PW 是不起作用的，即采用windows身份验证模式。
    只有设置为 False 或省略该项的时候，才按照 UserID, PW 来连接。
    Integrated Security 还可以设置为：sspi ，相当于 True，建议用这个代替 True。
     */
     
    private SqlConnection m_sqlConnection = null;//
    private SqlCommand m_sqlCommand = null;
    private bool m_bHasConnected=false;//是否连接上数据库
    public bool HasConnected() { return m_bHasConnected; }
    //=======================================
    public int Connect()//打开数据库连接
    {
        try
        {
            m_sqlConnection = new SqlConnection(m_strMySQLConnectionString);
            m_sqlConnection.Open();
            m_bHasConnected = true;
            return 1;
        }
        catch (System.Exception ex)
        {
            m_bHasConnected = false;
            return -1;
        }
    }
    public int DisConnect()// 关闭数据库连接
    {
        try
        {
            if (m_sqlConnection.State == ConnectionState.Open)
            {
                m_sqlConnection.Close();
                m_sqlConnection.Dispose();
                m_bHasConnected = false;
            }
            GC.Collect();
            return 1;
        }
        catch (System.Exception ex)
        {
            return -1;
        } 
    }
 
    //========================================================
    //运行Sql语句 
    public int RunSQL(string strSQL)//Insert,Update,Delete
    {
        try
        {
            if (m_sqlConnection.State != ConnectionState.Open) 
                Connect();//如果没连上数据库，重新连接

            m_sqlCommand = null;
            m_sqlCommand = new SqlCommand(strSQL, m_sqlConnection);

            int result_count = 0;
            result_count = m_sqlCommand.ExecuteNonQuery();//返回受影响的行数
            return result_count;
        }
        catch (Exception Ex)
        {
            return -1;
        }
    }

    // 运行Sql语句返回SqlDataReader对象
    public SqlDataReader GetRecordSet(string strSQL)//返回结果集
    {
        try
        {
            if (m_sqlConnection.State != ConnectionState.Open)
                Connect();//如果没连上数据库，重新连接

            m_sqlCommand = null;
            m_sqlCommand = new SqlCommand(strSQL, m_sqlConnection);

            //SqlDataReader不能直接new出来。它的生只有一种方法--SqlDataReader dr = cmd.ExecuteReader();
            SqlDataReader dataReader = m_sqlCommand.ExecuteReader(CommandBehavior.Default);
            return dataReader;
        }
        catch (System.Exception ex)
        {
            return null;
        }
    }

}
