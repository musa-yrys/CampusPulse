using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationSystem.Models;

public class Student
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Атын енгізіңіз.")]
    [StringLength(60, ErrorMessage = "Аты 60 таңбадан аспауы керек.")]
    [Display(Name = "Аты")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Тегін енгізіңіз.")]
    [StringLength(60, ErrorMessage = "Тегі 60 таңбадан аспауы керек.")]
    [Display(Name = "Тегі")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email енгізіңіз.")]
    [EmailAddress(ErrorMessage = "Email форматын тексеріңіз.")]
    [StringLength(150)]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Телефон нөмірінің форматын тексеріңіз.")]
    [StringLength(30)]
    [Display(Name = "Телефон")]
    public string? Phone { get; set; }

    [Display(Name = "Тобы")]
    [Range(1, int.MaxValue, ErrorMessage = "Топты таңдаңыз.")]
    public int GroupId { get; set; }

    public StudentGroup? Group { get; set; }
}
