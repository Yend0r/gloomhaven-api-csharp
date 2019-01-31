using System;
using System.Collections.Generic;
using GloomChars.GameData.Models;

namespace GloomChars.Characters.Models
{
    public class Character
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public GloomClassName ClassName { get; set; }
        public int Experience { get; set; }        
        public int Gold { get; set; }
        public int Achievements { get; set; }
        public List<Perk> ClaimedPerks { get; set; }

        public Character()
        {
            ClaimedPerks = new List<Perk>();
        }
    }
}
