using System;
using System.Collections.Generic;
using System.Linq;
using GloomChars.Characters.Models;
using GloomChars.Characters.Interfaces;
using GloomChars.GameData.Interfaces;
using GloomChars.GameData.Models;

namespace GloomChars.Characters.Services
{
    public class DeckService : IDeckService
    {
        readonly IDeckRepository _deckRepo;
        readonly IGameDataService _gameData;

        public DeckService(IDeckRepository deckRepo, IGameDataService gameData)
        {
            _deckRepo = deckRepo;
            _gameData = gameData;
        }

        public ModifierDeck GetDeck(Character character)
        {
            List<ModifierCard> discards = _deckRepo.GetDiscards(character.Id);

            List<ModifierCard> fullDeck = GetFullDeck(character.ClaimedPerks);

            if (discards.Any())
            {
                return new ModifierDeck(fullDeck.Count, discards.First(), discards.Skip(1).ToList());
            }

            return new ModifierDeck(fullDeck.Count);
        }

        public ModifierDeck DrawCard(Character character)
        {
            List<ModifierCard> discards = _deckRepo.GetDiscards(character.Id);

            List<ModifierCard> fullDeck = GetFullDeck(character.ClaimedPerks);
            var totalCards = fullDeck.Count;

            //Remove discards
            foreach (ModifierCard card in discards)
            {
                fullDeck.Remove(card);
            }

            //Select random card 
            if (fullDeck.Count > 0)
            {
                var rnd = new Random();
                var index = rnd.Next(fullDeck.Count);
                var currentCard = fullDeck.ElementAt(index);

                _deckRepo.InsertDiscard(character.Id, currentCard);

                return new ModifierDeck(totalCards, currentCard, discards);
            }
            
            return new ModifierDeck(totalCards, discards.First(), discards.Skip(1).ToList());
        }

        public ModifierDeck Reshuffle(Character character)
        {
            _deckRepo.DeleteDiscards(character.Id);
            List<ModifierCard> fullDeck = GetFullDeck(character.ClaimedPerks);
            return new ModifierDeck(fullDeck.Count);
        }

        private List<ModifierCard> GetFullDeck(List<Perk> perks)
        {
            List<ModifierCard> startingDeck = GetStartingDeck();
            return ApplyPerks(startingDeck, perks);
        }

        private List<ModifierCard> GetStartingDeck()
        {
            var deck = new List<ModifierCard>
            {
                new ModifierCard(0, CardAction.MultiplyDamage, 2, false, true),
                new ModifierCard(0, CardAction.Miss, null, false, true),
                ModifierCard.DamageCard(-2),
                ModifierCard.DamageCard(2)
            };

            for (int i = 1; i <= 5; i++)
            {
                deck.Add(ModifierCard.DamageCard(-1));
                deck.Add(ModifierCard.DamageCard(1));
            }

            for (int i = 1; i <= 6; i++)
            {
                deck.Add(ModifierCard.DamageCard(0));
            }

            return deck;
        }

        private List<ModifierCard> ApplyPerks(List<ModifierCard> deck, List<Perk> perks)
        {
            var newDeck = deck;

            foreach (Perk perk in perks)
            {
                for (int i = 0; i < perk.Quantity; i++)
                {
                    foreach (IPerkAction perkAction in perk.Actions)
                    {
                        newDeck = ApplyPerkAction(newDeck, perkAction);
                    }
                }
            }
            
            return newDeck;
        }

        private List<ModifierCard> ApplyPerkAction(List<ModifierCard> deck, IPerkAction perkAction)
        {
            var newDeck = deck;

            switch(perkAction)
            {
                case AddCard addCard:
                    for (int i = 0; i < addCard.NumCards; i++)
                    {
                        newDeck.Add(addCard.Card);
                    }
                    break;
                case RemoveCard removeCard:
                    for (int i = 0; i < removeCard.NumCards; i++)
                    {
                        newDeck.Remove(removeCard.Card);
                    }
                    break;
            }

            return newDeck;
        }
    }
}
