// Models/User.cs
namespace AICalendar.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public ICollection<Participant> Participations { get; set; }
}
