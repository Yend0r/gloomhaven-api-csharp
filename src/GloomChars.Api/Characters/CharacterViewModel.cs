using System;
using System.Collections.Generic;
using System.Linq;
using GloomChars.Api.GameData;
using GloomChars.Characters.Models;

namespace GloomChars.Api.Characters
{
    public class CharacterViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public int Experience { get; set; }
        public int Gold { get; set; }
        public int Achievements { get; set; }
        public IEnumerable<PerkViewModel> Perks { get; set; }
    
        public CharacterViewModel(Character character)
        {
            Id           = character.Id;
            Name         = character.Name;
            ClassName    = character.ClassName.ToString();
            Experience   = character.Experience;
            Gold         = character.Gold;
            Achievements = character.Achievements;
            Perks        = character.Perks.Select(p => new PerkViewModel(p));
        }
    }
}
