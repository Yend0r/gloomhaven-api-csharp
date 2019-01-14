using System;
using System.Collections.Generic;

namespace GloomChars.Api.Characters
{
    public class CharacterPatchRequest
    {
        public string Name { get; set; }
        public int? Experience { get; set; }
        public int? Gold { get; set; }
        public int? Achievements { get; set; }
        public List<PerkRequest> Perks { get; set; }
    }
}
