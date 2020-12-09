﻿using AmigoPaperWorkProcessSystem.Core;
using AmigoPaperWorkProcessSystem.Core.Model;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmigoPaperWorkProcessSystem.Core
{
    public static class CheckUtility
    {
        #region SearchConditionCheck
        public static bool SearchConditionCheck(IWin32Window owner, string value, bool require, Utility.DataType encoding, int max, int min)
        {
            value = value.Trim();
            int length = value.Length;
            string msg = "";
            if (require) //require check
            {
                if (length <= 0)
                {
                    //error message
                    MetroMessageBox.Show(owner, "\n" + JimugoMessages.E000ZZ001, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            switch (encoding)
            {
                case Utility.DataType.FULL_WIDTH:
                    if (!FullWidth_Check(value))
                    {
                        //error message
                        MetroMessageBox.Show(owner, "\n" + JimugoMessages.E000ZZ002, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    break;
                case Utility.DataType.HALF_NUMBER:
                    if (!IsMatch(value, HALF_NUMBER))
                    {
                        //error message
                        MetroMessageBox.Show(owner, "\n" + JimugoMessages.E000ZZ002, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    break;
                case Utility.DataType.HALF_ALPHA_NUMERIC:
                    if (!IsMatch(value, HALF_ALPHA_NUMERIC))
                    {
                        //error message
                        MetroMessageBox.Show(owner, "\n" + JimugoMessages.E000ZZ002, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    break;
                case Utility.DataType.HALF_KANA_ALPHA_NUMERIC:
                    if (!IsMatch(value, HALF_KANA_ALPHA_NUMERIC))
                    {
                        //error message
                        MetroMessageBox.Show(owner, "\n" + JimugoMessages.E000ZZ002, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    break;
                case Utility.DataType.DATE:
                    if (!string.IsNullOrEmpty(value))
                    {
                        if (!DateFormatCheck(value, out msg))
                        {
                            //error message
                            MetroMessageBox.Show(owner, "\n" + msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                    break;
                case Utility.DataType.TIMESTAMP:
                    if (!string.IsNullOrEmpty(value))
                    {
                        if (!TimeStampCheck(value, out msg))
                        {
                            //error message
                            MetroMessageBox.Show(owner, "\n" + msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                    break;
                case Utility.DataType.EMAIL:
                    if (!string.IsNullOrEmpty(value))
                    {
                        if (!EmailCheck(value, out msg))
                        {
                            //error message
                            MetroMessageBox.Show(owner, "\n" + msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                    break;
                default:
                    break;
            }

            if (!DigitRangeCheck(length, min, max, out msg))
            {
                //error message
                MetroMessageBox.Show(owner, "\n" + msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        #endregion

        #region FullWidthCheck
        private static bool FullWidth_Check(string value)
        {
            Encoding Enc = Encoding.GetEncoding("Shift-JIS");

            if (Enc.GetByteCount(value) == value.Length * 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Patterns
        private const string EDI_ACCOUNT = "@[a-zA-Z0-9_]+([a-zA-Z0-9]+[a-zA-Z0-9])";
        private const string ASCII = "[!-~]+";
        private const string MAIL = @"^(\s?[^\s,]+@[^\s,]+\.[^\s,]+\s?,)*(\s?[^\s,]+@[^\s,]+\.[^\s,]+)$";
        private const string HALF_ALPHA_NUMERIC = "[A-Za-z0-9]+";
        private const string HALF_KANA_ALPHA_NUMERIC = "[ｱ-ﾝA-Za-z0-9]+";
        private const string HALF_NUMBER = "[0-9]+";
        #endregion

        public static bool RequireCheck(DataGridView dgvList, int rowIndex, List<Validate> items)
        {
            string msg = "";
            bool pass = true;

            //check for require fields
            foreach (Validate item in items)
            {
                string value = dgvList.Rows[rowIndex].Cells[item.Name].Value == null ? null : dgvList.Rows[rowIndex].Cells[item.Name].Value.ToString().Trim();
                if (String.IsNullOrEmpty(value) && item.Require)
                {
                    msg = dgvList.Columns[item.Name].HeaderText + ", " + JimugoMessages.E000ZZ001;
                    pass = false;
                    break;
                }
            }

            // If check is not passed
            if (!pass)
            {
                dgvList.Rows[rowIndex].Cells["colMK"].Value = "X";
                dgvList.Rows[rowIndex].Cells["colUPDATE_MESSAGE"].Value = msg;
            }

            return pass;
        }

        public static bool DataTypeCheck(DataGridView dgvList, int rowIndex, List<Validate> items)
        {
            string msg = "";

            //check for Data types
            foreach (Validate item in items)
            {
                string value = dgvList.Rows[rowIndex].Cells[item.Name].Value == null ? null : dgvList.Rows[rowIndex].Cells[item.Name].Value.ToString().Trim();
                switch (item.Type)
                {
                    case Utility.DataType.EMAIL:
                        EmailCheck(value, out msg);
                        break;
                    case Utility.DataType.NUMBER:
                        NumberCheck(value, item.Min, item.Max, out msg);
                        break;
                    case Utility.DataType.DATE:
                        DateFormatCheck(value, out msg);
                        if (!String.IsNullOrEmpty(msg))
                        {
                            msg = string.Format(msg, dgvList.Columns[item.Name].HeaderText);
                        }
                        break;
                    case Utility.DataType.TIMESTAMP:
                        TimeStampCheck(value, out msg);
                        break;
                    case Utility.DataType.EDI_ACCOUNT:
                        EDIaccountCheck(value, out msg);
                        break;
                    case Utility.DataType.ASCII:
                        ASCIICheck(value, out msg);
                        break;
                    case Utility.DataType.DATE_RANGE:
                        string strdate1, strdate2;
                        strdate1 = dgvList.Rows[rowIndex].Cells[item.Data1].Value.ToString();
                        strdate2 = dgvList.Rows[rowIndex].Cells[item.Data2].Value.ToString();
                        if ((!string.IsNullOrEmpty(strdate1)) && (!string.IsNullOrEmpty(strdate2)))
                        {
                            DateTime date1 = DateTime.Parse(strdate1);
                            DateTime date2 = DateTime.Parse(strdate2);
                            DateRationalCheck(date1, date2, out msg);
                            if (!String.IsNullOrEmpty(msg))
                            {
                                msg = string.Format(msg, date1.ToString("yyyy/MM/dd HH:mm"), date2.ToString("yyyy/MM/dd HH:mm"));
                            }
                        }
                        break;
                    case Utility.DataType.HALF_NUMBER:
                        if (!IsMatch(value, HALF_NUMBER))
                        {
                            msg = JimugoMessages.E000ZZ002;
                        }
                        break;
                    case Utility.DataType.HALF_KANA_ALPHA_NUMERIC:
                        if (!IsMatch(value, HALF_KANA_ALPHA_NUMERIC))
                        {
                            msg = JimugoMessages.E000ZZ002;
                        }
                        break;
                    case Utility.DataType.HALF_ALPHA_NUMERIC:
                        if (!IsMatch(value, HALF_ALPHA_NUMERIC))
                        {
                            msg = JimugoMessages.E000ZZ002;
                        }
                        break;
                    case Utility.DataType.FULL_WIDTH:
                        if (!FullWidth_Check(value))
                        {
                            msg = JimugoMessages.E000ZZ002;
                        }
                        break;
                    case Utility.DataType.MINIMUM:
                        MinimumCheck(value, item.Min, out msg);
                        break;
                    case Utility.DataType.INPUT_TYPE:
                        string data1 = dgvList.Rows[rowIndex].Cells[item.Data1].Value == null ? null : dgvList.Rows[rowIndex].Cells[item.Data1].Value.ToString();
                        string data2 = dgvList.Rows[rowIndex].Cells[item.Data2].Value == null ? null : dgvList.Rows[rowIndex].Cells[item.Data2].Value.ToString();
                        InputTypecheck(data1, data2, out msg);
                        break;
                    default:
                        break;
                }

                if (!string.IsNullOrEmpty(msg))
                {
                    break;
                }
            }

            // If check is not passed
            if (!string.IsNullOrEmpty(msg))
            {
                dgvList.Rows[rowIndex].Cells["colMK"].Value = "X";
                dgvList.Rows[rowIndex].Cells["colUPDATE_MESSAGE"].Value = msg;
                return false;
            }

            return true;
        }

        public static bool InputTypecheck(string data1, string data2, out string msg)
        {
            msg = "";
            if (data1 == "数量")
            {
                if (string.IsNullOrEmpty(data2))
                {
                    msg = JimugoMessages.E000WB012;
                    return false;
                }
            }
            return true;
        }

        public static bool LengthCheck(DataGridView dgvList, int rowIndex, List<Validate> items)
        {
            bool pass = true;
            string msg = "";

            //check for require fields
            foreach (Validate item in items)
            {
                if (item.Type != Utility.DataType.MINIMUM)
                {
                    string value = dgvList.Rows[rowIndex].Cells[item.Name].Value == null ? null : dgvList.Rows[rowIndex].Cells[item.Name].Value.ToString().Trim();
                    if (item.Min == 0)
                    {
                        if (item.Require == false && value.Length == 0)
                        {
                            pass = true;
                        }
                        else
                        {
                            pass = DigitRangeCheck(value.Length, item.Min, item.Max, out msg);
                        }

                    }
                    else
                    {
                        if (item.Require == false && value.Length == 0)
                        {
                            pass = true;
                        }
                        else
                        {
                            pass = IsInRange(value.Length, item.Max, out msg);
                        }
                    }

                    if (!pass)
                    {
                        break;
                    }
                }
            }

            // If check is not passed
            if (!pass)
            {
                dgvList.Rows[rowIndex].Cells["colMK"].Value = "X";
                dgvList.Rows[rowIndex].Cells["colUPDATE_MESSAGE"].Value = msg;
            }

            return pass;
        }

        #region DateFormatCheck
        public static bool DateFormatCheck(string value)
        {
            bool pass = true;

            DateTime Date;
            try
            {
                Date = DateTime.ParseExact(value, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None);
            }
            catch (Exception)
            {
                try
                {
                    Date = DateTime.ParseExact(value, "yyyy/M/d", CultureInfo.InvariantCulture, DateTimeStyles.None);
                }
                catch (Exception)
                {
                    pass = false;
                }
            }
            return pass;
        }

        public static bool YearCheck(string value)
        {
            DateTime Date;
            try
            {
                Date = DateTime.ParseExact(value, "yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool YearMonthCheck(string value)
        {

            DateTime Date;
            try
            {
                Date = DateTime.ParseExact(value, "yyyy/MM", CultureInfo.InvariantCulture, DateTimeStyles.None);
                return true;
            }
            catch (Exception)
            {
                try
                {
                    Date = DateTime.ParseExact(value, "yyyy/M", CultureInfo.InvariantCulture, DateTimeStyles.None);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }


        #endregion

        public static bool DateFormatCheck(string value, out string msg)
        {
            bool pass = true;
            msg = "";

            DateTime Date;
            try
            {
                Date = DateTime.ParseExact(value, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None);
            }
            catch (Exception)
            {
                try
                {
                    Date = DateTime.ParseExact(value, "yyyy/M/d", CultureInfo.InvariantCulture, DateTimeStyles.None);
                }
                catch (Exception)
                {
                    pass = false;
                    msg = string.Format(JimugoMessages.E000ZZ009, value);
                }
            }
            return pass;
        }

        public static bool TimeStampCheck(string value, out string msg)
        {
            bool pass = true;
            msg = "";
            if (!string.IsNullOrEmpty(value))
            {
                DateTime Date;
                try
                {
                    Date = DateTime.ParseExact(value, "yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None);
                }
                catch (Exception)
                {
                    pass = false;
                    //data type wrong
                    msg = JimugoMessages.E000ZZ002;
                }
            }
            return pass;
        }

        public static bool DateRationalCheck(DateTime fromDate, DateTime toDate, out string msg)
        {
            msg = "";

            //check if todate is greater than fromdate
            if (fromDate.Date > toDate.Date)
            {
                if (toDate.Date != new DateTime())
                {
                    msg = string.Format(JimugoMessages.E000ZZ045, fromDate.ToString("yyyy/MM/dd"), toDate.ToString("yyyy/MM/dd"));
                    return false;
                }
            }
            if (fromDate == null && toDate != new DateTime()) //check if only to date is inserted 
            {
                msg = "failed";
                return false;
            }

            return true;
        }
        #region RationalDateCheck
        public static bool DateRationalCheck(String fromDate, String toDate)
        {
            DateTime? from = null;
            DateTime? to = null;

            string value1 = fromDate.Length == 4 ? fromDate + "/01/01" : fromDate;
            string value2 = toDate.Length == 4 ? toDate + "/01/01" : toDate;
            try
            {
                from = DateTime.Parse(value1);
            }
            catch (Exception)
            {
            }

            try
            {
                to = DateTime.Parse(value2);
            }
            catch (Exception)
            {

            }

            //check if todate is greater than fromdate
            if (to != null)
            {
                if (from > to)
                {
                    return false;
                }
            }

            if (from == null && to != null) //check if only to date is inserted 
            {
                return false;
            }

            return true;
        }

        #endregion

        public static bool NumberCheck(string value, int Min, int Max, out string msg)
        {
            msg = "";
            try
            {
                int result = int.Parse(value);
                if (result > Max)
                {
                    msg = JimugoMessages.E000ZZ005;
                }

                if (result < Min)
                {
                    msg = JimugoMessages.E000ZZ006;
                }
            }
            catch (Exception)
            {
                msg = JimugoMessages.E000ZZ007;
            }
            if (string.IsNullOrEmpty(msg))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool MinimumCheck(string value, int Min, out string msg)
        {
            msg = "";
            try
            {
                int result = int.Parse(value);

                if (result < Min)
                {
                    msg = string.Format(JimugoMessages.E000ZZ046, value, Min);
                }
            }
            catch (Exception)
            {
                msg = JimugoMessages.E000ZZ007;
            }
            if (string.IsNullOrEmpty(msg))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool DigitRangeCheck(int value, int? min, int max, out string msg)
        {
            msg = "";

            if (min != -1 && max != -1)
            {
                if (value < min)
                {
                    //input is less than minimum
                    msg = JimugoMessages.E000ZZ006;
                    return false;
                }

                if (value > max)
                {
                    //input is greater than maximum
                    msg = JimugoMessages.E000ZZ005;
                    return false;
                }
            }

            return true;
        }

        public static bool IsInRange(int value, int max, out string msg)
        {
            if (value > max)
            {
                //input is greater than maximum
                msg = JimugoMessages.E000ZZ005;
                return false;
            }
            msg = "";
            return true;
        }
        public static bool IsMatch(string value, string pattern)
        {
            if (!string.IsNullOrEmpty(value))
            {
                Regex regex = new Regex(pattern);
                return regex.IsMatch(value);
            }
            return true;
        }

        public static bool EDIaccountCheck(string value, out string msg)
        {
            msg = "";
            if (!IsMatch(value, EDI_ACCOUNT))
            {
                msg = "failed";
                return false;
            }
            return true;
        }

        public static bool EmailCheck(string value, out string msg)
        {
            msg = string.Format(JimugoMessages.E000ZZ053, value);
            if (IsMatch(value, MAIL))
            {
                msg = "";
                return true;
            }
            return false;
        }

        public static bool ASCIICheck(string value, out string msg)
        {
            msg = "";
            if (IsMatch(value, ASCII))
            {
                msg = "failed";
                return true;
            }
            return false;
        }

    }
}
