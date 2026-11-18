using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationSystem.Models;

public class StudentGroup
{
    public int Id { get; set; }

    [Required, StringLength(80)]
    [Display(Name = "Топ атауы")]
    public string Name { get; set; } = string.Empty;

    public ICollection<Student> Students { get; set; } = new List<Student>();
}
