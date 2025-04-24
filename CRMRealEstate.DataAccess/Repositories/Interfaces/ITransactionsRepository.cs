using CRMRealEstate.DataAccess.Entities;
using CRMRealEstate.DataAccess.Enums;

namespace CRMRealEstate.DataAccess.Repositories.Interfaces
{
    public interface ITransactionsRepository
    {
        Task<Transaction> AddTransactionAsync(Transaction transaction);
        Task<Transaction> UpdateTransactionAsync(int id);
        Task<List<Transaction>> ReadAllAsync();
        Task<Transaction?> GetByIdAsync(int id);
        Task<List<Transaction>> GetByAgentIdAsync(int agentId);
        Task<List<Transaction>> GetByPropertyIdAsync(int propertyId);
        Task<Transaction?> GetByUsernameAsync(string username);
        Task DeleteTransactionAsync(int id);
        Task<List<Transaction>> GetByMonthAsync(int year, int month);
        Task<List<Transaction>> GetByStatusAsync(TransactionStatusEnum status);
        Task<double> GetTotalValueByAgentIdAsync(int agentId);
        Task<int> GetTotalPropertyCountByAgentIdAsync(int agentId);
    }
}