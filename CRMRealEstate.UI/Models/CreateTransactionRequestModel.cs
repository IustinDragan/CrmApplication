using CRMRealEstate.DataAccess.Enums;

namespace CRMRealEstate.UI.Models
{
    public class CreateTransactionRequestModel
    {
        public int PropertyId { get; set; }
        public int AgentId { get; set; }
        public double Price { get; set; }
        public TransactionType TypeOfTransaction { get; set; }


        //public Transaction ToTransaction()
        //{
        //    return new Transaction
        //    {
        //        PropertyId = PropertyId,
        //        AgentId = AgentId,
        //        Date = DateTime.UtcNow,
        //        Price = Price,
        //        Status = TransactionStatusEnum.Pending,
        //        TypeOfTransaction = TypeOfTransaction
        //    };

        //}
    }
}
