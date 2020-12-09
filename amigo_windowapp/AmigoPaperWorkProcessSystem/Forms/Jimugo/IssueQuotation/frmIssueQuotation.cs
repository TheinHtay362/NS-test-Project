﻿using AmigoPaperWorkProcessSystem.Controllers;
using AmigoPaperWorkProcessSystem.Core;
using AmigoPaperWorkProcessSystem.Forms.Jimugo.Issue_Quotation;
using MetroFramework;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace AmigoPaperWorkProcessSystem.Forms.Jimugo.Issue_Quotation
{
    public partial class frmIssueQuotation : Form
    {
        #region Declare
        private UIUtility uIUtility;
        private string programID = "";
        private string programName = "";
        private string CompanyNoBox = "";
        private string REQ_SEQ = "";
        private string Reg_Complete_Date;
        private string Quotation_Date;
        private string Order_Date;
        private string Company_Name;
        private string CONTRACT_PLAN = "";
        private string INPUT_PERSON = "";
        private string CREATED_TIME = "";
        #endregion

        #region Constructor
        public frmIssueQuotation()
        {
            InitializeComponent();
        }

        public frmIssueQuotation(
            string programID, 
            string programName, 
            string CompanyNoBox, 
            string REQ_SEQ,
            string Quotation_Date,
            string Order_Date,
            string Reg_Complete_Date,
            string CompanyName) :this()
        {
            this.programID = programID;
            this.programName = programName;
            this.CompanyNoBox = CompanyNoBox;
            this.REQ_SEQ = REQ_SEQ;
            this.Quotation_Date = Quotation_Date;
            this.Order_Date = Order_Date;
            this.Reg_Complete_Date = Reg_Complete_Date;
            this.Company_Name = CompanyName;

        }
        #endregion

        #region FormLoad
        private void FrmIssueQuotation_Load(object sender, EventArgs e)
        {
            //Theme
            this.pTitle.BackColor = Properties.Settings.Default.JimugoBgColor;
            this.lblMenu.ForeColor = Properties.Settings.Default.jimugoForeColor;
            this.Font = Properties.Settings.Default.jimugoFont;

            //set title
            lblMenu.Text = programName; 
            uIUtility = new UIUtility();

            //set textboxes
            txtCompanyNoBox.Text = this.CompanyNoBox;
            txtCompanyName.Text = this.Company_Name;
            txtNotificationDate.Text = this.Reg_Complete_Date;
            txtIssueDate.Text = this.Quotation_Date;
            txtOrderDate.Text = this.Order_Date;
            txtNotificationDate.Text = this.Reg_Complete_Date;

            GetInitialData();
        }
        #endregion

        #region GetInitialData
        private void GetInitialData()
        {
            try
            {
                frmIssueQuotationController issueQuotation = new frmIssueQuotationController();
                DataTable result = issueQuotation.GetInitialData(CompanyNoBox, REQ_SEQ, out uIUtility.MetaData);
                if (result.Rows.Count > 0)
                {
                    SetValues(result.Rows[0]);
                }
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

        #region SetValues
        private void SetValues(DataRow dr)
        {
            //set EDI account
            txtEDIAccount.Text = dr["EDI_ACCOUNT"].ToString();
            try
            {
                //set quotation start date
                txtQuotationStartDate.Text = DateTime.ParseExact(dr["START_USE_DATE"].ToString(), "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("yyyy/MM/dd");

            }
            catch (Exception)
            {

            }
            //set email address
            txtDestinationMail.Text = dr["EMAIL_ADDRESS"].ToString();
            //Tax
            txtTax.Text = dr["CONSUMPTION_TAX"].ToString(); 

            //Expiration date
            txtQuotationExpireDay.Text = dr["EXPIRATION_DATE"].ToString();
            //check for contract code and manupulate controls
            CONTRACT_PLAN = Convert.ToString(dr["CONTRACT_PLAN"]).Trim(); 
            INPUT_PERSON = Convert.ToString(dr["INPUT_PERSON"]);
            CheckEditableRegions();
            
        }
        #endregion

        #region CheckEditableRegions
        private void CheckEditableRegions()
        {
            switch (CONTRACT_PLAN)
            {
                case "PRODUCT":
                    ChangeEditableDependingOnContractCode(true);
                    break;
                default:
                    ChangeEditableDependingOnContractCode(false);
                    break;
            }
        }
        #endregion

        #region void ChangeEditableDependingOnContractCode
        private void ChangeEditableDependingOnContractCode(bool isproduct)
        {
            chkInitialQuot.Checked = false;
            chkMonthlyQuote.Checked = false;
            chkProductionInfo.Checked = false;

            chkInitialQuot.Enabled = !isproduct;
            chkMonthlyQuote.Enabled = !isproduct;
            chkProductionInfo.Enabled = isproduct;
            txtPeriodFrom.Enabled = isproduct;
            txtPeriodTo.Enabled = isproduct;
            txtMonthlySpecialDiscount.Enabled = !isproduct;
            txtYearlySpecialDiscount.Enabled = isproduct;
        }
        #endregion

        #region Validate Inputs
        private bool ValidateInputs()
        {
            //validate

            if (!CheckUtility.SearchConditionCheck(this, txtCompanyNoBox.Text.Trim(), true, Utility.DataType.HALF_ALPHA_NUMERIC, 10, 10))
            {
                return false;
            }

            if (!CheckUtility.SearchConditionCheck(this, txtEDIAccount.Text.Trim(), false, Utility.DataType.EDI_ACCOUNT, -1, -1))
            {
                return false;
            }

            if (!CheckUtility.SearchConditionCheck(this, txtCompanyName.Text.Trim(), false, Utility.DataType.FULL_WIDTH, -1, -1))
            {
                return false;
            }

            if (!CheckUtility.SearchConditionCheck(this, txtIssueDate.Text.Trim(), false, Utility.DataType.DATE, -1, -1))
            {
                return false;
            }

            if (!CheckUtility.SearchConditionCheck(this, txtOrderDate.Text.Trim(), false, Utility.DataType.DATE, -1, -1))
            {
                return false;
            }

            if (!CheckUtility.SearchConditionCheck(this, txtNotificationDate.Text.Trim(), false, Utility.DataType.DATE, -1, -1))
            {
                return false;
            }

            if (!CheckUtility.SearchConditionCheck(this, txtTax.Text.Trim(), true, Utility.DataType.HALF_NUMBER, 2, 1))
            {
                return false;
            }

            if (!CheckUtility.SearchConditionCheck(this, txtQuotationStartDate.Text.Trim(), chkMonthlyQuote.Checked, Utility.DataType.DATE, -1, -1))
            {
                return false;
            }

            if (!CheckUtility.SearchConditionCheck(this, txtQuotationExpireDay.Text.Trim(), true, Utility.DataType.HALF_NUMBER, 3, 1))
            {
                return false;
            }

            if (!CheckUtility.SearchConditionCheck(this, txtInitialSpecialDiscount.Text.Trim(), false, Utility.DataType.HALF_NUMBER, 7, 0))
            {
                return false;
            }

            if (!CheckUtility.SearchConditionCheck(this, txtMonthlySpecialDiscount.Text.Trim(), false, Utility.DataType.HALF_NUMBER, 7, 0))
            {
                return false;
            }

            if (!CheckUtility.SearchConditionCheck(this, txtYearlySpecialDiscount.Text.Trim(), false, Utility.DataType.HALF_NUMBER, 7, 0))
            {
                return false;
            }

            if (!CheckUtility.SearchConditionCheck(this, txtDestinationMail.Text.Trim(), true, Utility.DataType.EMAIL, 255, 0))
            {
                return false;
            }

            if (!CheckUtility.SearchConditionCheck(this, txtPeriodFrom.Text.Trim(), CONTRACT_PLAN=="PRODUCT" ? true : false, Utility.DataType.DATE, -1, -1))
            {
                return false;
            }

            if (!CheckUtility.SearchConditionCheck(this, txtPeriodTo.Text.Trim(), CONTRACT_PLAN == "PRODUCT" ? true : false, Utility.DataType.DATE, -1, -1))
            {
                return false;
            }
            if (!CheckUtility.DateRationalCheck(txtPeriodFrom.Text.Trim(), txtPeriodTo.Text.Trim()))
            {
                MetroMessageBox.Show(this, "\n" + JimugoMessages.I000WB004, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!CheckUtility.SearchConditionCheck(this, txtInitialRemark.Text.Trim(), false, Utility.DataType.TEXT, 500, 0))
            {
                return false;
            }

            if (!CheckUtility.SearchConditionCheck(this, txtMonthlyRemark.Text.Trim(), false, Utility.DataType.TEXT, 500, 0))
            {
                return false;
            }

            if (!CheckUtility.SearchConditionCheck(this, txtOrderRemark.Text.Trim(), false, Utility.DataType.TEXT, 500, 0))
            {
                return false;
            }

            if (!CheckUtility.SearchConditionCheck(this, txtProductionInfoRemark.Text.Trim(), false, Utility.DataType.TEXT, 500, 0))
            {
                return false;
            }

            //checkboxes check message ?
            bool atLeastOneChecked = false;
            if (chkMonthlyQuote.Checked)
            {
                atLeastOneChecked = true;
            }

            if (chkInitialQuot.Checked)
            {
                atLeastOneChecked = true;
            }

            if (chkProductionInfo.Checked)
            {
                atLeastOneChecked = true;
            }

            if (chkOrderForm.Checked)
            {
                atLeastOneChecked = true;
            }
            if (!atLeastOneChecked)
            {
                MetroMessageBox.Show(this, "\n" + "少なくとも1つの帳票タイプをチェックしてください。", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!string.IsNullOrEmpty(txtInitialSpecialDiscount.Text.Trim()))
            {
                bool OneChecked = false;

                if (chkInitialQuot.Checked)
                {
                    OneChecked = true;
                }

                if (chkProductionInfo.Checked)
                {
                    OneChecked = true;
                }

                if (chkOrderForm.Checked)
                {
                    OneChecked = true;
                }

                if (!OneChecked)
                {
                    MetroMessageBox.Show(this, "\n" + string.Format(JimugoMessages.E000WB035, "特別値引き額(初期)", "初期見積書、生産情報閲覧、注文書"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (!CheckUtility.SearchConditionCheck(this, txtInitialRemark.Text.Trim(), false, Utility.DataType.FULL_WIDTH, -1, -1))
            {
                return false;
            }

            if (!CheckUtility.SearchConditionCheck(this, txtMonthlyRemark.Text.Trim(), false, Utility.DataType.FULL_WIDTH, -1, -1))
            {
                return false;
            }

            if (!CheckUtility.SearchConditionCheck(this, txtOrderRemark.Text.Trim(), false, Utility.DataType.FULL_WIDTH, -1, -1))
            {
                return false;
            }

            if (!CheckUtility.SearchConditionCheck(this, txtProductionInfoRemark.Text.Trim(), false, Utility.DataType.FULL_WIDTH, -1, -1))
            {
                return false;
            }

            return true;
        }
        #endregion

        #region Preview Button
        private async void BtnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInputs())
                {
                    frmIssueQuotationController oController = new frmIssueQuotationController();
                    decimal decSpecialAmt = 0;
                    DataTable dtExportInfo = new DataTable();
                    dtExportInfo.Columns.Add("ExportType");
                    dtExportInfo.Columns.Add("SpecialDiscount");
                    if (chkInitialQuot.Checked) //Initial
                    {
                        decimal.TryParse(txtInitialSpecialDiscount.Text, out decSpecialAmt);
                        decSpecialAmt = decSpecialAmt * -1;
                        DataRow dr = dtExportInfo.NewRow();
                        dr["ExportType"] = "1";
                        dr["SpecialDiscount"] = decSpecialAmt.ToString("N0");
                        dtExportInfo.Rows.Add(dr);
                    }
                    if (chkMonthlyQuote.Checked)//Monthly
                    {
                        decimal.TryParse(txtMonthlySpecialDiscount.Text, out decSpecialAmt);
                        decSpecialAmt = decSpecialAmt * -1;

                        DataRow dr = dtExportInfo.NewRow();
                        dr["ExportType"] = "2";
                        dr["SpecialDiscount"] = decSpecialAmt.ToString("N0");
                        dtExportInfo.Rows.Add(dr);
                    }
                    if (chkProductionInfo.Checked)//PIBrowsing
                    {
                        decimal IntialDiscount = 0;
                        decimal.TryParse(txtInitialSpecialDiscount.Text, out IntialDiscount);
                        decimal YearlyDsicount = 0;
                        decimal.TryParse(txtYearlySpecialDiscount.Text, out YearlyDsicount);
                        decSpecialAmt = (IntialDiscount + YearlyDsicount) * -1;

                        DataRow dr = dtExportInfo.NewRow();
                        dr["ExportType"] = "4";
                        dr["SpecialDiscount"] = decSpecialAmt.ToString("N0");
                        dtExportInfo.Rows.Add(dr);
                    }
                    if (chkOrderForm.Checked)//Order Form
                    {
                        if (CONTRACT_PLAN == "PRODUCT")
                        {
                            decimal.TryParse(txtInitialSpecialDiscount.Text, out decSpecialAmt);
                            decSpecialAmt = decSpecialAmt * -1;
                        }
                        else if (CONTRACT_PLAN != "PRODUCT")
                        {
                            decimal IntialDiscount = 0;
                            decimal.TryParse(txtInitialSpecialDiscount.Text, out IntialDiscount);
                            decimal YearlyDiscount = 0;
                            decimal.TryParse(txtYearlySpecialDiscount.Text, out YearlyDiscount);
                            decSpecialAmt = (IntialDiscount + YearlyDiscount) * -1;
                        }

                        DataRow dr = dtExportInfo.NewRow();
                        dr["ExportType"] = "3";
                        dr["SpecialDiscount"] = decSpecialAmt.ToString("N0");
                        dtExportInfo.Rows.Add(dr);
                    }
                   

                    decimal decTaxAmount = (decimal)0;
                    string strStartDate = "";
                    int ExpireDay = 0;
                    string strFromCertificate = "";
                    string strToCertificate = "";
                    string strExpireDate = "";
                    //ExpireDay = Convert.ToInt32(txtQuotationExpireDay.Text.Trim());
                    int.TryParse(txtQuotationExpireDay.Text, out ExpireDay);
                    decimal.TryParse(txtTax.Text, out decTaxAmount);
                    if (CheckUtility.SearchConditionCheck(this, txtQuotationStartDate.Text, false, Utility.DataType.DATE, 255, 0))
                    {
                        strStartDate = DateConverter(txtQuotationStartDate.Text).ToString("yyyyMMdd");
                        strExpireDate = DateConverter(txtQuotationStartDate.Text).AddDays(ExpireDay).ToString("yyyyMMdd");
                    }
                    else
                    {
                        strStartDate = DateTime.Now.ToString("yyyyMMdd");
                    }

                    if (CheckUtility.SearchConditionCheck(this, txtPeriodFrom.Text, false, Utility.DataType.DATE, 255, 0))
                    {
                        strFromCertificate = DateConverter(txtPeriodFrom.Text).ToString("yyyyMMdd");
                    }
                    else
                    {
                        strFromCertificate = DateTime.Now.ToString("yyyyMMdd");
                    }

                    if (CheckUtility.SearchConditionCheck(this, txtPeriodTo.Text, false, Utility.DataType.DATE, 255, 0))
                    {
                        strToCertificate = DateConverter(txtPeriodTo.Text).ToString("yyyyMMdd");
                    }
                    else
                    {
                        strToCertificate = DateTime.Now.ToString("yyyyMMdd");
                    }
                    string strExportInfo = Utility.DtToJSon(dtExportInfo,"ReqestPDF");
                    DataTable result = oController.PreviewFunction(txtCompanyNoBox.Text, txtCompanyName.Text, REQ_SEQ, decTaxAmount, strStartDate, txtQuotationExpireDay.Text.Trim(), strFromCertificate, strToCertificate, strExportInfo, CONTRACT_PLAN, txtInitialRemark.Text.Trim(), txtMonthlyRemark.Text.Trim(),txtProductionInfoRemark.Text.Trim(), txtOrderRemark.Text.Trim());
                    string error_message = "";
                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        CREATED_TIME = Convert.ToString(result.Rows[i]["Created Time"]);
                        error_message = Convert.ToString(result.Rows[i]["Error Message"]);
                        if (!string.IsNullOrEmpty(error_message))
                        {
                            MetroMessageBox.Show(this, "\n" + error_message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    DataTable dt = DTParameter();

                    #region CallPreviewScreen
                    string temp_deirectory = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\Temp";

                    if (!Directory.Exists(temp_deirectory))
                    {
                        Directory.CreateDirectory(temp_deirectory);
                    }

                    //delete temp files
                    Utility.DeleteFiles(temp_deirectory);

                    string destinationpath = temp_deirectory;
                    btnPreview.Enabled = false;
                    bool success = false;
                    result.Columns.Add("LocalPath");
                    result.Columns.Add("FileName");
                    for (int i=0;i< result.Rows.Count;i++)
                    {
                        string pdfLink = Convert.ToString(result.Rows[i]["DownloadLink"]);
                        string filename = WebUtility.GetFileNamefromURL(pdfLink);
                        success = await Core.WebUtility.Download(pdfLink, destinationpath + @"\" + filename);
                        if (success)
                        {
                            result.Rows[i]["LocalPath"] = destinationpath + @"\" + filename;
                            result.Rows[i]["FileName"] =  filename;
                        }
                    }
                    if (success)
                    {
                        frmIssueQuotationPrevew frm = new frmIssueQuotationPrevew(dt, result);
                        frm.ShowDialog();
                        this.Show();
                        this.BringToFront();
                    }
                    btnPreview.Enabled = true;
                    #endregion
                }

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

        #region AddToDataTable
        public DataTable DTParameter()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EMAIL_ADDRESS");
            dt.Columns.Add("COMPANY_NO_BOX");
            dt.Columns.Add("REQ_SEQ");
            dt.Columns.Add("CONSUMPTION_TAX");
            dt.Columns.Add("INITIAL_SPECIAL_DISCOUNTS");
            dt.Columns.Add("MONTHLY_SPECIAL_DISCOUNTS");
            dt.Columns.Add("YEARLY_SPECIAL_DISCOUNT");
            dt.Columns.Add("INPUT_PERSON");
            dt.Columns.Add("INITIAL_REMARK");
            dt.Columns.Add("MONTHLY_REMARK");
            dt.Columns.Add("PI_REMARK");
            dt.Columns.Add("ORDER_REMARK");
            dt.Columns.Add("CONTRACT_PLAN");
            dt.Columns.Add("Created Time");
            dt.Rows.Add(txtDestinationMail.Text.Trim(),
                        txtCompanyNoBox.Text.Trim(),
                        REQ_SEQ,
                        txtTax.Text.Trim(),
                        txtInitialSpecialDiscount.Text.Trim(),
                        txtMonthlySpecialDiscount.Text.Trim(),
                        txtYearlySpecialDiscount.Text.Trim(),
                        INPUT_PERSON,
                        txtInitialRemark.Text.Trim(),
                        txtMonthlyRemark.Text.Trim(),
                        txtOrderRemark.Text.Trim(),
                        txtProductionInfoRemark.Text.Trim(),
                        CONTRACT_PLAN,
                        CREATED_TIME
                        );
            return dt;
            
        }
        #endregion

        #region DateConverter
        protected DateTime DateConverter(string strDataValue)
        {
            DateTime dtm = new DateTime();
            try
            {
                dtm = DateTime.ParseExact(strDataValue, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            }
            catch(Exception)
            {
                try
                {
                    dtm = DateTime.ParseExact(strDataValue, "yyyy/M/d", CultureInfo.InvariantCulture);
                }
                catch (Exception)
                { 
                
                }
            }
            return dtm;
        }
        #endregion


    }
}
