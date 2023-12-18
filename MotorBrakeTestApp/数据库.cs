using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Xml.Serialization;

namespace MotorBrakeTestApp
{
    public partial class 数据库 : Form
    {
        public 数据库()
        {
            InitializeComponent();
        }

        private void 主界面_Load(object sender, EventArgs e)
        {
            ////this.Width = 1920;
            ////this.Height = 1040;
            ////this.Location = new Point(0,0);
            //try
            //{

            //    txProductID.Text = SaveDataClassIndex.ProductId;

            //    if (GlobalData.mysql.State != ConnectionState.Open)
            //    {
            //        GlobalData.mysql.Open();
            //    }
            //    if (GlobalData.mysql.State == ConnectionState.Open)
            //    {
            //        string sql = "select  *from brake_routine_test_detail  where 工单号_brake_routine_test_id ='" + txProductID.Text.Trim() + " '";
            //        DataSet Ds = new DataSet();
            //        SqlDataAdapter Da = new SqlDataAdapter(sql, GlobalData.mysql);
            //        Da.Fill(Ds, "brake_routine_test_detail");
            //        dataGridView1.Rows.Clear();
            //        dataGridView1.DataSource = Ds.Tables["brake_routine_test_detail"];
            //        Da.Dispose();
            //        Ds.Dispose();
            //    }
            //    else
            //    {
            //        MessageBox.Show("测试3");
            //        MessageBox.Show("数据库打开失败");
            //    }
            //}
            //catch (Exception E)
            //{
            //    MessageBox.Show(E.Message);
            //}
        }
        private void 查询_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)dataGridView1.DataSource;
                dt.Rows.Clear();
                dataGridView1.DataSource = dt;
                string sql = "select  *from brake_routine_test_detail  where 工单号_brake_routine_test_id ='" + txProductID.Text.Trim() + " '"; ;
                if (radioButton5.Checked == true)
                {
                    sql = "select  *from brake_routine_test_detail  where 型号_valid ='" + txProductMode.Text.Trim() + " ' and  时间_test_time>='" + dateTimePicker1.Value.ToString("yyyy-MM-dd").Trim() + " 00:00:00" + "' and 时间_test_time<='" + dateTimePicker2.Value.ToString("yyyy-MM-dd").Trim() + " 23:59:59" + "'"; 
                }
                else if(radioButton6.Checked == true)
                {
                    sql = "select  *from brake_routine_test_detail  where 工单号_brake_routine_test_id ='" + txProductID.Text.Trim() + " '";
                }
                //string sql = "select  *from brake_routine_test_detail  where test_time>='" + dateTimePicker1.Value.ToString("yyyy-MM-dd").Trim()+ " 00:00:00" + "' and test_time<='"+ dateTimePicker2.Value.ToString("yyyy-MM-dd").Trim() + " 23:59:59" + "'"; ;
               
                //if (radioButton1.Checked == true) sql = sql + "and 产品型号='BE1'";
                //if (radioButton2.Checked == true) sql = sql + "and 产品型号='BE2'";
                //if (radioButton3.Checked == true) sql = sql + "and 产品型号='BE05'";
                SqlDataAdapter Da = new SqlDataAdapter(sql, GlobalData.mysql);
                DataSet Ds = new DataSet();
                Da.Fill(Ds, "brake_routine_test_detail");
                dataGridView1.DataSource = Ds.Tables["brake_routine_test_detail"];
                Da.Dispose();
                Ds.Dispose();
            }
            catch (Exception E)
            {
                //Logger.Instance.WriteException(E);
            }
        }
        private void 刷新_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)dataGridView1.DataSource;
                dt.Rows.Clear();
                dataGridView1.DataSource = dt;
                string sql = "select  *from brake_routine_test_detail";
                SqlDataAdapter Da = new SqlDataAdapter(sql, GlobalData.mysql);
                DataSet Ds = new DataSet();
                Da.Fill(Ds, "brake_routine_test_detail");
                dataGridView1.DataSource = Ds.Tables["brake_routine_test_detail"];
                Da.Dispose();
                Ds.Dispose();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }
        private void ToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count == 0) return;
                string fileName = "";
                string saveFileName = "";
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.DefaultExt = "xlsx";
                saveDialog.Filter = "Excel(97-2003)文件|*.xls|Excel文件|*.xlsx";
                saveDialog.FileName = fileName;
                saveDialog.ShowDialog();
                saveFileName = saveDialog.FileName;
                if (saveFileName.IndexOf(":") < 0) return;
                Excel.Application excel = new Excel.Application();
                if (excel  == null)
                {
                    MessageBox.Show("无法创建Excel对象，您的电脑可能未安装Excel");
                    return;
                }
                Excel.Workbooks workbooks =excel.Workbooks;
                excel.Visible =false ;
                Excel.Workbook workbook=workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet );
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];
                Excel.Range range = null;
                for (int i=0;i<dataGridView1 .Columns.Count;i++)
                {
                    if(this.dataGridView1 .Columns [i].Visible ==true )
                    {
                        range = (Excel.Range)worksheet.Cells[1, i + 1];
                        excel.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText;
                        range.EntireColumn.AutoFit();//自动调整列宽
                        range.EntireRow.AutoFit();//自动调整行高
                    }
                }
                for(int i=0;i<dataGridView1 .Rows .Count -1;i++)
                {
                    System.Windows.Forms.Application.DoEvents();
                    for(int j=0;j<dataGridView1 .Columns.Count;j++)
                    {
                        if(this.dataGridView1 .Columns [j].Visible ==true )
                        {
                            if(dataGridView1 [j,i].ValueType ==typeof (string ))
                            {
                                excel.Cells[i + 2, j + 1] = "'" + dataGridView1[j, i].Value.ToString();
                            }
                            else
                            {
                                excel.Cells[i + 2, j + 1] = dataGridView1[j, i].Value.ToString();
                            }
                        }
                    }
                }
                workbook.Saved = true;
                workbook.SaveCopyAs(saveFileName);
                workbook.Close(false);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet );
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbooks);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                MessageBox.Show("数据导出成功o(￣▽￣)d");
            }
            catch (Exception E)
            {
                //Logger.Instance.WriteException(E);
            }
        }


    }
}
