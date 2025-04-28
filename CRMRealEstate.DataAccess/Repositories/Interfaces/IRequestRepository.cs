using CRMRealEstate.DataAccess.Entities;

namespace CRMRealEstate.DataAccess.Repositories.Interfaces
{
    public interface IRequestRepository
    {
        Task<List<Request>> GetRequestsAsync();
        Task<Request> CreateAsync(Request request);
        Task<Request> UpdateAsync(int id);
        Task DeleteAsync(int id);
        Task<Request> GetByIdAsync(int id);
        Task<List<Request>> GetRequestForCustomerAsync(int customerId);
        Task<List<Request>> GetRequestForAgentAsync(int agentId);
    }
}
