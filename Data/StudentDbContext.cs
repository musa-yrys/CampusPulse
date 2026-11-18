using Microsoft.EntityFrameworkCore;
using StudentRegistrationSystem.Models;

namespace StudentRegistrationSystem.Data;

public class StudentDbContext(DbContextOptions<StudentDbContext> options) : DbContext(options)
{
    public DbSet<Student> Students => Set<Student>();
    public DbSet<StudentGroup> Groups => Set<StudentGroup>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentGroup>()
            .HasIndex(group => group.Name)
            .IsUnique();

        modelBuilder.Entity<Student>()
            .HasOne(student => student.Group)
            .WithMany(group => group.Students)
            .HasForeignKey(student => student.GroupId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
