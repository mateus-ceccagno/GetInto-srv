namespace GetInto.Domain
{
    public class Job
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal SalaryBonus { get; set; }
        public DateTime? StartDate { get; set; }
        public int Amount { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
