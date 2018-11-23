using System;
using System.Linq;
using System.Collections.Generic;
using GloomChars.GameData.Interfaces;

namespace GloomChars.GameData.Models
{
    public class Perk
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public List<IPerkAction> Actions { get; set; }

        public Perk(string id, int qty)
        {
            this.Id = id;
            this.Quantity = qty;
            this.Actions = new List<IPerkAction>();
        }

        public string ActionsToString()
        {   
            var actions = this.Actions.Select((action, idx) => GetActionText(idx, action));
            return string.Join(" ", actions);
        }

        private string NumText(int num)
        {
            switch(num) 
            {
                case 1: 
                    return "one";
                case 2: 
                    return "two";
                case 3: 
                    return "three";
                case 4: 
                    return "four";
                default: 
                    return "INFINITE";
            }
        }

        private string CardDmgText(ModifierCard card)
        {
            switch(card.Damage) 
            {
                case int x when x < 0: 
                    return x.ToString();
                case int x when x == 0 && card.Action != CardAction.Damage: 
                    return String.Empty;
                default: 
                    return $"+{card.Damage}";
            }
        }

        private string CardActionText(ModifierCard card)
        {
            var actionText = card.Action.ToString().ToUpper();

            switch(card.Action) 
            {
                case CardAction.Damage: 
                case CardAction.MultiplyDamage: 
                    return String.Empty;

                case CardAction.Push: 
                case CardAction.Pull: 
                case CardAction.Pierce: 
                case CardAction.Heal: 
                case CardAction.Shield: 
                    return $"{actionText} {card.ActionAmount}";

                case CardAction.RefreshItem:
                    return "REFRESH AN ITEM";

                case CardAction.AddTarget:
                    return "ADD TARGET";

                //Compiler won't catch missing cases (unlike F#) so just handle generically
                default: 
                    return actionText;
            }
        }

        private string CardDrawText(bool drawAnother) =>
            drawAnother ? "DRAW ANOTHER" : String.Empty;

        private string NumCardsText(int numCards) =>
            (numCards == 1) ? "card" : "cards";

        private string ActionText(int numCards, ModifierCard card)
        {
            var num = NumText(numCards);
            var cards = NumCardsText(numCards);
            var dmg = CardDmgText(card);
            var action = CardActionText(card);
            var draw = CardDrawText(card.DrawAnother);

            var items = new List<string> { num, dmg, action, draw, cards };

            var nonEmptyItems = items.Where(s => !string.IsNullOrWhiteSpace(s));

            return string.Join(" ", nonEmptyItems);
        }

        private string AddCardText(int idx, AddCard addCard)
        {
            var startText = (idx == 0) ? "Add" : "and add";
            var actionText = ActionText(addCard.NumCards, addCard.Card);
            return $"{startText} {actionText}";
        }

        private string RemoveCardText(int idx, RemoveCard removeCard)
        {
            var startText = (idx == 0) ? "Remove" : "and remove";
            var actionText = ActionText(removeCard.NumCards, removeCard.Card);
            return $"{startText} {actionText}";
        }

        private string IgnoreScenarioText(int idx) 
        {
            var startText = (idx == 0) ? "Ignore" : "and ignore";
            return $"{startText} negative scenario effects";
        }
    
        private string IgnoreItemText(int idx) 
        {
            var startText = (idx == 0) ? "Ignore" : "and ignore";
            return $"{startText} negative item effects";
        }

        private string GetActionText(int idx, IPerkAction perkAction)
        {
            switch(perkAction)
            {
                case AddCard addCard:
                    return AddCardText(idx, addCard);
                case RemoveCard removeCard:
                    return RemoveCardText(idx, removeCard);
                case IgnoreItemEffects _:
                    return IgnoreItemText(idx);
                default:
                    return IgnoreScenarioText(idx);
            }
        }
    }
}
