using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Configuration;
using System.Reflection;
using ExlInterop = Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;

namespace  Nimble.Automation.Accelerators
{
    public class CSVDataManipulator
    {
        /// <summary>
        /// Loads specified CSV content to DataTable
        /// </summary>
        /// <param name="filename">Filename of CSV</param>
        /// <returns>DataTable</returns>
        public static DataTable ReadCSVContent(String filename)
        {
            DataTable table = new DataTable();

            string[] lines = File.ReadAllLines(filename);

            // identify columns
            foreach (String columnName in lines[0].Split(new char[] { ',' }))
                table.Columns.Add(columnName, typeof(String));

            foreach (String data in lines.Where((val, index) => index != 0))
            {
                table.Rows.Add(data.Split(new Char[] { ',' }));
            }

            return table;
        }

        public static DataTable ReadExcelFile(string dataFile)
        {
            DataTable dt = new DataTable();
            //tblExlData holds only data rows of excel sheet
            DataTable tblExcelData = new DataTable();
            string exlFileName = string.Empty;
            try
            {
                string fileName = dataFile;// + ".xlsx";
                                           //string sheetname = fileName.Replace(".xlsx", "");
                string sheetname = fileName.Substring(fileName.LastIndexOf("\\") + 1).Replace(".xlsx", "");

                //string TestDataFolder = ConfigurationManager.AppSettings["TestDataFolder"];
                string TestDataFolder = TestUtility.GetAssemblyDirectory();
                string[] drTestdataFiles = Directory.GetFiles(TestDataFolder).
                                          Where(x => x.EndsWith(".xls") || x.EndsWith(".xlsx")).ToArray();

                foreach (string tstDataFile in drTestdataFiles)
                {
                    exlFileName = tstDataFile.Substring(tstDataFile.LastIndexOf("\\") + 1);
                    if (fileName.Contains(exlFileName))
                    {
                        string exlDataSource = Path.Combine(TestDataFolder, exlFileName);
                        dt = ReadExcelUsingInterop(exlDataSource, sheetname);
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

        public static DataTable ReadExcelFile(string filepath, string fileName)
        {
            DataTable dt = new DataTable();
            //tblExlData holds only data rows of excel sheet
            DataTable tblExcelData = new DataTable();
            //string exlFileName = string.Empty;
            try
            {
                //string TestDataFolder = filepath;

                //string exlDataSource = Path.Combine(TestDataFolder, fileName + ".xlsx");
                Console.WriteLine("Sunil- Path" + filepath);
                string exlDataSource = filepath + "//" + fileName + ".xlsx";
                Console.WriteLine("Sunil- Path2" + exlDataSource);
                dt = ReadExcelUsingInterop(exlDataSource, fileName);
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


        public static DataTable ReadExcelUsingInterop(string exlDataSource, string sheetname)
        {
            ExlInterop.Application exlApp = new ExlInterop.Application();
            ExlInterop.Workbook exlWb = null;
            ExlInterop.Worksheet exlSheet;
            Console.WriteLine("Sunil- Path3 " + exlDataSource);
            int rCnt = 0; int cCnt = 0;
            object cellValue = null;
            string colValue = string.Empty;

            //dt holds both the empty and data rows of excel sheet
            DataTable dt = new DataTable();

            try
            {
                exlWb = exlApp.Workbooks.Open(exlDataSource);

                int numSheets = exlWb.Sheets.Count;
                for (int sheetNum = 1; sheetNum < numSheets + 1; sheetNum++)
                {
                    exlSheet = (ExlInterop.Worksheet)exlWb.Sheets[sheetNum];
                    string strWorksheetName = exlSheet.Name;

                    exlWb.RefreshAll();
                    if (strWorksheetName.Equals(sheetname))
                    {
                        ExlInterop.Range range = exlSheet.UsedRange;
                        //                ExlInterop.Range range = exlSheet.UsedRange.SpecialCells(
                        //                               ExlInterop.XlCellType.xlCellTypeVisible, 
                        //                               Type.Missing);
                        for (rCnt = 1; rCnt <= range.Rows.Count; rCnt++)
                        {
                            DataRow drow = dt.NewRow();
                            for (cCnt = 1; cCnt <= range.Columns.Count; cCnt++)
                            {
                                cellValue = (range.Cells[rCnt, cCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
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

                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
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

        public static void UpdateSpecificColumnInCSV(string filePath, string columnName, int rowNumber, string valueToBeUpdated)
        {


            int internalCounter = 0;
            string[] lines = File.ReadAllLines(filePath);
            string[] arrLine = lines[rowNumber - 1].Split(',');
            string[] arrHeader = lines[0].Split(',');
            foreach (string arrValue in arrHeader)
            {
                internalCounter = internalCounter + 1;
                if (arrValue == columnName)
                {
                    arrLine[internalCounter - 1] = valueToBeUpdated;
                    lines[rowNumber - 1] = string.Join(",", arrLine);

                }





            }
            File.WriteAllLines(filePath, lines);
        }
        /// <summary>
        /// Returns an Array of ArrayList (String type) with all the CSV data of
        /// proper size of m x n. Each columns is represented in an ArrayList with
        /// first column represented by ArrayList[0], second one with ArrayList[1],
        /// and so on...
        /// </summary>
        /// <param name="filePath">
        ///            : FileName along with its path to read data from. </param>
        /// <returns> ArrayList<String>[] inputData. </returns>
        /// <exception cref="IOException"> </exception>
        public virtual List<string>[] getAllRecordsAsArrayList(string filePath)
        {

            System.IO.StreamReader br = null;
            List<string>[] inputData = null;
            string row;
            string[] cellValues;
            int n = 0;

            try
            {
                br = new System.IO.StreamReader(filePath);
                row = br.ReadLine();
                if (row != null)
                {
                    cellValues = row.Split(",", true); // cellValues holds the all fields
                    // of first row which is used to
                    // represent the number of
                    // columns
                    n = cellValues.Length; // Number of Columns.
                    Console.WriteLine(n);
                    inputData = new List<string>[n]; // we are initializing the Array
                    // (of ArrayList) with size n.
                    for (int i = 0; i < n; i++)
                    {
                        //inputData[i] = new List<string>();
                        inputData[i] = new List<string>();
                    }
                }

                while (row != null)
                {
                    if (row.Contains(","))
                    {
                        cellValues = row.Split(",", true);
                        if (cellValues.Length == n || cellValues.Length > n)
                        {
                            // If the number of fields in the row equals or greater
                            // than num of columns. If ignore the extra fields.
                            for (int i = 0; i < n; i++)
                            {
                                if (cellValues[i].Length > 0) // If any cell value
                                {
                                    // is empty
                                    inputData[i].Add(cellValues[i]); // Sending the
                                }
                                // each cell
                                // value of
                                // a given
                                // row to
                                // all
                                // ArrayLists.
                                else
                                {
                                    inputData[i].Add(""); // If any cell value in
                                }
                                // the middle of a given
                                // row is empty then
                                // fill with empty
                                // string.
                            }
                        }
                        else
                        { // If last cell value is missing in the given row,
                            // then we fill it with empty string.
                            for (int i = 0; i < n - 1; i++)
                            {
                                if (cellValues[i].Length > 0)
                                {
                                    inputData[i].Add(cellValues[i]); // Sending the
                                }
                                // each cell
                                // value of
                                // a given
                                // row to
                                // all
                                // ArrayLists.
                                else
                                {
                                    inputData[i].Add(""); // If any cell value in
                                }
                                // the middle of a given
                                // row is empty then
                                // fill with empty
                                // string.
                            }
                            inputData[n - 1].Add("");
                        }
                    }
                    else if (row.Length == 0)
                    {
                        for (int i = 0; i < n; i++)
                        {
                            inputData[i].Add(""); // If entire row is empty (any
                        }
                        // line breaks) then fill the
                        // cells with empty string
                    }
                    else
                    {
                        for (int i = 0; i < n; i++)
                        {
                            if (i == 0)
                            {
                                inputData[i].Add(row); // If any row lacks commas
                            }
                            // then send the entire row
                            // to first field.
                            else
                            {
                                inputData[i].Add(""); // And fill remaining fields
                            }
                        }
                        // with empty strings.
                    }

                    row = br.ReadLine();
                }

                return inputData;
            }
            catch (FieldAccessException e)
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
                return inputData;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
                return inputData;
            }
            finally
            {
                br.Close();
            }
        }

        public virtual string[][] getAllRecordsAs2DArray(string filePath)
        {
            return getAllRecordsAs2DArray(filePath, true);
        }

        public virtual string[][] getAllRecordsAs2DArray(string filePath, bool ignoreHeaders)
        {

            string[][] data = null;
            try
            {
                List<string>[] inputData = (new CSVDataManipulator()).getAllRecordsAsArrayList(filePath);
                int cols = inputData.Length;
                int rows = inputData[0].Count;
                data = RectangularArrays.ReturnRectangularStringArray(rows, cols);
                int i;
                if (ignoreHeaders)
                {
                    i = 1;
                }
                else
                {
                    i = 0;
                }
                for (; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        data[i][j] = inputData[j][i];
                        Console.Write(data[i][j] + "|");
                    }
                    Console.WriteLine();
                }
                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
                return data;
            }
        }

        /// <summary>
        /// Reads the line at specified line Number and returns an array with cell
        /// values.
        /// </summary>
        /// <param name="lineNumber">
        ///            : An integer representing the line number. </param>
        /// <param name="filePath">
        ///            : : FileName along with its path to read data from.
        /// @return </param>
        /// <exception cref="IOException"> </exception>
        public virtual string[] readSpecifiedLine(int lineNumber, string filePath)
        {
            System.IO.StreamReader br = null;
            string row;
            string[] cellValues = null;
            int i = 0;
            try
            {
                br = new System.IO.StreamReader(filePath);
                row = br.ReadLine();

                while (row != null)
                {
                    i++;
                    if (i == lineNumber)
                    {
                        if (row.Contains(","))
                        {
                            cellValues = row.Split(",", true); // cellValues holds the all
                            // fields of first row
                            // which is used to
                            // represent the number
                            // of columns
                        }
                        else if (row.Length == 0)
                        {
                            cellValues = new string[] { "" };
                        }
                        else
                        {
                            cellValues = new string[] { row };
                        }
                        break;
                    }
                    row = br.ReadLine();
                }
                if (lineNumber > i)
                {
                    Console.WriteLine("No Such line number exists");
                    cellValues = new string[] { "" };
                }
                return cellValues;
            }
            catch (ApplicationException e)
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
                return cellValues;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
                return cellValues;
            }
            finally
            {
                br.Close();
            }
        }

        internal static partial class RectangularArrays
        {
            internal static string[][] ReturnRectangularStringArray(int size1, int size2)
            {
                string[][] newArray;
                if (size1 > -1)
                {
                    newArray = new string[size1][];
                    if (size2 > -1)
                    {
                        for (int array1 = 0; array1 < size1; array1++)
                        {
                            newArray[array1] = new string[size2];
                        }
                    }
                }
                else
                    newArray = null;
                return newArray;
            }
        }

        public virtual bool writeData(List<string>[] data, string filePath)
        {

            System.IO.StreamWriter bw = null;
            try
            {
                bw = new System.IO.StreamWriter(filePath, true);

                for (int rowNum = 0; rowNum < data[0].Count; rowNum++)
                {
                    string row = "";
                    for (int j = 0; j < data.Length; j++)
                    {
                        row = row + data[j][rowNum]; // Concatenating all fields
                        // of a record to a row
                        if (j != data.Length - 1) // Dont concatenate the symbol ','
                        {
                            // (comma) to the last element.
                            row = row + ",";
                        }
                    }

                    byte[] bytes = System.Text.Encoding.UTF8.GetBytes(row);
                    foreach (var b in bytes)
                    {
                        bw.BaseStream.WriteByte(b);
                    }
                    //bw.BaseStream.WriteByte(Convert.ToByte(row));
                    //bw.BaseStream.Write(bytes, 0, bytes.Length);

                    if (rowNum != data[0].Count - 1)
                    { // Go to next line if its
                        // not the last row.
                        //string s = bw.NewLine;
                        //bw.Write(Environment.NewLine);
                        bw.WriteLine("\n");

                    }
                }
                return true;
            }

            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.Write(ex.InnerException);
                return false;
            }
            finally
            {
                bw.Close();
            }
        }

        /// <summary>
        /// Appends the data to a csv file with data passed by an Array of ArrayList
        /// parameter .
        /// </summary>
        /// <param name="data">
        ///            : An Array of ArrayList parameter representing each column in
        ///            a ArrayList. </param>
        /// <param name="filePath">
        ///            : FileName along with its path to write data to.
        /// @return </param>
        /// <exception cref="IOException"> </exception>
        public virtual bool appendData(List<string>[] data, string filePath)
        {
            System.IO.StreamWriter bw = null;
            try
            {
                bw = new System.IO.StreamWriter(filePath, true);
                for (int rowNum = 0; rowNum < data[0].Count; rowNum++)
                {
                    string row = "";
                    for (int j = 0; j < data.Length; j++)
                    {
                        row = row + data[j][rowNum]; // Concatenating all fields
                        // of a record to a row
                        if (j != data.Length - 1) // Dont concatenate the symbol ','
                        {
                            // (comma) to the last element.
                            row = row + ",";
                        }
                    }
                    if (rowNum != data[0].Count - 1)
                    { // Go to next line if its
                        // not the last row.
                        string s = bw.NewLine; // For appending, A line break should come
                        // first.
                    }
                    bw.BaseStream.WriteByte(Convert.ToByte(row));
                }
                return true;
            }
            catch (SystemException e)
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
                return false;
            }
            finally
            {
                bw.Close();
            }
        }

    }
    public static class StringHelperClass
    {
        public static string SubstringSpecial(this string self, int start, int end)
        {
            return self.Substring(start, end - start);
        }

        public static bool StartsWith(this string self, string prefix, int toffset)
        {
            return self.IndexOf(prefix, toffset, System.StringComparison.Ordinal) == toffset;
        }

        public static string[] Split(this string self, string regexDelimiter, bool trimTrailingEmptyStrings)
        {
            string[] splitArray = System.Text.RegularExpressions.Regex.Split(self, regexDelimiter);

            if (trimTrailingEmptyStrings)
            {
                if (splitArray.Length > 1)
                {
                    for (int i = splitArray.Length; i > 0; i--)
                    {
                        if (splitArray[i - 1].Length > 0)
                        {
                            if (i < splitArray.Length)
                                System.Array.Resize(ref splitArray, i);

                            break;
                        }
                    }
                }
            }

            return splitArray;
        }

        //public static string NewString(sbyte[] bytes)
        //{
        //    return NewString(bytes, 0, bytes.Length);
        //}
        //internal static string NewString(sbyte[] bytes, int index, int count)
        //{
        //    return System.Text.Encoding.UTF8.GetString((byte[])(object)bytes, index, count);
        //}
        //internal static string NewString(sbyte[] bytes, string encoding)
        //{
        //    return NewString(bytes, 0, bytes.Length, encoding);
        //}
        public static string NewString(sbyte[] bytes, int index, int count, string encoding)
        {
            return System.Text.Encoding.GetEncoding(encoding).GetString((byte[])(object)bytes, index, count);
        }

        public static sbyte[] GetBytes(this string self)
        {
            return GetSBytesForEncoding(System.Text.Encoding.UTF8, self);
        }
        //internal static sbyte[] GetBytes(this string self, string encoding)
        //{
        //    return GetSBytesForEncoding(System.Text.Encoding.GetEncoding(encoding), self);
        //}
        public static sbyte[] GetSBytesForEncoding(System.Text.Encoding encoding, string s)
        {
            sbyte[] sbytes = new sbyte[encoding.GetByteCount(s)];
            encoding.GetBytes(s, 0, s.Length, (byte[])(object)sbytes, 0);
            return sbytes;
        }



    }


}
