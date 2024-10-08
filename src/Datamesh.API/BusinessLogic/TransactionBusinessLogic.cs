using Org.Eclipse.TractusX.Portal.Backend.PortalBackend.DBAccess;
using PortalBackend.DBAccess.Repositories.IsoPay;
using PortalBackend.PortalEntities.Enums.IsoPay;
using PortalBackend.PortalEntities.EntitiesIsoPay;
using PortalBackend.DBAccess.Models.IsoPay;

namespace Datamesh.APIBusinessLogic
{
    public interface ITransactionBusinessLogic
    {
        Task Transfer(TransferDto transferDto);
    }

    public class TransactionBusinessLogic : ITransactionBusinessLogic
    {
        private readonly IPortalRepositories _portalRepositories;
        private readonly ILogger<TransactionBusinessLogic> _logger;

        public TransactionBusinessLogic(IPortalRepositories portalRepositories, ILogger<TransactionBusinessLogic> logger)
        {
            _portalRepositories = portalRepositories;
            _logger = logger;
        }
        public async Task Transfer(TransferDto transferDto)
        {

            string senderAcc = transferDto.SenderAcc;
            string recieverAcc = transferDto.RecieverAcc;
            decimal amt = transferDto.Amt;
            string currency = transferDto.Currency;

            IIsoPayRepository isoPayRepository = _portalRepositories.GetInstance<IIsoPayRepository>();
            var accounts = await isoPayRepository.GetAccountsAsync(senderAcc, recieverAcc);
            var senderAccDetail = accounts.Where(a => a.AccNumber == senderAcc).FirstOrDefault();
            var recieverAccDetail = accounts.Where(a => a.AccNumber == recieverAcc).FirstOrDefault();

            decimal funCheckPreTransaction = accounts.Sum(x => x.Balance);

            if (senderAccDetail != null && recieverAccDetail != null)//&& senderAccDetail.Balance > amt)
            {

                await Transact(senderAcc, amt, currency, TransactionTypeId.Debit, senderAccDetail.Balance);
                isoPayRepository.UpdateBalance(senderAccDetail.Id, senderAccDetail.Balance - amt);
                // isoPayRepository.AttachAndModifyAccount(senderAccDetail.Id, (acc) => { acc.Balance = senderAccDetail.Balance - amt;}, null) ;

                await Transact(recieverAcc, amt, currency, TransactionTypeId.Credit, recieverAccDetail.Balance);
                //isoPayRepository.AttachAndModifyAccount(recieverAccDetail.Id, (acc) => { acc.Balance = recieverAccDetail.Balance + amt; }, null);
                isoPayRepository.UpdateBalance(recieverAccDetail.Id, recieverAccDetail.Balance + amt);

                isoPayRepository.RecordTransaction(senderAccDetail, recieverAccDetail, transferDto);

                await _portalRepositories.SaveAsync();
            }

            accounts = await isoPayRepository.GetAccountsAsync(senderAcc, recieverAcc).ConfigureAwait(false);
            decimal funCheckPostTransaction = accounts.Sum(x => x.Balance);

            if (funCheckPostTransaction != funCheckPreTransaction)
                throw new Exception("fund transfer major issue.");
        }
        private async Task Transact(string acc, decimal amt, string currency, TransactionTypeId transactionType, decimal amtBal)
        {
            Console.WriteLine($"Started acc:{acc} amt: {amt} bal: {amtBal} {currency} trasaction: {transactionType}");
            // transact
            switch (transactionType)
            {
                case TransactionTypeId.Credit:
                    {
                        amtBal += amt;
                    }
                    break;
                case TransactionTypeId.Debit:
                    amtBal -= amt;
                    break;
                default:
                    break;
            }
            Console.WriteLine($"Done acc:{acc} amt: {amt} bal: {amtBal} {currency}trasaction: {transactionType}");
        }
    }


}
