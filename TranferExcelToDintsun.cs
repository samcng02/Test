using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using NPOI.HSSF.UserModel;
using System.Collections;

namespace Test001.TransferExcel
{
    class TranferExcelToDintsun
    {
        string tmp_folder = "WebApply\\SMS\\";

        public string Run(DataRowCollection dataRows, ArrayList arrHeader)
        {
            TranferExcel clsExcel = new TranferExcel("", tmp_folder, "");
            HSSFWorkbook book = new HSSFWorkbook();
            string strExcelFile = DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";
            string SheetName = "ABC";

            try
            {
                clsExcel.CreateSheet(book, SheetName, arrHeader, dataRows);
                //Export excel 
                clsExcel.ExportExcel(book, strExcelFile);
            }
            catch (Exception ex)
            {
            }
            //get fileName for write SaveLog
            string fileName = clsExcel.ShareFolder + "\\" + strExcelFile;
            return fileName;
        }
    }
}
