using CRMRealEstate.DataAccess.Entities;
using CRMRealEstate.DataAccess.Enums;

namespace CRMRealEstate.Application.Models.TransactionsModel
{
    public class TransactionResponseModel
    {
        public int? Id { get; set; }
        public int? PropertyId { get; set; }
        public string? PropertyTitle { get; set; }
        public int? AgentId { get; set; }
        public string? AgentName { get; set; }
        public DateTime? Date { get; set; }
        public double? Price { get; set; }
        public TransactionStatusEnum? Status { get; set; } = TransactionStatusEnum.Pending;
        public TransactionType? TypeOfTransaction { get; set; }

        public static TransactionResponseModel FromTransaction(Transaction transaction)
        {
            return new TransactionResponseModel
            {
                Id = transaction.Id,
                PropertyId = transaction.PropertyId,
                PropertyTitle = transaction.Property?.Announcement?.Title ?? "Nu exista acest anunt",
                AgentId = transaction.AgentId,
                AgentName = transaction.Agent != null ? $"{transaction.Agent.FirstName} {transaction.Agent.LastName}" : "Nedefinit",
                Date = transaction.Date,
                Price = transaction.Price,
                Status = transaction.Status,
                TypeOfTransaction = transaction.TypeOfTransaction,

            };
        }
    }
}
