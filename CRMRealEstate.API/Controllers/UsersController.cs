using CRMRealEstate.Application.Models.UsersModels;
using CRMRealEstate.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMRealEstate.API.Controllers;

[ApiController]
[Route("Users")]
[Authorize(Roles = "Admin")]
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
        if (!await _usersServices.isEmailUniqueAsync(createUsersRequestModel.Email))
            return Conflict("Email already associated with an account.");

        var createUserEntity = await _usersServices.CreateUserAsync(createUsersRequestModel);

        return Created("", createUserEntity);
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
            return NotFound($"No user found with ID {id}.");

        return Ok(userEntityById);
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetUserByName(string name, bool includeCompanyDetails)
    {
        var userEntityByName = await _usersServices.GetUserByNameAsync(name, includeCompanyDetails);

        return Ok(userEntityByName);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult>
        UpdateUserAsync(int id, CreateUsersRequestModel createUsersRequestModel) //ToDo base model 
    {
        if (!await _usersServices.isEmailUniqueAsync(createUsersRequestModel.Email))
            return BadRequest("Email already associated with an account.");

        var userEntityByIdForUpdate = await _usersServices.UpdateUserAsync(id, createUsersRequestModel);

        return Ok(userEntityByIdForUpdate);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsersByIdAsync(int id)
    {
        await _usersServices.DeleteUserAsync(id);

        return NoContent();
    }
}