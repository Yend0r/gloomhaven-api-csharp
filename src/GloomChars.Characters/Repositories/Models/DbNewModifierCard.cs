using System;
using GloomChars.GameData.Models;

namespace GloomChars.Characters.Repositories.Models
{
    public class DbNewModifierCard
    {
        public int CharacterId { get; set; }
        public int Damage { get; set; }
        public bool DrawAnother { get; set; }
        public bool Reshuffle { get; set; }
        public string Action { get; set; }
        public int? ActionAmount { get; set; }
        public DateTime DateDiscarded { get; set; }

        public DbNewModifierCard(int characterId, ModifierCard card)
        {
            Damage = card.Damage;
            DrawAnother = card.DrawAnother;
            Reshuffle = card.Reshuffle;
            Action = card.Action.ToString();
            ActionAmount = card.ActionAmount;
            CharacterId = characterId;
            DateDiscarded = DateTime.Now;
        }
    }
}
