using CRMRealEstate.Application.Helpers;
using CRMRealEstate.Application.Helpers.Constants;
using CRMRealEstate.Application.Helpers.Exceptions;
using CRMRealEstate.Application.Models.AnnouncementModels;
using CRMRealEstate.Application.Models.UsersModels;
using CRMRealEstate.Application.Services.Interfaces;
using CRMRealEstate.DataAccess.Entities;
using CRMRealEstate.DataAccess.Enums;
using CRMRealEstate.DataAccess.Repositories;
using CRMRealEstate.DataAccess.Repositories.Interfaces;
using CRMRealEstate.DataAccess.Scripts;
using CRMRealEstate.Shared.Models.Users;
using System.Data;

namespace CRMRealEstate.Application.Services;

public class UsersService : IUsersServices
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUsersRepository _usersRepository;
    private readonly IAnnouncementRepository _announcementRepository;

    public UsersService(IUsersRepository userRepository, ICurrentUserService currentUserService, IAnnouncementRepository announcementRepository)
    {
        _usersRepository = userRepository;
        _currentUserService = currentUserService;
        _announcementRepository = announcementRepository;
    }

    public async Task<IEnumerable<UsersResponseModel>> RealAllUsersAsync()
    {
        var users = await _usersRepository.ReadAllAsync();

        return users.Select(UsersResponseModel.FromUser).ToList();
    }

    public async Task<UsersResponseModel> GetUserByIdAsync(int id, bool includeCompanyDetails)
    {
        var userByIdFromDb = await _usersRepository.GetById(id);
        if (userByIdFromDb == null)
            return null;

        if (includeCompanyDetails && _usersRepository.GetById(id).Result?.Company == null)
            Console.WriteLine(CompanyConstants.NO_COMPANY_DETAILS_FOUND);

        var fromUser = UsersResponseModel.FromUser(userByIdFromDb);
        return fromUser;
    }

    public async Task<UsersResponseModel> GetUserByNameAsync(string userName, bool includeCompanyDetails)
    {
        if (includeCompanyDetails && _usersRepository.GetByUsername(userName).Result?.Company == null)
            Console.WriteLine(CompanyConstants.NO_COMPANY_DETAILS_FOUND);

        var userByNameFromDb = await _usersRepository.GetByUsername(userName);
        if (userByNameFromDb != null)
        {
            var fromUser = UsersResponseModel.FromUser(userByNameFromDb);
            return fromUser;
        }

        return null;
    }

    public async Task<UsersResponseModel> CreateUserAsync(CreateUsersRequestModel requestModel)
    {
        var newUser = requestModel.ToUser();
        newUser.Roles = requestModel.isAgent ? Roles.SalesAgent : Roles.Customer;

        await _usersRepository.AddAsync(newUser);

        return UsersResponseModel.FromUser(newUser);
    }

    //public async Task<UsersResponseModel> CreateUserAsync(CreateUsersRequestModel requestModel)
    //{
    //    var user = requestModel.ToUser();
    //    user.Roles = requestModel.isAgent ? Roles.SalesAgent : Roles.Customer;

    //    // dacă este agent, trebuie să aibă companie
    //    if (requestModel.isAgent && requestModel.Company != null)
    //    {
    //        // presupunem că adaugi compania în baza de date și primești ID-ul
    //        var newCompany = await _companyRepository.AddAsync(requestModel.Company);
    //        user.CompanyId = newCompany.Id;
    //    }
    //    else
    //    {
    //        // client simplu — fără companie
    //        user.CompanyId = null;
    //    }

    //    await _usersRepository.AddAsync(user);

    //    return UsersResponseModel.FromUser(user);
    //}

    public async Task<UsersResponseModel> UpdateUserAsync(int id, CreateUsersRequestModel requestModel)
    {
        var userFromDb = _usersRepository.GetById(id);

        if (userFromDb == null) throw new NotFoundException(string.Format(UsersConstants.USER_ID_NOT_FOUND, id));

        if (userFromDb.Result != null)
        {
            userFromDb.Result.FirstName = requestModel.FirstName;
            userFromDb.Result.LastName = requestModel.LastName;
            userFromDb.Result.UserName = requestModel.UserName;
            userFromDb.Result.Email = requestModel.Email;
            userFromDb.Result.Password = requestModel.Password;
            userFromDb.Result.PhoneNumber = requestModel.PhoneNumber;
            userFromDb.Result.Roles = requestModel.isAgent ? Roles.SalesAgent : Roles.Customer;

            if (requestModel.isAgent && userFromDb.Result.Company != null)
            {
                userFromDb.Result.Company.CompanyName = requestModel.Company.CompanyName;
                userFromDb.Result.Company.CompanyIdentityNumber = requestModel.Company.CompanyIdentityNumber;
                userFromDb.Result.Company.CompanyPhoneNumber = requestModel.Company.CompanyPhoneNumber;
            }

            await _usersRepository.UpdateAsync(id);

            var updateUserResponseModel = new UsersResponseModel
            {
                Id = userFromDb.Id,
                FirstName = userFromDb.Result.FirstName,
                LastName = userFromDb.Result.LastName,
                UserName = userFromDb.Result.UserName,
                EmailAddress = userFromDb.Result.Email,
                PhoneNumber = userFromDb.Result.PhoneNumber,
                RoleName = userFromDb.Result.Roles
            };

            return updateUserResponseModel;
        }

        return null;
    }

    public async Task DeleteUserAsync(int id)
    {
        await _usersRepository.DeleteAsync(id);
    }

    public async Task<LoginResponseModel> LoginAsync(LoginRequestModel requestModel)
    {
        var user = await _usersRepository.GetByUsername(requestModel.Username);

        if (user == null)
            throw new NotFoundException(UsersConstants.USERNAME_OR_PASSWORD_NOT_FOUND);

        if (user.Password != requestModel.Password)
            throw new NotFoundException(UsersConstants.USERNAME_OR_PASSWORD_NOT_FOUND);

        var token = JwtHelper.GenerateToken(user, "MySuperSecretKeyMySuperSecretKey2");


        return new LoginResponseModel
        {
            Username = user.UserName,
            Id = user.Id,
            Email = user.Email,
            Token = token
        };
    }

    public async Task<bool> isEmailUniqueAsync(string email)
    {
        var emailAddress = _usersRepository.GetEmail(email).Result;

        if (emailAddress)
            return false;

        return true;
    }

    public async Task<int?> AddAnnouncementToFavoriteAsync(int userId, int announcementId)
    {
        var user = await _usersRepository.GetUserWithFavoritesAsync(userId);
        if (user == null)
            throw new NotFoundException($"User with ID {userId} not found.");

        var announcement = await _announcementRepository.ReadByIdAsync(announcementId);
        if (announcement == null)
            throw new NotFoundException($"Announcement with ID {announcementId} not found.");

        var alreadyFavorite = user.FavoritesAnnouncements.Any(fav => fav.AnnouncementId == announcementId);

        if (alreadyFavorite)
            return announcementId;

        var favorite = new UserAnnouncement
        {
            UserId = userId,
            AnnouncementId = announcementId
        };

        await _usersRepository.AddFavoriteAnnouncementsAsync(favorite);

        return announcementId;
    }



    //
    //public async Task<int?> AddAnnouncementToFavoriteAsync(int userId, int announcementId)
    //{
    //    var currentUserId = _currentUserService.UserId;

    //    if (currentUserId != userId)
    //    {
    //        throw new Exception("You can't add favorite announcements for someone else.");
    //    }

    //    var userAnnouncement = await this._usersRepository.GetFavoriteAnnouncementAsync(userId, announcementId);

    //    if (userAnnouncement is not null)
    //    {
    //        throw new Exception("This announcement is already added to favorites.");
    //    }

    //    userAnnouncement = new UserAnnouncement
    //    {
    //        UserId = userId,
    //        AnnouncementId = announcementId
    //    };

    //    await this._usersRepository.AddFavoriteAnnouncementsAsync(userAnnouncement);

    //    return announcementId;
    //}

    public async Task<List<AnnouncementResponseModel>> GetFavoriteAnnouncementsAsync(int userId)
    {
        var favorites = await _usersRepository.GetFavoriteAnnouncementsAsync(userId);

        var announcements = favorites
            .Where(f => f != null && f.Announcement != null)
            .Select(f => AnnouncementResponseModel.FromAnnouncement(f.Announcement))
            .ToList();

        return announcements;
    }
}
