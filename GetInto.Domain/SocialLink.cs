namespace GetInto.Domain
{
    public class SocialLink
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public long? ProjectId { get; set; }
        public Project Project { get; set; }
        public long? HumanId { get; set; }
        public Human Human { get; set; }
    }
}
