using System.ComponentModel.DataAnnotations;

namespace CampusPulse.Models;

public class CampusEvent
{
    public int Id { get; set; }
    [Required, StringLength(100)] public string Title { get; set; } = "";
    [Required, StringLength(60)] public string Club { get; set; } = "";
    [Required, StringLength(40)] public string Category { get; set; } = "";
    [Required, StringLength(500)] public string Summary { get; set; } = "";
    [Required, StringLength(80)] public string Venue { get; set; } = "";
    [Required, StringLength(40)] public string Organizer { get; set; } = "";
    [DataType(DataType.DateTime)] public DateTime StartsAt { get; set; }
    [Range(1, 500)] public int Capacity { get; set; }
    public bool IsFeatured { get; set; }
    public ICollection<EventRegistration> Registrations { get; set; } = new List<EventRegistration>();
}
