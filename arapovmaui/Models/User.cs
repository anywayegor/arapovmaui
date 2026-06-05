namespace arapovmaui.Models;

public class User
{
    public int IdUser { get; set; }

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string Patronumic { get; set; } = "";

    public string Login { get; set; } = "";

    public string Role { get; set; } = "";

    public string FullName =>
        $"{LastName} {FirstName} {Patronumic}";
}