
public class User
{
    public string Login { get; set; }
    public string Password { get; set; }
    public DateOnly RegistrationDate { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }

    public override string ToString() => FullName;
}


