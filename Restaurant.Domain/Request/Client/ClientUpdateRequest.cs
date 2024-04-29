namespace Restaurant.Domain.Request.Client;

public class ClientUpdateRequest
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Phone { get; set; }
}