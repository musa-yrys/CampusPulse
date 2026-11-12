using CampusPulse.Models;

namespace CampusPulse.ViewModels;

public class EventDetailsViewModel
{
    public CampusEvent Event { get; set; } = new();
    public int RegisteredCount { get; set; }
    public int PlacesLeft => Math.Max(0, Event.Capacity - RegisteredCount);
}
