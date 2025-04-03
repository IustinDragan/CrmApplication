using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CRMRealEstate.DataAccess.Enums;

namespace CRMRealEstate.DataAccess.Entities;

public class Users
{
    public Users()
    {
        UserCreatedAt = DateTime.Now;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [MaxLength(50)]
    [Required]
    public string FirstName { get; set; }

    [MaxLength(50)]
    [Required]
    public string LastName { get; set; }

    [MaxLength(50)] 
    public string? UserName { get; set; }
    
    [EmailAddress] 
    public string Email { get; set; }
    
    [Required]
    [MinLength(6)]
    public string Password { get; set; }
    
    [Phone]
    public string PhoneNumber { get; set; }
    
    public int CompanyId { get; set; }
    [ForeignKey("CompanyId")] public Company? Company { get; set; }
    
    [Required]
    public Roles Roles { get; set; }
    
    public DateTime UserCreatedAt { get; private set; }

    public ICollection<Announcement> Announcements { get; set; }

    public ICollection<UserAnnouncement> FavoritesAnnouncements { get; set; }
}