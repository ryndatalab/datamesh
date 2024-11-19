namespace Datamesh.UI.Web.Components.Model
{ 
    public class RootTransaction
    {
        public TransactionMutation transactionMutation { get; set; }
    }

    public class TransactionMutation
    {
        public MutionResponse transfer { get; set; }
    }

    public class MutionResponse
    {
        public string message { get; set; }
        public string resultId { get; set; }
        public bool success { get; set; }
    }
}
