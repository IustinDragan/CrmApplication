using CRMRealEstate.Application.Helpers.Constants;
using CRMRealEstate.Application.Helpers.Exceptions;
using CRMRealEstate.Application.Helpers.Validators;
using CRMRealEstate.Application.Models.UsersModels;
using CRMRealEstate.Application.Services;
using CRMRealEstate.Application.Services.Interfaces;
using CRMRealEstate.DataAccess.Enums;
using CRMRealEstate.Shared.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMRealEstate.API.Controllers;

[ApiController]
[Route("Users")]
//[Authorize(Roles = "Admin")]
public class UsersController : ControllerBase
{
    private readonly IUsersServices _usersServices;

    public UsersController(IUsersServices usersServices)
    {
        _usersServices = usersServices ?? throw new ArgumentNullException(nameof(usersServices));
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateUsersAsync(CreateUsersRequestModel createUsersRequestModel)
    {
        var validator = new CreateUsersRequestValidator();
        var result = await validator.ValidateAsync(createUsersRequestModel);

        if (!result.IsValid)
            return BadRequest(result.Errors);
        //return BadRequest(result.Errors[0].ErrorMessage);

        if (!await _usersServices.isEmailUniqueAsync(createUsersRequestModel.Email))
            return Conflict(UsersConstants.EMAIL_ALREADY_ASSOCIATED_WITH_AN_ACCOUNT);

        var createUserEntity = await _usersServices.CreateUserAsync(createUsersRequestModel);

        return Created("", createUserEntity);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginAsync(LoginRequestModel requestModel)
    {
        try
        {
            var response = await _usersServices.LoginAsync(requestModel);

            return Ok(response);
        }
        catch (NotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        var userEntities = await _usersServices.RealAllUsersAsync();

        return Ok(userEntities);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserByIdAsync(int id, bool includeCompanyDetails = false)
    {
        var userEntityById = await _usersServices.GetUserByIdAsync(id, includeCompanyDetails);

        if (userEntityById == null)
            return NotFound(string.Format(UsersConstants.USER_ID_NOT_FOUND, id));

        return Ok(userEntityById);
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetUserByName(string name, bool includeCompanyDetails)
    {
        var userEntityByName = await _usersServices.GetUserByNameAsync(name, includeCompanyDetails);

        return Ok(userEntityByName);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateUserAsync(int id, CreateUsersRequestModel createUsersRequestModel) //TODO base model 
    {
        var validator = new CreateUsersRequestValidator();
        var result = await validator.ValidateAsync(createUsersRequestModel);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        if (!await _usersServices.isEmailUniqueAsync(createUsersRequestModel.Email))
            return Conflict(UsersConstants.EMAIL_ALREADY_ASSOCIATED_WITH_AN_ACCOUNT);

        var userEntityByIdForUpdate = await _usersServices.UpdateUserAsync(id, createUsersRequestModel);

        return Ok(userEntityByIdForUpdate);
    }

    [Authorize(Roles = "Customer")]
    //[Authorize(Roles = "SalesAgent")]
    [HttpPost("{userId}/favorites/{announcementId}")]
    public async Task<IActionResult> AddToFavoriteAsync(int userId, int announcementId)
    {
        var response = await _usersServices.AddAnnouncementToFavoriteAsync(userId, announcementId);

        if (response == null)
            return NotFound("Announcement or User not found.");

        return Ok(new { message = $"Announcement {announcementId} added to favorites." });
    }

    //[Authorize(Roles = "Customer")]
    //[Authorize(Roles = "SalesAgent")]
    [HttpGet("{userId}/favorites")]
    public async Task<IActionResult> GetFavorites(int userId)
    {
        var favorites = await _usersServices.GetFavoriteAnnouncementsAsync(userId);

        return Ok(favorites);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsersByIdAsync(int id)
    {
        await _usersServices.DeleteUserAsync(id);

        return NoContent();
    }
}