using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CRMRealEstate.DataAccess.Enums;

namespace CRMRealEstate.DataAccess.Entities;

public class Users
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [MaxLength(50)] public string FirstName { get; set; }
    [MaxLength(50)] public string LastName { get; set; }
    [MaxLength(50)] public string? UserName { get; set; }
    [EmailAddress] public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public int CompanyId { get; set; }
    [ForeignKey("CompanyId")] public Company? Company { get; set; }
    public Roles Roles { get; set; }
    public DateTime UserCreatedAt { get; private set; }

    public Users()
    {
        UserCreatedAt = DateTime.Now;
    }
}