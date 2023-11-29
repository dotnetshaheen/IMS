namespace Application.Models.Response;

public class ApiResult : IApiResult
{
    public int StatusCode { get; set; }
    public List<string> Message { get; set; } = new();

    public static IApiResult Success(string message = "Success") =>
          new ApiResult { StatusCode = 200, Message = new List<string> { message } };

    public static IApiResult NoContent() =>
     new ApiResult { StatusCode = 204 };

    public static IApiResult Fail(string message = "Failed") =>
       new ApiResult { StatusCode = 400, Message = new List<string> { message } };

    public static IApiResult Fail(List<string> message) =>
    new ApiResult { StatusCode = 400, Message = message };

    public static IApiResult Unauthorized(string message = "Unauthorized") =>
    new ApiResult { StatusCode = 401, Message = new List<string> { message } };
}



public class ApiResult<T> : ApiResult, IApiResult<T>
{
    public T Data { get; set; }
    public static IApiResult<T> Success(T data, string message = "Success") =>
        new ApiResult<T> { StatusCode = 200, Message = new List<string> { message }, Data = data };    
    public static IApiResult<T> Fail(T data, string message = "Failed") =>
        new ApiResult<T> { StatusCode = 400, Message = new List<string> { message }, Data = data };    
}
