using GetInto.Domain.Identity;

namespace GetInto.Domain
{
    public class Human
    {
        public long Id { get; set; }
        public string MiniCurriculum { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public IEnumerable<SocialLink> SocialLinks { get; set; }
        public IEnumerable<HumanProject> HumansProjects { get; set; }
    }
}
