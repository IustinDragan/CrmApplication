using CRMRealEstate.Application.Models.TransactionsModel;
using CRMRealEstate.Application.Services.Interfaces;
using CRMRealEstate.DataAccess.Enums;
using CRMRealEstate.DataAccess.Repositories;
using CRMRealEstate.DataAccess.Repositories.Interfaces;

namespace CRMRealEstate.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionsRepository _transactionsRepository;
        private readonly IAnnouncementRepository _announcementRepository;

        public TransactionService(ITransactionsRepository transactionsRepository, IAnnouncementRepository announcementRepository)
        {
            _transactionsRepository = transactionsRepository;
            _announcementRepository = announcementRepository;
        }

        public async Task DeleteTransactionAsync(int id)
        {
            await _transactionsRepository.DeleteTransactionAsync(id);
        }

        public async Task<TransactionResponseModel> FinalizeTransactionAsync(CreateTransactionRequestModel model)
        {
            if (model.Price <= 0)
            {
                throw new ArgumentException("Pretul transaztiei trebuie sa fie mai mare ca 0");
            }

            var transaction = model.ToTransaction();
            transaction.Status = TransactionStatusEnum.Completed;
            transaction.Date = DateTime.UtcNow;

            await _transactionsRepository.AddTransactionAsync(transaction);

            var announcement = await _announcementRepository.GetByPropertyIdAsync(model.PropertyId);

            if (announcement != null)
            {
                if (model.TypeOfTransaction == TransactionType.Sale)
                    announcement.IsSold = true;

                else if (model.TypeOfTransaction == TransactionType.Rent)
                    announcement.IsRent = true;

                await _announcementRepository.UpdateAsync(announcement);
            }

            return TransactionResponseModel.FromTransaction(transaction);
        }

        public async Task<List<TransactionResponseModel>> GetByAgentIdAsync(int agentId)
        {
            var transaction = await _transactionsRepository.GetByAgentIdAsync(agentId);
            return transaction.Select(TransactionResponseModel.FromTransaction).ToList();
        }

        public async Task<List<TransactionResponseModel>> GetByMonthAsync(int year, int month)
        {
            var transaction = await _transactionsRepository.GetByMonthAsync(year, month);
            return transaction.Select(TransactionResponseModel.FromTransaction).ToList();
        }

        public async Task<List<TransactionResponseModel>> GetByPropertyIdAsync(int propertyId)
        {
            var transaction = await _transactionsRepository.GetByPropertyIdAsync(propertyId);

            return transaction.Select(TransactionResponseModel.FromTransaction).ToList();
        }

        public async Task<List<TransactionResponseModel>> GetByStatusAsync(TransactionStatusEnum status)
        {
            var transaction = await _transactionsRepository.GetByStatusAsync(status);
            return transaction.Select(TransactionResponseModel.FromTransaction).ToList();
        }

        public async Task<Dictionary<string, double>> GetMonthlyTotalsAsync()
        {
            return await _transactionsRepository.GetMonthlyTotalsAsync();
        }

        public async Task<double> GetTotalAmountByDateRangeAsync(DateTime start, DateTime end)
        {
            return await _transactionsRepository.GetTotalAmountByDateRangeAsync(start, end);
        }

        public async Task<int> GetTotalPropertyCountByAgentIdAsync(int agentId)
        {
            return await _transactionsRepository.GetTotalPropertyCountByAgentIdAsync(agentId);
        }

        public async Task<double> GetTotalValueByAgentIdAsync(int agentId)
        {
            return await _transactionsRepository.GetTotalValueByAgentIdAsync(agentId);
        }

        public async Task<TransactionResponseModel> GetTransactionByIdAsync(int id)
        {
            var transaction = await _transactionsRepository.GetByIdAsync(id);

            if (transaction == null)
            {
                throw new Exception("Tranzactia nu a fost gasita");
            }

            return TransactionResponseModel.FromTransaction(transaction);
        }

        public async Task<Dictionary<string, int>> GetTransactionCountByAgentAsync()
        {
            return await _transactionsRepository.GetTransactionCountByAgentAsync();
        }

        public async Task<List<TransactionResponseModel>> ReadAllTransactionsAsync()
        {
            var transactions = await _transactionsRepository.ReadAllAsync();
            return transactions.Select(TransactionResponseModel.FromTransaction).ToList();
        }

        public async Task<double> GetTotalAmountAsync(DateTime startDate, DateTime endDate)  => await _transactionsRepository.GetTotalAmountAsync(startDate, endDate);

        public async Task<List<TransactionResponseModel>> GetCompletedTransactionsByAgentIdAsync(int agentId)
        {
            var transactions = await _transactionsRepository.GetCompletedByAgentIdAsync(agentId);
            return transactions.Select(TransactionResponseModel.FromTransaction).ToList();
        }
    }
}
