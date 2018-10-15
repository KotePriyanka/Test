#region Namespaces
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ExlInterop = Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Web;
using DateTime = System.DateTime;


#endregion

namespace Nimble
{
    /// <summary>
    /// Description of ExcelReader.
    /// </summary>
    public static class ExcelReader
    {
        public static DataTable ReadExcelFile(string dataFile, bool isOLEDB)
        {
            DataTable dt = new DataTable();
            //tblExlData holds only data rows of excel sheet
            DataTable tblExcelData = new DataTable();
            string exlFileName = string.Empty;
            try
            {
                string fileName = dataFile + ".xlsx";
               string TestDataFolder = ConfigurationManager.AppSettings["TestDataFolder"];
                string[] drTestdataFiles = Directory.GetFiles(TestDataFolder).
                                          Where(x => x.EndsWith(".xls") || x.EndsWith(".xlsx")).ToArray();

                foreach (string tstDataFile in drTestdataFiles)
                {
                    exlFileName = tstDataFile.Substring(tstDataFile.LastIndexOf("\\") + 1);
                    if (fileName.Equals(exlFileName))
                    {
                        string exlDataSource = Path.Combine(TestDataFolder, exlFileName);
                        if (isOLEDB)
                        {
                            dt = ReadExcelUsingOLEDB(exlDataSource);
                        }
                        else
                        {
                            dt = ReadExcelUsingInterop(exlDataSource);
                        }

                        break;
                    }
                }
                tblExcelData = dt.Clone();
                tblExcelData.TableName = "TestData";
                foreach (DataRow row in dt.Rows)
                {
                    bool isEmptyRow = row.ItemArray.All(x => string.IsNullOrWhiteSpace(x.ToString()));
                    if (!isEmptyRow)
                    {
                        tblExcelData.Rows.Add(row.ItemArray);
                        tblExcelData.AcceptChanges();
                    }
                }
            }
            catch (Exception ex)
            {
               // Report.Error(ex.Message.ToString() + "Stack Trace:" + ex.StackTrace.ToString());
                throw ex;
            }
            return tblExcelData;
        }
        
        public static DataTable ReadExcelUsingOLEDB(string exlDataSource)
        {
        	OleDbConnection ocon; OleDbDataAdapter oda;
            DataTable tblExcelSchema;
            string sheetName = string.Empty;
            //dt holds both the empty and data rows of excel sheet
            DataTable dt = new DataTable();
                
        	try
        	{
        		string ExcelConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + exlDataSource + ";Extended Properties='Excel 12.0 Xml;HDR=Yes'";
                ocon = new OleDbConnection(ExcelConn);
            	ocon.Open();
            	tblExcelSchema = ocon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            	sheetName = tblExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            	oda = new OleDbDataAdapter("select * from [" + sheetName + "]", ocon);
            	dt.TableName = "TestData";
            	oda.Fill(dt);
            	ocon.Close();
        	}
        	 catch (Exception ex)
            {
               // Report.Error(ex.Message.ToString() + "Stack Trace:" + ex.StackTrace.ToString());
                throw ex;
                
            }
        	 return dt;
        }

        public static DataTable ReadExcelUsingInterop(string exlDataSource)
        {
            ExlInterop.Application exlApp = null;
            ExlInterop.Workbook exlWb = null;
            DataTable dt = null;
            try
            {
                exlApp = new ExlInterop.Application();
                ExlInterop.Worksheet exlSheet;

                int rCnt = 0; int cCnt = 0;
                object cellValue = null;
                string colValue = string.Empty;

                //dt holds both the empty and data rows of excel sheet
                dt = new DataTable();

                exlWb = exlApp.Workbooks.Open(exlDataSource);
                exlSheet = (ExlInterop.Worksheet)exlWb.Sheets[1];
                exlWb.RefreshAll();
                ExlInterop.Range range = exlSheet.UsedRange;
                //                ExlInterop.Range range = exlSheet.UsedRange.SpecialCells(
                //                               ExlInterop.XlCellType.xlCellTypeVisible, 
                //                               Type.Missing);
                for (rCnt = 1; rCnt <= range.Rows.Count; rCnt++)
                {
                    DataRow drow = dt.NewRow();
                    for (cCnt = 1; cCnt <= range.Columns.Count; cCnt++)
                    {
                        cellValue = (range.Cells[rCnt, cCnt] as Excel.Range).Value2;
                        colValue = cellValue == null ? string.Empty : Convert.ToString(cellValue);
                        //Adding Header Row to DataTable
                        if (rCnt == 1)
                        {
                            dt.Columns.Add(colValue);
                        }
                        else
                        {
                            drow[cCnt - 1] = colValue;
                        }
                    }
                    if (rCnt > 1)
                    {
                        dt.Rows.Add(drow);
                        dt.AcceptChanges();
                    }
                }

            }
            catch (Exception ex)
            {
                //Report.Error(ex.Message.ToString() + "Stack Trace:" + ex.StackTrace.ToString());
                throw ex;

            }
            finally
            {
                exlWb.Close();
                exlApp.Quit();
                releaseProcessObject(exlWb);
                releaseProcessObject(exlApp);
            }
            return dt;
        }


        private static void releaseProcessObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
               // Report.Error(ex.Message.ToString() + "Stack Trace:" + ex.StackTrace.ToString());
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        } 
    }
}
