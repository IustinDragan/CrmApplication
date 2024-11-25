using System.ComponentModel.DataAnnotations;
using CRMRealEstate.DataAccess.Entities;
using CRMRealEstate.DataAccess.Enums;

namespace CRMRealEstate.Application.Models.UsersModels;

public class UsersResponseModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? UserName { get; set; }
    [EmailAddress] public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public Roles RoleName { get; set; }
    public DateTime UserCreatedAt { get; set; }
    //trebuie adaugat si Company?

    public static UsersResponseModel FromUser(Users user)
    {
        return new UsersResponseModel
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            EmailAddress = user.Email,
            PhoneNumber = user.PhoneNumber,
            RoleName = user.Roles,
            UserCreatedAt = user.UserCreatedAt
        };
    }
}