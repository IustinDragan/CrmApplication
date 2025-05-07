using CRMRealEstate.Application.Models.TransactionsModel;
using CRMRealEstate.DataAccess.Enums;

namespace CRMRealEstate.Application.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionResponseModel> GetTransactionByIdAsync(int id);
        Task<List<TransactionResponseModel>> ReadAllTransactionsAsync();
        Task<List<TransactionResponseModel>> GetByAgentIdAsync(int agentId);
        Task<List<TransactionResponseModel>> GetByMonthAsync(int year, int month);
        Task<List<TransactionResponseModel>> GetByStatusAsync(TransactionStatusEnum status);
        Task<double> GetTotalValueByAgentIdAsync(int agentId);
        Task<TransactionResponseModel> FinalizeTransactionAsync(CreateTransactionRequestModel createTransactionRequestModel);
        Task<List<TransactionResponseModel>> GetByPropertyIdAsync(int propertyId);
        Task DeleteTransactionAsync(int id);
        Task<int> GetTotalPropertyCountByAgentIdAsync(int agentId);
        Task<double> GetTotalAmountByDateRangeAsync(DateTime start, DateTime end);
        Task<Dictionary<string, int>> GetTransactionCountByAgentAsync();
        Task<Dictionary<string, double>> GetMonthlyTotalsAsync();
        Task<double> GetTotalAmountAsync(DateTime startDate, DateTime endDate);

    }
}