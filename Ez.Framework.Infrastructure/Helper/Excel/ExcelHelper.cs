using System.ComponentModel;
using System.Data;
using System.Net;
using System.Reflection;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Ez.Infrastructure.Helper.Excel;

/// <summary>
/// 
/// </summary>
public class ExcelHelper
{
        private  XSSFWorkbook workBook;
    #region 导出Excel文件，可以是多个sheet【必须在Append()之后调用】

    /// <summary>
    /// 导出Excel文件，可以是多个sheet【必须在Append()之后调用】
    /// </summary>
    /// <returns></returns>
    public byte[] ExportExcelToByte()
    {
        if (workBook == null)
        {
            return Array.Empty<byte>();
        }

        byte[] buffer = null;
        using (MemoryStream ms = new())
        {
            workBook.Write(ms);
            buffer = ms.ToArray();
            ms.Close();
        }

        workBook = null;
        return buffer;
    }

    #endregion

    #region 追加需要导出的Excel文件的sheet【必须与ExportExcelToByte一起使用】

    /// <summary>
    /// 追加需要导出的Excel文件的sheet【必须与ExportExcelToByte一起使用】
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="sheetTitle"></param>
    /// <param name="title">不用传</param>
    /// <returns></returns>
    public ExcelHelper Append<T>(List<T> list, string sheetTitle, string title = "") where T : class, new()
    {
        if (list == null || list.Count <= 0)
        {
            return this;
        }

        //HSSFWorkbook => xls
        //XSSFWorkbook => xlsx
        workBook ??= new();

        List<ISheet> sheets = new();

        var dicColumn = GetColumnHeaderDictionary(list, null);

        //名称自定义
        ISheet sheet = workBook.CreateSheet(sheetTitle);
        IRow cellsColumn;
        //获取实体属性名
        PropertyInfo[] properties = list[0].GetType().GetProperties();
        int cellsIndex = 0;

        ICellStyle style = workBook.CreateCellStyle();
        IFont font = workBook.CreateFont();

        //标题
        if (!string.IsNullOrWhiteSpace(title))
        {
            //边框  
            style.BorderBottom = BorderStyle.Dotted;
            style.BorderLeft = BorderStyle.Hair;
            style.BorderRight = BorderStyle.Hair;
            style.BorderTop = BorderStyle.Dotted;
            //水平对齐  
            style.Alignment = HorizontalAlignment.Left;

            //垂直对齐  
            style.VerticalAlignment = VerticalAlignment.Center;

            //设置字体
            font.FontHeightInPoints = 10;
            font.FontName = "微软雅黑";
            style.SetFont(font);

            IRow cellsTitle = sheet.CreateRow(0);
            cellsTitle.CreateCell(0).SetCellValue(title);
            cellsTitle.RowStyle = style;
            //合并单元格
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 1, 0, dicColumn.Count - 1));
            cellsIndex = 2;
        }
        else
        {
            font.FontHeightInPoints = 10;
            font.FontName = "微软雅黑";
            font.IsBold = true;
            style.SetFont(font);
        }

        int index = 0;
        Dictionary<string, int> columns = new();
        //列名
        cellsColumn = sheet.CreateRow(cellsIndex);

        foreach (var item in dicColumn)
        {
            cellsColumn.CreateCell(index).SetCellValue(item.Value);
            columns.Add(item.Value, index);
            cellsColumn.GetCell(index).CellStyle = style;

            #region 设置单元格自适应

            var intcolumnWidth = sheet.GetColumnWidth(index) / 256;
            ICell currentCell = cellsColumn.GetCell(index);
            int length = Encoding.UTF8.GetBytes(currentCell.ToString()).Length; //获取当前单元格的内容宽度
            if (intcolumnWidth < length) //若当前单元格内容宽度大于列宽，则调整列宽为当前单元格宽度
            {
                sheet.SetColumnWidth(index, length * 256);
            }

            #endregion

            index++;
        }

        cellsIndex += 1;
        //数据
        foreach (var item in list)
        {
            IRow cellsData = sheet.CreateRow(cellsIndex);
            for (int i = 0; i < properties.Length; i++)
            {
                //单元格样式：cellsData.GetCell(index).CellStyle = style2;

                if (!dicColumn.ContainsKey(properties[i].Name)) continue;
                //这里可以也根据数据类型做不同的赋值，也可以根据不同的格式参考上面的ICellStyle设置不同的样式
                object[] entityValues = new object[properties.Length];
                entityValues[i] = properties[i].GetValue(item);

                var value = entityValues[i]?.ToString() ?? string.Empty;
                if (properties[i].PropertyType.Name.Equals("datetime", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (DateTime.TryParse(value, out DateTime dateTimeValue))
                    {
                        value = dateTimeValue.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }

                //获取对应列下标
                index = columns[dicColumn[properties[i].Name]];
                cellsData.CreateCell(index).SetCellValue(value);
            }

            cellsIndex++;
        }

        // 将工作表添加到列表中
        sheets.Add(sheet);

        return this;
    }

    #endregion

    #region 导出Excel文件【单个Sheet】

    /// <summary>
    /// 导出Excel文件【单个Sheet】
    /// </summary>
    /// <param name="list"></param>
    /// <param name="dicColumn">Excel表头，传入例如：key:Name，value：姓名，为空则根据ExcelHeader属性自动获取</param>
    /// <param name="sheetTitle"></param>
    /// <param name="title">不用传</param>
    /// <returns></returns>
    public byte[] ExportExcelToByte<T>(List<T> list, Dictionary<string, string> dicColumn = null,
        string sheetTitle = "Sheet1", string title = "")
    {
        if (list.Count <= 0)
        {
            return Array.Empty<byte>();
        }

        if (dicColumn == null || dicColumn.Count == 0)
        {
            dicColumn = GetColumnHeaderDictionary(list, dicColumn);
        }

        //HSSFWorkbook => xls
        //XSSFWorkbook => xlsx
        XSSFWorkbook workbook = new();
        ISheet sheet = workbook.CreateSheet(sheetTitle); //名称自定义
        IRow cellsColumn;
        //获取实体属性名
        PropertyInfo[] properties = list[0].GetType().GetProperties();
        int cellsIndex = 0;

        ICellStyle style = workbook.CreateCellStyle();
        IFont font = workbook.CreateFont();

        //标题
        if (!string.IsNullOrEmpty(title))
        {
            //边框  
            style.BorderBottom = BorderStyle.Dotted;
            style.BorderLeft = BorderStyle.Hair;
            style.BorderRight = BorderStyle.Hair;
            style.BorderTop = BorderStyle.Dotted;
            //水平对齐  
            style.Alignment = HorizontalAlignment.Left;

            //垂直对齐  
            style.VerticalAlignment = VerticalAlignment.Center;

            //设置字体
            font.FontHeightInPoints = 10;
            font.FontName = "微软雅黑";
            style.SetFont(font);

            IRow cellsTitle = sheet.CreateRow(0);
            cellsTitle.CreateCell(0).SetCellValue(title);
            cellsTitle.RowStyle = style;
            //合并单元格
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 1, 0, dicColumn.Count - 1));
            cellsIndex = 2;
        }
        else
        {
            font.FontHeightInPoints = 10;
            font.FontName = "微软雅黑";
            font.IsBold = true;
            style.SetFont(font);
        }

        int index = 0;
        Dictionary<string, int> columns = new();
        //列名
        cellsColumn = sheet.CreateRow(cellsIndex);

        foreach (var item in dicColumn)
        {
            cellsColumn.CreateCell(index).SetCellValue(item.Value);
            columns.Add(item.Value, index);
            cellsColumn.GetCell(index).CellStyle = style;

            #region 设置单元格自适应

            var intcolumnWidth = sheet.GetColumnWidth(index) / 256;
            ICell currentCell = cellsColumn.GetCell(index);
            int length = Encoding.UTF8.GetBytes(currentCell.ToString()).Length; //获取当前单元格的内容宽度
            if (intcolumnWidth < length) //若当前单元格内容宽度大于列宽，则调整列宽为当前单元格宽度
            {
                sheet.SetColumnWidth(index, length * 256);
            }

            #endregion

            index++;
        }

        cellsIndex += 1;
        //数据
        foreach (var item in list)
        {
            IRow cellsData = sheet.CreateRow(cellsIndex);
            for (int i = 0; i < properties.Length; i++)
            {
                //单元格样式：cellsData.GetCell(index).CellStyle = style2;

                if (!dicColumn.ContainsKey(properties[i].Name)) continue;
                //这里可以也根据数据类型做不同的赋值，也可以根据不同的格式参考上面的ICellStyle设置不同的样式
                object[] entityValues = new object[properties.Length];
                entityValues[i] = properties[i].GetValue(item);

                var value = entityValues[i]?.ToString() ?? string.Empty;
                if (properties[i].PropertyType.Name.Equals("datetime", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (DateTime.TryParse(value, out DateTime dateTimeValue))
                    {
                        value = dateTimeValue.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }else if (properties[i].PropertyType.IsEnum)
                {
                    // 获取枚举成员的描述
                    var enumMember = properties[i].PropertyType.GetMember(value)[0];
                    var descriptionAttribute = (DescriptionAttribute)Attribute.GetCustomAttribute(enumMember, typeof(DescriptionAttribute));
                    if (descriptionAttribute != null)
                    {
                        value = descriptionAttribute.Description;
                    }
                }

                //获取对应列下标
                index = columns[dicColumn[properties[i].Name]];
                cellsData.CreateCell(index).SetCellValue(value);
            }

            cellsIndex++;
        }

        byte[] buffer = null;
        using (MemoryStream ms = new())
        {
            workbook.Write(ms);
            buffer = ms.ToArray();
            ms.Close();
        }

        return buffer;
    }

    #endregion


    #region 读取Attribute标识

    /// <summary>
    /// 获取Attribute转表头
    /// </summary>
    /// <param name="list"></param>
    /// <param name="dicColumn">Excel表头，传入例如：key:Name，value：姓名，为空则根据ExcelHeader属性自动获取</param>
    /// <returns></returns>
    private static Dictionary<string, string> GetColumnHeaderDictionary<T>(List<T> list,
        Dictionary<string, string> dicColumn = null)
    {
        if (dicColumn != null && dicColumn.Count > 0)
        {
            return dicColumn;
        }

        var model = list.FirstOrDefault();
        if (model == null)
        {
            return new Dictionary<string, string>();
        }

        dicColumn = new Dictionary<string, string>();

        var type = model.GetType();
        var propertyInfos = type.GetProperties()
            .OrderBy(p => ((ExcelHeaderAttribute)p.GetCustomAttributes(typeof(ExcelHeaderAttribute), false)
                .FirstOrDefault())?.ColumnIndex)
            .ToArray();
        foreach (var item in propertyInfos)
        {
            var info = (ExcelHeaderAttribute)item.GetCustomAttributes(typeof(ExcelHeaderAttribute), false)
                .FirstOrDefault();
            if (info == null) continue;

            dicColumn.Add(item.Name, info.HeaderName?.Trim() ?? string.Empty);
        }

        return dicColumn;
    }

    #endregion

    #region 读取Excel

    /// <summary>
    /// 读取Excel
    /// </summary>
    /// <param name="fileStream">文件流</param>
    /// <param name="sheetName">指定读取的工作表名称</param>
    /// <param name="isFirstRowColumnTitle">第一行是否存在列名</param>
    /// <returns></returns>
    public DataTable ReadExcelToDataTable(Stream fileStream, string sheetName = null, bool isFirstRowColumnTitle = true)
    {
        //定义要返回的datatable对象
        DataTable data = new();
        //根据文件流创建excel数据结构,NPOI的工厂类WorkbookFactory会自动识别excel版本，创建出不同的excel数据结构
        IWorkbook workbook = WorkbookFactory.Create(fileStream);
        //excel工作表
        ISheet sheet;
        //如果有指定工作表名称
        if (!string.IsNullOrEmpty(sheetName))
        {
            sheet = workbook.GetSheet(sheetName);
            //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
            if (sheet == null)
            {
                sheet = workbook.GetSheetAt(0);
            }
        }
        else
        {
            //如果没有指定的sheetName，则尝试获取第一个sheet
            sheet = workbook.GetSheetAt(0);
        }

        if (sheet == null)
        {
            return data;
        }

        IRow firstRow = sheet.GetRow(0);
        //一行最后一个cell的编号 即总的列数
        int cellCount = firstRow.LastCellNum;
        //数据开始行(排除标题行)
        int startRow;
        //如果第一行是标题列名
        if (isFirstRowColumnTitle)
        {
            for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
            {
                ICell cell = firstRow.GetCell(i);
                if (cell == null)
                {
                    continue;
                }

                string cellValue = cell.StringCellValue;
                if (cellValue != null)
                {
                    DataColumn column = new(cellValue);
                    data.Columns.Add(column);
                }
            }

            startRow = sheet.FirstRowNum + 1;
        }
        else
        {
            for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
            {
                ICell cell = firstRow.GetCell(i);
                if (cell != null)
                {
                    DataColumn dataColumn = new(i.ToString());
                    data.Columns.Add(dataColumn);
                }
            }

            startRow = sheet.FirstRowNum;
        }

        //最后一列的标号
        int rowCount = sheet.LastRowNum;
        for (int i = startRow; i <= rowCount; ++i)
        {
            IRow row = sheet.GetRow(i);
            if (row == null || row.FirstCellNum < 0) continue; //没有数据的行默认是null　　　　　　　

            DataRow dataRow = data.NewRow();
            for (int j = row.FirstCellNum; j < cellCount; ++j)
            {
                //同理，没有数据的单元格都默认是null
                ICell cell = row.GetCell(j);
                if (cell == null)
                {
                    continue;
                }

                if (cell.CellType == CellType.Numeric)
                {
                    //判断是否日期类型
                    if (DateUtil.IsCellDateFormatted(cell))
                    {
                        dataRow[j] = row.GetCell(j).DateCellValue;
                    }
                    else
                    {
                        dataRow[j] = row.GetCell(j).ToString().Trim();
                    }
                }
                else
                {
                    dataRow[j] = row.GetCell(j).ToString().Trim();
                }
            }

            data.Rows.Add(dataRow);
        }

        return data;
    }

    #endregion

    #region 读取Excel返回List

    /// <summary>
    /// 读取Excel返回List
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fileStream"></param>
    /// <param name="sheetName"></param>
    /// <returns></returns>
    public List<T> ReadExcelToList<T>(Stream fileStream, string sheetName = null) where T : class, new()
    {
        //定义要返回的datatable对象
        DataTable dataTable = ReadExcelToDataTable(fileStream, sheetName, true);
        if (dataTable == null || dataTable.Rows.Count == 0)
        {
            return new List<T>();
        }

        List<T> list = new();
        Type type = typeof(T);
        PropertyInfo[] properties = type.GetProperties();

        foreach (DataRow row in dataTable.Rows)
        {
            T item = new();
            foreach (PropertyInfo property in properties)
            {
                var info = (ExcelHeaderAttribute)property.GetCustomAttributes(typeof(ExcelHeaderAttribute), false)
                    .FirstOrDefault();
                if (info == null)
                {
                    continue;
                }

                var headerName = info?.HeaderName?.Trim() ?? string.Empty;
                // 检查列是否存在
                if (!row.Table.Columns.Contains(headerName))
                {
                    continue;
                }

                //获取属性的类型
                var propertyTypeName = property.PropertyType.Name.ToLowerInvariant();
                if (propertyTypeName == "int32")
                {
                    _ = int.TryParse(row[headerName]?.ToString(), out int result);
                    // 使用 SetValue 方法来设置属性值，它确保了类型转换和空值检查
                    property.SetValue(item, result, null);
                }
                else
                {
                    // 使用 SetValue 方法来设置属性值，它确保了类型转换和空值检查
                    property.SetValue(item, row[headerName]?.ToString()?.Trim() ?? string.Empty, null);
                }
            }

            list.Add(item);
        }

        return list;
    }

    #endregion
    /// <summary>
    /// 下载文件
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public  async Task<Stream> HttpDownloadAsync(string url)
    {
        await using var memStream = new MemoryStream();
        memStream.Position = 0;
        using var webClient = new WebClient();
        var byteArray = await webClient.DownloadDataTaskAsync(url);
        return new MemoryStream(byteArray);
    }
}