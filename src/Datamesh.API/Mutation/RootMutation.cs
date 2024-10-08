using Datamesh.API.Query;

namespace Datamesh.API.Mutation
{
    public class RootMutation
    {
        public MasterMutation MasterMutation { get; set; }
        public TransactionMutation TransactionMutation { get; set; }
        public RootMutation()
        {
            MasterMutation = new MasterMutation();
            TransactionMutation = new TransactionMutation();
        }
    }

    public class MutationResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? ResultId { get; set; }
    }
}
