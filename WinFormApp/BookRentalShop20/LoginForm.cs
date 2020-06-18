﻿using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework;
using System.Data.SqlClient;
using System.Diagnostics;

namespace BookRentalShop20
{
    public partial class LoginForm : MetroForm
    {
        string strConnString = "Data Source=192.168.0.10;Initial Catalog=BookRentalshopDB;Persist Security Info=True;User ID=sa;Password=p@ssw0rd!";

        public LoginForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 취소버튼 클릭이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            //Application.Exit();// 정확하게 메모리 해제가 안되는 경우가 있음
            Environment.Exit(0);// 0(FLASE): 에러가 없이 정상종료 // 1(TRUE) : 에러가 있어 종료가 안됨
        }

        /// <summary>
        /// 로그인 처리버튼 클릭이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOK_Click(object sender, EventArgs e)
        {
            LoginProcess();
        }

        private void TxtUserID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13) // 엔터
            {
                TxtPassWord.Focus();
            }
        }

        private void TxtPassWord_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) // 엔터
            {
                LoginProcess();
            }
        }

        private void LoginProcess()
        {
            //if(TxtUserID.Text == null || TxtUserID.Text == "" || TxtPassWord.Text == null || TxtPassWord.Text == "")
            if(string.IsNullOrEmpty(TxtUserID.Text) || string.IsNullOrEmpty(TxtPassWord.Text) )
            {
                MetroMessageBox.Show(this, "아이디/패스워드를 입력하세요!", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string strUserId = string.Empty;

            using (SqlConnection conn = new SqlConnection(strConnString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT userID FROM userTbl" 
                                + " WHERE userID = @userID"
                                  + " AND password = @password";
                
                // DB 해킹(SQL Injection)을 방지하기 위해 변수를 사용하여 접속
                SqlParameter parmUserID = new SqlParameter("@userID", SqlDbType.VarChar, 12);
                parmUserID.Value = TxtUserID.Text;
                cmd.Parameters.Add(parmUserID);

                SqlParameter parmPassword = new SqlParameter("@password", SqlDbType.VarChar, 20);
                parmPassword.Value = TxtPassWord.Text;
                cmd.Parameters.Add(parmPassword);

                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                strUserId = reader["userID"].ToString();
                MetroMessageBox.Show(this, "접속성공", "로그인");
                Debug.WriteLine("On the Debug");
            }
        }
    }
}