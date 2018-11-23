using System;
using System.Collections.Generic;

namespace GloomChars.Characters.Models
{
    public class CharacterUpdate
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public int Experience { get; set; }
        public int Gold { get; set; }
        public int Achievements { get; set; }
        public List<PerkUpdate> Perks { get; set; }
    }
}
