namespace Restaurant.Domain.Response;

public class BaseResponse<T> : IBaseResponse<T>
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public T? Data { get; set; }

    public BaseResponse(bool success, string? errorMessage, T? data)
    {
        Success = success;
        ErrorMessage = errorMessage;
        Data = data;
    }
}