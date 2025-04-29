using CRMRealEstate.Application.Models.AdressModels;
using CRMRealEstate.Application.Models.AnnouncementModels;
using CRMRealEstate.Application.Services.Interfaces;
using CRMRealEstate.Application.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMRealEstate.API.Controllers
{
    [ApiController]
    [Route("announcement")]
    //[Authorize(Roles = "SalesAgent")]
    [AllowAnonymous]
    public class AnnouncementsController : ControllerBase
    {
        private readonly IAnnouncementService _announcementService;
        private readonly IValidator<CreateAdressRequestModel> _validator;

        public AnnouncementsController(IAnnouncementService announcementService,
            IValidator<CreateAdressRequestModel> validator)
        {
            _announcementService = announcementService;
            _validator = validator;
        }

        [HttpPost("{agentId:int}")]
        [Authorize(Roles = "SalesAgent")]
        public async Task<IActionResult> CreateAnnouncementAsync(int agentId, CreateAnnouncementRequestModel createAnnouncementRequestModel)
        {
            var validationResponse = _validator.GetValidationResult(createAnnouncementRequestModel.Property.Adress);
            if (validationResponse != null)
                return validationResponse;


            //var userAgentId = int.Parse(User.FindFirst("id").Value);
            //if (agentId != userAgentId)
            //{
            //    return Forbid();
            //}


            var announcementEntity = await _announcementService.CreateAsync(createAnnouncementRequestModel, agentId);

            return Created("", announcementEntity);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAnnouncementsAsync([FromQuery] ReadAnnouncementRequestModel requestModel)
        {
            var announcementEntity = await _announcementService.ReadAllAsync(requestModel);

            return Ok(announcementEntity);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAnnouncementByIdAsync(int id)
        {
            var announcementEntityById = await _announcementService.ReadByIdAsync(id);

            return Ok(announcementEntityById);
        }

        // [HttpGet("search/{searchText}")]
        // public async Task<IActionResult> SearchAnnouncements(string searchText)
        // {
        //     return Ok(await _announcementService.SearchAsync(searchText));
        //
        // }

        [HttpPut("{id}")]
        [Authorize(Roles = "SalesAgent")]
        public async Task<IActionResult> UpdateAnnouncementAsync(int id,
            UpdateAnnouncementRequestModel updateAnnouncementRequestModel)
        {
            //var validationResponse = _validator.GetValidationResult(updateAnnouncementRequestModel.Property.Adress);
            //if (validationResponse != null)
            //    return validationResponse;

            var announcementEntityByIdForUpdate = await _announcementService.UpdateAsync(id, updateAnnouncementRequestModel);

            return Ok(announcementEntityByIdForUpdate);
        }

        [HttpGet("agentId/{agentId:int}")]
        public async Task<IActionResult> GetMyAnnouncements(int agentId)
        {
            //recomandare Gpt:
            //var agentId = int.Parse(User.FindFirst("agentId").Value);
            //var userAgentId = int.Parse(User.FindFirst("id").Value);

            //if (agentId != userAgentId)
            //{
            //    return Forbid();
            //}

            var announcementsByAgentId = await _announcementService.GetMyAnnouncementsAsync(agentId);

            return Ok(announcementsByAgentId);

        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "SalesAgent")]
        public async Task<IActionResult> DeleteAnnouncementById(int id)
        {
            await _announcementService.DeleteAsync(id);

            return NoContent();
        }
    }
}
