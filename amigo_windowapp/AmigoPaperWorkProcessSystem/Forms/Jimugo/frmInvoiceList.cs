using AmigoPaperWorkProcessSystem.Controllers;
using AmigoPaperWorkProcessSystem.Core;
using AmigoPaperWorkProcessSystem.Core.Model;
using AmigoPaperWorkProcessSystem.Forms.Jimugo;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmigoPaperWorkProcessSystem.Forms
{
    public partial class frmInvoiceList : Form
    {
        #region Declare
        private UIUtility uIUtility;
        private string programID = "";
        private string programName = "顧客マスタメンテナンス";

        private List<Validate> Modifiable = new List<Validate>{
            new Validate{ Name = "colEFFECTIVE_DATE", Type = Utility.DataType.DATE, Edit = true, Require = false, Max = 10, Min=8},
            new Validate{ Name = "colCONTRACT_DATE", Type = Utility.DataType.DATE, Edit = true, Require = false, Max = 10, Min=8},
            new Validate{ Name = "colSPECIAL_NOTES_1", Type = Utility.DataType.FULL_WIDTH, Edit = true, Require = false, Max = 50},
            new Validate{ Name = "colSPECIAL_NOTES_2", Type = Utility.DataType.FULL_WIDTH, Edit = true, Require = false, Max = 50},
            new Validate{ Name = "colSPECIAL_NOTES_3", Type = Utility.DataType.FULL_WIDTH, Edit = true, Require = false, Max = 50},
            new Validate{ Name = "colSPECIAL_NOTES_4", Type = Utility.DataType.FULL_WIDTH, Edit = true, Require = false, Max = 50},
            new Validate{ Name = "colNCS_CUSTOMER_CODE", Type = Utility.DataType.FULL_WIDTH, Edit=true, Require=false, Max=6 },
            new Validate{ Name = "colBILL_BANK_ACCOUNT_NAME_1", Type = Utility.DataType.FULL_WIDTH, Edit = true, Require = false, Max = 48},
            new Validate{ Name = "colBILL_BANK_ACCOUNT_NUMBER_1", Type = Utility.DataType.HALF_ALPHA_NUMERIC, Edit = true, Require = false, Max = 10},
            new Validate{ Name = "colBILL_BANK_ACCOUNT_NAME_2", Type = Utility.DataType.FULL_WIDTH, Edit = true, Require = false, Max = 48},
            new Validate{ Name = "colBILL_BANK_ACCOUNT_NUMBER_2", Type = Utility.DataType.HALF_ALPHA_NUMERIC, Edit = true, Require = false, Max = 10},
            new Validate{ Name = "colBILL_BANK_ACCOUNT_NAME_3", Type = Utility.DataType.FULL_WIDTH, Edit = true, Require = false, Max = 48},
            new Validate{ Name = "colBILL_BANK_ACCOUNT_NUMBER_3", Type = Utility.DataType.HALF_ALPHA_NUMERIC, Edit = true, Require = false, Max = 10},
            new Validate{ Name = "colBILL_BANK_ACCOUNT_NAME_4", Type = Utility.DataType.FULL_WIDTH, Edit = true, Require = false, Max = 48},
            new Validate{ Name = "colBILL_BANK_ACCOUNT_NUMBER_4", Type = Utility.DataType.HALF_ALPHA_NUMERIC, Edit = true, Require = false, Max = 10},
            new Validate{ Name = "colBILL_BILLING_INTERVAL", Type = Utility.DataType.TEXT, Edit = true, Require = false, Max = 255},
            new Validate{ Name = "colBILL_DEPOSIT_RULES", Type = Utility.DataType.TEXT, Edit = true, Require = false, Max = 255},
            new Validate{ Name = "colBILL_TRANSFER_FEE", Type = Utility.DataType.MINIMUM_TRANSFER_FEE, Edit = true, Require = false, Max = 255},
            new Validate{ Name = "colBILL_EXPENSES", Type = Utility.DataType.MINIMUM_EXPENSE, Edit = true, Require = false, Max = 255},
            new Validate{ Name = "colCOMPANY_NAME_CHANGED_DATE", Type = Utility.DataType.DATE, Edit = true, Require = false, Max = 10},
            new Validate{ Name = "colPREVIOUS_COMPANY_NAME", Type = Utility.DataType.FULL_WIDTH, Edit = true, Require = false, Max = 80},
            new Validate{ Name = "colOBOEGAKI_DATE", Type = Utility.DataType.DATE, Edit = true, Require = false, Max = 10}
        };

        private string[] dummyColumns = {
            "No",
            "COMPANY_NO_BOX",
            "COMPANY_NAME",
            "KEY_SOURCE_MONTHLY_USAGE_FEE",
            "SUPPLIER_INITIAL_EXPENSE",
            "SUPPLIER_MONTHLY_USAGE_FEE",
            "BROWSING_INITIAL_EXPENSE",
            "YEARLY_USAGE_FEE",
            "POSTAL_MAIL",
            "WEB",
            "Email",
            "CREDIT_CARD",
            "OTHER",
            "Key_source_Monthly_usage_fee_REQ_SEQ",
            "Supplier_Initial_expense_REQ_SEQ",
            "Supplier_Monthly_usage_fee_REQ_SEQ",
            "Production_information_browsing_Initial_expense_REQ_SEQ",
            "View_production_information_Annual_usage_fee_REQ_SEQ",
         };

        private string[] alignBottoms = {
               "colKEY_SOURCE_MONTHLY_USAGE_FEE",
               "coLSUPPLIER_INITIAL_EXPENSE",
               "colSUPPLIER_MONTHLY_USAGE_FEE",
               "colBROWSING_INITIAL_EXPENSE",
               "colYEARLY_USAGE_FEE",

               "coLPOSTAL_MAIL",
               "colWEB",
               "coLEmail",
               "colCREDIT_CARD",
               "colOTHER"
        };

        #endregion

        #region Properties

        private decimal _keySourceTotal = 0;
        public decimal keySourceTotal
        {
            get { return _keySourceTotal; }
            set { _keySourceTotal = value; }
        }

        private decimal _SupplierExpenseTotal =0;
        public decimal SupplierExpenseTotal
        {
            get { return _SupplierExpenseTotal; }
            set { _SupplierExpenseTotal = value; }
        }

        private decimal _SupplierMonthlyUsageFeeTotal = 0;
        public decimal SupplierMonthlyUsageFeeTotal
        {
            get { return _SupplierMonthlyUsageFeeTotal; }
            set { _SupplierMonthlyUsageFeeTotal = value; }
        }

        private decimal _SupplierBrowsingInitialExpenseTotal = 0;
        public decimal SupplierBrowsingInitialExpenseTotal
        {
            get { return _SupplierBrowsingInitialExpenseTotal; }
            set { _SupplierBrowsingInitialExpenseTotal = value; }
        }

        private decimal _YearlyUsageFeeTotal = 0;
        public decimal YearlyUsageFeeTotal
        {
            get { return _YearlyUsageFeeTotal; }
            set { _YearlyUsageFeeTotal = value; }
        }

        private decimal _InvoiceAmountTotal = 0;
        public decimal InvoiceAmountTotal
        {
            get { return _InvoiceAmountTotal; }
            set { _InvoiceAmountTotal = value; }
        }

        private decimal _PostalMailTotal = 0;
        public decimal PostalMailTotal
        {
            get { return _PostalMailTotal; }
            set { _PostalMailTotal = value; }
        }

        private decimal _EmailTotal = 0;
        public decimal EmailTotal
        {
            get { return _EmailTotal; }
            set { _EmailTotal = value; }
        }

        private decimal _WebTotal = 0;
        public decimal WebTotal
        {
            get { return _WebTotal; }
            set { _WebTotal = value; }
        }

        private decimal _CreditCardTotal = 0;
        public decimal CreditCardTotal
        {
            get { return _CreditCardTotal; }
            set { _CreditCardTotal = value; }
        }

        private decimal _OtherTotal = 0;
        public decimal OtherTotal
        {
            get { return _OtherTotal; }
            set { _OtherTotal = value; }
        }

        #endregion

        #region Constructors
        public frmInvoiceList()
        {
            InitializeComponent();
        }
        #endregion

        #region AlignBottomHeaders
        private void AlignBottomHeaders()
        {
            foreach (string item in alignBottoms)
            {
                dgvList.Columns[item].HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomRight;
            }
        }
        #endregion

        #region FormLoad
        private void FrmInvoiceList_Load(object sender, EventArgs e)
        {
            
            //set title
            lblMenu.Text = programName;

            //utility
            uIUtility = new UIUtility(dgvList, null, null, null, dummyColumns);
            uIUtility.CheckPagination(btnFirst, btnPrev, btnNext, btnLast, lblcurrentPage.Text, lblTotalPages.Text);
            //SetDefaultColumnWidths(); //adjust checkbox sizes
            uIUtility.DummyTable(150);// add dummy table to merge columns
            uIUtility.DisableAutoSort();//disable autosort
            PopulateDropdowns();

            //Theme
            this.pTitle.BackColor = Properties.Settings.Default.JimugoBgColor;
            this.lblMenu.ForeColor = Properties.Settings.Default.jimugoForeColor;
            try
            {

                this.Font = Properties.Settings.Default.jimugoFont;
            }
            catch (Exception)
            {
            }

            this.dgvList.ColumnHeadersDefaultCellStyle.BackColor = Properties.Settings.Default.GridHeaderColor;
            this.dgvList.ColumnHeadersDefaultCellStyle.ForeColor = Properties.Settings.Default.GridHeaderFontColor;
            this.WindowState = FormWindowState.Maximized;
            dgvList.Columns["colCOMPANY_NAME"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomCenter;

            AlignBottomHeaders();
        }
        #endregion

        #region DrawGradHeader
        public void dataGridHeaders()
        {
            String H1 = (25160).ToString();
            String H2 = (25160).ToString();
            String H3 = (25160).ToString();
            String H4 = (25160).ToString();
            String H5 = (25160).ToString();


            //int sum = 0;
            //for (int i = 0; i < dgvList.Rows.Count; ++i)
            //{
            //    sum += Convert.ToInt32(dgvList.Rows[i].Cells[5].Value);
            //    //dataGridView1.SelectedCells[0].Value.ToString()
            //}

            dgvList.Columns["colNO"].HeaderText = "No";
            dgvList.Columns["coLCOMPANY_NO_BOX"].HeaderText = "会社番号+BOX";
            dgvList.Columns["colCOMPANY_NAME"].HeaderText = "会社名";
            //dgvList.Columns["colKEY_SOURCE_MONTHLY_USAGE_FEE"].HeaderText = sum.ToString();
            dgvList.Columns["coLSUPPLIER_INITIAL_EXPENSE"].HeaderText = H2;
            dgvList.Columns["colSUPPLIER_MONTHLY_USAGE_FEE"].HeaderText = H3;
            dgvList.Columns["colBROWSING_INITIAL_EXPENSE"].HeaderText = H4;
            dgvList.Columns["colYEARLY_USAGE_FEE"].HeaderText = H5;

            dgvList.Columns[8].HeaderText = "会社名";
            dgvList.Columns[9].HeaderText = "会社名";
            dgvList.Columns[10].HeaderText = "会社名";
            dgvList.Columns[11].HeaderText = "会社名";
            dgvList.Columns[12].HeaderText = "会社名";
        }
        #endregion

        #region DrawColumnHeaders

        private void Add_Second_Column_Header(PaintEventArgs e, int index, int count, string text, int rowcount, int row, int extra_merge, StringAlignment align)
        {
            UIUtility.Merge_Header(e, index, count, text, dgvList, rowcount, row, extra_merge, align);
        }

        private void Add_Third_Column_Header(PaintEventArgs e, int index, int count, string text, int rowcount, int row, int extra_merge, StringAlignment align)
        {
            UIUtility.Merge_Header(e, index, count, text, dgvList, rowcount, row, extra_merge, align);
        }

        private void DgvList_Paint(object sender, PaintEventArgs e)
        {
            Add_Third_Column_Header(e, 2, 1, "会社名", 4, 0, 2, StringAlignment.Center);

            Add_Third_Column_Header(e, 3, 1, "請求金額", 4, 0, 0, StringAlignment.Center);
            Add_Third_Column_Header(e, 4, 4, InvoiceAmountTotal.ToString(), 4, 0, 0, StringAlignment.Far);
            InvoiceAmountTotal = 0;

            //Third ColumnMerge
            Add_Third_Column_Header(e, 3, 1, "要元", 4, 1, 0, StringAlignment.Center);
            Add_Second_Column_Header(e, 3, 1, "月額利用料", 4, 2, 0, StringAlignment.Center);
            Add_Second_Column_Header(e, 3, 1, keySourceTotal.ToString(), 4, 3, 0, StringAlignment.Far); //"123,123"
            keySourceTotal = 0;

            Add_Third_Column_Header(e, 4, 2, "サプライヤ", 4, 1, 0, StringAlignment.Center);
            Add_Second_Column_Header(e, 4, 1, "初期費用", 4, 2, 0, StringAlignment.Center);
            Add_Second_Column_Header(e, 4, 1, SupplierExpenseTotal.ToString() , 4, 3, 0, StringAlignment.Far);
            SupplierExpenseTotal = 0;
            Add_Second_Column_Header(e, 5, 1, "月額利用料", 4, 2, 0, StringAlignment.Center);
            Add_Second_Column_Header(e, 5, 1, SupplierMonthlyUsageFeeTotal.ToString(), 4, 3, 0, StringAlignment.Far);
            SupplierMonthlyUsageFeeTotal = 0;

            Add_Third_Column_Header(e, 6, 2, "生産情報閲覧", 4, 1, 0, StringAlignment.Center);
            Add_Second_Column_Header(e, 6, 1, "初期費用", 4, 2, 0, StringAlignment.Center);
            Add_Second_Column_Header(e, 6, 1, SupplierBrowsingInitialExpenseTotal.ToString(), 4, 3, 0, StringAlignment.Far);
            SupplierBrowsingInitialExpenseTotal = 0;
            Add_Second_Column_Header(e, 7, 1, "年額利用料", 4, 2, 0, StringAlignment.Center);
            Add_Second_Column_Header(e, 7, 1, YearlyUsageFeeTotal.ToString() , 4, 3, 0, StringAlignment.Far);
            YearlyUsageFeeTotal = 0;

            Add_Third_Column_Header(e, 8, 5, "請求方法", 4, 0, 0, StringAlignment.Center);
            Add_Second_Column_Header(e, 8, 1, "郵送", 4, 1, 1, StringAlignment.Center);
            Add_Second_Column_Header(e, 8, 1, PostalMailTotal.ToString(), 4, 3, 0, StringAlignment.Far);
            PostalMailTotal = 0;
            Add_Second_Column_Header(e, 9, 1, "WEB", 4, 1, 1, StringAlignment.Center);
            Add_Second_Column_Header(e, 9, 1, WebTotal.ToString(), 4, 3, 0, StringAlignment.Far);
            WebTotal = 0;
            Add_Second_Column_Header(e, 10, 1, "Email", 4, 1, 1, StringAlignment.Center);
            Add_Second_Column_Header(e, 10, 1, EmailTotal.ToString(), 4, 3, 0, StringAlignment.Far);
            EmailTotal = 0;
            Add_Second_Column_Header(e, 11, 1, "クレカ", 4, 1, 1, StringAlignment.Center);
            Add_Second_Column_Header(e, 11, 1, CreditCardTotal.ToString(), 4, 3, 0, StringAlignment.Far);
            CreditCardTotal = 0;
            Add_Second_Column_Header(e, 12, 1, "その他", 4, 1, 1, StringAlignment.Center);
            Add_Second_Column_Header(e, 12, 1, OtherTotal.ToString(), 4, 3, 0, StringAlignment.Far);
            OtherTotal = 0;
        }

        private void DgvList_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            //to trigger repaint 
            this.dgvList.Invalidate();
        }

        private void DgvList_Scroll(object sender, ScrollEventArgs e)
        {
            //to trigger repaint 
            //only update on HorizontalScroll Scroll
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                this.dgvList.Invalidate();
            }
        }


        #endregion

        #region DrawColumnHeaders
        //private void Add_Amount_Header(PaintEventArgs e, int index, int count, string text)
        //{
        //    UIUtility.Merge_Header(e, index, count, text, dgvList);
        //}

        //private void Add_Type_Header(PaintEventArgs e, int index, int count, string text)
        //{
        //    UIUtility.Merge_Header(e, index, count, text, dgvList);
        //}

        //private void Add_Second_Column_Header(PaintEventArgs e, int index, int count, string text, int rowcount, int row)
        //{
        //    UIUtility.Merge_Header(e, index, count, text, dgvList, rowcount, row);
        //}

        //private void Add_Third_Column_Header(PaintEventArgs e, int index, int count, string text, int rowcount, int row)
        //{
        //    UIUtility.Merge_Header(e, index, count, text, dgvList, rowcount, row);
        //}

        //private void DgvList_Paint(object sender, PaintEventArgs e)
        //{
        //    Add_Third_Column_Header(e, 2, 1, "会社名", 3, 1);

        //    //Third ColumnMerge
        //    Add_Third_Column_Header(e, 3, 1, "Key Source", 3, 0);
        //    Add_Second_Column_Header(e, 3, 1, "月額利用料", 3, 1);

        //    Add_Third_Column_Header(e, 4, 2, "Supplier", 3, 0);
        //    Add_Second_Column_Header(e, 4, 1, "初期費用", 3, 1);
        //    Add_Second_Column_Header(e, 5, 1, "月額利用料", 3, 1);

        //    Add_Third_Column_Header(e, 6, 2, "Production information browsing", 3, 0);
        //    Add_Second_Column_Header(e, 6, 1, "初期費用", 3, 1);
        //    Add_Second_Column_Header(e, 7, 1, "年額利用料", 3, 1);

        //    Add_Third_Column_Header(e, 8, 5, "Invoice method", 3, 0);
        //    Add_Second_Column_Header(e, 8, 1, "郵送", 3, 1);
        //    Add_Second_Column_Header(e, 9, 1, "WEB", 3, 1);
        //    Add_Second_Column_Header(e, 10, 1, "Email", 3, 1);
        //    Add_Second_Column_Header(e, 11, 1, "クレカ", 3, 1);
        //    Add_Second_Column_Header(e, 12, 1, "その他", 3, 1);
        //}

        //private void DgvList_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        //{
        //    //to trigger repaint 
        //    this.dgvList.Invalidate();
        //}

        //private void DgvList_Scroll(object sender, ScrollEventArgs e)
        //{
        //    //to trigger repaint 
        //    //only update on HorizontalScroll Scroll
        //    if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
        //    {
        //        this.dgvList.Invalidate();
        //    }
        //}


        #endregion

        #region PopulateDropdowns
        private void PopulateDropdowns()
        {
            uIUtility.DisplayCountCombo(cboLimit); //search filter
        }
        #endregion

        #region SearchButton
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            uIUtility.MetaData.Offset = 0;
            try
            {
                uIUtility.MetaData.Limit = int.Parse(cboLimit.SelectedValue.ToString());

            }
            catch (Exception)
            {
                uIUtility.MetaData.Limit = 0;
            }
            try
            {
                    BindGrid();
            }
            catch (Exception)
            {
            }
            //BindGrid();
            //dataGridHeaders();
        }
        #endregion

        #region BindGrid
        private void BindGrid()
        {
            try
            {
                //assign search keywords
                DateTime YEAR_MONTH = Convert.ToDateTime(txtBilling_Date.Text);
                String strYearMonth = YEAR_MONTH.ToString("yyyyMM");
                
                //assign search keywords
                frmInvoiceListController oController = new frmInvoiceListController();
                DataTable dt = oController.GetInvoiceList(strYearMonth, uIUtility.MetaData.Offset, uIUtility.MetaData.Limit, out uIUtility.MetaData); //need to add more parameter

                //int sum = dt.AsEnumerable().Sum(row => row.Field<int>("KEY_SOURCE_MONTHLY_USAGE_FEE"));

                foreach (DataRow row in dt.Rows)
                {
                    keySourceTotal += Convert.ToDecimal(row["KEY_SOURCE_MONTHLY_USAGE_FEE"].ToString());
                    SupplierExpenseTotal += Convert.ToDecimal(row["SUPPLIER_INITIAL_EXPENSE"].ToString());
                    SupplierMonthlyUsageFeeTotal += Convert.ToDecimal(row["SUPPLIER_MONTHLY_USAGE_FEE"].ToString());
                    SupplierBrowsingInitialExpenseTotal += Convert.ToDecimal(row["BROWSING_INITIAL_EXPENSE"].ToString());
                    YearlyUsageFeeTotal += Convert.ToDecimal(row["YEARLY_USAGE_FEE"].ToString());
                    InvoiceAmountTotal = keySourceTotal + SupplierExpenseTotal + SupplierMonthlyUsageFeeTotal + SupplierBrowsingInitialExpenseTotal + YearlyUsageFeeTotal;

                    if (row["POSTAL_MAIL"].ToString() == "●")
                    {
                        PostalMailTotal++;
                    }
                    if (row["WEB"].ToString() == "●")
                    {
                        WebTotal++;
                    }
                    if (row["Email"].ToString() == "●")
                    {
                        EmailTotal++;
                    }
                    if (row["CREDIT_CARD"].ToString() == "●")
                    {
                        CreditCardTotal++;
                    }
                    if (row["OTHER"].ToString() == "●")
                    {
                        OtherTotal++;
                    }

                }

                dgvList.Columns["colCOMPANY_NAME"].HeaderText = "請求金額計";
                dgvList.Columns["colKEY_SOURCE_MONTHLY_USAGE_FEE"].HeaderText = keySourceTotal.ToString();
                dgvList.Columns["coLSUPPLIER_INITIAL_EXPENSE"].HeaderText = SupplierExpenseTotal.ToString();
                dgvList.Columns["colSUPPLIER_MONTHLY_USAGE_FEE"].HeaderText = SupplierMonthlyUsageFeeTotal.ToString();
                dgvList.Columns["colBROWSING_INITIAL_EXPENSE"].HeaderText = SupplierBrowsingInitialExpenseTotal.ToString();
                dgvList.Columns["colYEARLY_USAGE_FEE"].HeaderText = YearlyUsageFeeTotal.ToString();

                dgvList.Columns["coLEmail"].HeaderText = EmailTotal.ToString(); //coLEmail
                dgvList.Columns["coLPOSTAL_MAIL"].HeaderText = PostalMailTotal.ToString(); //coLEmail
                dgvList.Columns["colWEB"].HeaderText = WebTotal.ToString(); //coLEmail
                dgvList.Columns["colCREDIT_CARD"].HeaderText = CreditCardTotal.ToString(); //coLEmail
                dgvList.Columns["colOTHER"].HeaderText = OtherTotal.ToString(); //coLEmail

                if (dt.Rows.Count > 0)
                {
                    uIUtility.dtList = dt;
                    dgvList.DataSource = uIUtility.dtList;
                    uIUtility.dtOrigin = uIUtility.dtList.Copy();

                    //pagination
                    uIUtility.CalculatePagination(lblcurrentPage, lblTotalPages, lblTotalRecords);
                }
                else
                {
                    //clear data except headers
                    uIUtility.ClearDataGrid();
                    uIUtility.CalculatePagination(lblcurrentPage, lblTotalPages, lblTotalRecords);
                }

                uIUtility.CheckPagination(btnFirst, btnPrev, btnNext, btnLast, lblcurrentPage.Text, lblTotalPages.Text);
                //uIUtility.FormatUpdatedat();

                //check for disable flag
                //uIUtility.CheckForDisableField();
            }
            catch (System.TimeoutException)
            {
                MetroMessageBox.Show(this, "\n" + Messages.General.ServerTimeOut, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.Net.WebException)
            {
                MetroMessageBox.Show(this, "\n" + Messages.General.NoConnection, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ex.Message, ex, false);
                MetroMessageBox.Show(this, "\n" + Messages.General.ThereWasAnError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region CreateCSVButton
        private void BtnCreateCSVFile_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveCSV = new SaveFileDialog();
                saveCSV.Filter = "CSV file (*.csv)|*.csv| All Files (*.*)|*.*";
                DateTime yearMonth = Convert.ToDateTime(txtBilling_Date.Text);
                String YEAR_MONTH = yearMonth.ToString("yyyyMM");
                saveCSV.FileName = "NCS" + YEAR_MONTH + "-" + System.DateTime.Now.ToString("yyyyMMddHHmmss");

                frmInvoiceListController oController = new frmInvoiceListController();
                DataTable dt = oController.GetInvoiceListForCSVCreate(txtBilling_Date.Text);

                try
                {
                    string return_message = "";
                    return_message = dt.Rows[0]["Error Message"].ToString();
                    if (!string.IsNullOrEmpty(return_message))
                    {
                        MetroMessageBox.Show(this, "\n" + return_message, "Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                catch (Exception)
                {
                    //Exporting to Excel
                    if (dt.Rows.Count > 0)
                    {
                        if (saveCSV.ShowDialog() == DialogResult.OK) // save as dialog
                        {
                            Utility.WriteToCsvFile(dt, saveCSV.FileName);
                            MetroMessageBox.Show(this, "\n" + Messages.ComparisonResultDetail.CSVDownloaded, "CSV Downloaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MetroMessageBox.Show(this, "\n" + Messages.ComparisonResultDetail.NoRecordToDownload, "Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                
            }
            catch (System.TimeoutException)
            {
                MetroMessageBox.Show(this, "\n" + Messages.General.ServerTimeOut, "Download Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.Net.WebException)
            {
                MetroMessageBox.Show(this, "\n" + Messages.General.NoConnection, "Download Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ex.Message, ex, false);
                MetroMessageBox.Show(this, "\n" + Messages.General.ThereWasAnError, "Download Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region ButtonCreateInvoiceData
        private void BtnCreateInvoiceData_Click(object sender, EventArgs e)
        {
            string status = "0";
            frmInvoiceListController oController = new frmInvoiceListController();
            DataTable dt = oController.CreateInvoiceData(txtBilling_Date.Text,status);

            string return_message = "";
            string count = "";
            try
            {
                return_message = dt.Rows[0]["Error Message"].ToString();
                count = dt.Rows[0]["Count"].ToString();
            }
            catch (Exception)
            {

            }

            if (!string.IsNullOrEmpty(return_message) && count != "0")
            {
                var confirmResult = MetroMessageBox.Show(this, "\n" + return_message, "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (confirmResult == DialogResult.OK)
                {
                    status = "1";
                    //send to web service
                    frmInvoiceListController controller = new frmInvoiceListController();
                    DataTable dtResult = oController.CreateInvoiceData(txtBilling_Date.Text,status);

                    //DataTable result = oController.SubmitConfirmationRequest(dt, out uIUtility.MetaData);

                }
            }
            

        }
        #endregion

        #region CellContentClickEvent
        private void DgvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int Key_source_Monthly_usage_fee_REQ_SEQ = 0;
            int Supplier_Initial_expense_REQ_SEQ = 0;
            int Supplier_Monthly_usage_fee_REQ_SEQ = 0;
            int Production_information_browsing_Initial_expense_REQ_SEQ = 0;
            int View_production_information_Annual_usage_fee_REQ_SEQ = 0;

            string colName = dgvList.Columns[e.ColumnIndex].Name;

            string COMPANY_NO_BOX= dgvList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            int REQ_SEQ;

            if (!string.IsNullOrEmpty(COMPANY_NO_BOX) && colName == "coLCOMPANY_NO_BOX")
            {
                if (!String.IsNullOrEmpty(dgvList.Rows[e.RowIndex].Cells["Key_source_Monthly_usage_fee_REQ_SEQ"].Value.ToString()))
                {
                    Key_source_Monthly_usage_fee_REQ_SEQ = Convert.ToInt32(dgvList.Rows[e.RowIndex].Cells["Key_source_Monthly_usage_fee_REQ_SEQ"].Value.ToString());

                }

                if (!String.IsNullOrEmpty(dgvList.Rows[e.RowIndex].Cells["Supplier_Initial_expense_REQ_SEQ"].Value.ToString()))
                {
                    Supplier_Initial_expense_REQ_SEQ = Convert.ToInt32(dgvList.Rows[e.RowIndex].Cells["Supplier_Initial_expense_REQ_SEQ"].Value.ToString());
                }

                if (!String.IsNullOrEmpty(dgvList.Rows[e.RowIndex].Cells["Supplier_Monthly_usage_fee_REQ_SEQ"].Value.ToString()))
                {
                    Supplier_Monthly_usage_fee_REQ_SEQ = Convert.ToInt32(dgvList.Rows[e.RowIndex].Cells["Supplier_Monthly_usage_fee_REQ_SEQ"].Value.ToString());
                }

                if (!String.IsNullOrEmpty(dgvList.Rows[e.RowIndex].Cells["Production_information_browsing_Initial_expense_REQ_SEQ"].Value.ToString()))
                {
                    Production_information_browsing_Initial_expense_REQ_SEQ = Convert.ToInt32(dgvList.Rows[e.RowIndex].Cells["Production_information_browsing_Initial_expense_REQ_SEQ"].Value.ToString());
                }

                if (!String.IsNullOrEmpty(dgvList.Rows[e.RowIndex].Cells["View_production_information_Annual_usage_fee_REQ_SEQ"].Value.ToString()))
                {
                    View_production_information_Annual_usage_fee_REQ_SEQ = Convert.ToInt32(dgvList.Rows[e.RowIndex].Cells["View_production_information_Annual_usage_fee_REQ_SEQ"].Value.ToString());
                }

                var anArray = new int[] { Key_source_Monthly_usage_fee_REQ_SEQ, Supplier_Initial_expense_REQ_SEQ, Supplier_Monthly_usage_fee_REQ_SEQ, Production_information_browsing_Initial_expense_REQ_SEQ, View_production_information_Annual_usage_fee_REQ_SEQ };
                REQ_SEQ = anArray.Max();

                //string value = Convert.ToString(row.Cells["colBREAK_DOWN"].Value);
                //if (value != null)
                //{
                frmCustomerMasterPopup frm = new frmCustomerMasterPopup(
                COMPANY_NO_BOX,
                REQ_SEQ.ToString()
                    );

                frm.Show();

                //    //if (frm.Close()==true)
                //    //{
                //    //    row.Cells["colORDER_DATE"].Value = frm.ORDER_DATE;
                //    //    row.Cells["colUPDATED_BY"].Value = Utility.Id;
                //    //    frm.Dispose();
                //    //}
                //}
            }

        }
        #endregion
    }
}
