using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Text;
using NPOI;
using NPOI.HPSF;
using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.POIFS;
using NPOI.Util; 
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Collections.Generic;
using NPOI.XSSF.UserModel;
namespace Common
{
    public class NPOIHelper
    {
        /// <summary>
        /// DataTable导出到Excel文件
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="strFileName">保存位置</param>
        public static void Export(DataTable dtSource, string strHeaderText, string strFileName,string password=null)
        {
            using (MemoryStream ms = Export(dtSource, strHeaderText, password))
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }

        /// <summary>
        /// List<DataTable> 导出到Excel文件
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="strFileName">保存位置</param>
        public static void ExportList(List<DataTable> dtSource, string strHeaderText, string strFileName, string password = null)
        {
            using (MemoryStream ms = ExportXlsx(dtSource, strHeaderText, password))
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }

        /// <summary>
        /// DataTable导出到Excel的MemoryStream
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        public static MemoryStream Export(DataTable dtSource, string strHeaderText, string passaord=null)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet();
            if (string.IsNullOrEmpty(passaord) == false)
            {
                sheet.ProtectSheet(passaord);
            }

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "NPOI";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "文件作者信息"; //填加xls文件作者信息
                si.ApplicationName = "创建程序信息"; //填加xls文件创建程序信息
                si.LastAuthor = "最后保存者信息"; //填加xls文件最后保存者信息
                si.Comments = "作者信息"; //填加xls文件作者信息
                si.Title = "标题信息"; //填加xls文件标题信息
                si.Subject = "主题信息";//填加文件主题信息
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            NPOI.SS.UserModel.ICellStyle dateStyle = workbook.CreateCellStyle();
            NPOI.SS.UserModel.IDataFormat format = workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            //取得列宽
            int[] arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns)
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }
            int rowIndex = 0;
            foreach (DataRow row in dtSource.Rows)
            {
                #region 新建表，填充表头，填充列头，样式
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = workbook.CreateSheet();
                        if (string.IsNullOrEmpty(passaord) == false)
                        {
                            sheet.ProtectSheet(passaord);
                        }
                    }

                    #region 表头及样式
                    {
                        if (string.IsNullOrEmpty(strHeaderText) == false)
                        {
                            NPOI.SS.UserModel.IRow headerRow = sheet.CreateRow(0);
                            headerRow.HeightInPoints = 25;
                            headerRow.CreateCell(0).SetCellValue(strHeaderText);

                            NPOI.SS.UserModel.ICellStyle headStyle = workbook.CreateCellStyle();
                            headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                            NPOI.SS.UserModel.IFont font = workbook.CreateFont();
                            font.FontHeightInPoints = 20;
                            font.Boldweight = 700;
                            headStyle.SetFont(font);
                            headerRow.GetCell(0).CellStyle = headStyle;
                            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, dtSource.Columns.Count - 1));
                            rowIndex += 1;
                        }
                        //headerRow.Dispose();
                    }
                    #endregion


                    #region 列头及样式
                    {
                        NPOI.SS.UserModel.IRow headerRow = sheet.CreateRow(rowIndex);
                        NPOI.SS.UserModel.ICellStyle headStyle = workbook.CreateCellStyle();
                        headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                        NPOI.SS.UserModel.IFont font = workbook.CreateFont();
                        font.FontHeightInPoints = 10;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);
                        foreach (DataColumn column in dtSource.Columns)
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                            headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                            //设置列宽
                            sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);
                        }
                        //headerRow.Dispose();
                        rowIndex += 1;
                    }
                    #endregion

                }
                #endregion


                #region 填充内容
                NPOI.SS.UserModel.IRow dataRow = sheet.CreateRow(rowIndex);
                foreach (DataColumn column in dtSource.Columns)
                {
                    NPOI.SS.UserModel.ICell newCell = dataRow.CreateCell(column.Ordinal);

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            newCell.CellStyle = dateStyle;//格式化显示
                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }

                }
                #endregion

                rowIndex++;
            }
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;

                // sheet.Dispose();
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        }
        /// <summary>
        /// DataTable导出到Excel的MemoryStream xlsx
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="strFileName">文件名</param>
        public static MemoryStream ExportXlsx(IEnumerable<DataTable> dataTables, string strHeaderText,string passaord=null)
        {
            XSSFWorkbook workbook = new XSSFWorkbook();
            int i = 0;

            NPOI.SS.UserModel.ICellStyle dateStyle = workbook.CreateCellStyle();
            NPOI.SS.UserModel.IDataFormat format = workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");
            
            #region 右击文件 属性信息
            {
            //    DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            //    dsi.Company = "NPOI";

            //    SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            //    si.Author = "文件作者信息"; //填加xlsx文件作者信息
            //    si.ApplicationName = "创建程序信息"; //填加xlsx文件创建程序信息
            //    si.LastAuthor = "最后保存者信息"; //填加xls文件最后保存者信息
            //    si.Comments = "作者信息"; //填加xls文件作者信息
            //    si.Title = "标题信息"; //填加xls文件标题信息
            //    si.Subject = "主题信息";//填加文件主题信息
            //    si.CreateDateTime = DateTime.Now;
            //    workbook.GetProperties().CustomProperties.AddProperty("Company", "NPOI");
            //    if (!workbook.GetProperties().CustomProperties.Contains("Company"))
            //        workbook.GetProperties().CustomProperties.AddProperty("Company", dsi.Company);

            }
            #endregion

            foreach (DataTable dt in dataTables)
            {
                string sheetName = string.IsNullOrEmpty(dt.TableName)
                    ? "Sheet " + (++i).ToString()
                    : dt.TableName;
                ISheet sheet = workbook.CreateSheet(sheetName);
                if (string.IsNullOrEmpty(passaord)==false)
                {
                    sheet.ProtectSheet(passaord);
                }

                int rowIndex = 0;
                #region 表头及样式
                {
                    if (string.IsNullOrEmpty(strHeaderText) == false)
                    {
                        NPOI.SS.UserModel.IRow headerRow = sheet.CreateRow(rowIndex);
                        headerRow.HeightInPoints = 25;
                        headerRow.CreateCell(0).SetCellValue(strHeaderText);

                        NPOI.SS.UserModel.ICellStyle headStyle = workbook.CreateCellStyle();
                        headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                        NPOI.SS.UserModel.IFont font = workbook.CreateFont();
                        font.FontHeightInPoints = 20;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);
                        headerRow.GetCell(0).CellStyle = headStyle;
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, dt.Columns.Count - 1));
                        rowIndex += 1;
                    }
                    //headerRow.Dispose();
                }
                #endregion


                #region 列头及样式
                {

                    //取得列宽
                    int[] arrColWidth = new int[dt.Columns.Count];
                    foreach (DataColumn item in dt.Columns)
                    {
                        arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
                    }
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            int intTemp = Encoding.GetEncoding(936).GetBytes(dt.Rows[k][j].ToString()).Length;
                            if (intTemp > arrColWidth[j])
                            {
                                arrColWidth[j] = intTemp;
                            }
                        }
                    }

                    NPOI.SS.UserModel.IRow headerRow = sheet.CreateRow(rowIndex);
                    NPOI.SS.UserModel.ICellStyle headStyle = workbook.CreateCellStyle();
                    headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                    NPOI.SS.UserModel.IFont font = workbook.CreateFont();
                    font.FontHeightInPoints = 10;
                    font.Boldweight = 700;
                    headStyle.SetFont(font);
                    foreach (DataColumn column in dt.Columns)
                    {
                        headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                        headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                        //设置列宽
                        sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);
                    }
                    //headerRow.Dispose();
                    rowIndex += 1;
                }
                #endregion

                //#region 表头
                //for (int j = 0; j < dt.Columns.Count; j++)
                //{
                //    string columnName = string.IsNullOrEmpty(dt.Columns[j].ColumnName)
                //        ? "Column " + j.ToString()
                //        : dt.Columns[j].ColumnName;
                //    headerRow.CreateCell(j).SetCellValue(columnName);
                //}
                //#endregion
                #region 内容
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    DataRow dr = dt.Rows[a];
                    IRow row = sheet.CreateRow(a + rowIndex);
                    for (int b = 0; b < dt.Columns.Count; b++)
                    {
                        row.CreateCell(b).SetCellValue(dr[b] != DBNull.Value ? dr[b].ToString() : string.Empty);
                        DataColumn dc = dt.Columns[b];
                        NPOI.SS.UserModel.ICell newCell = row.CreateCell(dc.Ordinal);

                        string drValue = dr[b].ToString();

                        switch (dc.DataType.ToString())
                        {
                            case "System.String"://字符串类型
                                newCell.SetCellValue(drValue);
                                break;
                            case "System.DateTime"://日期类型
                                DateTime dateV;
                                DateTime.TryParse(drValue, out dateV);
                                newCell.SetCellValue(dateV);
                                newCell.CellStyle = dateStyle;//格式化显示
                                break;
                            case "System.Boolean"://布尔型
                                bool boolV = false;
                                bool.TryParse(drValue, out boolV);
                                newCell.SetCellValue(boolV);
                                break;
                            case "System.Int16"://整型
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                int intV = 0;
                                int.TryParse(drValue, out intV);
                                newCell.SetCellValue(intV);
                                break;
                            case "System.Decimal"://浮点型
                            case "System.Double":
                                double doubV = 0;
                                double.TryParse(drValue, out doubV);
                                newCell.SetCellValue(doubV);
                                break;
                            case "System.DBNull"://空值处理
                                newCell.SetCellValue("");
                                break;
                            default:
                                newCell.SetCellValue("");
                                break;
                        }
                    }
                }
                #endregion
            }

            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                // ms.Position = 0;

                // sheet.Dispose();
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        } 
        /// <summary>

        /// 用于Web导出
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="strFileName">文件名</param>
        public static void ExportByWeb(DataTable dt, string strHeaderText, string strFileName, string passaord = null)
        {
            List<DataTable> dts = new List<DataTable>();
            dts.Add(dt);
            ExportByWeb(dts, strHeaderText, strFileName, passaord);
        }
        /// <summary>
        /// 用于Web导出
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="strFileName">文件名</param>
        public static void ExportByWeb(List<DataTable> dts, string strHeaderText, string strFileName, string passaord = null)
        {
            HttpContext curContext = HttpContext.Current;

            // 设置编码和附件格式
            curContext.Response.ContentType = "application/vnd.ms-excel";
            //curContext.Response.Clear();
            //  curContext.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode(strFileName, Encoding.UTF8));

            curContext.Response.BinaryWrite(TableToExcel(dts[0], strFileName, passaord)); 

            //if (strFileName.ToLower().IndexOf(".xlsx") != -1)
            //{
            //    //curContext.Response.BinaryWrite(ExportXlsx(dts, strHeaderText).GetBuffer()); 
            //}
            //else
            //{
            //    curContext.Response.BinaryWrite(Export(dts[0], strHeaderText).GetBuffer());
            //}
            curContext.Response.Flush();
            curContext.Response.End();
        }

        /// <summary>读取excel
        /// 默认第一行为标头
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <returns></returns>
        public static DataTable Import(string strFileName)
        {
            DataTable dt = new DataTable();

            // HSSFWorkbook hssfworkbook;

            IWorkbook hssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                string fileExt = Path.GetExtension(strFileName);
                if (fileExt == ".xls")
                {
                    hssfworkbook = new HSSFWorkbook(file);
                }
                else if (fileExt == ".xlsx")
                {
                    hssfworkbook = new XSSFWorkbook(file);
                }
                else
                {
                    return new DataTable();
                }
            }

            NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            NPOI.SS.UserModel.IRow headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            for (int j = 0; j < cellCount; j++)
            {
                NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
                dt.Columns.Add(cell.ToString());
            }

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                NPOI.SS.UserModel.IRow row = sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                dt.Rows.Add(dataRow);
            }
            return dt;
        }


        /// <summary>
        /// Datable导出成Excel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="file"></param>
        public static byte[] TableToExcel(DataTable dt, string file, string passaord = null)
        {
            IWorkbook workbook;
            string fileExt = Path.GetExtension(file).ToLower();
            if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(); } else if (fileExt == ".xls") { workbook = new HSSFWorkbook(); } else { workbook = null; }
            if (workbook == null) { return null; }
            ISheet sheet = string.IsNullOrEmpty(dt.TableName) ? workbook.CreateSheet("Sheet1") : workbook.CreateSheet(dt.TableName);
            //if (string.IsNullOrEmpty(passaord) == false)
            //{
                sheet.ProtectSheet(passaord);
            //}

            //表头  
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].ColumnName);
            }

            //数据  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row1.CreateCell(j);
                    cell.SetCellValue(dt.Rows[i][j].ToString());
                }
            }

            //using (NpoiMemoryStream ms = new NpoiMemoryStream())
            //{ 
            //    ms.AllowClose = false;
            //    workbook.Write(ms);
            //    ms.Flush();
            //    ms.Seek(0, SeekOrigin.Begin);
            //    ms.AllowClose = true;
            //    return ms;
            //}

            //转为字节数组  
            MemoryStream stream = new MemoryStream();
            workbook.Write(stream);
            var buf = stream.ToArray();
            return buf;

            //保存为Excel文件  
            //using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            //{
            //    fs.Write(buf, 0, buf.Length);
            //    fs.Flush();
            //}
        }


    }
    public class NpoiMemoryStream : MemoryStream
    {
        public NpoiMemoryStream()
        {
            AllowClose = true;
        }

        public bool AllowClose { get; set; }

        public override void Close()
        {
            if (AllowClose)
                base.Close();
        }
    }

}