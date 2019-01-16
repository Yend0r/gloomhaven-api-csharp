using System;
using GloomChars.GameData.Models;

namespace GloomChars.Api.Decks
{
    public class CardViewModel
    {
        public int Damage { get; set; }
        public bool DrawAnother { get; set; }
        public bool Reshuffle { get; set; }
        public string Action { get; set; }
        public int? ActionAmount { get; set; }

        public CardViewModel(ModifierCard card)
        {
            Damage = card.Damage;
            DrawAnother = card.DrawAnother;
            Reshuffle = card.Reshuffle;
            Action = card.Action.ToString();
            ActionAmount = card.ActionAmount; 
        }
    }
}
