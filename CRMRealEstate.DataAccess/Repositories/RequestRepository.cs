using CRMRealEstate.DataAccess.Entities;
using CRMRealEstate.DataAccess.Repositories.Interfaces;
using CRMRealEstate.DataAccess.Scripts;
using Microsoft.EntityFrameworkCore;

namespace CRMRealEstate.DataAccess.Repositories
{
    public class RequestRepository : IRequestRepository
    {

        private readonly DatabaseContext _databaseContext;

        public RequestRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public async Task<Request> CreateAsync(Request request)
        {
            _databaseContext.Requests.Add(request);
            await _databaseContext.SaveChangesAsync();
            return request;
        }

        public async Task DeleteAsync(int id)
        {
            var requestToDelete = await GetByIdAsync(id);
            
            if (requestToDelete == null) 
            {
                throw new KeyNotFoundException($"Request with ID {id} not found.");
            }

            _databaseContext.Requests.Remove(requestToDelete);

            await _databaseContext.SaveChangesAsync();
        }

        public async Task<Request> GetByIdAsync(int id)
        {
            return await _databaseContext.Requests
                .Include(x => x.Customer)
                .Include(x => x.Agent)
                .Include(x =>x.Announcement)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Request>> GetRequestForAgentAsync(int agentId)
        {
            return await _databaseContext.Requests
                .Include(x => x.Customer)
                .Include(x => x.Agent)
                .Include(x => x.Announcement)
                .Where(x => x.AgentId == agentId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Request>> GetRequestForCustomerAsync(int customerId)
        {
            return await _databaseContext.Requests
                .Include(x => x.Customer)
                .Include(x => x.Agent)
                .Include(x => x.Announcement)
                .Where(x=> x.CustomerId == customerId)
                .OrderByDescending(x =>x.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Request>> GetRequestsAsync()
        {
            return await _databaseContext.Requests
                .Include(x => x.Customer)
                .Include(x => x.Agent)
                .Include(x => x.Announcement)
                .OrderByDescending(x=>x.CreatedAt).ToListAsync();
        }

        //public async Task<Request> UpdateAsync(Request request)
        //{
        //    _databaseContext.Requests.Update(request);
        //    await _databaseContext.SaveChangesAsync();
        //    return request;
        //}


        public async Task<Request> UpdateAsync(int id)
        {
            var updatedEntity = await _databaseContext.Requests.Where(x => x.Id == id).FirstAsync();

            await _databaseContext.SaveChangesAsync();

            return updatedEntity;
        }
    }
}
