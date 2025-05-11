using CRMRealEstate.DataAccess.Enums;

namespace CRMRealEstate.UI.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? UserName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public Roles RoleName { get; set; }
        public DateTime UserCreatedAt { get; set; }
    }
}
