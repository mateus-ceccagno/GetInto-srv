namespace GetInto.Application.Dtos
{
    public class HumanAddDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public UserDto UserDto { get; set; }
        public string MiniCurriculum { get; set; }
    }
}
