using System;
using System.Linq;
using System.Collections.Generic;
using GloomChars.Characters.Models;

namespace GloomChars.Api.Decks
{
    public class DeckViewModel
    {
        public int TotalCards { get; set; }
        public CardViewModel CurrentCard { get; set; }
        public List<CardViewModel> Discards { get; set; }

        public DeckViewModel(ModifierDeck deck)
        {
            TotalCards = deck.TotalCards;
            CurrentCard = deck.CurrentCard != null ? new CardViewModel(deck.CurrentCard) : null;
            Discards = deck.Discards.Select(c => new CardViewModel(c)).ToList();
        }
    }
}
