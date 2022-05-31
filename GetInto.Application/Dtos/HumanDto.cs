namespace GetInto.Application.Dtos
{
    public class HumanDto
    {
        public long Id { get; set; }
        public string MiniCurriculum { get; set; }
        public IEnumerable<ProjectDto> Projects { get; set; }
        public IEnumerable<SocialLinkDto> SocialLinks { get; set; }
        
        // TODO: User
    }
}
