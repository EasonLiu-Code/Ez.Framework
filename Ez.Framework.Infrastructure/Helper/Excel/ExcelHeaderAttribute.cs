namespace Ez.Infrastructure.Helper.Excel;

/// <summary>
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class ExcelHeaderAttribute : Attribute
{
    /// <summary>
    /// 导入时，必须与excel的headerName一致
    /// </summary>
    public string HeaderName { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int ColumnIndex { get; set; } = 10000;
    /// <summary>
    /// ctor
    /// </summary>
    public ExcelHeaderAttribute()
    {
    }

    /// <summary>
    /// ExcelHeaderAttribute
    /// </summary>
    public ExcelHeaderAttribute(string headerName)
    {
        HeaderName = headerName?.Trim() ?? string.Empty;
    }
    
    
}