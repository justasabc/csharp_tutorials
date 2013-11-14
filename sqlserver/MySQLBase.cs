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
public class MySQLBase
{
    private string m_strMySQLConnectionString =
  "Data Source=ke\\SQLEXPRESS;Initial Catalog=BBS;Integrated Security=SSPI;";
    private string m_strMySQLConnectionString2 =
    "Data Source=ke\\SQLEXPRESS;Initial Catalog=BBS;Integrated Security=False;User ID=sa;Password=123;";
    /* 
    Integrated Security可取3个值
    (1)True   SSPI(相当于 True):采用windows身份验证模, 此时不需要User ID,Password
     (2)False 或省略该项，才按照 UserID,Password来连接
    */
    private SqlConnection m_sqlConnection = null;
    private SqlCommand m_sqlCommand = null;
    //=======================================
    public bool HasConnected()
    {
        return m_sqlConnection.State == ConnectionState.Open;//Closed
     }
    //=======================================
    public int Connect()
    {//打开数据库连接
        try
        {
            m_sqlConnection = new SqlConnection(m_strMySQLConnectionString);
            m_sqlConnection.Open();//打开数据源
            return 1;
        }
        catch (System.Exception ex)
        {
            return -1;
        }
    }
    public int DisConnect()
    {// 关闭数据库连接
        try
        {
            if (m_sqlConnection.State == ConnectionState.Open)
            {
                m_sqlConnection.Close();
                m_sqlConnection.Dispose();
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
    //执行SQL命令：普通文本+存储过程
    /*函数功能说明：执行标准格式的SQL语句(Insert,Update,Delete)
     * 参数：strFormatSQL标准格式SQL语句，sqlParams为传入的参数
     */
    public int RunSQL(string strFormatSQL, SqlParameter[] sqlParams)
    {
        try
        {
            m_sqlCommand = new SqlCommand();
            m_sqlCommand.Connection = m_sqlConnection;//数据库连接对象
            m_sqlCommand.CommandText = strFormatSQL;//SQL语句或者存储过程
            m_sqlCommand.CommandType = CommandType.Text;//Text文本   StoredProcedure存储过程
            //********************************************
            //添加参数
            if (sqlParams != null)
            {
                for (int i = 0; i < sqlParams.Length; i++)
                    m_sqlCommand.Parameters.Add(sqlParams[i]);
            }
            //********************************************
            int result_count = 0;
            result_count = m_sqlCommand.ExecuteNonQuery();//返回受影响的行数
            return result_count;
        }
        catch (Exception Ex)
        {
            return -1;
        }
    }
    /*函数功能说明：执行存储过程()
     * 参数：strExecSQL存储过程名称，sqlParams为传入的参数
     */
    public int RunExec(string strExecName, SqlParameter[] sqlParams)
    {
        try
        {
            m_sqlCommand = new SqlCommand();
            m_sqlCommand.Connection = m_sqlConnection;//数据库连接对象
            m_sqlCommand.CommandText = strExecName;//存储过程名称
            m_sqlCommand.CommandType = CommandType.StoredProcedure;//Text文本   StoredProcedure存储过程
            //********************************************
            //添加参数
            if (sqlParams != null)
            {
                for (int i = 0; i < sqlParams.Length; i++)
                    m_sqlCommand.Parameters.Add(sqlParams[i]);
            }
            //********************************************
            int result_count = 0;
            result_count = m_sqlCommand.ExecuteNonQuery();//返回受影响的行数
            return result_count;
        }
        catch (Exception Ex)
        {
            return -1;
        }
    }
    //========================================================
    /*函数功能说明：运行Sql语句返回SqlDataReader对象
     * 参数：strFormatSQL标准格式SQL语句，sqlParams为传入的参数
     */
    public SqlDataReader GetDataReader(string strFormatSQL, SqlParameter[] sqlParams)
    {//返回结果集
        try
        {
            m_sqlCommand = new SqlCommand(strFormatSQL, m_sqlConnection);
            //********************************************
            //添加参数
            if (sqlParams != null)
            {
                for (int i = 0; i < sqlParams.Length; i++)
                    m_sqlCommand.Parameters.Add(sqlParams[i]);
            }
            //********************************************
            //SqlDataReader不能直接new出来.它的生只有一种方法--SqlDataReader dr = cmd.ExecuteReader();
            SqlDataReader dataReader = m_sqlCommand.ExecuteReader(CommandBehavior.Default);
            return dataReader;
        }
        catch (System.Exception ex)
        {
            return null;
        }
    }
    //========================================================
    /*函数功能说明：使用ExecuteScalar进行查询操作
     * 参数：strTextSQL普通文本类型完整SQL语句 
     * 返回值:第一行的第一列(仅有一个值)
     */
    public object GetValue(string strFormatSQL, SqlParameter[] sqlParams)
    {//ExecuteScalar用于执行SELECT查询，得到的返回结果为一个值的情况
        //比如使用count函数求表中记录个数或者使用sum函数求和等。
        try
        {
            m_sqlCommand = new SqlCommand(strFormatSQL, m_sqlConnection);
            //********************************************
            //添加参数
            if (sqlParams != null)
            {
                for (int i = 0; i < sqlParams.Length; i++)
                    m_sqlCommand.Parameters.Add(sqlParams[i]);
            }
            //********************************************
            object obj = m_sqlCommand.ExecuteScalar();//
            return obj;
        }
        catch (System.Exception ex)
        {
            return null;
        }
    }
    //========================================================
    //========================================================
    // 运行Sql语句返回SqlDataAdapter对象
    public SqlDataAdapter GetDataAdapter(string strFormatSQL, SqlParameter[] sqlParams)
    {
        try
        {
            m_sqlCommand = new SqlCommand(strFormatSQL, m_sqlConnection);
            //********************************************
            //添加参数
            if (sqlParams != null)
            {
                for (int i = 0; i < sqlParams.Length; i++)
                    m_sqlCommand.Parameters.Add(sqlParams[i]);
            }
            //********************************************
            //SqlDataAdapter dataAdapter = new SqlDataAdapter(strSQL, m_sqlConnection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(m_sqlCommand);//关联起来
            //InsertCommand  UpdateCommand DeleteCommand SelectCommand = m_sqlCommand;
            return dataAdapter;
        }
        catch (System.Exception ex)
        {
            return null;
        }
    }
    public DataSet GetDataSet(string strFormatSQL, SqlParameter[] sqlParams, string strTableName)
    {
        SqlDataAdapter dataAdapter = this.GetDataAdapter(strFormatSQL, sqlParams);
        if (dataAdapter != null)
        {
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet, strTableName);//dataAdapter.Fill(dataSet)默认表明为"Table"         
            return dataSet;
        }
        else
        {
            return null;
        }
    }
    public DataSet GetDataSet2(string strFormatSQL, SqlParameter[] sqlParams, int nStartIndex, int nPageSize, string strTableName)
    { // 运行Sql语句,返回DataSet对象，将数据进行了分页
        SqlDataAdapter dataAdapter = this.GetDataAdapter(strFormatSQL, sqlParams);
        if (dataAdapter != null)
        {
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet, nStartIndex, nPageSize, strTableName);
            return dataSet;
        }
        else
        {
            return null;
        }
    }
    public DataTable GetDataTable(string strFormatSQL, SqlParameter[] sqlParams, string strTableName)
    {

        SqlDataAdapter dataAdapter = this.GetDataAdapter(strFormatSQL, sqlParams);
        if (dataAdapter != null)
        {
            DataTable dataTable = new DataTable(strTableName);
            dataAdapter.Fill(dataTable);//SqlDataAdapter填充已经创建的DataTable对象
            return dataTable;
        }
        else
        {
            return null;
        }
    }
    //========================================================
}


//******************************************************************
//使用代码

/*
1GetDataReader
MySQLBase sqlBase = new MySQLBase();
try
{
    string strSQL = "select * from Info where ID=" + ID ;
    sqlBase.Connect();
    SqlDataReader reader = sqlBase.GetDataReader(strSQL, null);
    if (reader!=null)
    {
        if (reader.HasRows)
        {         
            while (reader.Read())
            {// ID  TitleName ContentInfo PostTime ReplyID   UserName       
                this.Label_UserName.Text = reader["UserName"].ToString();
                this.Label_PostTime.Text = reader["PostTime"].ToString();
                this.TextBox_Title.Text = reader["TitleName"].ToString();
                this.TextBox_Content.Text = reader["ContentInfo"].ToString();
            }
            reader.Close();//读取完毕后必须关闭
        }
    }
}
catch (System.Exception ex)
{

}
finally
{
    if (sqlBase.HasConnected())
        sqlBase.DisConnect();
}
*/


/*2GetDataSet 
MySQLBase sqlBase = new MySQLBase();
try
{
    sqlBase.Connect();
    string strSQL = "select* from Info";
    DataSet dataSet = sqlBase.GetDataSet(strSQL, null, "kzlTable");
    DataTableReader reader = dataSet.CreateDataReader();//创建Reader
    if (reader != null)
    {
        if (reader.HasRows)
        {
            while (reader.Read())//读取表中数据
            {
                this.Label_UserName.Text = reader["UserName"].ToString();
                this.Label_PostTime.Text = reader["PostTime"].ToString();
                this.TextBox_Title.Text = reader["TitleName"].ToString();
                this.TextBox_Content.Text = reader["ContentInfo"].ToString();
            }
            reader.Close();//读取完毕后必须关闭
        }
    }
}
catch (System.Exception ex)
{
	
}
finally
{
    if (sqlBase.HasConnected())
        sqlBase.DisConnect();
}
*/

//构造SqlDataAdapter
//(1)SqlDataAdapter da＝new SqlDataAdapter();  da.SelectCommand=cmd;
//(2)SqlDataAdapter da＝new SqlDataAdapter(strSql,strConn);
// (3)SqlDataAdapter da＝new SqlDataAdapter(strSql,sqlconn);
//(4)SqlDataAdapter da＝new SqlDataAdapter(sqlcmd);

//Fill方法
//(1)dataAdapter.Fill(dataSet, strTableName); //dataAdapter.Fill(dataSet)默认为“Table”
//(2)DataTable dataTable=new DataTable(strTableName); dataAdapter.Fill(dataTable);
//(3)dataAdapter.Fill(dataSet, nStartIndex, nPageSize, strTableName);
//如果 DataAdapter 遇到多个结果集，它将在 DataSet 中创建多个表。
//将向这些表提供递增的默认名称 TableN，以表示 Table0 的“Table”为第一个表名.
//DataSet ds = new DataSet();
//adapter.Fill(ds,"kzlTable");//填充数据
////填充完了后就可以方便的访问数据
//ds.Tables["table1"].Rows[0][1] = "NewValue";//修改操作
//adapter.Update(ds, "table1");//把内存中的数据同步到数据库中(更新到数据库)
//原理就是修改了内存中数据集的数据，然后调用一下Update()方法就同步到数据库中去了
//SqlDataAdapter帮我们自动生成了Sql语句，并且在这里Update()方法是带了事务处理功能的。
//其他的删除，增加操作同理。在对内存中的数据集进行相关修改后，只需要调用一下Update()方法
//即可同步到数据库中去。但遗憾的是，目前这种方式只支持单表的操作，不支持任何与多表相关的操作
//（包括同一视图中来自不同表的列）。








