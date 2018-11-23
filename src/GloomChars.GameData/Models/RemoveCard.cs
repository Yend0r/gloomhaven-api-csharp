using System;
using GloomChars.GameData.Interfaces;

namespace GloomChars.GameData.Models
{
    public class RemoveCard : IPerkAction
    {
        public PerkActionName ActionName { get => PerkActionName.RemoveCard; }
        public int NumCards { get; set; }
        public ModifierCard Card { get; set; }
    }
}
