namespace GetInto.Domain
{
    public class HumanProject
    {
        public long HumanId { get; set; }
        public Human Human { get; set; }
        public long ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
