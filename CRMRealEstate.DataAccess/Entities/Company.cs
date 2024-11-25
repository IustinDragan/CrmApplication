using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMRealEstate.DataAccess.Entities;

public class Company
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CompanyId { get; set; }

    public string CompanyName { get; set; }
    public string CompanyIdentityNumber { get; set; }
    public string CompanyPhoneNumber { get; set; }
    public DateTime CompanyCreatedAt { get; set; }
    public ICollection<Users> Users { get; set; }

    public Company()
    {
        CompanyCreatedAt = DateTime.Now;
    }
}