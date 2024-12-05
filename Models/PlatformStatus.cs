public class PlatformStatus
{
    public int Id { get; set; }
    public required string PlatformName { get; set; }
    public required string Version { get; set; }
    public required string Status { get; set; }
    public string Url { get; set; }
}
