using System;
namespace GloomChars.GameData.Models
{
    public class ModifierCard
    {
        public ModifierCard() { }

        public ModifierCard(int dmg, CardAction action, int actionAmnt, bool draw, bool reshuffle)
        {
            this.DrawAnother = draw;
            this.Reshuffle = reshuffle;
            this.Action = action;
            this.ActionAmount = actionAmnt;
            this.Damage = dmg;
        }

        public bool DrawAnother { get; set; }
        public bool Reshuffle { get; set; }
        public CardAction Action { get; set; }
        public int ActionAmount { get; set; }
        public int Damage { get; set; }
    }
}