using CRMRealEstate.Application.Models.RequestsModel;
using CRMRealEstate.DataAccess.Enums;
using Microsoft.AspNetCore.Http;

namespace CRMRealEstate.Application.Services.Interfaces
{
    public interface IRequestService
    {
        Task<RequestResponseModel> CreateRequestsAsync(CreateRequestModel requestModel);
        Task<RequestResponseModel> UpdateRequestsStatusAsync(int requestId, RequestStatus status, int agentId);
        Task DeleteRequestsAsync(int requestId);
        Task<List<RequestResponseModel>> GetAllRequestsAsync();
        Task<List<RequestResponseModel>> GetRequestsForCustomerAsync(int customerId);   
        Task<List<RequestResponseModel>> GetRequestsForAgentAsync(int agentId);
        Task UploadAsync(int requestId, IFormFile formFile);
    }
}
