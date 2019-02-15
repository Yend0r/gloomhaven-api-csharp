using System;
namespace GloomChars.Characters.Models
{
    public class ScenarioState
    {
        public ScenarioInfo Info { get; set; }
        public ScenarioCharacterStats CharacterStats  { get; set; }
        public ModifierDeck ModifierDeck { get; set; }

        public ScenarioState(ScenarioInfo info, ScenarioCharacterStats stats, ModifierDeck deck)
        {
            Info = info;
            CharacterStats = stats;
            ModifierDeck = deck;
        }
    }
}
