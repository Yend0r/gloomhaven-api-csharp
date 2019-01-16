using System;
using System.Collections.Generic;
using GloomChars.GameData.Models;

namespace GloomChars.Characters.Models
{
    public class ModifierDeck
    {
        public int TotalCards { get; set; }
        public ModifierCard CurrentCard { get; set; }
        public List<ModifierCard> Discards { get; set; }

        public ModifierDeck() { }

        public ModifierDeck(int totalCards, ModifierCard currentCard, List<ModifierCard> discards)
        {
            TotalCards = totalCards;
            CurrentCard = currentCard;
            Discards = discards;
        }

        public ModifierDeck(int totalCards)
           => new ModifierDeck(totalCards, null, new List<ModifierCard>());
    }
}
