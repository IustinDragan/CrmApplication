using CRMRealEstate.DataAccess.Enums;

namespace CRMRealEstate.Application.Models.RequestsModel
{
    public class UpdateStatusRequestModel
    {
        public RequestStatus newStatus { get; set; }
    }
}
