using System;
using GloomChars.GameData.Interfaces;

namespace GloomChars.GameData.Models
{
    public class AddCard : IPerkAction
    {
        public PerkActionName ActionName { get => PerkActionName.AddCard; }
        public int NumCards { get; set; }
        public ModifierCard Card { get; set; }
    }
}
