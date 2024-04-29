namespace Restaurant.Domain.Response;

public interface IBaseResponse<T>
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public T? Data { get; set; }
}