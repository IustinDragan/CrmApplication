using CRMRealEstate.DataAccess.Enums;

namespace CRMRealEstate.DataAccess.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public Property Property { get; set; }

        public int AgentId { get; set; }
        public Users Agent { get; set; }

        public DateTime Date { get; set; }
        public double Price { get; set; }

        public TransactionStatusEnum Status { get; set; } = TransactionStatusEnum.Pending;
        
        public TransactionType TypeOfTransaction { get; set; }
    }
}
