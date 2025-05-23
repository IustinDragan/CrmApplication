﻿using CRMRealEstate.Application.Models.RequestsModel;
using CRMRealEstate.Application.Services.Interfaces;
using CRMRealEstate.DataAccess.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMRealEstate.API.Controllers
{
    [ApiController]
    [Route("requests")]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestsController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateRequestAsync([FromBody] CreateRequestModel createRequestModel)
        {
            var createRequest = await _requestService.CreateRequestsAsync(createRequestModel);

            return Created("", createRequest);
        }

        [HttpPost("{requestId:int}/respond")]
        public async Task<IActionResult> RespondToRequestAsync(int requestId, [FromBody] RespondToRequestModel respondToRequestModel)
        {
            try
            {
                //var agentId = int.Parse(User.FindFirst("id").Value);

                var result = await _requestService.RespondToRequestAsync(requestId, respondToRequestModel);

                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid($"Nu poti raspunde la aceasta solicitare");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRequestsAsync()
        {
            var requestsEntity = await _requestService.GetAllRequestsAsync();

            return Ok(requestsEntity);
        }

        [HttpGet("customer/{customerId:int}")]
        public async Task<IActionResult> GetCustomerRequestsByIdAsync(int customerId)
        {
            var requestById = await _requestService.GetRequestsForCustomerAsync(customerId);

            if (requestById == null)
            {
                throw new ArgumentNullException(nameof(requestById));
            }

            return Ok(requestById);
        }


        [HttpGet("agent/{agentId:int}")]
        public async Task<IActionResult> GetAgentRequestsByIdAsync(int agentId)
        {
            var requestById = await _requestService.GetRequestsForAgentAsync(agentId);

            if (requestById == null)
            {
                throw new ArgumentNullException(nameof(requestById));
            }

            return Ok(requestById);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateRequestStatusAsync(int id, [FromBody] RequestStatus newStatus)
        {
            if (!Enum.IsDefined(typeof(RequestStatus), newStatus))
            {
                return BadRequest("Invalid status value");
            }

            var agentId = int.Parse(User.FindFirst("id").Value);

            var updatedRequest = await _requestService.UpdateRequestsStatusAsync(id, newStatus, agentId);

            return Ok(updatedRequest);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRequestAsync(int id)
        {
            await _requestService.DeleteRequestsAsync(id);

            return NoContent();
        }

        [HttpPost("{id}/Upload")]
        public async Task<IActionResult> Upload(int id, IFormFile formFile)
        {
            await _requestService.UploadAsync(id, formFile);

            return Ok(id);
        }
    }
}