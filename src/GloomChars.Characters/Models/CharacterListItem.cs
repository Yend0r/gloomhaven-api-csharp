using System;
using GloomChars.GameData.Models;

namespace GloomChars.Characters.Models
{
    public class CharacterListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GloomClassName ClassName { get; set; }
        public int Experience { get; set; }
        public int Gold { get; set; }
    }
}
