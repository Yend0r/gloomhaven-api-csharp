using System;
using GloomChars.GameData.Interfaces;

namespace GloomChars.GameData.Models
{
    public class IgnoreScenarioEffects : IPerkAction
    {
        public PerkActionName ActionName { get => PerkActionName.IgnoreItemEffects; }
    }
}
