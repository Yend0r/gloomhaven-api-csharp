using System;
using System.Collections.Generic;
using System.Linq;
using GloomChars.Api.GameData;
using GloomChars.Characters.Models;

namespace GloomChars.Api.Characters
{
    public class CharacterListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public int Experience { get; set; }
        public int Level { get; set; }
        public int Gold { get; set; }
    
        public CharacterListModel(int level, CharacterListItem character)
        {
            Id           = character.Id;
            Name         = character.Name;
            ClassName    = character.ClassName.ToString();
            Experience   = character.Experience;
            Level        = level;
            Gold         = character.Gold;
        }
    }
}
