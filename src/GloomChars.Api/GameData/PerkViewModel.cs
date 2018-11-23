using System;
using GloomChars.GameData.Models;

namespace GloomChars.Api.GameData
{
    public class PerkViewModel
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public string Actions { get; set; }

        public PerkViewModel(Perk perk)
        {
            Id       = perk.Id;
            Quantity = perk.Quantity;
            Actions  = perk.ActionsToString();
        }
    }
}
