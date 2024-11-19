namespace Datamesh.API.Query
{
    public class DataFlow
    {
        public Flow Flow01()
        {
            return new Flow();
        }
    }
    public class FlowBase
    {
        public int ID { get; set; } = new Random().Next();
        //public virtual string SubID() => this.ID.ToString();
        public string Desc { get; set; }
    }
    public class Flow : FlowBase
    {
        public int ID { get; set; } = new Random().Next();
        public FlowStep FlowStep01 { get; set; } = new FlowStep();
    }

    public class FlowStep : FlowBase
    {
        public string Query { get; set; } = "Hello Query";
        public StepProcess Process { get; set; } = new StepProcess();
    }

    public class StepProcess : FlowBase
    {
        public (string a, string b) Query() => new("a", "b");
        
        public (string a, string b) Process() => new("a", "b");
        public (string a, string b) Return() => new("a", "b");

        public FlowStep NextFlowStep { get; set; } = new FlowStep();
        public FlowStep PreFlowStep { get; set; } = new FlowStep();
    }

}
