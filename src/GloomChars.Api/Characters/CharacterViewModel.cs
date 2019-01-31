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
        public int Level { get; set; }
        public int HP { get; set; }
        public int? PetHP { get; set; }
        public int Gold { get; set; }
        public int Achievements { get; set; }
        public IEnumerable<PerkViewModel> ClaimedPerks { get; set; }
    
        public CharacterViewModel(int level, int hp, int? petHP, Character character)
        {
            Id           = character.Id;
            Name         = character.Name;
            ClassName    = character.ClassName.ToString();
            Experience   = character.Experience;
            Level        = level;
            HP           = hp;
            PetHP        = petHP;
            Gold         = character.Gold;
            Achievements = character.Achievements;
            ClaimedPerks = character.ClaimedPerks.Select(p => new PerkViewModel(p));
        }
    }
}
