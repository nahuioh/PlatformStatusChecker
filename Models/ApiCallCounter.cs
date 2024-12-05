public class ApiCallCounter
{
    public int Id { get; set; }
    public required string EndpointName { get; set; }
    public required int CallCount { get; set; }
}