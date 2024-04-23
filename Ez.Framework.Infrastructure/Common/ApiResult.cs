namespace Ez.Infrastructure.Common;

/// <summary>
/// </summary>
/// <typeparam name="TResult"></typeparam>
public sealed class ApiResult<TResult>
{
    public ApiResult(int errorCode, string message, TResult result)
    {
        Message = message;
        this.Result = result;
    }

    public ApiResult()
    {
      
    }
    public bool IsSuccess { get; set; }

    public int ErrorCode { get; set; }

    public string Message { get; set; }
    public TResult Result { get; set; }
    public override string ToString()
    {
        return $"{(object)this.IsSuccess},ErrorCode:{(object)this.ErrorCode},Message:{(object)this.Message}";
    }
}
