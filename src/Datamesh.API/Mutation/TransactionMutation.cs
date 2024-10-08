using Datamesh.APIBusinessLogic;
using PortalBackend.DBAccess.Models.IsoPay;

namespace Datamesh.API.Mutation
{
    public class TransactionMutation
    {
        public async Task<MutationResult> Transfer(
             [Service] ITransactionBusinessLogic transactionBusinessLogic,
            [Service] PortalDbContext _context,
            TransferDto transferDto)
        {
            await transactionBusinessLogic.Transfer(transferDto);
            return new MutationResult() { Success = true, Message = "added", ResultId = "".ToString() };
        }
    }
}
