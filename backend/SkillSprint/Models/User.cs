using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SkillSprint.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool isActive { get; set; } = true;
        [NotMapped]
        public string? token { get; set; }
    }
}
