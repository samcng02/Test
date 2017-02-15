using System;
using System.Collections.Generic;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Data;
using System.IO;
using System.Collections;
using System.Net.Mail;
using FileSrvLib;

namespace Test001.TransferExcel
{
    class TranferExcel
    {
        string tmp_folder = "WebApply\\SMS", ShareUser, SharePwd;
        public string ShareFolder;
        int MaxRows = 65534;

        public TranferExcel(string JOBName, string tmp_folder, string ToMail)
        {
            //get info drive map
            //Dictionary<String, object> driveInfo = ObjectFactory.Instance.Get<Setting>().Get<Dictionary<String, object>>("TranferSMSReport");
            //ShareFolder = driveInfo["FolderPath"].ToString();
            //ShareUser = driveInfo["UserName"].ToString();
            //SharePwd = driveInfo["Password"].ToString();
        }

        public void CreateSheet(HSSFWorkbook book, string sheetName, ArrayList arrHeader, DataRowCollection drData)
        {
            ISheet sheet;
            IRow row;
            if (drData == null)
            {
                sheet = book.CreateSheet(sheetName);
                row = sheet.CreateRow(0);
                row.CreateCell(0).SetCellValue("No Data");
                return;
            }

            if (arrHeader.Count - 1 != drData[0].Table.Columns.Count)
            {
            }

            int MaxRowsConfig = MaxRows;
            int MaxRowsData = drData.Count;
            int indexSheet = 0;

            for (int i = 0; i < MaxRowsData; i += MaxRowsConfig)  //So lan tao sheet
            {
                sheet = i == 0 ? book.CreateSheet(sheetName) : book.CreateSheet(sheetName + "_" + indexSheet);
                indexSheet++;
                //Truong hop sheet cuoi hoac 1 sheet
                if ((MaxRowsData - i) < MaxRowsConfig)
                {
                    CreateChildSheet(sheet, arrHeader, drData, i, MaxRowsData);
                }
                else //Truong hop con sheet tiep theo
                {
                    CreateChildSheet(sheet, arrHeader, drData, i, MaxRowsConfig * indexSheet);
                }
            }
        }

        public void CreateChildSheet(ISheet sheet, System.Collections.ArrayList arrHeader, DataRowCollection drData, int StartRow, int EndRow)
        {
            int rowIndex = 0;

            IRow rowHeader = sheet.CreateRow(0);
            for (int i = 0; i < arrHeader.Count; i++)
            {
                rowHeader.CreateCell(i).SetCellValue(arrHeader[i].ToString());
            }
            rowIndex++;
            IRow row;

            if (drData != null)
            {
                int cellIndex;
                for (int i = StartRow; i < EndRow; i++)
                {
                    row = sheet.CreateRow(rowIndex++);
                    cellIndex = 0;

                    row.CreateCell(cellIndex++).SetCellValue(rowIndex - 1);

                    foreach (DataColumn col in drData[i].Table.Columns)
                        row.CreateCell(cellIndex++).SetCellValue(drData[i][col].ToString());
                }
            }
            else
            {
                row = sheet.CreateRow(0);
                row.CreateCell(0).SetCellValue("No Data");
            }
        }

        public bool ExportExcel(HSSFWorkbook book, string strExcelFile)
        {
            //using (new Impersonation(ShareFolder, ShareUser, SharePwd))
            {
                try
                {
                    //Export excel file to disk
                    ExportExcelToDisk(book, strExcelFile);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public void ExportExcelToDisk(HSSFWorkbook book, string strExcelFile)
        {
            try
            {
                Dictionary<string, object> excelDic = new Dictionary<string, object>();
                using (MemoryStream ms = new MemoryStream())
                {
                    book.Write(ms);
                    excelDic["Bytes"] = ms.ToArray();
                    excelDic["FileName"] = strExcelFile;

                    ms.Dispose();
                    ms.Flush();
                    ms.Close();
                }

                byte[] bytes = excelDic["Bytes"] as byte[];
                ByteArrayToFile(excelDic["FileName"].ToString(), bytes);
            }
            catch (Exception ex)
            {
            }
        }

        public bool ByteArrayToFile(string _FileName, byte[] _ByteArray)
        {
            string PathFile = tmp_folder + "\\" + _FileName;

            // Open file for reading
            FileStream _FileStream =
               new FileStream(PathFile, FileMode.Create, FileAccess.Write);
            // Writes a block of bytes to this stream using data from
            // a byte array.
            _FileStream.Write(_ByteArray, 0, _ByteArray.Length);

            _FileStream.Flush();
            _FileStream.Dispose();
            _FileStream.Close();

            #region transfer file excel to dest disk
            //using (new Impersonation(ShareFolder, ShareUser, SharePwd))
            //{
            //    FileInfo TMP_File = new FileInfo(PathFile);
            //    TMP_File.CopyTo(ShareFolder + "\\" + _FileName, true);
            //    //Delete temp file   
            //    TMP_File.Delete();
            //}
            #endregion
            return true;
        }
    }
}
