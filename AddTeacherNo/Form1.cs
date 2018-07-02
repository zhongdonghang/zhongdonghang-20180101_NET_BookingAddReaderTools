using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using DBLib;
using System.Collections;

namespace AddTeacherNo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        bool IsExistCardNo(String cardno)
        {
            bool isTrue = false;
            string sql = "select count(CardNo) from T_SM_Reader where CardNo = '" + cardno+"'";
            //int count = SqlHelper.ExcuteSql(sql);
            if (SqlHelper.ExcuteCountRecordSql(sql)>0)
            {
                isTrue = true;
            }
            else
            {
                isTrue = false;
            }
            return isTrue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //SqlConnection connection = new SqlConnection("Server=(local);Database=SeatBooking1_1;uid=sa;pwd=!QAZxsw2");
            try
            {
                if (teaNo.Text.Trim() == "" || teaName.Text.Trim() == "")
                {
                    MessageBox.Show("请填写学工号或者姓名");
                }
                else if (IsExistCardNo(teaNo.Text.Trim()))
                {
                    MessageBox.Show("学工号已经存在，请确认");
                }
                else
                {
                    try
                    {
                        string cardNo = teaNo.Text;
                        string name = teaName.Text;
                        string sex = cbSex.Text;
                        string dept = string.IsNullOrEmpty(teaType.Text) ? "未知" : teaType.Text;
                        string UsrPwd = "202CB962AC59075B964B07152D234B70";
                        int UsrEnabled = 1;
                        string Remark = "读者";
                        int roleid = 3;

                        string sql = "insert into T_SM_Reader(CardNo,CardID,ReaderName,Sex,ReaderTypeName,ReaderDeptName) values ('" + cardNo + "','" + cardNo + "','" + name + "','" + sex + "','" + dept + "','" + dept + "')";
                        string sql_usr = "insert into Users_ALL(loginid,UsrName,UsrPwd,UsrType,UsrEnabled,Remark) values('" + cardNo + "','" + name + "','" + UsrPwd + "','1','" + UsrEnabled + "','自动激活')";
                        string sql_sys = "insert into sysEmpRoles(roleid,loginid) values('3','" + cardNo + "')";
                        ArrayList sqlList = new ArrayList();
                        sqlList.Add(sql);
                        sqlList.Add(sql_usr);
                        sqlList.Add(sql_sys);
                        int count = SqlHelper.ExecuteSqlTran(sqlList);

                        //int count = SqlHelper.ExcuteSql(sql);

                        if (count > 0)
                        {
                            MessageBox.Show(this, "保存成功");
                        }
                        else
                        {
                            MessageBox.Show(this, "保存失败");
                        }


                    }
                    catch (Exception ex)
                    {
                        SeatManage.SeatManageComm.WriteLog.Write(ex.ToString());
                    }


                }
            }catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.ToString());
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
