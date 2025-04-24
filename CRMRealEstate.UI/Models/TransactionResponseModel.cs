using CRMRealEstate.DataAccess.Enums;

namespace CRMRealEstate.UI.Models
{
    public class TransactionResponseModel
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string PropertyTitle { get; set; }
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public TransactionStatusEnum Status { get; set; } = TransactionStatusEnum.Pending;
        public TransactionType TypeOfTransaction { get; set; }
    }
}
