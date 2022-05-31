namespace GetInto.Domain
{
    public class Project
    {
        public long Id { get; set; }
        public string Location { get; set; }
        public DateTime? StartDate { get; set; }
        public string Title { get; set; }
        public string ImageURL { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public IEnumerable<Job> Jobs { get; set; }
        public IEnumerable<SocialLink> SocialLinks { get; set; }
        public IEnumerable<HumanProject> HumansProjects { get; set; }
    }
}