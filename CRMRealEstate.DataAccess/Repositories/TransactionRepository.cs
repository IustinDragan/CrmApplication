using CRMRealEstate.DataAccess.Entities;
using CRMRealEstate.DataAccess.Enums;
using CRMRealEstate.DataAccess.Repositories.Interfaces;
using CRMRealEstate.DataAccess.Scripts;
using Microsoft.EntityFrameworkCore;

namespace CRMRealEstate.DataAccess.Repositories
{
    public class TransactionRepository : ITransactionsRepository
    {
        private readonly DatabaseContext _databaseContext;

        public TransactionRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Transaction> AddTransactionAsync(Transaction transaction)
        {
            var addedEntity = await _databaseContext.Transactions.AddAsync(transaction);

            await _databaseContext.SaveChangesAsync();

            return addedEntity.Entity;
        }

        public Task<Transaction> UpdateTransactionAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Transaction>> ReadAllAsync()
        {
            return await _databaseContext.Transactions
                .Include(t => t.Property)
                    .ThenInclude(p => p.Announcement)
                .Include(t => t.Agent)
                .ToListAsync();
        }

        public async Task<Transaction?> GetByIdAsync(int id)
        {
            return await _databaseContext.Transactions
                .Include(t => t.Property)
                .Include(t => t.Agent)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Transaction>> GetByAgentIdAsync(int agentId)
        {
            return await _databaseContext.Transactions
                .Where(t => t.AgentId == agentId)
                .Include(t => t.Property)
                .ToListAsync();                
        }

        public async Task<List<Transaction>> GetByPropertyIdAsync(int propertyId)
        {
            return await _databaseContext.Transactions
                .Where(t => t.PropertyId == propertyId)
                .ToListAsync();
        }

        public Task<Transaction?> GetByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteTransactionAsync(int id)
        {
            var transaction = await GetByIdAsync(id);

            _databaseContext.Transactions.Remove(transaction);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<List<Transaction>> GetByMonthAsync(int year, int month)
        {
            return await _databaseContext.Transactions
                .Where(t => t.Date.Year == year && t.Date.Month == month)
                .Include(t => t.Property)
                .Include(t => t.Agent)
                .ToListAsync();
        }

        public async Task<List<Transaction>> GetByStatusAsync(TransactionStatusEnum status)
        {
            return await _databaseContext.Transactions
                .Where(t => t.Status == status)
                .ToListAsync();
        }

        public async Task<double> GetTotalValueByAgentIdAsync(int agentId)
        {
            return await _databaseContext.Transactions
                .Where(t => t.AgentId == agentId && t.Status == TransactionStatusEnum.Completed)
                .SumAsync(t => (double)t.Price);
        }

        public async Task<int> GetTotalPropertyCountByAgentIdAsync(int agentId)
        {
            return await _databaseContext.Transactions
                .Where(t=> t.AgentId == agentId && t.Status == TransactionStatusEnum.Completed)
                .CountAsync(t => t.Status == TransactionStatusEnum.Completed);
        }

        public async Task<double> GetTotalAmountByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _databaseContext.Transactions
                .Where(t => t.Date >= startDate && t.Date <= endDate)
                .SumAsync(t => (double)t.Price);
        }

        public async Task<Dictionary<string, int>> GetTransactionCountByAgentAsync()
        {
            return await _databaseContext.Transactions
                .Include(t =>t.Agent)
                .GroupBy(t => t.Agent.UserName)
                .ToDictionaryAsync(x => x.Key, x => x.Count());

            //.Select(g => new { AgentName = g.Key, Count = g.Count() })
        }

        //public async Task<Dictionary<string, double>> GetMonthlyTotalsAsync()
        //{
        //    return await _databaseContext.Transactions
        //        .GroupBy(t => new { t.Date.Year, t.Date.Month })
        //        .Select(g => new
        //        {
        //            Month = $"{g.Key.Month:00}/{g.Key.Year}",
        //            Total = g.Sum(x => x.Price)
        //        })
        //        .ToDictionaryAsync(x => x.Month, x => x.Total);
        //}

        public async Task<Dictionary<string, double>> GetMonthlyTotalsAsync()
        {
            return await _databaseContext.Transactions
                .GroupBy(t => t.Date.ToString("yyyy-MM"))
                .OrderBy(g => g.Key)
                .ToDictionaryAsync(g => g.Key, g => g.Sum(t => t.Price));
        }

        public async Task<double> GetTotalAmountAsync(DateTime startDate, DateTime endDate)
        {
            var adjustedStart = startDate.Date;
            var adjustedEnd = endDate.Date.AddDays(1).AddTicks(-1);

            return await _databaseContext.Transactions
                .Where(t => t.Date >= adjustedStart && t.Date <= adjustedEnd)
                .SumAsync(t => t.Price);
        }
    }
}
