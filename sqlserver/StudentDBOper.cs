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
public class StudentDBOper
{
	public StudentDBOper()
	{
		
	}
    //=======================================
    //根据Sno获取对应的Image
    public bool GetImage(string strSno, out byte[] image)
    {
        image = null;
        if (string.IsNullOrEmpty(strSno))
            return false;

        MySQLBase sqlBase = new MySQLBase();
        try
        {
            //strSno不为空
            sqlBase.Connect();
            string strSQL = "select Simage from Student where Sno='"+strSno+"'";
            object objValue = sqlBase.GetValue(strSQL, null);//获取Image
            if (Convert.IsDBNull(objValue) || objValue == null)
                return false;
            image = (byte[])objValue;
            return true;
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
    //=======================================
    //为Sno插入Image
    public bool InsertImage(string strSno, byte[] byteImage)
    {
        if (string.IsNullOrEmpty(strSno) || byteImage==null)
            return false;

        MySQLBase sqlBase = new MySQLBase();
        try
        {
            string strSQL = "update Student set Simage=@varImage where Sno=@varSno";
            sqlBase.Connect();
            //设置相应的参数
            int paramCount = 2;
            SqlParameter[] sqlParams = new SqlParameter[paramCount];
            //参数1
            int index = 0;
            sqlParams[index] = new SqlParameter();
            sqlParams[index].ParameterName = "@varImage";//参数名称
            sqlParams[index].SqlDbType = SqlDbType.Image;//参数类型(二进制image)
            sqlParams[index].Value = byteImage;//fileUpload.FileBytes;//参数值
            sqlParams[index].Direction = ParameterDirection.Input;//输入参数
            //参数2
            index++;
            sqlParams[index] = new SqlParameter();
            sqlParams[index].ParameterName = "@varSno";//参数名称
            sqlParams[index].SqlDbType = SqlDbType.Char;//参数类型(char(6))
            sqlParams[index].Size = 6;//参数的长度
            sqlParams[index].Value = strSno;//参数值
            sqlParams[index].Direction = ParameterDirection.Input;//输入参数

            //执行SQL语句
            int ret = -1;
            ret=sqlBase.RunSQL(strSQL, sqlParams);//
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
    //=======================================
    //Insert一个完整的学生的信息(除image可以为空外，其它都不为空)
    public bool InsertStudentInfo(string strSno,string strSname,bool bSsex,DateTime dtSbirth,string strSdept,byte[] image)
    {
        if (string.IsNullOrEmpty(strSno) || string.IsNullOrEmpty(strSname) || string.IsNullOrEmpty(strSdept) || bSsex == null || dtSbirth == null)
            return false;

        MySQLBase sqlBase = new MySQLBase();
        try
        {
            //insert into Student values(@varSno,@varSname,@varSsex,@varSbirth,@varSdept,@varSimage)
            string strSQL;
            int paramCount; 
            if (image==null)
           {
               paramCount = 5;
               strSQL = "insert into Student values(@varSno,@varSname,@varSsex,@varSbirth,@varSdept,null)";
           }
            else
            {
                paramCount = 6;
                strSQL = "insert into Student values(@varSno,@varSname,@varSsex,@varSbirth,@varSdept,@varSimage)";
            }
            sqlBase.Connect();
            //设置相应的参数
            SqlParameter[] sqlParams = new SqlParameter[paramCount];
            //参数1  varSno char(6)
            int index = 0;//
            sqlParams[index] = new SqlParameter();
            sqlParams[index].ParameterName = "@varSno";//参数名称
            sqlParams[index].SqlDbType = SqlDbType.Char;//参数类型
            sqlParams[index].Size = 6;//参数的长度
            sqlParams[index].Value = strSno;//参数值
            sqlParams[index].Direction = ParameterDirection.Input;//输入参数
            //参数2  varSname nvarchar
            index++;
            sqlParams[index] = new SqlParameter();
            sqlParams[index].ParameterName = "@varSname";//参数名称
            sqlParams[index].SqlDbType = SqlDbType.NVarChar;//参数类型
            sqlParams[index].Size = 20;//参数的长度
            sqlParams[index].Value = strSname;//参数值
            sqlParams[index].Direction = ParameterDirection.Input;//输入参数
            //参数3  varSsex bit
            index++;
            sqlParams[index] = new SqlParameter();
            sqlParams[index].ParameterName = "@varSsex";//参数名称
            sqlParams[index].SqlDbType = SqlDbType.Bit;//参数类型
            sqlParams[index].Value = bSsex;//参数值
            sqlParams[index].Size = 1;//参数的长度
            sqlParams[index].Direction = ParameterDirection.Input;//输入参数
            //参数4  varSbirth  datetime
            index++;
            sqlParams[index] = new SqlParameter();
            sqlParams[index].ParameterName = "@varSbirth";//参数名称
            sqlParams[index].SqlDbType = SqlDbType.DateTime;//参数类型
            sqlParams[index].Value = dtSbirth;//参数值
            sqlParams[index].Direction = ParameterDirection.Input;//输入参数
            //参数5  varSdept  nvarchar
            index++;
            sqlParams[index] = new SqlParameter();
            sqlParams[index].ParameterName = "@varSdept";//参数名称
            sqlParams[index].SqlDbType = SqlDbType.NVarChar;//参数类型
            sqlParams[index].Size = 20;//参数的长度
            sqlParams[index].Value = strSdept;//参数值
            sqlParams[index].Direction = ParameterDirection.Input;//输入参数

            if (image!=null)
            {
                //参数6   varSimage 
                index++;
                sqlParams[index] = new SqlParameter();
                sqlParams[index].ParameterName = "@varSimage";//参数名称
                sqlParams[index].SqlDbType = SqlDbType.Image;//参数类型(二进制image)
                sqlParams[index].Value = image;//参数值
                sqlParams[index].Direction = ParameterDirection.Input;//输入参数
            }

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
    //不管Grade是否null,都可以Update
    public bool UpdateGrade(string strSno,string strCno,float fGrade)
    {
        if (string.IsNullOrEmpty(strSno) || string.IsNullOrEmpty(strCno))
            return false;

        MySQLBase sqlBase = new MySQLBase();
        try
        {
            string strSQL = "update  SC set Grade=@varGrade where Sno=@varSno and Cno=@varCno";
            sqlBase.Connect();
            //设置相应的参数
            int paramCount = 3;
            SqlParameter[] sqlParams = new SqlParameter[paramCount];
            //参数1
            int index = 0;
            sqlParams[index] = new SqlParameter();
            sqlParams[index].ParameterName = "@varGrade";//参数名称
            sqlParams[index].SqlDbType = SqlDbType.Float;//参数类型
            sqlParams[index].Value = fGrade;//参数值
            sqlParams[index].Direction = ParameterDirection.Input;//输入参数
            //参数2
            index ++;
            sqlParams[index] = new SqlParameter();
            sqlParams[index].ParameterName = "@varSno";//参数名称
            sqlParams[index].SqlDbType = SqlDbType.Char;//参数类型(char(6))
            sqlParams[index].Size = 6;//参数的长度
            sqlParams[index].Value = strSno;//参数值
            sqlParams[index].Direction = ParameterDirection.Input;//输入参数
            //参数3
            index++;
            sqlParams[index] = new SqlParameter();
            sqlParams[index].ParameterName = "@varCno";//参数名称
            sqlParams[index].SqlDbType = SqlDbType.Char;//参数类型(char(3))
            sqlParams[index].Size = 3;//参数的长度
            sqlParams[index].Value = strCno;//参数值
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

    public object GetSname(string strSno)
    {
        if (string.IsNullOrEmpty(strSno))
            return null;

        MySQLBase sqlBase = new MySQLBase();
        try
        {
            string strSQL = "select Sname from Student where Sno='" + strSno + "'";
            sqlBase.Connect();
            object objValue=sqlBase.GetValue(strSQL, null);
            return objValue;
        }
        catch (System.Exception ex)
        {
            return null;
        }
        finally
        {
            if (sqlBase.HasConnected())
                sqlBase.DisConnect();
        }
    }
    public object GetCcredit(string strCno)
    {
        if (string.IsNullOrEmpty(strCno))
            return null;

        MySQLBase sqlBase = new MySQLBase();
        try
        {
            string strSQL = "select Ccredit from Course where Cno='" + strCno + "'";
            sqlBase.Connect();
            object objValue = sqlBase.GetValue(strSQL, null);
            return objValue;
        }
        catch (System.Exception ex)
        {
            return null;
        }
        finally
        {
            if (sqlBase.HasConnected())
                sqlBase.DisConnect();
        }
    }

    public bool InsertSCRecord(string strSno, string strCno, object objGrade)
    {//objGrade可能为null，所以不能使用float类型
        if (string.IsNullOrEmpty(strSno)||string.IsNullOrEmpty(strCno))
            return false;

        MySQLBase sqlBase = new MySQLBase();
        try
        {
            string strSQL;
            int paramCount;
            if (objGrade == null)
            {
                paramCount = 2;
                strSQL = "insert into SC values(@varSno,@varCno,null)";
            }
            else
            {
                paramCount = 3;
                strSQL = "insert into SC values(@varSno,@varCno,@varGrade)";
            }

            sqlBase.Connect();
            //设置相应的参数
             SqlParameter[] sqlParams = new SqlParameter[paramCount];
            //参数1
            int index = 0;
            sqlParams[index] = new SqlParameter();
            sqlParams[index].ParameterName = "@varSno";//参数名称
            sqlParams[index].SqlDbType = SqlDbType.Char;//参数类型(char(6))
            sqlParams[index].Size = 6;//参数的长度
            sqlParams[index].Value = strSno;//参数值
            sqlParams[index].Direction = ParameterDirection.Input;//输入参数
            //参数2
            index++;
            sqlParams[index] = new SqlParameter();
            sqlParams[index].ParameterName = "@varCno";//参数名称
            sqlParams[index].SqlDbType = SqlDbType.Char;//参数类型(char(3))
            sqlParams[index].Size = 3;//参数的长度
            sqlParams[index].Value = strCno;//参数值
            sqlParams[index].Direction = ParameterDirection.Input;//输入参数
            if (objGrade!=null)
            {
                //参数3
                index++;
                sqlParams[index] = new SqlParameter();
                sqlParams[index].ParameterName = "@varGrade";//参数名称
                sqlParams[index].SqlDbType = SqlDbType.Float;//参数类型
                sqlParams[index].Value = float.Parse(objGrade.ToString());//参数值
                sqlParams[index].Direction = ParameterDirection.Input;//输入参数
            }

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
