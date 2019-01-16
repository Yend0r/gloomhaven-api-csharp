using System;
using System.Collections.Generic;
using GloomChars.GameData.Interfaces;

namespace GloomChars.GameData.Models
{
    public class PerkBuilder
    {
        private Perk _perk { get; set; }

        public PerkBuilder(string id, int qty)
        {
            _perk = new Perk(id, qty);
        }

        public static PerkBuilder Create(string id, int qty)
        {
            return new PerkBuilder(id, qty);
        }

        public PerkBuilder AddCard(int numCards, CardAction cardAction, int? actionAmnt, int dmg, bool draw)
        {
            var card = new ModifierCard(dmg, cardAction, actionAmnt, draw, false);
            var perkAction = new AddCard { NumCards = numCards, Card = card };
            _perk.Actions.Add(perkAction);

            return this;
        }

        public PerkBuilder AddCard(int numCards, CardAction cardAction, int dmg, bool draw) =>
            this.AddCard(numCards, cardAction, null, dmg, draw);

        public PerkBuilder RemoveCard(int numCards, CardAction cardAction, int? actionAmnt, int dmg, bool draw)
        {
            var card = new ModifierCard(dmg, cardAction, actionAmnt, draw, false);
            var perkAction = new RemoveCard { NumCards = numCards, Card = card };
            _perk.Actions.Add(perkAction);

            return this;
        }

        public PerkBuilder RemoveCard(int numCards, CardAction cardAction, int dmg, bool draw) =>
            this.RemoveCard(numCards, cardAction, null, dmg, draw);

        public PerkBuilder IgnoreItems()
        {
            var perkAction = new IgnoreItemEffects();
            _perk.Actions.Add(perkAction);

            return this;
        }

        public PerkBuilder IgnoreScenario()
        {
            var perkAction = new IgnoreScenarioEffects();
            _perk.Actions.Add(perkAction);

            return this;
        }

        public Perk Build()
        {
            return _perk;
        }
    }
}
