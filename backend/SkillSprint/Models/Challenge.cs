namespace SkillSprint.Models
{
    public class Challenge
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Difficulty { get; set; }
        public string Tags { get; set; }
        public string PostedBy { get; set; }
    }
}
