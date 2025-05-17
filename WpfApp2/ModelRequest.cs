public class Request
{
    public string Article { get; set; }
    public string ShortDescription { get; set; }
    public string Type { get; set; }
    public string FullDescription { get; set; }
    public DateOnly RegistrationDate { get; set; }
    public string Status { get; set; }
    public User Executor { get; set; }

    public override string ToString() => $"{Article} - {ShortDescription} ({Status})";
}