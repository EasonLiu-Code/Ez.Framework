using System.Data;

namespace Ez.Infrastructure.Helper.Excel;

/// <summary>
/// 
/// </summary>
public interface IExcelHelper
{
    /// <summary>
    /// 导出Excel文件，可以是多个sheet【必须在Append()之后调用】
    /// </summary>
    /// <returns></returns>
    byte[] ExportExcelToByte();

    /// <summary>
    /// 追加需要导出的Excel文件的sheet【必须与ExportExcelToByte()一起使用】
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="sheetTitle"></param>
    /// <param name="title">不用传</param>
    /// <returns></returns>
    ExcelHelper Append<T>(List<T> list, string sheetTitle, string title = "") where T : class, new();

    /// <summary>
    /// 导出Excel文件【单个Sheet】
    /// </summary>
    /// <param name="list"></param>
    /// <param name="dicColumn">Excel表头，传入例如：key:Name，value：姓名，为空则根据ExcelHeader属性自动获取</param>
    /// <param name="sheetTitle"></param>
    /// <param name="title">不用传</param>
    /// <returns></returns>
    byte[] ExportExcelToByte<T>(List<T> list, Dictionary<string, string> dicColumn = null, string sheetTitle = "Sheet1", string title = "");

    /// <summary>
    /// 读取Excel
    /// </summary>
    /// <param name="fileStream">文件流</param>
    /// <param name="sheetName">指定读取的工作表名称</param>
    /// <param name="isFirstRowColumnTitle">第一行是否存在列名</param>
    /// <returns></returns>
    DataTable ReadExcelToDataTable(Stream fileStream, string sheetName = null, bool isFirstRowColumnTitle = true);

    /// <summary>
    /// 读取Excel返回List
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fileStream"></param>
    /// <param name="sheetName"></param>
    /// <returns></returns>
    List<T> ReadExcelToList<T>(Stream fileStream, string sheetName = null) where T : class, new();

    /// <summary>
    /// 下载
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    Task<Stream> HttpDownloadAsync(string url);
}