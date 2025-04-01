using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMRealEstate.DataAccess.Entities;

public class Company
{
    public Company()
    {
        CompanyCreatedAt = DateTime.Now;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CompanyId { get; set; }
    [Required]
    public string CompanyName { get; set; }
    [Required]
    public string CompanyIdentityNumber { get; set; } 
    public string CompanyPhoneNumber { get; set; }
    public DateTime CompanyCreatedAt { get; set; }
    public List<Users> Users { get; set; } = new();
    //public ICollection<Users> Users { get; set; }
}