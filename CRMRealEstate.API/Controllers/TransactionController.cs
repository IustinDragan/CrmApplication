using CRMRealEstate.Application.Models.TransactionsModel;
using CRMRealEstate.Application.Services.Interfaces;
using CRMRealEstate.DataAccess.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CRMRealEstate.API.Controllers
{
    [ApiController]
    [Route("Transaction")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task<IActionResult> FinalizeTransactionAsync([FromBody] CreateTransactionRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _transactionService.FinalizeTransactionAsync(model);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransactionsAsync()
        {
            var transactions = await _transactionService.ReadAllTransactionsAsync();
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            return Ok(transaction);
        }

        [HttpGet("byAgentId/{agentId}")]
        public async Task<IActionResult> GetTransactionByAgentIdAsync(int agentId) 
        {
            var transaction = await _transactionService.GetByAgentIdAsync(agentId);
            return Ok(transaction);
        }

        [HttpGet("{byMonth}")]
        public async Task<IActionResult> GetTransactionByMonth([FromQuery] int year,  [FromQuery] int month)
        {
            var result = await _transactionService.GetByMonthAsync(year, month);
            return Ok(result);
        }

        [HttpGet("{byStatus}")]
        public async Task<IActionResult> GetTransactionByStatus([FromQuery] TransactionStatusEnum status)
        {
            var result = await _transactionService.GetByStatusAsync(status);
            return Ok(result);
        }

        [HttpGet("totalValue/{agentId}")]
        public async Task<IActionResult> GetTotalValueByAgent(int agentId)
        {
            var result = await _transactionService.GetTotalValueByAgentIdAsync(agentId);
            return Ok(result);
        }

        [HttpGet("totalSalesValue/{agentId}")]
        public async Task<IActionResult> GetTotalSalesPropertyCountByAgent(int agentId)
        {
            var result = await _transactionService.GetTotalPropertyCountByAgentIdAsync(agentId);
            return Ok(result);
        }

        [HttpGet("{propertyId}")]
        public async Task<IActionResult> GetTransactionByPropertyId(int propertyId)
        {
            var result = _transactionService.GetByPropertyIdAsync(propertyId);
            return Ok(result);
        }

        [HttpGet("totalSalesCount/{agentId}")]
        public async Task<IActionResult> GetAllPropertyCountByAgentAsync(int agentId)
        {
            var result = _transactionService.GetTotalPropertyCountByAgentIdAsync(agentId);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransactionById(int id)
        {
            await _transactionService.DeleteTransactionAsync(id);
            return NoContent();
        }

        //[HttpGet("report")]
        //public async Task<IActionResult> GetReport([FromQuery] string? agentName, [FromQuery] int? year, [FromQuery] int? month, [FromQuery] string? type)
        //{
        //    var all = await _transactionService.ReadAllTransactionsAsync();

        //    if (!string.IsNullOrEmpty(agentName))
        //        all = all.Where(t => t.AgentName.Contains(agentName, StringComparison.OrdinalIgnoreCase)).ToList();

        //    if (year.HasValue && month.HasValue)
        //        all = all.Where(t => t.Date.Year == year && t.Date.Month == month).ToList();

        //    if (!string.IsNullOrEmpty(type))
        //        all = all.Where(t => t.TypeOfTransaction.ToString().Equals(type, StringComparison.OrdinalIgnoreCase)).ToList();

        //    return Ok(all);
        //}

        [HttpGet("report")]
        public async Task<IActionResult> GetReport([FromQuery] int? agentId, [FromQuery] int? year, [FromQuery] int? month, [FromQuery] TransactionType? type)
        {
            var transactions = await _transactionService.ReadAllTransactionsAsync();

              transactions = transactions
                    .Where(t => t.AgentId != null && t.AgentId .Equals(agentId))
                    .ToList();

            if (year.HasValue && month.HasValue)
                transactions = transactions
                    .Where(t => t.Date?.Year == year && t.Date?.Month == month)
                    .ToList();

            if (type.HasValue)
                transactions = transactions
                    .Where(t => t.TypeOfTransaction == type)
                    .ToList();

            return Ok(transactions);
        }
    }
}