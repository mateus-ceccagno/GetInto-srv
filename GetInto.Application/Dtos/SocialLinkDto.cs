namespace GetInto.Application.Dtos
{
    public class SocialLinkDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public long? ProjectId { get; set; }
        public ProjectDto Project { get; set; }
        public long? HumanId { get; set; }
        public HumanDto Human { get; set; }
    }
}
