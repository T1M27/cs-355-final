using Microsoft.EntityFrameworkCore;
using SkillSprint.Models;

namespace SkillSprint.Data
{
    public class SkillSprintContext : DbContext
    {
        public SkillSprintContext(DbContextOptions<SkillSprintContext> options) : base(options) { }
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<User> Users {  get; set; }
    }
}
