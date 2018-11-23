using System;
using GloomChars.GameData.Interfaces;

namespace GloomChars.GameData.Models
{
    public class IgnoreItemEffects : IPerkAction
    {
        public PerkActionName ActionName { get => PerkActionName.IgnoreItemEffects; }
    }
}
