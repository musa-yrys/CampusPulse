using System.ComponentModel.DataAnnotations;

namespace CampusPulse.Models;

public class EventRegistration
{
    public int Id { get; set; }
    public int CampusEventId { get; set; }
    public CampusEvent? CampusEvent { get; set; }
    [Required, StringLength(60)] public string StudentName { get; set; } = "";
    [Required, EmailAddress, StringLength(120)] public string Email { get; set; } = "";
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    public bool IsWaitlisted { get; set; }
}
