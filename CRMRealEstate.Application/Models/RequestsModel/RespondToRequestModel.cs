using CRMRealEstate.DataAccess.Entities;

namespace CRMRealEstate.Application.Models.RequestsModel
{
    public class RespondToRequestModel
    {
        public int RequestId { get; set; }
        public string? AgentMessage { get; set; }
        //public int AgentId {  get; set; }

        public void ApplyToRequest(Request request)
        {
            {
                request.UpdatedAt = DateTime.UtcNow;
                request.AgentMessage = this.AgentMessage;
                //request.AgentId = this.AgentId;
            }
            ;
        }
    }
}
