namespace Application.Models.Response;

public interface IApiResult
{
    int StatusCode { get; set; }
    List<string> Message { get; set; }
}

public interface IApiResult<out T> : IApiResult
{
    T Data { get; }
}
