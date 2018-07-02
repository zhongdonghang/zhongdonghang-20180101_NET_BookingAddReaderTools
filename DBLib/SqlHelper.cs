using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DBLib
{
    public class SqlHelper
    {
        public static int ExcuteCountRecordSql(string sql)
        {
            int count = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["sqlConnStr"]))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    Object o  = cmd.ExecuteScalar();
                    count = int.Parse(o.ToString());
                    cn.Close();
                }
            }
            catch
            {
                throw;
            }
            return count;
        }
             


        //执行sql 语句
        public static int  ExcuteSql(string sql)
        {
            int count = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["sqlConnStr"]))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    count = cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
            catch 
            {
                throw;
            }
            return count;
        }

        public static int ExecuteSqlTran(ArrayList SQLStringList)
        {
            int count = 0;
            using (SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["sqlConnStr"]))
            {
                SqlTransaction tx = null;
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    tx = cn.BeginTransaction();
                    cmd.Transaction = tx;
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString();
                        cmd.CommandText = strsql;
                        count = cmd.ExecuteNonQuery();
                    }
                    tx.Commit();
                }
                catch (Exception E)
                {
                    tx.Rollback();
                    throw new Exception(E.Message);
                }
            }
            return count;
        }

    }
}
