﻿namespace GetInto.Domain
{
    public class Human
    {
        public long Id { get; set; }
        public string MiniCurriculum { get; set; }
        public IEnumerable<HumanProject> HumansProjects { get; set; }

        // TODO: Social Media for Human
        // TODO: User
    }
}
