using CRMRealEstate.Application.Models.RequestsModel;
using CRMRealEstate.Application.Services.Interfaces;
using CRMRealEstate.DataAccess.Enums;
using CRMRealEstate.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace CRMRealEstate.Application.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;
        private readonly StorageService _storageService;

        public RequestService(IRequestRepository requestRepository, StorageService storageService)
        {
            _requestRepository = requestRepository;
            _storageService = storageService;
        }
        public async Task<RequestResponseModel> CreateRequestsAsync(CreateRequestModel requestModel)
        {
            var newRequest = requestModel.ToRequest();

            await _requestRepository.CreateAsync(newRequest); 

            return RequestResponseModel.FromUser(newRequest);
        }

        public async Task DeleteRequestsAsync(int requestId)
        {
           await _requestRepository.DeleteAsync(requestId);
        }

        public async Task<List<RequestResponseModel>> GetAllRequestsAsync()
        {            
            var requests = await _requestRepository.GetRequestsAsync();

            return requests.Select(RequestResponseModel.FromUser).ToList();
        }

        public async Task<List<RequestResponseModel>> GetRequestsForAgentAsync(int agentId)
        {
            var response = await _requestRepository.GetRequestForAgentAsync(agentId);

            return response.Select(RequestResponseModel.FromUser).ToList();
        }

        public async Task<List<RequestResponseModel>> GetRequestsForCustomerAsync(int customerId)
        {
            var response = await _requestRepository.GetRequestForCustomerAsync(customerId);

            return response.Select(RequestResponseModel.FromUser).ToList();
        }

        public async Task UploadAsync(int requestId, IFormFile formFile)
        {
            //sa mai verific odata aici ca e ok

            var request = _requestRepository.GetByIdAsync(requestId);

            await _storageService.SaveFileAsync(formFile);
        }

        public async Task<RequestResponseModel> UpdateRequestsStatusAsync(int requestId, RequestStatus status, int agentId)
        {
            var requestFromDb = await _requestRepository.GetByIdAsync(requestId);

            if (requestFromDb == null)
            {
                throw new Exception("Request not fount");
            }

            if (requestFromDb != null) 
            {
                requestFromDb.Status = status;
                requestFromDb.AgentId = agentId;
                requestFromDb.UpdatedAt = DateTime.UtcNow;

                await _requestRepository.UpdateAsync(requestId);

                var updatedRequestModel = new RequestResponseModel
                {
                    Id = requestId,
                    CustomerId= requestFromDb.CustomerId,
                    AgentId = agentId,
                    Status = status,
                    UpdatedAt = DateTime.UtcNow
                };

                return updatedRequestModel;
            }

            return null;
        }
       
        public async Task<RespondToRequestModel> RespondToRequestAsync(int requestId, RespondToRequestModel model)
        {
            var request = await _requestRepository.GetByIdAsync(requestId);

            model.ApplyToRequest(request);
            
            if (!string.IsNullOrWhiteSpace(model.AgentMessage))
            {
                request.Status = RequestStatus.Completed;
            }

            await _requestRepository.UpdateMessageAsync(request);

            return model;
        }
    }
}