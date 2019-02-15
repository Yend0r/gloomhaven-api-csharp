using System;
namespace GloomChars.Characters.Repositories.Models
{
    public class DbScenario
    {
        public int Id { get; set; }
        public int CharacterId { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Experience { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime DateLastEvent { get; set; }
    }
}
