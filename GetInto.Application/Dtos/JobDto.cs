namespace GetInto.Application.Dtos
{
    public class JobDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal SalaryBonus { get; set; }
        public DateTime? StartDate { get; set; }
        public int Amount { get; set; }
        public long ProjectId { get; set; }
        public ProjectDto Project { get; set; }
    }
}
