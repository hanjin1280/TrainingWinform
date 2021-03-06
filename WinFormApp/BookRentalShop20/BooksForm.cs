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
    public partial class BooksForm : MetroForm
    {
        string mode = "";

        public BooksForm()
        {
            InitializeComponent();
        }

        private void UpdateData()
        {
            using (SqlConnection conn = new SqlConnection(Commons.CONNSTRING))
            {
                conn.Open(); // DB 열기
                //string strQuery = "SELECT Idx, Author, Division, Names, ReleaseDate, ISBN, Price "
                //                 + " FROM bookstbl ";
                string strQuery = "SELECT b.Idx, b.Author, b.Division, d.Names AS 'DivNames', b.Names, b.ReleaseDate, b.ISBN "
                                    + " , REPLACE(CONVERT(VARCHAR, CONVERT(MONEY, b.Price), 1), '.00', '') AS 'price' "
                                 + " FROM bookstbl AS b "
                                + " INNER JOIN divtbl AS d ON d.Division = b.Division";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(strQuery, conn);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds, "bookstbl");

                GrdBooksTbl.DataSource = ds;
                GrdBooksTbl.DataMember = "bookstbl";
            }
            DataGridViewColumn column = GrdBooksTbl.Columns[2];
            column.Visible = false;// Division 비활성화를 통한 화면에 안보이게 하기.
        }

        private void GrdDivTbl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex > -1)
            {
                DataGridViewRow data = GrdBooksTbl.Rows[e.RowIndex];
                TxtIdx.Text = data.Cells[0].Value.ToString();
                TxtIdx.ReadOnly = true;
                TxtAuthor.Text = data.Cells[1].Value.ToString();
                
                //아래의 두 소스는 같은 역할을 한다.
                //CboDivision.SelectedIndex = CboDivision.FindString(data.Cells[3].Value.ToString());
                CboDivision.SelectedValue = data.Cells[2].Value;
                
                TxtNames.Text = data.Cells[4].Value.ToString();

                DtpReleaseDate.CustomFormat = "yyyy-MM-dd";
                DtpReleaseDate.Format = DateTimePickerFormat.Custom;
                DtpReleaseDate.Value = DateTime.Parse(data.Cells[5].Value.ToString());
                TxtISBN.Text = data.Cells[6].Value.ToString();
                TxtPrice.Text = data.Cells[7].Value.ToString();

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
            if(string.IsNullOrEmpty(TxtNames.Text) || string.IsNullOrEmpty(TxtAuthor.Text)
                || string.IsNullOrEmpty(TxtISBN.Text) || string.IsNullOrEmpty(TxtNames.Text) ||string.IsNullOrEmpty(TxtPrice.Text))
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
            TxtAuthor.Text = "";
            TxtNames.Text = "";
            TxtISBN.Text = "";
            TxtPrice.Text = "";
            CboDivision.SelectedIndex = -1;// 아무것도 선택안함
            DtpReleaseDate.CustomFormat = " ";//  공백이 한칸 들어가야함
            DtpReleaseDate.Format = DateTimePickerFormat.Custom;
            TxtAuthor.Focus();
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
                    strQuery = "UPDATE bookstbl SET Author = @Author, Division = @Division, Names = @Names, ReleaseDate = @ReleaseDate, "
                                                + " ISBN = @ISBN, Price = @Price "
                             + " WHERE Idx = @Idx";
                }
                else if (mode == "INSERT")
                {
                    strQuery = "INSERT INTO bookstbl(Author, Division, Names, ReleaseDate, ISBN, Price) "
                            + " VALUES(@Author, @Division, @Names, @ReleaseDate, @ISBN, @Price)";
                }
                cmd.CommandText = strQuery;

                SqlParameter parmAuthor = new SqlParameter("@Author", SqlDbType.NVarChar, 45);
                parmAuthor.Value = TxtAuthor.Text;
                cmd.Parameters.Add(parmAuthor);

                SqlParameter parmDivision = new SqlParameter("@Division", SqlDbType.Char, 4);
                parmDivision.Value = CboDivision.SelectedValue;
                cmd.Parameters.Add(parmDivision);

                SqlParameter parmNames = new SqlParameter("@Names", SqlDbType.VarChar, 100);
                parmNames.Value = TxtNames.Text;
                cmd.Parameters.Add(parmNames);

                SqlParameter parmReleaseDate = new SqlParameter("@ReleaseDate", SqlDbType.Date);
                parmReleaseDate.Value = DtpReleaseDate.Value;
                cmd.Parameters.Add(parmReleaseDate);

                SqlParameter parmISBN = new SqlParameter("@ISBN", SqlDbType.VarChar, 200);
                parmISBN.Value = TxtISBN.Text;
                cmd.Parameters.Add(parmISBN);

                SqlParameter parmPrice = new SqlParameter("@Price", SqlDbType.Decimal, 10);
                parmPrice.Value = TxtPrice.Text;
                cmd.Parameters.Add(parmPrice);

                if (mode == "UPDATE")
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
            if (string.IsNullOrEmpty(TxtIdx.Text) || string.IsNullOrEmpty(TxtAuthor.Text))
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
            DtpReleaseDate.CustomFormat = "yyyy-MM-dd";//
            DtpReleaseDate.Format = DateTimePickerFormat.Custom;
            UpdateData(); // 데이터그리드 DB 데이터 로딩하기
            UpdateCboDivision();
        }

        private void UpdateCboDivision()
        {
            using(SqlConnection conn = new SqlConnection(Commons.CONNSTRING))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT Division, Names FROM divtbl ";
                SqlDataReader reader = cmd.ExecuteReader();

                Dictionary<string, string> temps = new Dictionary<string, string>();
                while(reader.Read())
                {
                    temps.Add(reader[0].ToString(), reader[1].ToString());
                }

                CboDivision.DataSource = new BindingSource(temps, null);
                CboDivision.DisplayMember = "Value";
                CboDivision.ValueMember = "Key";
                CboDivision.SelectedIndex = -1;

            }
        }

         private void DtpReleaseDate_ValueChanged(object sender, EventArgs e)
        {
            DtpReleaseDate.CustomFormat = "yyyy-MM-dd";
            DtpReleaseDate.Format = DateTimePickerFormat.Custom;
        }
    }
}
