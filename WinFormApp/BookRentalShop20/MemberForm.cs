﻿using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookRentalShop20
{
    public partial class MemberForm : MetroForm
    {
        string mode = "";

        public MemberForm()
        {
            InitializeComponent();
        }

        private void UpdateData()
        {
            using (SqlConnection conn = new SqlConnection(Commons.CONNSTRING))
            {
                conn.Open(); // DB 열기
                string strQuery = "SELECT Idx, Names, Levels, Addr, Mobile, Email "
                                 + " FROM membertbl";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(strQuery, conn);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds, "membertbl");

                GrdMemberTbl.DataSource = ds;
                GrdMemberTbl.DataMember = "membertbl";
            }
        }

        private void GrdDivTbl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex > -1)
            {
                DataGridViewRow data = GrdMemberTbl.Rows[e.RowIndex];
                TxtIdx.Text = data.Cells[0].Value.ToString();
                TxtNames.Text = data.Cells[1].Value.ToString();
                CboLevels.SelectedIndex = CboLevels.FindString(data.Cells[2].Value.ToString());
                TxtAddr.Text = data.Cells[3].Value.ToString();
                TxtMobile.Text = data.Cells[4].Value.ToString();
                TxtEmail.Text = data.Cells[5].Value.ToString();

                mode = "UPDATE"; // 수정은 UPDATE
            }
        }

        /// <summary>
        /// 새로운 데이터 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNew_Click(object sender, EventArgs e)
        {
            ClearTextControls();

            mode = "INSERT"; // 신규는 INSERT
        }

        /// <summary>
        /// 데이터 수정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(TxtAddr.Text) || string.IsNullOrEmpty(TxtMobile.Text) || string.IsNullOrEmpty(TxtNames.Text)
                || string.IsNullOrEmpty(TxtEmail.Text))
            {
                MetroMessageBox.Show(this, "빈값은 저장할 수 없습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveProcess();
            UpdateData();
            ClearTextControls();
        }

        /// <summary>
        /// 입력창 초기화
        /// </summary>
        private void ClearTextControls()
        {
            TxtIdx.Text = "";
            TxtNames.Text = "";
            TxtAddr.Text = "";
            TxtEmail.Text = "";
            TxtMobile.Text = "";
            CboLevels.SelectedIndex = -1;// 아무것도 선택안함
            TxtNames.Focus();
        }

        /// <summary>
        /// DB 저장 프로세스
        /// </summary>
        private void SaveProcess()
        {
            if(string.IsNullOrEmpty(mode))
            {
                MetroMessageBox.Show(this, "신규버튼을 누르고 데이터를 저장하십시오.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(Commons.CONNSTRING))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                string strQuery = "";

                if (mode == "UPDATE")
                {
                    strQuery = "UPDATE membertbl "
                               + " SET Names = @Names, Levels = @Levels, Addr = @Addr, Mobile = @Mobile, Email = @Email "
                             + " WHERE Idx = @Idx";
                }
                else if (mode == "INSERT")
                {
                    strQuery = "INSERT INTO membertbl(Names, Levels, Addr, Mobile, Email) "
                            + " VALUES(@Names, @Levels, @Addr, @Mobile, @Email)";
                }
                cmd.CommandText = strQuery;

                SqlParameter parmNames = new SqlParameter("@Names", SqlDbType.NVarChar, 45);
                parmNames.Value = TxtNames.Text;
                cmd.Parameters.Add(parmNames);

                SqlParameter parmLevels = new SqlParameter("@Levels", SqlDbType.Char, 1);
                parmLevels.Value = CboLevels.SelectedItem;
                cmd.Parameters.Add(parmLevels);

                SqlParameter parmAddr = new SqlParameter("@Addr", SqlDbType.VarChar, 100);
                parmAddr.Value = TxtAddr.Text;
                cmd.Parameters.Add(parmAddr);

                SqlParameter parmMobile = new SqlParameter("@Mobile", SqlDbType.VarChar, 13);
                parmMobile.Value = TxtMobile.Text;
                cmd.Parameters.Add(parmMobile);

                SqlParameter parmEmail = new SqlParameter("@Email", SqlDbType.VarChar, 50);
                parmEmail.Value = TxtEmail.Text;
                cmd.Parameters.Add(parmEmail);

                if(mode == "UPDATE")
                {
                    SqlParameter parmIdx = new SqlParameter("@Idx", SqlDbType.Int);
                    parmIdx.Value = TxtIdx.Text;
                    cmd.Parameters.Add(parmIdx);
                }    

                cmd.ExecuteNonQuery();
            }
        }

        private void TxtNames_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                BtnSave_Click(sender,new EventArgs());
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtIdx.Text) || string.IsNullOrEmpty(TxtNames.Text))
            {
                MetroMessageBox.Show(this, "빈값은 삭제할 수 없습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DeleteProcess();
            UpdateData();
            ClearTextControls();
        }

        private void DeleteProcess()
        {
            using(SqlConnection conn = new SqlConnection(Commons.CONNSTRING))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM divtbl "
                                + " WHERE Division = @Division";
                SqlParameter parmDivision = new SqlParameter("@Division", SqlDbType.Char,4);
                parmDivision.Value = TxtIdx.Text;
                cmd.Parameters.Add(parmDivision);

                cmd.ExecuteNonQuery();
            }
        }

        private void MemberForm_Load(object sender, EventArgs e)
        {
            UpdateData(); // 데이터그리드 DB 데이터 로딩하기
        }
    }
}