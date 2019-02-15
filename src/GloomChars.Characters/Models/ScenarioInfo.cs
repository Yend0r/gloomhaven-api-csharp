using System;
namespace GloomChars.Characters.Models
{
    public class ScenarioInfo
    {
        public int Id { get; set; }
        public int CharacterId { get; set; }
        public string Name { get; set; }
        public int MaxHealth { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime DateLastEvent { get; set; }
    }
}
