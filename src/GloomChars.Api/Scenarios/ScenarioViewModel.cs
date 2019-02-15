using System;
using GloomChars.Characters.Models;

namespace GloomChars.Api.Scenarios
{
    public class ScenarioViewModel
    {
        public int CharacterId { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Experience { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime DateLastEvent { get; set; }
        public DeckViewModel ModifierDeck { get; set; }

        public ScenarioViewModel(ScenarioState scenario)
        {
            CharacterId = scenario.Info.CharacterId;
            Name = scenario.Info.Name;
            Health = scenario.CharacterStats.Health;
            MaxHealth = scenario.Info.MaxHealth;
            Experience = scenario.CharacterStats.Experience;
            DateStarted = scenario.Info.DateStarted;
            DateLastEvent = scenario.Info.DateLastEvent;
            ModifierDeck = new DeckViewModel(scenario.ModifierDeck); 
        }
    }
}
